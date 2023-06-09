USE [HMO_DB]
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 10/05/2023 22:53:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[PatientId] [int] NOT NULL,
	[FirstName] [varchar](20) NOT NULL,
	[LastName] [varchar](20) NOT NULL,
	[City] [varchar](20) NOT NULL,
	[Street] [varchar](20) NOT NULL,
	[HouseNumber] [int] NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[MobilePhone] [varchar](20) NOT NULL,
	[PositiveResultDate] [date] NOT NULL,
	[RecoveryDate] [date] NOT NULL,
	[Photo] [varbinary](max) NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PatientVaccination]    Script Date: 10/05/2023 22:53:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientVaccination](
	[PatientId] [int] NOT NULL,
	[VaccinationId] [int] NOT NULL,
	[VDate] [date] NULL,
 CONSTRAINT [PK_PatientVaccination] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC,
	[VaccinationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vaccinations]    Script Date: 10/05/2023 22:53:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vaccinations](
	[VaccinationId] [int] NOT NULL,
	[VName] [varchar](20) NOT NULL,
	[Manufacturer] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Vaccinations] PRIMARY KEY CLUSTERED 
(
	[VaccinationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PatientVaccination]  WITH CHECK ADD  CONSTRAINT [FK_PatientVaccination_Patients] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([PatientId])
GO
ALTER TABLE [dbo].[PatientVaccination] CHECK CONSTRAINT [FK_PatientVaccination_Patients]
GO
ALTER TABLE [dbo].[PatientVaccination]  WITH CHECK ADD  CONSTRAINT [FK_PatientVaccination_Vaccinations] FOREIGN KEY([VaccinationId])
REFERENCES [dbo].[Vaccinations] ([VaccinationId])
GO
ALTER TABLE [dbo].[PatientVaccination] CHECK CONSTRAINT [FK_PatientVaccination_Vaccinations]
GO
