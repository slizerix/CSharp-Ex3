using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public string ModelName { get; set; }
        public string LicenseNumber { get; set; }
        public List<Wheel> Wheels { get; set; }
        public EnergySource EnergySource { get; set; }

        public abstract string GetSpecificDetails();
    }
}
