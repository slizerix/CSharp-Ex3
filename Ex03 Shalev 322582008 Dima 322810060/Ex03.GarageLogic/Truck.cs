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
        public Truck(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName)
        {
        }

        public bool CarriesHazardousMaterials { get; set; }
        public float CargoVolume { get; set; }
        public FuelType FuelType { get; set; } = FuelType.Soler;

        public override float MaxAmount { get; set; } = 135; // Fixed by overriding the MaxAmount property from Vehicle.  
        public void Refuel(float i_Amount, FuelType i_Type)
        {
            if (i_Type != FuelType)
                throw new ArgumentException("Incorrect fuel type.");
            if (CurrentAmount + i_Amount > MaxAmount)
                throw new ValueOutOfRangeException(0, MaxAmount - CurrentAmount, "Fuel exceeds tank capacity.");

            CurrentAmount += i_Amount;
        }
        public float EnergyPercentage()
        {
            return (CurrentAmount / MaxAmount) * 100;
        }

        //need to print all availiable info about the vehicle
        public override string GetVhicleInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Model Name: {ModelName}");
            sb.AppendLine($"License Number: {LicenseNumber}");
            sb.AppendLine($"Current Fuel Amount: {CurrentAmount} liters");
            sb.AppendLine($"Max Fuel Amount: {MaxAmount} liters");
            sb.AppendLine($"Fuel Type: {FuelType}");
            sb.AppendLine($"Energy Percentage: {EnergyPercentage()}%");
            sb.AppendLine($"Carries Hazardous Materials: {CarriesHazardousMaterials}");
            sb.AppendLine($"Cargo Volume: {CargoVolume} cubic meters");
            sb.AppendLine($"Air preassure : {Wheels[0].CurrentAirPressure} Manufacturer : {Wheels[0].Manufacturer}");

            return sb.ToString();
        }
    }
}