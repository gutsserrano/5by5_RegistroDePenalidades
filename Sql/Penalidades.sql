USE DBPenalidades
GO

CREATE TABLE Penalidades
(
    ID int IDENTITY(1,1) PRIMARY KEY,
    Cnpj varchar(18) not null,
    RazaoSocial varchar(100) not null,
    NomeMotorista varchar(50) not null,
    cpf varchar(15) not null,
    VigenciaCadastro DATE not null
)
GO

DROP TABLE Penalidades
GO

SELECT * FROM Penalidades

DELETE FROM Penalidades
GO

CREATE or ALTER PROC InserirMotoristas
    @Cnpj varchar(18),
    @RazaoSocial varchar(100),
    @NomeMotorista varchar(50),
    @Cpf varchar(15),
    @VigenciaCadastro DATE
AS
BEGIN
    INSERT INTO Penalidades
    VALUES (@Cnpj, @RazaoSocial, @NomeMotorista, @Cpf, @VigenciaCadastro)
END
GO

