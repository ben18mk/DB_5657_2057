 SELECT *
FROM routes
NATURAL JOIN (SELECT route_id, SUM(price) AS income
      FROM route_prices
      NATURAL JOIN route_tlines
      NATURAL JOIN tlines
      NATURAL JOIN tickets
      GROUP BY route_id) AS incomeperroute