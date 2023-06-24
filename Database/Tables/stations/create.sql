CREATE TABLE stations (
    station_id INT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    latitude DECIMAL(9, 7) NOT NULL,
    longitude DECIMAL(10, 7) NOT NULL
);