using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class CarData
    {
        public CarBrand CarBrand { get; set; }

        public List<CarModel> CarModels { get; set; }
    }
}
