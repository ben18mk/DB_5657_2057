# JCT Database Mini-Project
## Project Theme:
**Israel Railways**
<ul>
  <li>Schedule</li>
  <li>Tickets</li>
</ul>

## Introduction
The purpose of the project is to represent a database of the timetable department and the ticket department of the Israel Railways.
The project contains 11 tables.

## Tables
### Stations (stations)
This table represents all the train stations in Israel. The information that the table contains is mainly technical information about the train stations.</br>
Each record has 4 fields.

‎ |‎ Field | Type |‎ Description
|---|---|---|---
:key: | Station ID | INT | The station ID provided by Israel Railways
|| Name | VARCHAR | Station name
|| Latitude | DECIMAL | Latitude of the station geographic location
|| Longitude | DECIMAL | Longitude of the station geographic location

### Travel Routes (routes)
This table represents the routes that a train can travel. Each route has an origin and a destination accompanied by technical information.</br>
Each record has 5 fields.

‎ |‎ Field | Type |‎ Description
|---|---|---|---
:key: | Route ID | INT | The route ID
|| Start Station ID | INT | The station ID of the origin station
|| End Station ID | INT | The station ID of the destination station
|| Dist | DECIMAL | Route total distance in kilometers (km)
|| Travel Time | TIME | Estimated travel time

### Train Lines (tlines)
This table represents all the train lines. The information that the table contains is mainly technical information.</br>
Each record has a 2 fields.

‎ |‎ Field | Type |‎ Description
|---|---|---|---
:key: | Line ID | INT | The line ID, A.k.a Line number
|| Bike Accessible | BOOLEAN | True if the line is accessible for bicycles. Otherwise False

### Lines to Routes assignment (route_tlines)
This table assignment the placement between line and track.
This table is a connecting table between the train lines table and the route table, so that the sharp arrow is directed into the route table.</br>
Each record has 2 fields.

‎ |‎ Field | Type |‎ Description
|---|---|---|---
:key: | Line ID | INT | The line ID, A.k.a Line number
|| Route ID | INT | The route ID

### Line intermediate stop stations (stops)
This table represents the intermediate stations of the train lines. Each intermediate station has a line number and a station ID accompanied by technical information.
This table depends on the station table and the line table and is linked to them by means of a round weak binding arrow directed towards them.</br>
Each record has 4 fields.

‎ |‎ Field | Type |‎ Description
|---|---|---|---
:key: | Line ID | INT | The line ID, A.k.a Line number
:key: | Station ID | INT | The station ID provided by Israel Railways
|| Seq | INT | The sequence number of the intermediate station on the line
|| Operation Time | TIME | Estimated operation time (operation - entering and exiting the station)

### Train Types (train_types)
This table represents all types of train in use. The information that the table contains is mainly technical information about the train.</br>
Each record has 2 fields.

‎ |‎ Field | Type |‎ Description
|---|---|---|---
:key: | Type ID | INT | The train type ID
|| Type Name | VARCHAR | The type name

### Train Types to Lines assignment (tline_train_types)
This table represents the assignment between train type and line.
This table is a table connecting the table of types of train and the table of lines, so that the sharp arrow is directed into the table of types of train.</br>
Each record has 2 fields.

‎ |‎ Field | Type |‎ Description
|---|---|---|---
:key: | Line ID | INT | The line ID, A.k.a Line number
|| Type ID | INT | The train type ID

### Route Prices (route_prices)
This table represents the information on the price charged to a passenger on a certain route.</br>
Each record has 2 fields.

‎ |‎ Field | Type |‎ Description
|---|---|---|---
:key: | Route ID | INT | The route ID
|| Price | INT | The price of traveling the route in Shekels

### Passengers (passengers)
This table represents the information on all passengers who purchased tickets in their name.</br>
Each record has 5 fields.

‎ |‎ Field | Type |‎ Description
|---|---|---|---
:key: | Passenger ID | INT | The passenger ID number
|| Name | VARCHAR | The passenger name
|| Email | VARCHAR | The passenger email address
|| Phone | INT | The passenger phone number
|| Address | VARCHAR | The passenger home address

### Tables (tables)
This table represents the information about the tickets booked by passengers.</br>
Each record has 3 fields.

‎ |‎ Field | Type |‎ Description
|---|---|---|---
:key: | Ticket ID | INT | The ticket ID
:key: | Line ID | INT | The line ID, A.k.a Line number
:key: | Passenger ID | INT | The passenger ID number

### Payment Confirmation (payments)
This table represents the payment confirmation information for ordered tickets.</br>
Each record has 4 fields.

‎ |‎ Field | Type |‎ Description
|---|---|---|---
:key: | Payment ID | INT | The payment ID
:key: | Ticket ID | INT | The ticket ID
|| Date | DATE | The Payment date
|| Method | VARCHAR | The payment method - Cash or Card
