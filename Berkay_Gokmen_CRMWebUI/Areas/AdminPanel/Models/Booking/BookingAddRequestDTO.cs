namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Booking
{
	public class BookingAddRequestDTO
	{
		public DateTime BookingDate { get; set; }

		public int FlightId { get; set; }


		public int PassengerId { get; set; }
		public string PassengerName { get; set; }
	}
}
