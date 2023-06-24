CREATE TABLE route_prices (
    route_id INT NOT NULL,
    price INT NOT NULL,
    FOREIGN KEY (route_id) REFERENCES routes(route_id)
);