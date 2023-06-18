/*
	{0} - line_id
*/

CALL GetLineInfo({0}, @bike_accessible, @dist, @travel_time, @type_name);

SELECT
	@bike_accessible AS bike_accessible,
	@dist AS dist,
	@travel_time AS travel_time,
	@type_name AS type_name