using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        public FuelCar() => EnergySource = new FuelEnergy(FuelType.Octan95, 48f);
        public override string GetSpecificDetails() => $"Fuel Car: {Color}, {NumOfDoors} doors";
    }
}
