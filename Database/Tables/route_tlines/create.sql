CREATE TABLE route_tlines (
    line_id INT NOT NULL,
    route_id INT NOT NULL,
    FOREIGN KEY (line_id) REFERENCES tlines(line_id)
);