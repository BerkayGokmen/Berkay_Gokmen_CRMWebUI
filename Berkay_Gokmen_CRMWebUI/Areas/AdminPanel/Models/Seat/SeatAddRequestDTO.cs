﻿namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Seat
{
	public class SeatAddRequestDTO
	{
		public string SeatNumber { get; set; }
		public string IsBooked { get; set; }

		public int FlightId { get; set; }
		public string Flight { get; set; }
		
		
	}
}