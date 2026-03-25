using RealEstate.WebUILayer.DTOs.ProductDTOs;

namespace RealEstate.WebUILayer.Areas.Admin.Models
{
    public class AdminDashboardViewModel
    {
        public int ProductCount { get; set; }
        public int CategoryCount { get; set; }
        public int SubscriberCount { get; set; }
        public int MessageCount { get; set; }

        public decimal AverageProductPrice { get; set; }
        public decimal MaxProductPrice { get; set; }
        public decimal MinProductPrice { get; set; }

        public string? CategoryNameByMaxProductCount { get; set; }
        public string? CityNameByMaxProductCount { get; set; }
        public string? EmployeeNameByMaxProductCount { get; set; }

        public List<ProductPriceChartItemDTO> ProductsPriceChart { get; set; } = new();
        public List<ProductPriceChartItemDTO> PopularProductsPriceChart { get; set; } = new();

        public string Chart1LabelsJson { get; set; } = "[]";
        public string Chart1PricesJson { get; set; } = "[]";
        public string Chart2LabelsJson { get; set; } = "[]";
        public string Chart2PricesJson { get; set; } = "[]";
    }
}
