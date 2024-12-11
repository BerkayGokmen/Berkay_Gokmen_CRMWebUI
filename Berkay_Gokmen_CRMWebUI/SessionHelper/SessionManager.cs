using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Login.DTO;

namespace Berkay_Gokmen_CRMWebUI.SessionHelper
{
	public class SessionManager
	{
		public static LoginResponse? loginResponse
		{
			get => AppHttpContext.Current.Session.GetObject<LoginResponse>("LoginResponse");
			set => AppHttpContext.Current.Session.SetObject("LoginResponse", value);
		}
	}
}
