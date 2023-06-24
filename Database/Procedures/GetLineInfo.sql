DELIMITER //

CREATE PROCEDURE GetLineInfo
(
	IN line_id INT,
	OUT bike_accessible BOOLEAN,
	OUT dist DECIMAL (4, 1),
	OUT travel_time TIME(6),
	OUT type_name VARCHAR(10)
)
BEGIN
    SELECT
    	tl.bike_accessible,
    	r.dist,
        r.travel_time
	INTO
		bike_accessible,
		dist,
		travel_time
  	FROM tlines tl
    NATURAL JOIN route_tlines
    NATURAL JOIN routes r
    WHERE tl.line_id = line_id;
    
    SELECT tt.type_name INTO type_name
    FROM tline_train_types ttt
    NATURAL JOIN train_types tt
    WHERE ttt.line_id = line_id;
END;
//

DELIMITER;