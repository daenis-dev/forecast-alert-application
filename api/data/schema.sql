CREATE SEQUENCE IF NOT EXISTS alerts_id_seq;
CREATE SEQUENCE IF NOT EXISTS specifications_id_seq;
CREATE SEQUENCE IF NOT EXISTS operators_id_seq;
CREATE SEQUENCE IF NOT EXISTS alert_specifications_id_seq;

CREATE TABLE IF NOT EXISTS alerts
(
    id INT NOT NULL DEFAULT nextval('alerts_id_seq') PRIMARY KEY,
    name TEXT NOT NULL,
    is_urgent BOOLEAN NOT NULL DEFAULT FALSE,
    created_datetime_utc TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_datetime_utc TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO alerts (name, is_urgent) VALUES ('Bring an Umbrella', true);
INSERT INTO alerts (name, is_urgent) VALUES ('Wear a Wind Breaker', false);

CREATE TABLE IF NOT EXISTS specifications
(
    id INT NOT NULL DEFAULT nextval('specifications_id_seq') PRIMARY KEY,
    name TEXT NOT NULL
);

INSERT INTO specifications (name) VALUES ('wind speed in miles per hour');
INSERT INTO specifications (name) VALUES ('temperature in degrees fahrenheit');
INSERT INTO specifications (name) VALUES ('chance of precipitation percentage');
INSERT INTO specifications (name) VALUES ('humidity percentage');

CREATE TABLE IF NOT EXISTS operators
(
    id INT NOT NULL DEFAULT nextval('operators_id_seq') PRIMARY KEY,
    name TEXT NOT NULL,
    symbol TEXT NOT NULL,
    CONSTRAINT valid_symbol CHECK (symbol IN ('=', '!=', '<', '<=', '>', '>='))
);

INSERT INTO operators (name, symbol) VALUES ('equal to', '=');
INSERT INTO operators (name, symbol) VALUES ('not equal to', '!=');
INSERT INTO operators (name, symbol) VALUES ('less than', '<');
INSERT INTO operators (name, symbol) VALUES ('less than or equal to', '<=');
INSERT INTO operators (name, symbol) VALUES ('greater than', '>');
INSERT INTO operators (name, symbol) VALUES ('greather than or equal to', '>=');

CREATE TABLE IF NOT EXISTS alert_specifications
(
    id INT NOT NULL DEFAULT nextval('alert_specifications_id_seq') PRIMARY KEY,
    alert_id INT NOT NULL,
    specification_id INT NOT NULL,
    operator_id INT NOT NULL,
    threshold_value INT NOT NULL,
    created_datetime_utc TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    modified_datetime_utc TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (alert_id) REFERENCES alerts (id),
    FOREIGN KEY (specification_id) REFERENCES specifications (id),
    FOREIGN KEY (operator_id) REFERENCES operators (id)
);

INSERT INTO alert_specifications (alert_id, specification_id, operator_id, threshold_value) VALUES (1, 3, 6, 50);
INSERT INTO alert_specifications (alert_id, specification_id, operator_id, threshold_value) VALUES (2, 1, 6, 15);
