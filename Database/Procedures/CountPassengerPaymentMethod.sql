DELIMITER //

CREATE PROCEDURE CountPassengerPaymentMethod
(
    IN passenger_id INT,
    OUT cash_count INT,
    OUT card_count INT,
    OUT total_payments INT
)
BEGIN
	SELECT
    	COUNT(*) INTO cash_count
    FROM passengers p
    NATURAL JOIN tickets
    NATURAL JOIN payments
    WHERE p.passenger_id = passenger_id AND payment_method = 'cash';
    
    SELECT
    	COUNT(*) INTO card_count
    FROM passengers p
    NATURAL JOIN tickets
    NATURAL JOIN payments
    WHERE p.passenger_id = passenger_id AND payment_method = 'card';
    
    SELECT cash_count + card_count INTO total_payments;
END;
//

DELIMITER;