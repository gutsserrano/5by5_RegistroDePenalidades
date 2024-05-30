CREATE TABLE controle_processamento
(
    Id int IDENTITY(1,1) PRIMARY KEY,
    description varchar(100) not null,
    processing_date DATE not null,
    Number_of_records int not null
)
GO

DROP TABLE controle_processamento
GO

SELECT * FROM controle_processamento
GO
