CREATE TABLE tline_train_types (
	line_id INT NOT NULL,
	type_id INT NOT NULL,
	FOREIGN KEY (line_id) REFERENCES tlines(line_id)
);