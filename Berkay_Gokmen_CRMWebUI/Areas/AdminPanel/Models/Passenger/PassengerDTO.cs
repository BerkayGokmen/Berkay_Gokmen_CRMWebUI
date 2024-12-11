namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Passenger
{
    public class PassengerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassengerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Guid? Guid { get; set; }

    }
}
