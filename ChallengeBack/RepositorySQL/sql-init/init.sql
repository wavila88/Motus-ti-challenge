CREATE DATABASE Crud_Example;

USE Crud_Example;

CREATE TABLE Users (
    user_id INT PRIMARY KEY IDENTITY(1,1),
    first_name NVARCHAR(100),
    last_name NVARCHAR(100),
    email NVARCHAR(100),
    position NVARCHAR(100)
   
);

INSERT INTO Users (first_name, last_name,email, position)
VALUES ('William', 'Avila', 'william@gmail.com', 'Senios FullStack Software Developer');
