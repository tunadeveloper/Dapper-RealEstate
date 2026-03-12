using Dapper;
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
            string query = "SELECT COUNT(*) FROM Categories WHERE CategoryStatus=1";
            using(var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;
            }
        }

        public int ApartmentCount()
        {
            string query = "SELECT COUNT(*) FROM Products WHERE ProductTitle LIKE '%Daire%'";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;
            }
        }

        public decimal AverageProductPrice()
        {
            string query = "SELECT AVERAGE(ProductPrice) FROM Products";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<decimal>(query);
                return values;
            }
        }

        public int CategoryCount()
        {
            string query = "SELECT COUNT(*) FROM Categories";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;
            }
        }

        public string CategoryNameByMaxProductCount()
        {
            string query = "SELECT TOP(1)CategoryName from Products INNER JOIN Categories ON Products.ProductCategory=Categories.CategoryId GROUP BY CategoryName ORDER BY Count(*) DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;
            }
        }

        public string CityNameByMaxProductCount()
        {
            string query = "SELECT ProductCity FROM Products GROUP BY ProductCity ORDER BY COUNT(*) DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;
            }
        }

        public string CityNameByMaxProductPrice()
        {
            string query = "SELECT ProductCity FROM Products GROUP BY ProductPrice, ProductCity ORDER BY MAX(ProductPrice) DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;
            }
        }

        public string CityNameByMinProductPrice()
        {
            string query = "SELECT ProductCity FROM Products GROUP BY ProductCity, ProductPrice ORDER BY MIN(ProductPrice)";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;
            }
        }

        public int DifferentCityCount()
        {
            string query = "SELECT COUNT(DISTINCT ProductCity) FROM Products";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;
            }
        }

        public int DifferentDistrictCount()
        {
            string query = "SELECT COUNT(DISTINCT ProductDistrict) FROM Products";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;
            }
        }

        public string DistrictNameByMaxProductCount()
        {
            string query = "SELECT TOP(1)ProductDistrict FROM Products GROUP BY ProductDistrict ORDER BY COUNT(*) DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;
            }
        }

        public string EmployeeNameByMaxProductCount()
        {
            string query = "SELECT TOP(1)EmployeeNameSurname FROM Products INNER JOIN Employee ON Products.EmployeeId=Employee.EmployeeId GROUP BY EmployeeNameSurname ORDER BY COUNT(Products.ProductId) DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;
            }
        }

        public string EmployeeNameByMinProductCount()
        {
            string query = "SELECT TOP(1)EmployeeNameSurname FROM Products INNER JOIN Employee ON Products.EmployeeId=Employee.EmployeeId GROUP BY EmployeeNameSurname ORDER BY COUNT(Products.ProductId)";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;
            }
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
