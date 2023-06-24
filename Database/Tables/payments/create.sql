CREATE TABLE payments (
    payment_id INT PRIMARY KEY,
    ticket_id INT NOT NULL,
    payment_date DATE NOT NULL,
    payment_method VARCHAR(10) NOT NULL,
    FOREIGN KEY (ticket_id) REFERENCES tickets(ticket_id)
);