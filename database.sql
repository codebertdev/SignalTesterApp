CREATE DATABASE signaltester;
USE signaltester;

CREATE TABLE decoded_data (
    id INT AUTO_INCREMENT PRIMARY KEY,
    input_type VARCHAR(50),
    raw1 VARCHAR(10),
    raw2 VARCHAR(10),
    output VARCHAR(100)
);
