namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Login.DTO
{
	public class LoginResponse
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
        public string Token { get; set; }

    }
}