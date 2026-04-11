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
            string query = "SELECT CAST(ISNULL(AVG(CAST(ProductPrice AS DECIMAL(18,2))), 0) AS DECIMAL(18,2)) FROM Products";
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
            string query = "SELECT TOP(1)ProductPrice FROM Products ORDER BY ProductId DESC";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<decimal>(query);
                return values;
            }
        }

        public decimal MaxProductPrice()
        {
            string query = "SELECT MAX(ProductPrice) FROM Products";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<decimal>(query);
                return values;
            }
        }

        public decimal MinProductPrice()
        {
            string query = "SELECT MIN(ProductPrice) FROM Products";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<decimal>(query);
                return values;
            }
        }

        public int PassiveCategoryCount()
        {
            string query = "SELECT COUNT(*) FROM Categories WHERE CategoryStatus=0";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;
            }
        }

        public int ProductCount()
        {
            string query = "SELECT COUNT(*) FROM Products";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query);
                return values;
            }
        }

        public string ProductNameByMaxProductPrice()
        {
            string query = "SELECT ProductTitle FROM Products WHERE ProductPrice = (SELECT MAX(ProductPrice) FROM Products)";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;
            }
        }

        public string ProductNameByMinProductPrice()
        {
            string query = "SELECT ProductTitle FROM Products WHERE ProductPrice = (SELECT MIN(ProductPrice) FROM Products)";
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<string>(query);
                return values;
            }
        }

        public int ProductCountByEmployee(int employeeId)
        {
            string query = "SELECT COUNT(*) FROM Products WHERE EmployeeId = @employeeId";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", employeeId);
            using (var connection = _context.CreateConnection())
            {
                var values = connection.QueryFirstOrDefault<int>(query, parameters);
                return values;
            }
        }
    }
}
