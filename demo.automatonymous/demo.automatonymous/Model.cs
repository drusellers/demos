using System;
using System.Collections.Generic;

namespace demo.auto
{
    public class Order
    {
        public int Id { get; set; }
        public string PurchaseOrder { get; set; }
        public IList<OrderLine> Lines { get; set; }
    }

    public class OrderLine
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
    }

    public class Shipment
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; }
        public IList<ShipmentLine> Lines { get; set; }
        public Invoice Invoice { get; set; }
    }

    public class ShipmentLine
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
    }

    public class Invoice
    {
        public int Id { get; set; }
        public DateTime ShippedOn { get; set; }
        public decimal FreightCost { get; set; }
        public IList<InvoiceLine> Lines { get; set; }
    }

    public class InvoiceLine
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
    }
}
