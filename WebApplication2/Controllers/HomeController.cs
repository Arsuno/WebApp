using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebApplication2.Models;
using WebApplication2.ViewModels;
using Dapper;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _config;


        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;

            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public IActionResult Index()
        {

            CarsIndexViewModel carsView = new CarsIndexViewModel();
            carsView.Brands = GetBrandNames();
            carsView.Models = GetModelNames();

            List<CarBrand> sortedBrands = GetSortedBrandsByName();
            List<CarData> sortedData = new List<CarData>();

            foreach (CarBrand carBrand in sortedBrands)
            {
                sortedData.Add(new CarData { CarBrand = carBrand, CarModels = GetSortedModelNamesById(carBrand.Id) });
            }

            carsView.SortedData = sortedData;
            
            return View(carsView);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<CarBrand> GetBrandNames()
        {
            using (IDbConnection db = Connection)
            {
                var result = db.Query<CarBrand>("SELECT * FROM Brands").ToList();
                return result;
            }
        }

        private List<CarModel> GetModelNames()
        {
            using (IDbConnection db = Connection)
            {
                var result = db.Query<CarModel>("SELECT * FROM Models").ToList();
                return result;
            }
        }

        private List<CarBrand> GetSortedBrandsByName()
        {
            using (IDbConnection db = Connection)
            {
                var result = db.Query<CarBrand>("SELECT * FROM Brands ORDER BY NAME ASC").ToList();
                return result;
            }

        }

        private List<CarModel> GetSortedModelNamesById(int id)
        {
            using (IDbConnection db = Connection)
            {
                var result = db.Query<CarModel>("SELECT * FROM Models where BrandId=" + id.ToString() + "ORDER BY NAME ASC").ToList();
                return result;
            }
        }
        
    }
}
