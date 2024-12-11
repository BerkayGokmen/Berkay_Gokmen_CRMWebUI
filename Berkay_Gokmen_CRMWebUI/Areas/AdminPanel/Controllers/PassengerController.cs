using System.Net;
using System.Text.Json;
using Berkay_Gokmen_CRM.Helper.Const;
using Berkay_Gokmen_CRM.Helper.Result;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Login.DTO;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Passenger;
using Berkay_Gokmen_CRMWebUI.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class PassengerController : Controller
    {
        [HttpGet("/Admin/Yolcular")]
        public async Task< IActionResult> Passengers()
        {
            var url = ApiEndPoint.ApiEndPointURL + "/Passengers";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization","Bearer "+SessionManager.loginResponse.Token);
            RestResponse response = await client.ExecuteAsync(request);
            var responseObject = JsonSerializer.Deserialize<ApiResult<List<PassengerDTO>>>(response.Content);
            var passengerList = responseObject.Data;
            return View(passengerList);
     
        }

        [HttpGet("/Admin/Yolcu/{passengerGuid}")]
        public async Task<IActionResult> GetPassenger(Guid passengerGuid)
        {
			var url = ApiEndPoint.ApiEndPointURL + "/Passenger/"+ passengerGuid;
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Get);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<PassengerDTO>>(response.Content);
			var passenger = responseObject.Data;
			return Json(passenger);
		}

		[HttpPost("/Admin/UpdatePassenger")]
		public async Task<IActionResult> UpdatePassenger(PassengerUpdateRequestDTO updatePassengerDTO)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Passenger/";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Put);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			request.AddBody(JsonSerializer.Serialize(updatePassengerDTO));
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
			if(response.StatusCode==HttpStatusCode.OK)
			{
				return Json(new {success= true});
			}
			return Json(new {success=false});
		}
		//[ValidateAntiForgeryToken]
		[HttpPost("/Admin/AddPassenger")]
		public async Task<IActionResult> AddPassenger(PassengerAddRequestDTO addPassengerDTO)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Passenger/";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Post);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			request.AddBody(JsonSerializer.Serialize(addPassengerDTO));
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
