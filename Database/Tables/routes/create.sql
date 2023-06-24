CREATE TABLE routes (
    route_id INT PRIMARY KEY,
    start_station_id INT NOT NULL,
    end_station_id INT NOT NULL,
    dist DECIMAL (4, 1) NOT NULL,
    travel_time TIME(6) NOT NULL
);