using System.Net;
using System.Text.Json;
using Berkay_Gokmen_CRM.Helper.Const;
using Berkay_Gokmen_CRM.Helper.Result;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Login.DTO;
using Berkay_Gokmen_CRMWebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Controllers
{
	[Area("AdminPanel")]
	[Route("[action]")]
	public class AccountController : Controller
	{
		[HttpGet("/AdminAccount/Login")]
		public IActionResult LoginPage()
		{
			return View();
		}
		[HttpPost("/Account/AdminLogin")]
		public async Task<IActionResult> AdminLogin(LoginRequestDTO loginRequestDTO)
		{
			var url = ApiEndPoint.ApiEndPointURL+"/Login";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Post);
			request.AddHeader("Content-Type", "application/json");
			var body = JsonSerializer.Serialize(loginRequestDTO);
			request.AddBody(body, "application/json");
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<LoginResponse>>(response.Content);
			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				ViewData["LoginError"] = "Kullanıcı Adı veya Şifre Yanlış";
				return View("LoginPage");

			}
			else
			{
				SessionManager.loginResponse = responseObject.Data;
				return RedirectToAction("Index", "Home");

			}

		}
		public async Task<IActionResult> LogOut()
		{
			SessionManager.loginResponse = null;
			return RedirectToAction("LoginPage", "Account");
		}
	}
}
