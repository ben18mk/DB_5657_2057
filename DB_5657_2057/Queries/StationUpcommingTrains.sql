/*
	0 param - station_id
*/

SELECT
	line_id,
	es.name AS destination,
	operation_time,
    MINUTE(SUBTIME(operation_time, CONVERT(LOCALTIME(), TIME))) AS minutes_left
FROM stops st
NATURAL JOIN route_tlines
NATURAL JOIN routes
JOIN stations es
WHERE st.station_id = {0}
  AND es.station_id = end_station_id
  AND SUBTIME(operation_time, CONVERT(LOCALTIME(), TIME)) <= '01:00'
  AND SUBTIME(operation_time, CONVERT(LOCALTIME(), TIME)) >= '00:00'
ORDER BY operation_time ASC