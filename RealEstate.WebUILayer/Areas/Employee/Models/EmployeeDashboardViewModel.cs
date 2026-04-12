namespace RealEstate.WebUILayer.Areas.Employee.Models
{
    public class EmployeeDashboardViewModel
    {
        public int TotalProductCount { get; set; }
        public int MyProductCount { get; set; }
        public int CategoryCount { get; set; }
        public int DifferentCityCount { get; set; }
        public int DifferentDistrictCount { get; set; }
        public int ApartmentCount { get; set; }
        public decimal AverageProductPrice { get; set; }
        public decimal MaxProductPrice { get; set; }
        public decimal MinProductPrice { get; set; }
        public string EmployeeName { get; set; }
    }
}
