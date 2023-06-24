CREATE TABLE tickets (
    ticket_id INT PRIMARY KEY,
    line_id INT NOT NULL,
    passenger_id INT NOT NULL,
    FOREIGN KEY (line_id) REFERENCES tlines(line_id),
    FOREIGN KEY (passenger_id) REFERENCES passengers(passenger_id)
);