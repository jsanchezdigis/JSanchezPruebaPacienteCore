CREATE DATABASE JSanchezPruebaPaciente;
USE JSanchezPruebaPaciente;

CREATE TABLE TipoSangre(
	IdTipoSangre TINYINT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(50) NOT NULL,
);
GO
CREATE TABLE Paciente(
	IdPaciente INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(50),
	ApellidoPaterno VARCHAR(50),
	ApellidoMaterno VARCHAR(50),
	FechaNacimiento DATE,
	IdTipoSangre TINYINT,
	Sexo CHAR(2),
	FechaIngreso DATETIME,
	Diagnostico VARCHAR(100)
	CONSTRAINT fk_PacienteTipoSangre FOREIGN KEY (IdTipoSangre) REFERENCES TipoSangre (IdTipoSangre)
);
GO

CREATE PROCEDURE TipoSangreAdd 'A+'
@Nombre VARCHAR(50)
AS
INSERT INTO TipoSangre(Nombre) VALUES(@Nombre)
GO

SELECT * FROM TipoSangre

CREATE PROCEDURE PacienteAdd
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@FechaNacimiento VARCHAR(20),
@IdTipoSangre TINYINT,
@Sexo CHAR(2),
@Diagnostico VARCHAR(100)
AS
INSERT INTO Paciente(
	   Nombre,
	   ApellidoPaterno,
	   ApellidoMaterno,
	   FechaNacimiento,
	   IdTipoSangre,
	   Sexo,
	   FechaIngreso,
	   Diagnostico)
				   VALUES(
						  @Nombre,
						  @ApellidoPaterno,
						  @ApellidoMaterno,
						  CONVERT(DATE,@FechaNacimiento,105),
						  @IdTipoSangre,
						  @Sexo,
						  GETDATE(),
						  @Diagnostico)
GO

PacienteAdd 'Jose','Sanchez','Xihuitl','15-08-1999',1,'M','Tas bien'

CREATE PROCEDURE PacienteUpdate
@IdPaciente INT,
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@FechaNacimiento VARCHAR(20),
@IdTipoSangre TINYINT,
@Sexo CHAR(2),
@Diagnostico VARCHAR(100)
AS
UPDATE Paciente 
SET
Nombre=@Nombre,
ApellidoPaterno=@ApellidoPaterno,
ApellidoMaterno=@ApellidoMaterno,
FechaNacimiento=@FechaNacimiento,
IdTipoSangre=@IdTipoSangre,
Sexo=@Sexo,
Diagnostico=@Diagnostico
WHERE IdPaciente=@IdPaciente
GO

CREATE PROCEDURE PacienteDelete
@IdPaciente INT
AS
DELETE FROM Paciente WHERE IdPaciente=@IdPaciente
GO
SELECT * FROM Paciente

ALTER PROCEDURE PacienteGetAll
AS
SELECT IdPaciente,Paciente.Nombre,
	   ApellidoPaterno,
	   ApellidoMaterno,
	   FechaNacimiento,
	   TipoSangre.IdTipoSangre,
	   TipoSangre.Nombre AS TipoSangre,
	   Sexo,
	   FechaIngreso,
	   Diagnostico
	   FROM Paciente
	   INNER JOIN TipoSangre ON Paciente.IdTipoSangre = TipoSangre.IdTipoSangre
GO

ALTER PROCEDURE PacienteGetById
@IdPaciente INT
AS
SELECT IdPaciente,Paciente.Nombre,
	   ApellidoPaterno,
	   ApellidoMaterno,
	   FechaNacimiento,
	   TipoSangre.IdTipoSangre,
	   TipoSangre.Nombre AS TipoSangre,
	   Sexo,
	   FechaIngreso,
	   Diagnostico
	   FROM Paciente
	   INNER JOIN TipoSangre ON Paciente.IdTipoSangre = TipoSangre.IdTipoSangre
	   WHERE IdPaciente=@IdPaciente
GO

CREATE PROCEDURE TipoSangreGetAll
AS
SELECT IdTipoSangre,Nombre FROM TipoSangre
GO