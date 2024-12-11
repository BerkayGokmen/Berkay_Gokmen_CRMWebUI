namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Payment
{
	public class PaymentDTO
	{
		public int Amount { get; set; }
		public DateTime PaymentDate { get; set; }
		public string PaymentMethod { get; set; }


		public int BookingId { get; set; }
		public string Booking { get; set; }
        public Guid Guid { get; set; }
    }
}
