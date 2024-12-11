using System.Net;
using System.Text.Json;
using Berkay_Gokmen_CRM.Helper.Const;
using Berkay_Gokmen_CRM.Helper.Result;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Passenger;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Payment;
using Berkay_Gokmen_CRMWebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Controllers
{
	[Area("AdminPanel")]

	public class PaymentController : Controller
	{
		[HttpGet("/Admin/Payments")]
		
		public async Task< IActionResult> Payments()
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Payments";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Get);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<List<PaymentDTO>>>(response.Content);
			var paymentlist = responseObject.Data;
			return View(paymentlist);

			
		}

		[HttpGet("/Admin/Payment/{paymentguid}")]
		public async Task<IActionResult> GetPayment(Guid paymentguid)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Payment/" + paymentguid;
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Get);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<PaymentDTO>>(response.Content);
			var payment = responseObject.Data;
			return Json(payment);
		}

		[HttpPost("/Admin/UpdatePayment")]
		public async Task<IActionResult> UpdatePayment(PaymentUpdateRequestDTO updatePaymentDTO)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Payment/";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Put);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			request.AddBody(JsonSerializer.Serialize(updatePaymentDTO));
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				return Json(new { success = true });
			}
			return Json(new { success = false });
		}
		//[ValidateAntiForgeryToken]
		[HttpPost("/Admin/AddPayment")]
		public async Task<IActionResult> AddPayment(PaymentAddRequestDTO addPaymentDTO)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Payment/";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Post);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			request.AddBody(JsonSerializer.Serialize(addPaymentDTO));
			RestResponse response = await client.ExecuteAsync(request);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
				return Json(new { success = true });
			}
			var responseErrorObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
			return Json(new { success = false, HataAciklama = responseErrorObject.HataBilgisi.HataAciklama[0] });
		}
	}

}

