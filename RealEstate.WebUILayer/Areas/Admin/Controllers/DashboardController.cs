using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate.WebUILayer.Areas.Admin.Models;
using RealEstate.WebUILayer.DTOs.ProductDTOs;

namespace RealEstate.WebUILayer.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("RealEstateApi");

            var productCountTask = GetStatIntAsync(client, "api/Statistics/ProductCount");
            var categoryCountTask = GetStatIntAsync(client, "api/Statistics/CategoryCount");
            var avgPriceTask = GetStatDecimalAsync(client, "api/Statistics/AverageProductPrice");
            var maxPriceTask = GetStatDecimalAsync(client, "api/Statistics/MaxProductPrice");
            var minPriceTask = GetStatDecimalAsync(client, "api/Statistics/MinProductPrice");
            var catMaxTask = GetStatStringAsync(client, "api/Statistics/CategoryNameByMaxProductCount");
            var cityMaxTask = GetStatStringAsync(client, "api/Statistics/CityNameByMaxProductCount");
            var empMaxTask = GetStatStringAsync(client, "api/Statistics/EmployeeNameByMaxProductCount");

            var subscriberCountTask = GetListCountAsync(client, "api/Subscribers");
            var messageCountTask = GetListCountAsync(client, "api/Messages");
            var productsPriceChartTask = GetChartItemsAsync(client, "api/Products/GetTopByPriceForDashboard?take=15");
            var popularPriceChartTask = GetChartItemsAsync(client, "api/Products/GetPopularByPriceForDashboard?take=10");

            await Task.WhenAll(
                productCountTask, categoryCountTask,
                avgPriceTask, maxPriceTask, minPriceTask,
                catMaxTask, cityMaxTask, empMaxTask,
                subscriberCountTask, messageCountTask,
                productsPriceChartTask, popularPriceChartTask);

            var productsPriceChart = await productsPriceChartTask;
            var popularChart = await popularPriceChartTask;

            var model = new AdminDashboardViewModel
            {
                ProductCount = await productCountTask,
                CategoryCount = await categoryCountTask,
                SubscriberCount = await subscriberCountTask,
                MessageCount = await messageCountTask,
                AverageProductPrice = await avgPriceTask,
                MaxProductPrice = await maxPriceTask,
                MinProductPrice = await minPriceTask,
                CategoryNameByMaxProductCount = await catMaxTask,
                CityNameByMaxProductCount = await cityMaxTask,
                EmployeeNameByMaxProductCount = await empMaxTask,
                ProductsPriceChart = productsPriceChart,
                PopularProductsPriceChart = popularChart
            };

            FillChartJsonArrays(model);
            return View(model);
        }

        private static void FillChartJsonArrays(AdminDashboardViewModel model)
        {
            var titles1 = new List<string>();
            var prices1 = new List<decimal>();
            foreach (var x in model.ProductsPriceChart)
            {
                titles1.Add(string.IsNullOrWhiteSpace(x.ProductTitle) ? "—" : x.ProductTitle);
                prices1.Add(x.ProductPrice);
            }

            model.Chart1LabelsJson = JsonConvert.SerializeObject(titles1);
            model.Chart1PricesJson = JsonConvert.SerializeObject(prices1);

            var titles2 = new List<string>();
            var prices2 = new List<decimal>();
            foreach (var x in model.PopularProductsPriceChart)
            {
                titles2.Add(string.IsNullOrWhiteSpace(x.ProductTitle) ? "—" : x.ProductTitle);
                prices2.Add(x.ProductPrice);
            }

            model.Chart2LabelsJson = JsonConvert.SerializeObject(titles2);
            model.Chart2PricesJson = JsonConvert.SerializeObject(prices2);
        }

        private static async Task<List<ProductPriceChartItemDTO>> GetChartItemsAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return new List<ProductPriceChartItemDTO>();
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json)) return new List<ProductPriceChartItemDTO>();
            return JsonConvert.DeserializeObject<List<ProductPriceChartItemDTO>>(json) ?? new List<ProductPriceChartItemDTO>();
        }

        private static async Task<int> GetStatIntAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return 0;
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json)) return 0;
            return JsonConvert.DeserializeObject<int>(json);
        }

        private static async Task<decimal> GetStatDecimalAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return 0;
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json)) return 0;
            return JsonConvert.DeserializeObject<decimal>(json);
        }

        private static async Task<string?> GetStatStringAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;
            var body = (await response.Content.ReadAsStringAsync()).Trim();
            if (string.IsNullOrEmpty(body)) return null;
            if (body.Equals("null", StringComparison.OrdinalIgnoreCase)) return null;

            if (body[0] == '"')
            {
                try
                {
                    return JsonConvert.DeserializeObject<string>(body);
                }
                catch (JsonException)
                {
                    return body.Trim('"');
                }
            }

            return body;
        }

        private static async Task<int> GetListCountAsync(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return 0;
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json)) return 0;
            var list = JsonConvert.DeserializeObject<List<object>>(json);
            return list?.Count ?? 0;
        }
    }
}
