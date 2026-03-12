namespace RealEstate.WebAPILayer.Repositories.Statistics
{
    public interface IStatisticsService
    {
        int CategoryCount();
        int ActiveCategoryCount();
        int PassiveCategoryCount();
        int ProductCount();
        int ApartmentCount();
        string EmployeeNameByMaxProductCount();
        string CategoryNameByMaxProductCount();
        string CityNameByMaxProductCount();
        int DifferentCityCount();
        decimal LastProductPrice();
        decimal AverageProductPrice();
        int DifferentDistrictCount();
        string DistrictNameByMaxProductCount();
        decimal MaxProductPrice();
        decimal MinProductPrice();
        string ProductNameByMaxProductPrice();
        string ProductNameByMinProductPrice();
        string CityNameByMaxProductPrice();
        string CityNameByMinProductPrice();
        string EmployeeNameByMinProductCount();
    }
}
