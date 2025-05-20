using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private readonly Dictionary<string, VehicleInGarage> r_Vehicles = new Dictionary<string, VehicleInGarage>();

        public void AddOrUpdateVehicle(VehicleInGarage i_Vehicle)
        {
            r_Vehicles[i_Vehicle.Vehicle.LicenseNumber] = i_Vehicle;
        }

        public VehicleInGarage GetVehicle(string i_LicenseNumber)
        {
            if (!r_Vehicles.ContainsKey(i_LicenseNumber))
                throw new KeyNotFoundException("Vehicle not found.");
            return r_Vehicles[i_LicenseNumber];
        }

        public IEnumerable<string> GetAllLicenseNumbers(VehicleStatus? i_Filter = null)
        {
            List<string> licenseNumbers = new List<string>();

            foreach (KeyValuePair<string, VehicleInGarage> entry in r_Vehicles)
            {
                if (!i_Filter.HasValue || entry.Value.Status == i_Filter.Value)
                {
                    licenseNumbers.Add(entry.Key);
                }
            }

            return licenseNumbers;
        }

    }
}
