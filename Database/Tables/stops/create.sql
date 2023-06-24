CREATE TABLE stops (
    line_id INT NOT NULL,
    station_id INT NOT NULL,
    seq INT NOT NULL,
    operation_time TIME(4) NOT NULL,
    FOREIGN KEY (line_id) REFERENCES tlines(line_id),
    FOREIGN KEY (station_id) REFERENCES stations(station_id)
);