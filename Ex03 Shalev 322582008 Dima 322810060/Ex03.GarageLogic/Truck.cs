using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        public bool CarriesHazardousMaterials { get; set; }
        public float CargoVolume { get; set; }

        public Truck()
        {
            EnergySource = new FuelEnergy(FuelType.Soler, 135f);
        }

        public override string GetSpecificDetails() => $"Truck: Hazardous={CarriesHazardousMaterials}, Cargo={CargoVolume}m^3";
    }
}
