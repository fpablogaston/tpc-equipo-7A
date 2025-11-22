--  CREACIÓN BASE DE DATOS
USE master;
GO
DROP DATABASE Ecommerce;
GO
CREATE DATABASE Ecommerce;
GO
USE Ecommerce;
GO

--  TABLAS PRINCIPALES

-- Categorías
CREATE TABLE Categorias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(150) NULL
);
GO

-- Clientes
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Email VARCHAR(50) NOT NULL,
    Telefono VARCHAR(50) NOT NULL,
    Direccion VARCHAR(50) NOT NULL,
    FechaRegistro DATETIME NOT NULL
);
GO

-- Productos
CREATE TABLE Productos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    Precio MONEY NULL,
    Stock INT NULL,
    ImagenUrl NVARCHAR(500) NULL,
    IdCategoria INT NOT NULL,
    CONSTRAINT FK_Productos_Categorias FOREIGN KEY (IdCategoria)
        REFERENCES Categorias(Id)
);
GO

-- Pedidos
CREATE TABLE Pedidos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FechaPedido DATETIME NULL,
    Estado VARCHAR(50) NULL,
    Total MONEY NULL,
    IdCliente INT NULL,
    IdEnvio INT NULL,
    IdPago INT NULL,
    CONSTRAINT FK_Pedidos_Clientes FOREIGN KEY (IdCliente)
        REFERENCES Clientes(Id)
);
GO

-- Pagos
CREATE TABLE Pagos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MetodoPago VARCHAR(50) NOT NULL,
    Estado VARCHAR(50) NOT NULL,
    Monto MONEY NOT NULL,
    FechaPago DATETIME NOT NULL,
    IdPedido INT NOT NULL,
    CONSTRAINT FK_Pagos_Pedidos FOREIGN KEY (IdPedido)
        REFERENCES Pedidos(Id)
);
GO

-- Envíos
CREATE TABLE Envios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DireccionEnvio VARCHAR(50) NULL,
    Ciudad VARCHAR(50) NULL,
    Provincia VARCHAR(50) NULL,
    CodigoPostal VARCHAR(50) NULL,
    FechaEnvio DATETIME NULL,
    FechaEntrega DATETIME NULL,
    Estado VARCHAR(50) NULL,
    IdPedido INT NOT NULL,
    CONSTRAINT FK_Envios_Pedidos FOREIGN KEY (IdPedido)
        REFERENCES Pedidos(Id)
);
GO

-- Detalles del Pedido
CREATE TABLE DetallesPedido (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdPedido INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario MONEY NOT NULL,
    Subtotal MONEY NOT NULL,
    CONSTRAINT FK_Detalles_Pedido FOREIGN KEY (IdPedido)
        REFERENCES Pedidos(Id),
    CONSTRAINT FK_Detalles_Producto FOREIGN KEY (IdProducto)
        REFERENCES Productos(Id)
);
GO


--  SISTEMA DE USUARIOS

-- Roles
CREATE TABLE Roles (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);
GO

INSERT INTO Roles (Nombre)
VALUES ('Cliente'), ('Administrador');
GO

-- Usuarios
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(200) NOT NULL,
    IdRol INT NOT NULL,
    CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (IdRol)
        REFERENCES Roles(Id)
);
GO

-- Vincular Clientes con Usuarios
ALTER TABLE Clientes
ADD IdUsuario INT NULL;

ALTER TABLE Clientes
ADD CONSTRAINT FK_Clientes_Usuarios FOREIGN KEY (IdUsuario)
REFERENCES Usuarios(Id);
GO


INSERT INTO Categorias (Nombre, Descripcion)
VALUES
('Electrónica', 'Dispositivos electrónicos y accesorios'),
('Ropa', 'Indumentaria para todas las edades'),
('Hogar', 'Artículos para el hogar y decoración');
GO

INSERT INTO Clientes (Nombre, Apellido, Email, Telefono, Direccion, FechaRegistro)
VALUES
('Juan', 'Pérez', 'juanperez@email.com', '1111111111', 'Av. Siempre Viva 742', GETDATE()),
('María', 'Gómez', 'maria@email.com', '2222222222', 'Calle Falsa 123', GETDATE());
GO

INSERT INTO Usuarios (Username, PasswordHash, IdRol)
VALUES
('admin1', 'kakashihatake1', 2),
('admin2', 'kakashihatake2', 2),
('admin3', 'kakashihatake3', 2),
('juan_user', '1234', 1),
('maria_user', 'abcd', 1);
GO

-- Asignar usuarios a clientes
UPDATE Clientes SET IdUsuario = 4 WHERE Id = 1; 
UPDATE Clientes SET IdUsuario = 5 WHERE Id = 2;  

ALTER TABLE Clientes
ALTER COLUMN IdUsuario INT NOT NULL;

ALTER TABLE Clientes
ADD CONSTRAINT UQ_Clientes_IdUsuario UNIQUE (IdUsuario);
GO

--  INSERTS DE PRODUCTOS

INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, ImagenUrl, IdCategoria)
VALUES
('Auriculares Bluetooth', 'Auriculares inalámbricos con micrófono', 15000, 50, 'auriculares.jpg', 1),
('Remera Algodón', 'Remera básica de algodón 100%', 7000, 100, 'remera.jpg', 2),
('Lámpara LED', 'Lámpara LED de bajo consumo', 3500, 80, 'lampara.jpg', 3);
GO

--  INSERTS DE PEDIDOS Y DETALLES

INSERT INTO Pedidos (FechaPedido, Estado, Total, IdCliente)
VALUES
(GETDATE(), 'Pendiente', 20000, 1),
(GETDATE(), 'Pendiente', 10500, 2);
GO

INSERT INTO DetallesPedido (IdPedido, IdProducto, Cantidad, PrecioUnitario, Subtotal)
VALUES
(1, 1, 1, 15000, 15000),
(1, 3, 1, 5000, 5000),
(2, 3, 3, 3500, 10500);
GO

INSERT INTO Pagos (MetodoPago, Estado, Monto, FechaPago, IdPedido)
VALUES
('Tarjeta de crédito', 'Pendiente', 20000, GETDATE(), 1),
('Transferencia bancaria', 'Pendiente', 10500, GETDATE(), 2);
GO

INSERT INTO Envios (DireccionEnvio, Ciudad, Provincia, CodigoPostal, FechaEnvio, Estado, IdPedido)
VALUES
('Av. Siempre Viva 742', 'Buenos Aires', 'Buenos Aires', '1000', GETDATE(), 'Preparando', 1),
('Calle Falsa 123', 'Córdoba', 'Córdoba', '5000', GETDATE(), 'Preparando', 2);
GO

UPDATE p
SET p.IdEnvio = e.Id, p.IdPago = g.Id
FROM Pedidos p
JOIN Envios e ON e.IdPedido = p.Id
JOIN Pagos g ON g.IdPedido = p.Id;
GO


--  STORED PROCEDURE

CREATE PROCEDURE CrearUsuarioYCliente
(
    @Username       VARCHAR(50),
    @PasswordHash   VARCHAR(200),
    @Nombre         VARCHAR(50),
    @Apellido       VARCHAR(50),
    @Email          VARCHAR(50),
    @Telefono       VARCHAR(50),
    @Direccion      VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @IdUsuario INT;

        INSERT INTO Usuarios (Username, PasswordHash, IdRol)
        VALUES (@Username, @PasswordHash, 1);   -- 1 = Cliente

        SET @IdUsuario = SCOPE_IDENTITY();

        INSERT INTO Clientes (Nombre, Apellido, Email, Telefono, Direccion, FechaRegistro, IdUsuario)
        VALUES (@Nombre, @Apellido, @Email, @Telefono, @Direccion, GETDATE(), @IdUsuario);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW; 
    END CATCH
END;
GO

