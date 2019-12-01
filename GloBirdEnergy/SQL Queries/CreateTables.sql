CREATE TABLE Customer(
	id int,
    username varchar(30) NOT NULL,
    first_name varchar(50)  NOT NULL,
    last_name varchar(50) NOT NULL,
    phone_number varchar(50) NOT NULL,
	date_of_birth date NOT NULL,
	PRIMARY KEY(id)
);

CREATE TABLE CallNote(
	id int,
	parent_id int,
	customer_id int NOT NULL,
	text_content varchar(140),
	date_created datetime default CURRENT_TIMESTAMP, 
	PRIMARY KEY(id),
	FOREIGN KEY (parent_id) REFERENCES CallNote(id),
	FOREIGN KEY (customer_id) REFERENCES Customer(id)
);