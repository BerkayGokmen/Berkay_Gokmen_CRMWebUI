using Berkay_Gokmen_CRM.Helper.Const;
using Berkay_Gokmen_CRM.Helper.Result;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Passenger;
using Berkay_Gokmen_CRMWebUI.SessionHelper;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Models.Booking;
using System.Text.Json;

namespace Berkay_Gokmen_CRMWebUI.Areas.AdminPanel.Controllers
{
	[Area("AdminPanel")]
	public class BookingController : Controller
	{
		[HttpGet("/Admin/Bookings")]
		public async Task<IActionResult> Bookings()
		{

			var url = ApiEndPoint.ApiEndPointURL + "/Bookings";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Get);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<List<BookingDTO>>>(response.Content);
			var bookingList = responseObject.Data;
			return View(bookingList);

		}

		[HttpGet("/Admin/Booking/{bookingGuid}")]
		public async Task<IActionResult> GetBooking(Guid bookingGuid)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Booking/" + bookingGuid;
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Get);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<BookingDTO>>(response.Content);
			var booking = responseObject.Data;
			return Json(booking);
		}

		[HttpPost("/Admin/UpdateBooking")]
		public async Task<IActionResult> UpdateBooking(BookingUpdateRequestDTO updateBookingDTO)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Booking/";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Put);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			request.AddBody(JsonSerializer.Serialize(updateBookingDTO));
			RestResponse response = await client.ExecuteAsync(request);
			var responseObject = JsonSerializer.Deserialize<ApiResult<bool>>(response.Content);
			if (response.StatusCode == HttpStatusCode.OK)
			{
				return Json(new { success = true });
			}
			return Json(new { success = false });
		}
		//[ValidateAntiForgeryToken]
		[HttpPost("/Admin/AddBooking")]
		public async Task<IActionResult> AddBooking(BookingAddRequestDTO addBookingDTO)
		{
			var url = ApiEndPoint.ApiEndPointURL + "/Booking/";
			var client = new RestClient(url);
			var request = new RestRequest(url, Method.Post);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", "Bearer " + SessionManager.loginResponse.Token);
			request.AddBody(JsonSerializer.Serialize(addBookingDTO));
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
