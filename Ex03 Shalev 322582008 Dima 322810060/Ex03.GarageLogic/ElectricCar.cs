using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        public ElectricCar(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName)
        {
        }

        public override float MaxAmount { get; set; } = 4.8f; // Fixed by overriding the MaxAmount property from Vehicle.  

        public float EnergyPercentage()
        {
            return (CurrentAmount / MaxAmount) * 100;
        }

        public void Recharge(float i_Hours)
        {
            if (CurrentAmount + i_Hours > MaxAmount)
                throw new ValueOutOfRangeException(0, MaxAmount - CurrentAmount, "Charge exceeds capacity.");

            CurrentAmount += i_Hours;
        }

        public override string GetVhicleInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Model Name: {ModelName}");
            sb.AppendLine($"License Number: {LicenseNumber}");
            sb.AppendLine($"Current Energy Amount: {CurrentAmount}");
            sb.AppendLine($"Electric Car - Energy Percentage: {EnergyPercentage()}%");
            sb.AppendLine($"Max Charge Capacity: {MaxAmount} hours");
            sb.AppendLine($"Color: {Color}");
            sb.AppendLine($"Number of Doors: {NumOfDoors}");
            sb.AppendLine($"Air preassure : {Wheels[0].CurrentAirPressure} Manufacturer : {Wheels[0].Manufacturer}");

            return sb.ToString();
        }
    }
}
