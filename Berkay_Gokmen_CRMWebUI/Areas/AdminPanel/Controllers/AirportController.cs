using System.Net;
using System.Text.Json;
using Berkay_Gokmen_CRM.Helper.Const;
using Berkay_Gokmen_CRM.Helper.Result;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Airports;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Passenger;
using Berkay_Gokmen_CRMWebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Controllers
{
	[Area("AdminPanel")]
	public class AirportController : Controller
	{
		[HttpGet("/Admin/Airports")]


		public async Task<IActionResult> Index()
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Airports";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Get);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<List<AirportDTO>>>(response.Content);
			var airportList = responseObject.Data;
			return View(airportList);
		}
		//[ValidateAntiForgeryToken]
		[HttpPost("/Admin/AddAirport")]
		public async Task<IActionResult> AddAirport(AddAirportDTORequest addAirportDTORequest)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Airport/";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Post);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			request.AddBody(JsonSerializer.Serialize(addAirportDTORequest));
			RestResponse response = await client.ExecuteAsync(request);

			
			
			if(response.StatusCode==HttpStatusCode.OK)
			{
				var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
				return Json(new {success= true});
			}
			var responseErrorObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
			return Json(new { success = false });

		}

		[HttpGet("/Admin/Havaalanı/{airportGuid}")]
		public async Task<IActionResult> GetAirport(Guid airportGuid)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Airport/" + airportGuid;
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Get);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<AirportDTO>>(response.Content);
			var airport = responseObject.Data;
			return Json(airport);
		}



		[HttpPost("/Admin/UpdateAirport")]
		public async Task<IActionResult> UpdateAirport(AirportUpdateRequestDTO updateAirportDTO)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Airport/";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Put);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			request.AddBody(JsonSerializer.Serialize(updateAirportDTO));
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				return Json(new { success = true });
			}
			return Json(new { success = false });
		}
	}
}
