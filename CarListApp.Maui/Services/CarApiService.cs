using CarListApp.Maui.Models;
using CarListApp.Maui.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CarListApp.Maui.Services
{
	public class CarApiService
	{
		HttpClient _httpClient;
        public string StatusMessage;

		public CarApiService()
		{
			_httpClient = new() { BaseAddress = new Uri(GetBaseAddress()) };
		}

        private string GetBaseAddress()
        {
#if DEBUG
                        return DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2.8099" : "http://localhost:8099";
#elif RELEASE
            // published address here
            return "https://carinventoryapp-api.azurewebsites.net/";
            #endif
        }

        public async Task<List<Car>> GetCars()
        {
            try
            {
                SetAuthToken();
                var response = await _httpClient.GetStringAsync("/cars");
                return JsonSerializer.Deserialize<List<Car>>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return null;
        }

        public async Task<Car> GetCar(int id)
        {
            try
            {
                var response = await _httpClient.GetStringAsync("/cars/" + id);
                return JsonSerializer.Deserialize<Car>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return null;
        }

        public async Task AddCar(Car car)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/cars/", car);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Insert Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to add data.";
            }
        }

        public async Task DeleteCar(int id)
        {
            try
            {

                var response = await _httpClient.DeleteAsync("/cars/" + id);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Delete Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to delete data.";
            }
        }

        public async Task UpdateCar(int id, Car car)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/cars/" + id, car);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Update Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to update data.";
            }
        }

        public async Task<AuthResponseModel> Login(LoginModel loginModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/login", loginModel);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Login successful";

                return JsonSerializer.Deserialize<AuthResponseModel>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                StatusMessage = "Failed to log in";
                return default;
            }
        }

        public async Task SetAuthToken()
        {
            var token = await SecureStorage.GetAsync("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new
                System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}