namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Flights
{
	public class FlightAddRequestDTO
	{
		public int FlightId { get; set; }
		public string FlightNumber { get; set; }
		public string Origin { get; set; }
		public string Destination { get; set; }
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }
		public int Price { get; set; }
	}
}
