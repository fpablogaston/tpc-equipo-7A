USE master;
GO
-- Check if DB exists to prevent errors if it doesn't
IF EXISTS(SELECT * FROM sys.databases WHERE name = 'Ecommerce')
BEGIN
    ALTER DATABASE Ecommerce SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Ecommerce;
END
GO
CREATE DATABASE Ecommerce;
GO
USE Ecommerce;
GO

--  1. TABLAS INDEPENDIENTES O PADRES

-- Categorías
CREATE TABLE Categorias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(150) NULL
);
GO

-- Roles
CREATE TABLE Roles (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);
GO

-- Usuarios (Depende de Roles)
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(200) NOT NULL,
    IdRol INT NOT NULL,
    CONSTRAINT FK_Usuarios_Roles FOREIGN KEY (IdRol)
        REFERENCES Roles(Id)
);
GO

-- Clientes (Depende de Usuarios)
CREATE TABLE Clientes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Email VARCHAR(50) NOT NULL,
    Telefono VARCHAR(50) NOT NULL,
    Direccion VARCHAR(50) NOT NULL,
    Ciudad VARCHAR(100) NULL,
    Provincia VARCHAR(100) NULL,
    CodigoPostal VARCHAR(20) NULL,
    FechaRegistro DATETIME NOT NULL,
    IdUsuario INT NOT NULL, 
    CONSTRAINT FK_Clientes_Usuarios FOREIGN KEY (IdUsuario)
        REFERENCES Usuarios(Id),
    CONSTRAINT UQ_Clientes_IdUsuario UNIQUE (IdUsuario)
);
GO

-- Productos (Depende de Categorias)
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

-- Pedidos (Depende de Clientes)
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

-- Pagos (Depende de Pedidos)
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

-- Envíos (Depende de Pedidos)
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

-- Detalles del Pedido (Depende de Pedidos y Productos)
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

--  2. POBLADO DE DATOS (INSERTS)

-- Roles
INSERT INTO Roles (Nombre) VALUES ('Cliente'), ('Administrador');
GO

-- Usuarios (Admins y Clientes con passwords claros)
INSERT INTO Usuarios (Username, PasswordHash, IdRol)
VALUES
('admin', 'admin123', 2),          -- Id 1: Admin Principal
('ventas', 'ventas123', 2),         -- Id 2: Admin Ventas
('soporte', 'soporte123', 2),       -- Id 3: Admin Soporte
('juan', 'juan123', 1),             -- Id 4: Cliente
('maria', 'maria123', 1),           -- Id 5: Cliente
('carlos', 'carlos123', 1),         -- Id 6: Cliente
('ana', 'ana123', 1),               -- Id 7: Cliente
('luis', 'luis123', 1);             -- Id 8: Cliente
GO

-- Categorias
INSERT INTO Categorias (Nombre, Descripcion)
VALUES
('Electrónica', 'Dispositivos electrónicos, gadgets y accesorios'),
('Ropa', 'Indumentaria para hombres, mujeres y niños'),
('Hogar', 'Muebles, decoración y artículos para el hogar'),
('Deportes', 'Equipamiento deportivo y ropa de entrenamiento'),
('Libros', 'Literatura, educación y entretenimiento');
GO

-- Clientes
INSERT INTO Clientes (Nombre, Apellido, Email, Telefono, Direccion, Ciudad, Provincia, CodigoPostal, FechaRegistro, IdUsuario)
VALUES
('Admin', 'System', 'admin@ecommerce.com', '1111-1111', 'Oficina Central', 'CABA', 'Buenos Aires', '1000', '2020-01-01', 1),
('Ventas', 'Manager', 'ventas@ecommerce.com', '2222-2222', 'Sucursal Norte', 'Rosario', 'Santa Fe', '2000', '2021-03-15', 2),
('Soporte', 'Tecnico', 'soporte@ecommerce.com', '3333-3333', 'Sucursal Sur', 'Córdoba', 'Córdoba', '5000', '2021-06-20', 3),
('Juan', 'Pérez', 'juan@gmail.com', '15-4444-5555', 'Av. Rivadavia 1234', 'CABA', 'Buenos Aires', '1045', GETDATE(), 4),
('María', 'Gómez', 'maria@hotmail.com', '15-6666-7777', 'Calle Falsa 123', 'Lanús', 'Buenos Aires', '1824', GETDATE(), 5),
('Carlos', 'López', 'carlos@yahoo.com', '15-8888-9999', 'San Martín 456', 'Mendoza', 'Mendoza', '5500', GETDATE(), 6),
('Ana', 'Martínez', 'ana@outlook.com', '15-1111-0000', 'Belgrano 789', 'La Plata', 'Buenos Aires', '1900', GETDATE(), 7),
('Luis', 'Rodríguez', 'luis@gmail.com', '15-2222-3333', 'Mitre 101', 'Mar del Plata', 'Buenos Aires', '7600', GETDATE(), 8);
GO

-- Productos (Variados y abundantes)
INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, ImagenUrl, IdCategoria)
VALUES
-- Electrónica (IdCat 1)
('Auriculares Bluetooth Sony', 'Auriculares inalámbricos con cancelación de ruido', 45000, 30, 'https://images.fravega.com/f500/56120d447e6322693658344399d9143b.jpg', 1),
('Smartphone Samsung S23', 'Teléfono inteligente 5G, 128GB', 350000, 15, 'https://images.samsung.com/is/image/samsung/p6pim/ar/sm-s911bzekaro/gallery/ar-galaxy-s23-s911-sm-s911bzekaro-thumb-534844576', 1),
('Smart TV 50" LG', 'Televisor 4K UHD con WebOS', 180000, 10, 'https://www.lg.com/ar/images/televisores/md07547738/gallery/D-01.jpg', 1),
('Notebook HP Pavilion', 'Laptop Intel Core i5, 8GB RAM, 256GB SSD', 250000, 20, 'https://http2.mlstatic.com/D_NQ_NP_966864-MLA46604657203_072021-O.webp', 1),
('Mouse Logitech G502', 'Mouse gamer con pesas ajustables', 12000, 50, 'https://resource.logitechg.com/w_692,c_lpad,ar_4:3,q_auto:best,dpr_1.0,f_auto,d_transparent.gif/content/dam/gaming/en/products/g502-hero/g502-hero-gallery-1.png?v=1', 1),

-- Ropa (IdCat 2)
('Remera Algodón Blanca', 'Remera básica 100% algodón, corte clásico', 7500, 100, 'https://acdn.mitiendanube.com/stores/001/133/747/products/remera-lisa-blanca-algodon-premium-100-ph-frente1-10b0f635ec636f7f3e16105503448381-640-0.jpg', 2),
('Jean Azul Clásico', 'Pantalón de jean corte recto', 18000, 60, 'https://media.istockphoto.com/id/1135048651/photo/blue-jeans-isolated-on-white-background.jpg?s=612x612&w=0&k=20&c=y3b5_3C-t3Kj-Qx-K0x-C-1-1-1-1-1', 2),
('Zapatillas Running Nike', 'Calzado deportivo ligero y cómodo', 45000, 40, 'https://static.nike.com/a/images/t_PDP_1280_v1/f_auto,q_auto:eco/9b333728-f473-4803-822a-844756273763/revolution-6-next-nature-mens-road-running-shoes-XPTbLz.png', 2),
('Campera de Cuero', 'Campera estilo biker de cuero sintético', 35000, 25, 'https://http2.mlstatic.com/D_NQ_NP_796363-MLA43725785704_102020-O.webp', 2),
('Buzo Hoodie Gris', 'Buzo con capucha y bolsillo canguro', 15000, 80, 'https://http2.mlstatic.com/D_NQ_NP_825739-MLA44482062568_012021-O.webp', 2),

-- Hogar (IdCat 3)
('Lámpara LED Escritorio', 'Lámpara flexible con luz regulable', 5000, 90, 'https://http2.mlstatic.com/D_NQ_NP_768482-MLA46462799447_062021-O.webp', 3),
('Sillón 2 Cuerpos', 'Sillón tapizado en tela gris', 120000, 5, 'https://sillonesflorencia.com.ar/wp-content/uploads/2020/06/sillon-2-cuerpos-chenille-gris-1.jpg', 3),
('Juego de Sábanas King', 'Sábanas 100% algodón 400 hilos', 25000, 35, 'https://arredo.vteximg.com.br/arquivos/ids/244526-1000-1000/Sabanas-Liso-144-Hilos-Blanco-0.jpg?v=637660730000000000', 3),
('Set de Cubiertos 24pz', 'Acero inoxidable, diseño moderno', 15000, 50, 'https://http2.mlstatic.com/D_NQ_NP_663578-MLA44783820832_022021-O.webp', 3),

-- Deportes (IdCat 4)
('Pelota de Fútbol Adidas', 'Balón oficial talle 5', 22000, 60, 'https://assets.adidas.com/images/h_840,f_auto,q_auto,fl_lossy,c_fill,g_auto/3bbecbdf584e40398446a969013e839e_9366/Pelota_Al_Rihla_League_Blanco_H57782_01_standard.jpg', 4),
('Mancuernas 5kg (Par)', 'Mancuernas hexagonales recubiertas', 18000, 40, 'https://http2.mlstatic.com/D_NQ_NP_865893-MLA45726907641_042021-O.webp', 4),
('Colchoneta Yoga', 'Mat antideslizante 6mm', 6000, 100, 'https://http2.mlstatic.com/D_NQ_NP_706906-MLA44702832756_012021-O.webp', 4),

-- Libros (IdCat 5)
('El Señor de los Anillos', 'Trilogía completa J.R.R. Tolkien', 30000, 25, 'https://images.cuspide.com/9789505470640.jpg', 5),
('1984 - George Orwell', 'Clásico de ciencia ficción distópica', 8000, 50, 'https://images.cuspide.com/9789875506999.jpg', 5),
('Harry Potter y la Piedra Filosofal', 'Edición especial 20 aniversario', 12000, 40, 'https://images.cuspide.com/9788498389577.jpg', 5);
GO

-- Pedidos (Algunos históricos para el admin)
INSERT INTO Pedidos (FechaPedido, Estado, Total, IdCliente)
VALUES
(DATEADD(day, -10, GETDATE()), 'Entregado', 20000, 4), -- Juan
(DATEADD(day, -5, GETDATE()), 'Enviado', 10500, 5),   -- Maria
(DATEADD(day, -2, GETDATE()), 'Pendiente', 55000, 6), -- Carlos
(GETDATE(), 'Pendiente', 8000, 7);                    -- Ana
GO

-- Detalles de Pedidos
INSERT INTO DetallesPedido (IdPedido, IdProducto, Cantidad, PrecioUnitario, Subtotal)
VALUES
-- Pedido 1 (Juan - Entregado)
(1, 1, 1, 45000, 45000), -- Auriculares (Precio viejo simulado en total pedido arriba, ajustaremos)
-- Pedido 2 (Maria - Enviado)
(2, 6, 1, 7500, 7500),  -- Remera
(2, 11, 1, 5000, 5000), -- Lampara (Total 12500)
-- Pedido 3 (Carlos - Pendiente)
(3, 8, 1, 45000, 45000), -- Zapatillas
(3, 18, 1, 8000, 8000),  -- Libro 1984
-- Pedido 4 (Ana - Pendiente)
(4, 19, 1, 8000, 8000);  -- Libro 1984
GO

-- Pagos
INSERT INTO Pagos (MetodoPago, Estado, Monto, FechaPago, IdPedido)
VALUES
('Tarjeta de crédito', 'Aprobado', 20000, DATEADD(day, -10, GETDATE()), 1),
('Transferencia bancaria', 'Aprobado', 12500, DATEADD(day, -5, GETDATE()), 2),
('Efectivo', 'Pendiente', 53000, DATEADD(day, -2, GETDATE()), 3),
('Tarjeta de débito', 'Aprobado', 8000, GETDATE(), 4);
GO

-- Envios
INSERT INTO Envios (DireccionEnvio, Ciudad, Provincia, CodigoPostal, FechaEnvio, FechaEntrega, Estado, IdPedido)
VALUES
('Av. Rivadavia 1234', 'CABA', 'Buenos Aires', '1045', DATEADD(day, -9, GETDATE()), DATEADD(day, -8, GETDATE()), 'Entregado', 1),
('Calle Falsa 123', 'Lanús', 'Buenos Aires', '1824', DATEADD(day, -4, GETDATE()), NULL, 'En Camino', 2),
('San Martín 456', 'Mendoza', 'Mendoza', '5500', NULL, NULL, 'Pendiente', 3),
('Belgrano 789', 'La Plata', 'Buenos Aires', '1900', NULL, NULL, 'Pendiente', 4);
GO

-- Actualizar referencias circulares
UPDATE p
SET p.IdEnvio = e.Id, p.IdPago = g.Id
FROM Pedidos p
JOIN Envios e ON e.IdPedido = p.Id
JOIN Pagos g ON g.IdPedido = p.Id;
GO

--  3. STORED PROCEDURE

CREATE PROCEDURE CrearUsuarioYCliente
(
    @Username       VARCHAR(50),
    @PasswordHash   VARCHAR(200),
    @Nombre         VARCHAR(50),
    @Apellido       VARCHAR(50),
    @Email          VARCHAR(50),
    @Telefono       VARCHAR(50),
    @Direccion      VARCHAR(100),
    @Ciudad         VARCHAR(100) = NULL,
    @Provincia      VARCHAR(100) = NULL,
    @CodigoPostal   VARCHAR(100) = NULL
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

        INSERT INTO Clientes (Nombre, Apellido, Email, Telefono, Direccion, Ciudad, Provincia, CodigoPostal, FechaRegistro, IdUsuario)
        VALUES (@Nombre, @Apellido, @Email, @Telefono, @Direccion, @Ciudad, @Provincia, @CodigoPostal, GETDATE(), @IdUsuario);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW; 
    END CATCH
END;
GO

-- Verificación final
SELECT 'Usuarios' as Tabla, COUNT(*) as Cantidad FROM Usuarios
UNION ALL
SELECT 'Productos', COUNT(*) FROM Productos
UNION ALL
SELECT 'Pedidos', COUNT(*) FROM Pedidos;

SELECT * FROM Pedidos
SELECT * FROM Envios
SELECT * FROM Clientes

Select Id, FechaPedido, Estado, Total, IdCliente, IdEnvio, IdPago From Pedidos


-- ... existing code ...
-- Envíos
CREATE TABLE Envios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DireccionEnvio VARCHAR(50) NULL,
    Ciudad VARCHAR(50) NULL,
    Provincia VARCHAR(50) NULL,
    CodigoPostal VARCHAR(50) NULL,
    FechaEnvio DATETIME NULL,
    FechaEntrega DATETIME NULL,
    Estado VARCHAR(50) NULL, -- This will be replaced/updated
    IdPedido INT NOT NULL,
    CONSTRAINT FK_Envios_Pedidos FOREIGN KEY (IdPedido)
        REFERENCES Pedidos(Id)
);
GO

-- NEW TABLE: EstadoEnvio
CREATE TABLE EstadoEnvio (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(50) NOT NULL
);
GO

-- POPULATE EstadoEnvio
INSERT INTO EstadoEnvio (Descripcion) VALUES 
('Pendiente'), 
('Preparando'), 
('En Camino'), 
('Entregado'), 
('Cancelado'), 
('Retiro en Local');
GO

-- MODIFY Envios table to use IdEstadoEnvio instead of Estado string
-- First, add the new column
ALTER TABLE Envios ADD IdEstadoEnvio INT NULL;
GO

-- Migrate existing data (assuming 'Estado' string matches descriptions)
UPDATE Envios SET IdEstadoEnvio = (SELECT Id FROM EstadoEnvio WHERE Descripcion = Envios.Estado);
GO

-- If data migration leaves NULLs (because string didn't match), default to 1 (Pendiente)
UPDATE Envios SET IdEstadoEnvio = 1 WHERE IdEstadoEnvio IS NULL;
GO

-- Make it NOT NULL and add Foreign Key
ALTER TABLE Envios ALTER COLUMN IdEstadoEnvio INT NOT NULL;
ALTER TABLE Envios ADD CONSTRAINT FK_Envios_EstadoEnvio FOREIGN KEY (IdEstadoEnvio) REFERENCES EstadoEnvio(Id);
GO

-- Drop the old string column
ALTER TABLE Envios DROP COLUMN Estado;
GO

-- 1. Create table for Order Statuses (Milestones)
CREATE TABLE EstadoPedido (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(50) NOT NULL
);
GO

-- 2. Populate standard milestones
INSERT INTO EstadoPedido (Descripcion) VALUES 
('Pendiente de Pago'), 
('Pagado'), 
('En Preparación'), 
('En Camino'), 
('Listo para Retiro'),
('Entregado'), 
('Cancelado');
GO

-- 3. Update Pedidos table to use IdEstadoPedido
-- Add column
ALTER TABLE Pedidos ADD IdEstadoPedido INT NULL;
GO

-- Migrate existing string data to IDs (Best effort)
UPDATE Pedidos SET IdEstadoPedido = 1 WHERE Estado = 'Pendiente';
UPDATE Pedidos SET IdEstadoPedido = 6 WHERE Estado = 'Entregado';
UPDATE Pedidos SET IdEstadoPedido = 4 WHERE Estado = 'Enviado';
UPDATE Pedidos SET IdEstadoPedido = 1 WHERE IdEstadoPedido IS NULL; -- Default
GO

-- Make NOT NULL and add Foreign Key
ALTER TABLE Pedidos ALTER COLUMN IdEstadoPedido INT NOT NULL;
ALTER TABLE Pedidos ADD CONSTRAINT FK_Pedidos_EstadoPedido FOREIGN KEY (IdEstadoPedido) REFERENCES EstadoPedido(Id);
GO

-- Drop old string column
ALTER TABLE Pedidos DROP COLUMN Estado;
GO