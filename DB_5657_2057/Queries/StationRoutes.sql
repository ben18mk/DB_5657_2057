/*
    0 param - station_id
*/

SELECT ss.name AS origin, es.name AS destination
FROM stations ss, stations es
WHERE (ss.station_id, es.station_id) IN (SELECT start_station_id, end_station_id
                                         FROM routes
                                         WHERE route_id IN (SELECT rtl.route_id
										   				    FROM route_tlines rtl
														    WHERE {0} IN (SELECT s.station_id
                                                                                  FROM stops s
                                                                                  WHERE s.line_id = rtl.line_id)
                                                            GROUP BY rtl.route_id))