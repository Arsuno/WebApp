using System.Collections.Generic;
using WebApplication2.Models;



namespace WebApplication2.ViewModels
{
    public class CarsIndexViewModel
    {
        public List<CarBrand> Brands { get; set; }
        public List<CarModel> Models { get; set; }
        public List<CarData> SortedData { get; set; }
    }
}
