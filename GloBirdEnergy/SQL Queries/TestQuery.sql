INSERT INTO CallNote (id, parent_id, customer_id, text_content)
VALUES (1, NULL, 1, 'Danny wants to move');
INSERT INTO CallNote (id, parent_id, customer_id, text_content)
VALUES (2, 1, 1, 'He thinks he wants to move to BH');
INSERT INTO CallNote (id, parent_id, customer_id, text_content)
VALUES (3, 2, 1, 'We can do it if its BH');
INSERT INTO CallNote (id, parent_id, customer_id, text_content)
VALUES (4, 1, 1, 'He asked about the extra charge about moving');
INSERT INTO CallNote (id, parent_id, customer_id, text_content)
VALUES (5, NULL, 1, 'Danny asked if we can send him bill every week');
INSERT INTO CallNote (id, parent_id, customer_id, text_content)
VALUES (6, 5, 1, 'by mail and email BOTH');
INSERT INTO CallNote (id, parent_id, customer_id, text_content)
VALUES (7, NULL, 2, 'Aomeng wants to quit!');

INSERT INTO Customer(id, username, first_name, last_name, phone_number, date_of_birth)
VALUES (1, 'GiantCaiB', 'Danny', 'Yu', '0433700518', '1994-05-18');

INSERT INTO Customer(id, username, first_name, last_name, phone_number, date_of_birth)
VALUES (2, 'Elmo12', 'Aomeng', 'Zhang', '0432564990', '1999-11-12');

SELECT * from Customer
SELECT * from CallNote