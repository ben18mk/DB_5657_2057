/*
    {0} - passenger_id
*/

SELECT
	payment_date,
    line_id,
    (SELECT name
     FROM stations
     WHERE station_id = start_station_id) AS line_start,
    (SELECT name
     FROM stations
     WHERE station_id = end_station_id) AS line_end,
    price,
    payment_method
FROM tlines
NATURAL JOIN route_tlines
NATURAL JOIN routes
NATURAL JOIN route_prices
NATURAL JOIN tickets
NATURAL JOIN passengers
NATURAL JOIN payments
WHERE passenger_id = {0}
ORDER BY payment_date DESC