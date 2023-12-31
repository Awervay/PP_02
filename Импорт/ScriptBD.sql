USE [VinidiktovDEMnovember]
GO
/****** Object:  Table [dbo].[City]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[ID_City] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[ID_City] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Street]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Street](
	[ID_Street] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Street] PRIMARY KEY CLUSTERED 
(
	[ID_Street] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ID_Client] [int] NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ForeName] [nvarchar](50) NOT NULL,
	[Series] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[DataOfBirthday] [date] NOT NULL,
	[Index] [int] NOT NULL,
	[ID_City] [int] NOT NULL,
	[ID_Street] [int] NOT NULL,
	[Home] [int] NOT NULL,
	[Flat] [int] NOT NULL,
	[E-mail] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ID_Client] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID_Order] [int] NOT NULL,
	[DateCreation] [date] NOT NULL,
	[OrderTime] [time](7) NOT NULL,
	[ID_Client] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[ClosingDate] [date] NULL,
	[RentalTime] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID_Order] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceandOrder]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceandOrder](
	[ID_Order] [nvarchar](50) NOT NULL,
	[DateCreate] [date] NOT NULL,
	[ID_Client] [int] NOT NULL,
	[ID_Service] [int] NULL,
	[NumberOrder] [int] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_ServiceandOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[ID_Service] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ServiceCode] [nvarchar](50) NOT NULL,
	[Cost] [money] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ID_Service] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[ID_Status] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[ID_Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewingOrders]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewingOrders] AS
SELECT Client.ID_Client, CONCAT(Client.Surname, ' ', Client.Name, ' ',Client.Forename) AS Client, Service.Name AS Service,
City.Title as City,
[Order].DateCreation, [Order].OrderTime, [Order].ClosingDate, [Order].RentalTime, Status.Name AS Status, 
Street.ID_Street, Service.Cost
FROM City JOIN
Client ON City.ID_City = Client.ID_City JOIN
[Order] ON Client.ID_Client = [Order].ID_Client JOIN
ServiceandOrder ON Client.ID_Client = ServiceandOrder.ID_Client AND [Order].ID_Order = ServiceandOrder.NumberOrder JOIN
Service ON ServiceandOrder.ID_Service = Service.ID_Service JOIN
Status ON [Order].Status = Status.ID_Status JOIN
Street ON Client.ID_Street = Street.ID_Street
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[ID_Employee] [int] NOT NULL,
	[ID_Post] [int] NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Forename] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[ID_Employee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[History_Employee]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History_Employee](
	[ID_Employee] [int] NOT NULL,
	[Date] [datetime] NULL,
	[ID_Input] [int] NOT NULL,
	[ID_History] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_History] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type_input]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type_input](
	[ID_Input] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Type_input] PRIMARY KEY CLUSTERED 
(
	[ID_Input] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ViewingEmployee]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewingEmployee] AS
SELECT CONCAT(Employee.Surname,' ', Employee.Name, ' ', Employee.Forename) AS Employee, 
Employee.Login, History_Employee.ID_History,
History_Employee.Date,
Employee.ID_Employee, Type_input.Name AS Input
FROM Type_input
JOIN History_Employee ON Type_input.ID_Input = History_Employee.ID_Input 
JOIN dbo.Employee ON History_Employee.ID_Employee = Employee.ID_Employee
GO
/****** Object:  View [dbo].[ViewingClient]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewingClient] AS
SELECT ID_Client, CONCAT(Client.Surname, ' ', Client.Name, ' ', Client.ForeName) AS Client, Client.Series, Client.Number, Client.DataOfBirthday, Client.[Index], City.Title, Street.Name AS Street, Client.Home, Client.Flat, 
Client.[E-mail] as Email, Client.Password
FROM Client JOIN
City ON dbo.Client.ID_City = dbo.City.ID_City JOIN
Street ON dbo.Client.ID_Street = dbo.Street.ID_Street
GO
/****** Object:  Table [dbo].[Post]    Script Date: 06.12.2023 18:23:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[ID_Post] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[ID_Post] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_City] FOREIGN KEY([ID_City])
REFERENCES [dbo].[City] ([ID_City])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_City]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Street] FOREIGN KEY([ID_Street])
REFERENCES [dbo].[Street] ([ID_Street])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Street]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Post] FOREIGN KEY([ID_Post])
REFERENCES [dbo].[Post] ([ID_Post])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Post]
GO
ALTER TABLE [dbo].[History_Employee]  WITH CHECK ADD  CONSTRAINT [FK_History_Employee_Employee] FOREIGN KEY([ID_Employee])
REFERENCES [dbo].[Employee] ([ID_Employee])
GO
ALTER TABLE [dbo].[History_Employee] CHECK CONSTRAINT [FK_History_Employee_Employee]
GO
ALTER TABLE [dbo].[History_Employee]  WITH CHECK ADD  CONSTRAINT [FK_History_Employee_Type_input] FOREIGN KEY([ID_Input])
REFERENCES [dbo].[Type_input] ([ID_Input])
GO
ALTER TABLE [dbo].[History_Employee] CHECK CONSTRAINT [FK_History_Employee_Type_input]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Client] FOREIGN KEY([ID_Client])
REFERENCES [dbo].[Client] ([ID_Client])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Client]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Status] FOREIGN KEY([Status])
REFERENCES [dbo].[Status] ([ID_Status])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Status]
GO
ALTER TABLE [dbo].[ServiceandOrder]  WITH CHECK ADD  CONSTRAINT [FK_ServiceandOrder_Client] FOREIGN KEY([ID_Client])
REFERENCES [dbo].[Client] ([ID_Client])
GO
ALTER TABLE [dbo].[ServiceandOrder] CHECK CONSTRAINT [FK_ServiceandOrder_Client]
GO
ALTER TABLE [dbo].[ServiceandOrder]  WITH CHECK ADD  CONSTRAINT [FK_ServiceandOrder_Order] FOREIGN KEY([NumberOrder])
REFERENCES [dbo].[Order] ([ID_Order])
GO
ALTER TABLE [dbo].[ServiceandOrder] CHECK CONSTRAINT [FK_ServiceandOrder_Order]
GO
ALTER TABLE [dbo].[ServiceandOrder]  WITH CHECK ADD  CONSTRAINT [FK_ServiceandOrder_Service] FOREIGN KEY([ID_Service])
REFERENCES [dbo].[Service] ([ID_Service])
GO
ALTER TABLE [dbo].[ServiceandOrder] CHECK CONSTRAINT [FK_ServiceandOrder_Service]
GO
