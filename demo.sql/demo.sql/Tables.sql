CREATE TABLE [dbo].[OrderHeaders](
	[OrderHeaderId] [int] IDENTITY(1,1) NOT NULL,
	[DateEntered] [datetime2](7) NOT NULL,
	[ShipToFirstName] [varchar](250) NULL,
	[ShipToLastName] [varchar](250) NULL,
	[ShipToStreetAddress] [varchar](250) NULL,
	[ShipToCity] [varchar](250) NULL,
	[ShipToState] [varchar](250) NULL,
	[ShipToZipCode] [varchar](250) NULL,
	[PurchaseOrderNumber] [varchar](250) NULL,
	[RetailerReferenceNumber] [varchar](250) NULL,
	[OrderStatusId] [int] NOT NULL,
	[RetailerId] [int] NOT NULL,
	[DistributorId] [int] NOT NULL,
	[ShipmentMethodId] [int] NULL,
 CONSTRAINT [pk_OrderHeaders] PRIMARY KEY CLUSTERED 
(
	[OrderHeaderId] ASC
))

CREATE TYPE InsertList as TABLE (
   
    [DateEntered] [datetime2](7) NOT NULL,
    [ShipToFirstName] [varchar](250) NULL,
    [ShipToLastName] [varchar](250) NULL,
    [ShipToStreetAddress] [varchar](250) NULL,
    [ShipToCity] [varchar](250) NULL,
    [ShipToState] [varchar](250) NULL,
    [ShipToZipCode] [varchar](250) NULL,
    [PurchaseOrderNumber] [varchar](250) NULL,
    [RetailerReferenceNumber] [varchar](250) NULL,
    [OrderStatusId] [int] NOT NULL,
    [RetailerId] [int] NOT NULL,
    [DistributorId] [int] NOT NULL,
    [ShipmentMethodId] [int] NULL
    )

	CREATE PROCEDURE Insert_OrderHeaders
@TheDataList InsertList READONLY
AS
INSERT INTO OrderHeaders(
     
    [DateEntered],
    [ShipToFirstName] ,
    [ShipToLastName] ,
    [ShipToStreetAddress] ,
    [ShipToCity] ,
    [ShipToState] ,
    [ShipToZipCode] ,
    [PurchaseOrderNumber] ,
    [RetailerReferenceNumber] ,
    [OrderStatusId] ,
    [RetailerId] ,
    [DistributorId] ,
    [ShipmentMethodId] 
    )
SELECT
     
    [DateEntered],
    [ShipToFirstName] ,
    [ShipToLastName] ,
    [ShipToStreetAddress] ,
    [ShipToCity] ,
    [ShipToState] ,
    [ShipToZipCode] ,
    [PurchaseOrderNumber] ,
    [RetailerReferenceNumber] ,
    [OrderStatusId] ,
    [RetailerId] ,
    [DistributorId] ,
    [ShipmentMethodId] 
FROM
    @TheDataList