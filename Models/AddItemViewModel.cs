using System.ComponentModel.DataAnnotations;

namespace ItemManager.Models
{
    public class AddItemViewModel
    {
		public string CustomerName { get; set; }
		public DateTime InvoiceDate { get; set; }
		public int ItemCode { get; set; }
		public string ItemName { get; set; }
		public int UnitPrice { get; set; }
		public int Quantity { get; set; }
		public long SubTotal => UnitPrice * Quantity;
		public long DiscountRate { get; set; }
		public long DiscountValue => UnitPrice * Quantity * (DiscountRate / 100);
		public string TaxType { get; set; }
		public long TaxRate { get; set; }
		public long TaxValue => (SubTotal - DiscountValue) * (TaxRate / 100);
		public long Total => SubTotal - DiscountValue + TaxValue;
	}
}
