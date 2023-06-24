DELIMITER //

CREATE PROCEDURE GetLineOpTimeAtStation
(
	IN line_id INT,
	IN station_id INT,
	OUT result TIME
)
 BEGIN
  SELECT operation_time INTO result
  FROM stops s
  WHERE s.line_id = line_id AND s.station_id = station_id
  LIMIT 1;
 END;
//

DELIMITER;