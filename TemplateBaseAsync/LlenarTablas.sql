
-- Creación de la tabla
CREATE TABLE EjemploTabla (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Edad INT,
    Email NVARCHAR(100)
);
go
-- Procedimiento almacenado para insertar un registro
CREATE PROCEDURE InsertarRegistro
    @Nombre NVARCHAR(100),
    @Edad INT,
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO EjemploTabla (Nombre, Edad, Email)
    VALUES (@Nombre, @Edad, @Email);
END;
go
-- Procedimiento almacenado para actualizar un registro
CREATE PROCEDURE ActualizarRegistro
    @ID INT,
    @Nombre NVARCHAR(100),
    @Edad INT,
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE EjemploTabla
    SET Nombre = @Nombre, Edad = @Edad, Email = @Email
    WHERE ID = @ID;
END;
go
-- Procedimiento almacenado para eliminar un registro
CREATE PROCEDURE EliminarRegistro
    @ID INT
AS
BEGIN
    DELETE FROM EjemploTabla
    WHERE ID = @ID;
END;
go
-- Procedimiento almacenado para obtener todos los registros
CREATE PROCEDURE ObtenerRegistros
AS
BEGIN
    SELECT ID, Nombre, Edad, Email
    FROM EjemploTabla;
END;
go
-- Procedimiento almacenado para obtener un registro por su ID
CREATE PROCEDURE ObtenerRegistroPorID
    @ID INT
AS
BEGIN
    SELECT ID, Nombre, Edad, Email
    FROM EjemploTabla
    WHERE ID = @ID;
END;
