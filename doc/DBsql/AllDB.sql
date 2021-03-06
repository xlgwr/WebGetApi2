USE [PanamaDB]
GO
/****** Object:  Table [dbo].[Connections]    Script Date: 2016/6/16 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Connections](
	[Tid] [bigint] IDENTITY(1,1) NOT NULL,
	[nameFrom] [nvarchar](128) NULL,
	[nameFromDesc] [nvarchar](max) NULL,
	[nameFromURL] [nvarchar](max) NULL,
	[nameType] [nvarchar](256) NULL,
	[nameTo] [nvarchar](128) NULL,
	[nameToDesc] [nvarchar](max) NULL,
	[nameToURL] [nvarchar](max) NULL,
	[Remark] [nvarchar](max) NULL,
	[getPage] [int] NOT NULL,
	[tStatus] [int] NOT NULL,
	[ClientIP] [nvarchar](max) NULL,
	[addDate] [datetime] NOT NULL CONSTRAINT [DF__Connectio__addDa__25869641]  DEFAULT (getdate()),
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Connections] PRIMARY KEY CLUSTERED 
(
	[Tid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CountryItems]    Script Date: 2016/6/16 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CountryItems](
	[Tid] [bigint] IDENTITY(1,1) NOT NULL,
	[CountriesIndex] [int] NOT NULL CONSTRAINT [DF__CountryIt__Count__30F848ED]  DEFAULT ((0)),
	[Countries] [nvarchar](128) NULL,
	[CountriesDesc] [nvarchar](max) NULL,
	[ttype] [nvarchar](32) NULL,
	[name] [nvarchar](128) NULL,
	[nameDesc] [nvarchar](max) NULL,
	[nameURL] [nvarchar](max) NULL,
	[Remark] [nvarchar](max) NULL,
	[getPage] [int] NOT NULL,
	[tStatus] [int] NOT NULL,
	[ClientIP] [nvarchar](max) NULL,
	[addDate] [datetime] NOT NULL CONSTRAINT [DF__CountryIt__addDa__2D27B809]  DEFAULT (getdate()),
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.CountryItems] PRIMARY KEY CLUSTERED 
(
	[Tid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EntitysAll]    Script Date: 2016/6/16 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntitysAll](
	[Tid] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](128) NULL,
	[nameDesc] [nvarchar](max) NULL,
	[nameURL] [nvarchar](max) NULL,
	[ttype] [nvarchar](32) NULL,
	[Countries] [nvarchar](max) NULL,
	[Source] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[DormDate] [nvarchar](max) NULL,
	[CompanyType] [nvarchar](max) NULL,
	[Jurisdiction] [nvarchar](max) NULL,
	[Remark] [nvarchar](max) NULL,
	[getPage] [int] NOT NULL,
	[tStatus] [int] NOT NULL,
	[ClientIP] [nvarchar](max) NULL,
	[addDate] [datetime] NOT NULL CONSTRAINT [DF__EntitysAl__addDa__286302EC]  DEFAULT (getdate()),
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.EntitysAll] PRIMARY KEY CLUSTERED 
(
	[Tid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[v_allConnectionName]    Script Date: 2016/6/16 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_allConnectionName]
AS
SELECT DISTINCT nameFrom AS name,nameFromDesc as nameDesc, nameFromURL AS nameURL
FROM      dbo.Connections
UNION
SELECT DISTINCT nameTo AS name, nameToDesc  as nameDesc, nameToURL AS nameURL
FROM      dbo.Connections

GO
/****** Object:  View [dbo].[v_noCountryName]    Script Date: 2016/6/16 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_noCountryName]
AS
SELECT   x.name, y.nameURL, y.nameDesc, x.Countries, x.CountriesDesc
FROM      (SELECT   m.name, n.Countries, n.CountriesDesc
                 FROM      dbo.v_allConnectionName AS m LEFT OUTER JOIN
                                 dbo.CountryItems AS n ON m.name = n.name
                 WHERE   (n.Countries IS NULL)) AS x LEFT OUTER JOIN
                dbo.v_allConnectionName AS y ON x.name = y.name

GO
/****** Object:  View [dbo].[v_noCountryForAdd]    Script Date: 2016/6/16 14:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_noCountryForAdd]
AS
SELECT   m.name, m.nameURL, m.nameDesc, m.Countries, m.CountriesDesc
FROM      dbo.v_noCountryName AS m LEFT OUTER JOIN
                dbo.EntitysAll AS n ON m.name = n.name
WHERE   (n.Tid IS NULL)

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_allConnectionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_allConnectionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "m"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 243
               Right = 182
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "n"
            Begin Extent = 
               Top = 6
               Left = 220
               Bottom = 206
               Right = 392
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_noCountryForAdd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_noCountryForAdd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "y"
            Begin Extent = 
               Top = 6
               Left = 248
               Bottom = 168
               Right = 392
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "x"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 127
               Right = 210
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_noCountryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_noCountryName'
GO
