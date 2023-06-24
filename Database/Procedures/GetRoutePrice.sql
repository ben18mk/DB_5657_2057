DELIMITER //

CREATE PROCEDURE GetRoutePrice
(
    IN route_id INT,
    OUT price INT
)
BEGIN
	SELECT rp.price INTO price
    FROM route_prices rp
    WHERE rp.route_id = route_id;
END;
//

DELIMITER;