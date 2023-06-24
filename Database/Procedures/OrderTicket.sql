DELIMITER //

CREATE PROCEDURE OrderTicket
(
    IN passenger_id INT,
    IN line_id INT,
    IN payment_method VARCHAR(10),
    IN pdate DATE,
    OUT ticket_id INT,
    OUT payment_id INT
)
BEGIN
	INSERT INTO tickets
    VALUES
    (
        (SELECT t1.ticket_id
        FROM tickets t1
        ORDER BY t1.ticket_id DESC
        LIMIT 1) + 1,
        line_id,
        passenger_id
    );
    
    SELECT t1.ticket_id INTO ticket_id
    FROM tickets t1
    ORDER BY t1.ticket_id DESC
    LIMIT 1;
    
    INSERT INTO payments
    VALUES
    (
        (SELECT p1.payment_id
         FROM payments p1
         ORDER BY p1.payment_id DESC
         LIMIT 1) + 1,
        ticket_id,
        pdate,
        payment_method
    );
    
    SELECT p1.payment_id INTO payment_id
    FROM payments p1
    ORDER BY p1.payment_id DESC
    LIMIT 1;
END;
//

DELIMITER;