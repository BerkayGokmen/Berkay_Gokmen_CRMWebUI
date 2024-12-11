using Microsoft.AspNetCore.Mvc;

namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Controllers
{
	[Area("AdminPanel")]
	public class AboutController : Controller
	{
		[HttpGet("/Admin/Hakkımızda")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
