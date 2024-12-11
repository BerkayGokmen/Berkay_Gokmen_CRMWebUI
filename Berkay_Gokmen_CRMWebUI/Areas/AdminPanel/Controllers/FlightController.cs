using System.Net;
using System.Text.Json;
using Berkay_Gokmen_CRM.Helper.Const;
using Berkay_Gokmen_CRM.Helper.Result;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Airports;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Flights;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Passenger;
using Berkay_Gokmen_CRMWebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Controllers
{
	[Area("AdminPanel")]
	public class FlightController : Controller
	{
		[HttpGet("/Admin/Flights")]
		public async Task <IActionResult> Flights()
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Flights";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Get);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<List<FlightDTO>>>(response.Content);
			var flightList = responseObject.Data;
			return View(flightList);
		
		}
		[HttpGet("/Admin/Flight/{flightguid}")]
		public async Task<IActionResult> GetFlight(Guid flightguid)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Flight/" + flightguid;
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Get);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<FlightDTO>>(response.Content);
			var flight = responseObject.Data;
			return Json(flight);
		}

		[HttpPost("/Admin/UpdateFlight")]
		public async Task<IActionResult> UpdateFlight(FlightRequestDTO flightRequestDTO)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Flight/";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Put);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			request.AddBody(JsonSerializer.Serialize(flightRequestDTO));
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				return Json(new { success = true });
			}
			return Json(new { success = false });
		}
		//[ValidateAntiForgeryToken]
		[HttpPost("/Admin/AddFlight")]
		public async Task<IActionResult> AddFlight(FlightAddRequestDTO flightRequestDTO)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Flight/";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Post);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			request.AddBody(JsonSerializer.Serialize(flightRequestDTO));
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
