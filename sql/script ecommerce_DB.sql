use master 
go
create database Ecommerce
go
use Ecommerce
go

CREATE TABLE [dbo].[Categorias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](150) NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Clientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Telefono] [varchar](50) NOT NULL,
	[Direccion] [varchar](50) NOT NULL,
	[Contraseña] [varchar](50) NOT NULL,
	[FechaRegistro] [datetime] NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Productos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](150) NULL,
	[Precio] [money] NULL,
	[Stock] [int] NULL,
	[ImagenUrl] [varchar](50) NULL,
	[IdCategoria] [int] NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Categorias] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categorias] ([Id])
GO

ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Categorias]
GO

CREATE TABLE [dbo].[Pagos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MetodoPago] [varchar](50) NOT NULL,
	[Estado] [varchar](50) NOT NULL,
	[Monto] [money] NOT NULL,
	[FechaPago] [datetime] NOT NULL,
	[IdPedido] [int] NOT NULL,
 CONSTRAINT [PK_Pagos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Envios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DireccionEnvio] [varchar](50) NULL,
	[Ciudad] [varchar](50) NULL,
	[Provincia] [varchar](50) NULL,
	[CodigoPostal] [varchar](50) NULL,
	[FechaEnvio] [datetime] NULL,
	[FechaEntrega] [datetime] NULL,
	[Estado] [varchar](50) NULL,
	[IdPedido] [int] NOT NULL,
 CONSTRAINT [PK_Envios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DetallePedidos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cantidad] [int] NULL,
	[PrecioUnitario] [money] NULL,
	[Subtotal] [money] NULL,
	[IdPedido] [int] NOT NULL,
 CONSTRAINT [PK_DetallePedidos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Pedidos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FechaPedido] [datetime] NULL,
	[Estado] [varchar](50) NULL,
	[Total] [money] NULL,
	[IdCliente] [int] NULL,
	[IdEnvio] [int] NULL,
	[IdPago] [int] NULL,
 CONSTRAINT [PK_Pedidos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_Pedidos] FOREIGN KEY([IdPedido])
REFERENCES [dbo].[Pedidos] ([Id])
GO

ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_Pedidos]
GO

ALTER TABLE [dbo].[Envios]  WITH CHECK ADD  CONSTRAINT [FK_Envios_Pedidos] FOREIGN KEY([IdPedido])
REFERENCES [dbo].[Pedidos] ([Id])
GO

ALTER TABLE [dbo].[Envios] CHECK CONSTRAINT [FK_Envios_Pedidos]
GO

ALTER TABLE [dbo].[DetallePedidos]  WITH CHECK ADD  CONSTRAINT [FK_DetallePedidos_Pedidos] FOREIGN KEY([IdPedido])
REFERENCES [dbo].[Pedidos] ([Id])
GO

ALTER TABLE [dbo].[DetallePedidos] CHECK CONSTRAINT [FK_DetallePedidos_Pedidos]
GO

ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Clientes] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Clientes] ([Id])
GO

ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Clientes]
GO

ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Envios] FOREIGN KEY([IdEnvio])
REFERENCES [dbo].[Envios] ([Id])
GO

ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Envios]
GO

ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Pagos] FOREIGN KEY([IdPago])
REFERENCES [dbo].[Pagos] ([Id])
GO

ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Pagos]
GO

USE Ecommerce;
GO

INSERT INTO Categorias (Nombre, Descripcion)
VALUES
('Electrónica', 'Dispositivos electrónicos y accesorios'),
('Ropa', 'Indumentaria para todas las edades'),
('Hogar', 'Artículos para el hogar y decoración');
GO

INSERT INTO Clientes (Nombre, Apellido, Email, Telefono, Direccion, Contraseña, FechaRegistro)
VALUES
('Juan', 'Pérez', 'juanperez@email.com', '1111111111', 'Av. Siempre Viva 742', '1234', GETDATE()),
('María', 'Gómez', 'maria@email.com', '2222222222', 'Calle Falsa 123', 'abcd', GETDATE());
GO

INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, ImagenUrl, IdCategoria)
VALUES
('Auriculares Bluetooth', 'Auriculares inalámbricos con micrófono', 15000, 50, 'auriculares.jpg', 1),
('Remera Algodón', 'Remera básica de algodón 100%', 7000, 100, 'remera.jpg', 2),
('Lámpara LED', 'Lámpara LED de bajo consumo', 3500, 80, 'lampara.jpg', 3);
GO

INSERT INTO Pedidos (FechaPedido, Estado, Total, IdCliente, IdEnvio, IdPago)
VALUES
(GETDATE(), 'Pendiente', 20000, 1, NULL, NULL),
(GETDATE(), 'Pendiente', 10500, 2, NULL, NULL);
GO

INSERT INTO DetallePedidos (Cantidad, PrecioUnitario, Subtotal, IdPedido)
VALUES
(1, 15000, 15000, 1),
(1, 5000, 5000, 1),
(3, 3500, 10500, 2);
GO

INSERT INTO Pagos (MetodoPago, Estado, Monto, FechaPago, IdPedido)
VALUES
('Tarjeta de crédito', 'Aprobado', 20000, GETDATE(), 1),
('Transferencia bancaria', 'Pendiente', 10500, GETDATE(), 2);
GO

INSERT INTO Envios (DireccionEnvio, Ciudad, Provincia, CodigoPostal, FechaEnvio, FechaEntrega, Estado, IdPedido)
VALUES
('Av. Siempre Viva 742', 'Buenos Aires', 'Buenos Aires', '1000', GETDATE(), NULL, 'Preparando', 1),
('Calle Falsa 123', 'Córdoba', 'Córdoba', '5000', GETDATE(), NULL, 'Preparando', 2);
GO

UPDATE p
SET p.IdEnvio = e.Id, p.IdPago = g.Id
FROM Pedidos p
JOIN Envios e ON e.IdPedido = p.Id
JOIN Pagos g ON g.IdPedido = p.Id;
GO

SELECT * FROM Categorias;
SELECT * FROM Productos;
SELECT * FROM Clientes;
SELECT * FROM Pedidos;
SELECT * FROM DetallePedidos;
SELECT * FROM Pagos;
SELECT * FROM Envios;
GO
