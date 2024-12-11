namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Airports
{
	public class AddAirportDTORequest
	{
		public string AirportName { get; set; }
		public string Code { get; set; }
		public string Location { get; set; }

		public string DepartureFlights { get; set; }

		public string ArrivalFlights { get; set; }

	}
}
