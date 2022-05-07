# LetsGoBiking

## Auteur
Thomas De Sousa Vieira

## Composants du projet

** LightClient ** : Application web
** Routing ** : API pour l'application
** ProxyCache ** : Serveur intermédiaire entre Routing et les apis externes
** LetsgobikingHeavyClient ** : Client lourd permettant d'effectuer des recherches et d'obtenir des statistiques


## Pour lancer le projet

### Routing et Proxy
Lancer visual Studio en administrateur, ouvrir le projet "Lets go biking.sln". Ce projet réunit les solutions "Routing", "ProxyCache".
Pour lancer le projet il faut cliquer sur la fleche verte (sans débogage).
Cela lancera les projet en selfhosted (image ci-dessous).

### Web application

Lancer le fichier "lightClient/index.html" dans un navigateur.

### Heavy client

Lancer Visual Studio et ouvrir le projet "LetsgobikingHeavyClient.sln". Lancer le projet en cliquant sur la flèche verte.

## Docker 

Le projet peut être conteneuriser grâce aux DockerFile.


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

