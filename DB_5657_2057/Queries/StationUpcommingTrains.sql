SET @station_id = 17090;

SELECT
	line_id,
	es.name AS destination,
	operation_time,
    MINUTE(SUBTIME(operation_time, ADDTIME(CONVERT(LOCALTIME(), TIME), '03:00'))) AS minutes_left
FROM stops st
NATURAL JOIN route_tlines
NATURAL JOIN routes
JOIN stations es
WHERE st.station_id = @station_id
  AND es.station_id = end_station_id
  AND SUBTIME(operation_time, CONVERT(LOCALTIME(), TIME)) <= '04:00'
  AND SUBTIME(operation_time, CONVERT(LOCALTIME(), TIME)) >= '03:00'
ORDER BY operation_time ASC
  
/*
	+3 hours because of the timezone offset.
    Checking if the operation_time is in [0h, 1h]
*/