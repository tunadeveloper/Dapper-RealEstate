using RealEstate.WebAPILayer.Context;

namespace RealEstate.WebAPILayer.Repositories.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly DapperContext _context;

        public StatisticsService(DapperContext context)
        {
            _context = context;
        }

        public int ActiveCategoryCount()
        {
            throw new NotImplementedException();
        }

        public int ApartmentCount()
        {
            throw new NotImplementedException();
        }

        public decimal AverageProductPrice()
        {
            throw new NotImplementedException();
        }

        public int CategoryCount()
        {
            throw new NotImplementedException();
        }

        public string CategoryNameByMaxProductCount()
        {
            throw new NotImplementedException();
        }

        public string CityNameByMaxProductCount()
        {
            throw new NotImplementedException();
        }

        public string CityNameByMaxProductPrice()
        {
            throw new NotImplementedException();
        }

        public string CityNameByMinProductPrice()
        {
            throw new NotImplementedException();
        }

        public int DifferentCityCount()
        {
            throw new NotImplementedException();
        }

        public int DifferentDistrictCount()
        {
            throw new NotImplementedException();
        }

        public string DistrictNameByMaxProductCount()
        {
            throw new NotImplementedException();
        }

        public string EmployeeNameByMaxProductCount()
        {
            throw new NotImplementedException();
        }

        public string EmployeeNameByMinProductCount()
        {
            throw new NotImplementedException();
        }

        public decimal LastProductPrice()
        {
            throw new NotImplementedException();
        }

        public decimal MaxProductPrice()
        {
            throw new NotImplementedException();
        }

        public decimal MinProductPrice()
        {
            throw new NotImplementedException();
        }

        public int PassiveCategoryCount()
        {
            throw new NotImplementedException();
        }

        public int ProductCount()
        {
            throw new NotImplementedException();
        }

        public string ProductNameByMaxProductPrice()
        {
            throw new NotImplementedException();
        }

        public string ProductNameByMinProductPrice()
        {
            throw new NotImplementedException();
        }
    }
}
