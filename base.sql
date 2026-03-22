create database point ;
\c point ; 

CREATE TABLE matrices (
    id SERIAL PRIMARY KEY,
    data DOUBLE PRECISION[][]
);