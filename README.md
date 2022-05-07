# LetsGoBiking

## Auteur
Thomas De Sousa Vieira

## Composants du projet

- LightClient : Application web
- Routing : API pour l'application
- ProxyCache : Serveur intermédiaire entre Routing et les apis externes (communication REST)
- LetsgobikingHeavyClient : Client lourd permettant d'effectuer des recherches et d'obtenir des statistiques


## Pour lancer le projet

### Routing et Proxy
Lancer visual Studio en administrateur, ouvrir le projet "Lets go biking.sln". Ce projet réunit les solutions "Routing", "ProxyCache".
Pour lancer le projet, il faut cliquer sur la flèche verte (sans débogage).
![image](https://user-images.githubusercontent.com/59823412/167250302-7c5fde3c-1545-4435-848b-7bbe8215eb4e.png)

Cela lancera les projet en selfhosted (image ci-dessous).
![image](https://user-images.githubusercontent.com/59823412/167250250-3a7757a2-e380-4f8e-94ec-76fc040e16cc.png)


### Web application

Lancer le fichier "lightClient/index.html" dans un navigateur.

1. Pour rechercher une adresse, rentrer une adresse dans "Départ" et appuyer sur Entrée pour lancer la recherche. Puis sélectionner la bonne adresse.
2. Sélectionner l'arrivée
3. Rechercher le trajet

![image](https://user-images.githubusercontent.com/59823412/167250602-a2e1b61b-3ec3-442a-97a6-7d8677741c40.png)

![image](https://user-images.githubusercontent.com/59823412/167250644-76005a94-6f85-4f02-8e4e-6ad088c195f8.png)

#### Exemple de recherches

Trajet marche :
- **Départ** leclerc lyon
- **Arrivée** rue des girondins lyon

Trajet en vélo:
- **Départ** leclerc lyon
- **Arrivée** pont winston churchill lyon

### Heavy client

Lancer Visual Studio et ouvrir le projet "LetsgobikingHeavyClient.sln". Lancer le projet en cliquant sur la flèche verte.
![image](https://user-images.githubusercontent.com/59823412/167250747-98d38e83-47af-47ff-9372-ec1d35290df0.png)

![image](https://user-images.githubusercontent.com/59823412/167250759-cfc76d27-c6ea-440d-af8b-e234f1f9a26c.png)


## Docker 

Le projet peut être conteneurisé grâce aux DockerFile.

## EndPoints API disponibles

### Proxy 
- http://localhost:8735/Design_Time_Addresses/ProxyCache/Service1/rest/convert?address=Chemin+de+rabiac
- http://localhost:8735/Design_Time_Addresses/ProxyCache/Service1/rest/stations
- http://localhost:8735/Design_Time_Addresses/ProxyCache/Service1/rest/station?stationid=5015

### Routing
- http://localhost:8733/Design_Time_Addresses/Routing/Service1/rest/travel?startPoint=4.826075,45.73913&endPoint=4.830969,45.738765
- http://localhost:8733/Design_Time_Addresses/Routing/Service1/rest/convert?address=leclerc+lyon


## API externes utilisées

- api-adresse.data.gouv.fr
```
https://api-adresse.data.gouv.fr/search/?q=chemin de rabiac
```
- api.openrouteservice.org
```
https://api.openrouteservice.org/v2/directions/driving-car?api_key=5b3ce3597851110001cf6248e4dd7eb3344b478fa618aecc746388d3&start=15.645854, 46.557497&end=15.645854, 46.557497
```
- api.jcdecaux.com
```
https://api.jcdecaux.com/vls/v3/stations/1191?contract=lyon&apiKey=079e5c452a49c4c91275b1be1e96300f1194190a
```

