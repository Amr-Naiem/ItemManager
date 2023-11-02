using System;
using System.ComponentModel.DataAnnotations;

namespace ItemManager.Models.Domain
{
    public class Item
    {
        [Key]
        public Guid Serial { get; set; }
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int ItemCode { get; set; }
        public string ItemName { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public long DiscountRate { get; set; }
        public decimal DiscountValue { get; set; }
        public string TaxType { get; set; }
        public long TaxRate { get; set; }
        public decimal TaxValue { get; set; }
        public decimal Total { get; set; }

        public void UpdateCalculatedValues()
        {
            SubTotal = UnitPrice * Quantity;
            DiscountValue = UnitPrice * Quantity * (DiscountRate / 100M); // Use decimal for precision
            TaxValue = (SubTotal - DiscountValue) * (TaxRate / 100M); // Use decimal for precision
            Total = SubTotal - DiscountValue + TaxValue;
        }
    }
}
