namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Airports
{
	public class AirportDTO
	{
		public string AirportName { get; set; }
		public string Code { get; set; }
		public string Location { get; set; }

		public string DepartureFlights { get; set; }

		public string ArrivalFlights { get; set; }
		public Guid? guid { get; set; }
	}
}
