using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        public FuelMotorcycle() => EnergySource = new FuelEnergy(FuelType.Octan98, 5.8f);
        public override string GetSpecificDetails() => $"Fuel Motorcycle: {LicenseType}, {EngineVolume}cc";
    }
}
