SELECT
    route_id,
    (SELECT DISTINCT name
     FROM stations
     WHERE station_id = start_station_id) AS start,
    (SELECT DISTINCT name
     FROM stations
     WHERE station_id = end_station_id) AS end,
    dist,
    travel_time,
    income
FROM routes
NATURAL JOIN (SELECT route_id, SUM(price) AS income
      FROM route_prices
      NATURAL JOIN route_tlines
      NATURAL JOIN tlines
      NATURAL JOIN tickets
      GROUP BY route_id) AS incomeperroute