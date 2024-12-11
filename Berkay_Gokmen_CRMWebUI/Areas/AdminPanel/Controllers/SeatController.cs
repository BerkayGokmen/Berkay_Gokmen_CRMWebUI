using Berkay_Gokmen_CRM.Helper.Const;
using Berkay_Gokmen_CRM.Helper.Result;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Payment;
using Berkay_Gokmen_CRMWebUI.SessionHelper;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Seat;
using System.Text.Json;

namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Controllers
{
	[Area("AdminPanel")]
	public class SeatController : Controller
	{
		[HttpGet("/Admin/Seats")]

		public async Task<IActionResult> Seats()
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Seats";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Get);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<List<SeatDTO>>>(response.Content);
			var seatList = responseObject.Data;
			return View(seatList);


		}

		[HttpGet("/Admin/Seat/{seatguid}")]
		public async Task<IActionResult> GetSeat(Guid seatguid)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Seat/" + seatguid;
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Get);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<SeatDTO>>(response.Content);
			var seat = responseObject.Data;
			return Json(seat);
		}

		[HttpPost("/Admin/UpdateSeat")]
		public async Task<IActionResult> UpdateSeat(SeatUpdateRequestDTO updateSeatDTO)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Seat/";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Put);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			request.AddBody(JsonSerializer.Serialize(updateSeatDTO));
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				return Json(new { success = true });
			}
			return Json(new { success = false });
		}
		//[ValidateAntiForgeryToken]
		[HttpPost("/Admin/AddSeat")]
		public async Task<IActionResult> AddSeat(SeatAddRequestDTO addSeatDTO)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Seat/";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Post);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			request.AddBody(JsonSerializer.Serialize(addSeatDTO));
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
