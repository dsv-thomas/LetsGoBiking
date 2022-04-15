var map = new ol.Map({
    target: 'map',
    layers: [
        new ol.layer.Tile({
            source: new ol.source.OSM()
        })
    ],

    view: new ol.View({
        center: ol.proj.fromLonLat([7.0985774, 43.6365619]),
        zoom: 10
    })
});

var lineStyle = new ol.style.Style({
    stroke: new ol.style.Stroke({
        color: '#007ea7',
        width: 3
    })
});

var lineStyle2 = new ol.style.Style({
    stroke: new ol.style.Stroke({
        color: '#f7cb15',
        width: 3
    })
});

const urlApi = "http://localhost:8733/Design_Time_Addresses/Routing/Service1/rest/";

const adresses = [];

let startPoint;
let endPoint;

let editSE;

document.getElementById("result-label").hidden = true;


const node = document.getElementsByClassName("startLocation")[0];
node.addEventListener("keyup", function (event) {
    if (event.key === "Enter") {
        console.log("test");
        editSE = true;
        findLatLong(node.value);
    }
});
const node2 = document.getElementsByClassName("endLocation")[0];
node2.addEventListener("keyup", function (event) {
    if (event.key === "Enter") {
        console.log("endLocation");
        editSE = false;
        findLatLong(node2.value);
    }
});

function findLatLong(address) {
    const apiUrl = urlApi + "convert?address=" + address;
    const request = new XMLHttpRequest();
    request.open("GET", apiUrl, true);
    request.setRequestHeader("Accept", "application/json");
    request.onload = responseAddressFinder;
    request.send();
}

function responseAddressFinder() {
    if (this.status !== 200) {
        console.log("Recherche d'adresse impossible")
    } else {
        document.getElementById("result-label").hidden = false;
        const addressList = document.getElementById("address-select-list");
        addressList.innerHTML = "";
        const results = JSON.parse(this.responseText);
        for (let result of results.convertResult) {
            adresses.push(result);
            var newDiv = document.createElement("div");
            var innerDiv = document.createElement("div");
            newDiv.className = "address-select-item";
            innerDiv.className = "address-select-item-text"
            var p1 = document.createElement("p");
            var p2 = document.createElement("p");
            p1.innerText = result.properties.name;
            p2.innerText = result.properties.city;
            innerDiv.appendChild(p1);
            innerDiv.appendChild(p2);
            newDiv.id = adresses.length - 1;
            newDiv.appendChild(innerDiv);
            newDiv.addEventListener('click', selectAddress, false);
            addressList.appendChild(newDiv);
        }
    }
}

function selectAddress(e) {
    document.getElementById("result-label").hidden = true;
    const addressList = document.getElementById("address-select-list");
    addressList.innerHTML = "";
    if (editSE) {
        startPoint = adresses[e.currentTarget.id].geometry.coordinates;
    } else {
        endPoint = adresses[e.currentTarget.id].geometry.coordinates;
    }

    console.log("startPoint")
    console.log(startPoint)
    console.log(endPoint)

    map.getLayers().getArray()
        .filter(layer => layer.get('name') === 'pointer' + editSE)
        .forEach(layer => map.removeLayer(layer));

    map.getLayers().getArray()
        .filter(layer => layer.get('name') === 'pointer_station')
        .forEach(layer => map.removeLayer(layer));

    map.getLayers().getArray()
        .filter(layer => layer.get('name') === 'travel')
        .forEach(layer => map.removeLayer(layer));

    var layer2 = new ol.layer.Vector({
        name: 'pointer' + editSE,
        source: new ol.source.Vector({
            features: [
                new ol.Feature({
                    geometry: new ol.geom.Point(ol.proj.fromLonLat([parseFloat(adresses[e.currentTarget.id].geometry.coordinates[0]), parseFloat(adresses[e.currentTarget.id].geometry.coordinates[1])]))
                })
            ],
        }),
        style: new ol.style.Style({
            image: new ol.style.Icon({
                anchor: [0.5, 1],
                scale: [0.08, 0.08],
                src: 'assets/location-dot-solid.png'
            })
        })
    });
    map.addLayer(layer2);
    map.getView().animate({
        zoom: 15,
        center: ol.proj.fromLonLat([parseFloat(adresses[e.currentTarget.id].geometry.coordinates[0]), parseFloat(adresses[e.currentTarget.id].geometry.coordinates[1])])
    });
}

const btn = document.getElementById("searchTravel");
btn.addEventListener("click", function (event) {
    searchTravel(startPoint, endPoint)
});

function generateMarker(station) {
    var layer2 = new ol.layer.Vector({
        name: 'pointer_station',
        source: new ol.source.Vector({
            features: [
                new ol.Feature({
                    geometry: new ol.geom.Point(ol.proj.fromLonLat([parseFloat(station.position.longitude), parseFloat(station.position.latitude)]))
                })
            ],
        }),
        style: new ol.style.Style({
            image: new ol.style.Icon({
                anchor: [0.5, 1],
                scale: [0.08, 0.08],
                src: 'assets/bicycle-solid.png'
            })
        })
    });
    map.addLayer(layer2);
}


function cardStation(card) {
    console.log("test")
    const cards = document.getElementById("cards");
    const card_station = document.getElementsByClassName("card-station-template")[0];
    var cloned = card_station.cloneNode(true);
    //card_station.setAttribute("id", card.number)
    cloned.classList.remove("hidden");
    cloned.classList.remove("card-station-template");
    cloned.classList.add("card-station");
    cloned.querySelector(".badge-valid").innerText = card.status;
    cloned.querySelector("h5").innerText = card.name;
    cloned.querySelector("p").innerText = card.address;
    cloned.querySelector("#badge-info-bike").innerText = card.totalStands.availabilities.bikes;
    cloned.querySelector("#badge-info-electricbike").innerText = card.totalStands.availabilities.electricalBikes;
    cloned.querySelector("#badge-info-parking").innerText = card.totalStands.availabilities.stands;
    cards.appendChild(cloned);
}

function searchTravel(startPoint, endPoint) {
    const apiUrl = urlApi + "/travel?startPoint=" + startPoint[0] + "," + startPoint[1] + "&endPoint=" + endPoint[0] + "," + endPoint[1];
    const request = new XMLHttpRequest();
    request.open("GET", apiUrl, true);
    request.setRequestHeader("Accept", "application/json");
    request.onload = generateTravel;
    request.send();
}

function routeOverlay(travelChoice, linestyle) {
    const vector_overlays2 = new ol.layer.Group({
        name: 'travel',
        layers: [
            new ol.layer.Vector({
                name: 'travel',
                visible: true,
                style: [linestyle],
                source: new ol.source.Vector({
                    projection: 'EPSG:4326',
                    url: 'data:,' + encodeURIComponent(JSON.stringify(travelChoice)),
                    format: new ol.format.GeoJSON({
                        dataProjection: 'EPSG:4326',
                        featureProjection: 'EPSG:3857'
                    })
                })
            })
        ]
    });
    map.addLayer(vector_overlays2);
}

function generateTravel() {
    if (this.status !== 200) {
        console.log("Recherche d'adresse impossible")
    } else {
        //Remove old cards
        const cards = document.getElementById("cards");
        cards.innerHTML = "";
        const results = JSON.parse(this.responseText);
        console.log(results);
        console.log(results.travelResult.travelChoice);
        routeOverlay(results.travelResult.travelChoice, lineStyle)
        if (results.travelResult.stationStart != undefined) {
            cardStation(results.travelResult.stationStart);
            cardStation(results.travelResult.stationEnd);
            generateMarker(results.travelResult.stationStart)
            generateMarker(results.travelResult.stationEnd)

            routeOverlay(results.travelResult.toStation, lineStyle2)
            routeOverlay(results.travelResult.toEndPoint, lineStyle2)
        }
    }
}