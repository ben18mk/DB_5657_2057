/*
	0 param - line_id
	1 param - station_id
*/

CALL GetLineOpTimeAtStation({0}, {1}, @result);

SELECT @result AS operation_time