using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Utils;
using static Ex03.GarageLogic.VehicleCreator;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private readonly Dictionary<string, VehicleInGarage> r_Vehicles = new Dictionary<string, VehicleInGarage>();

        public void LoadDataBaseOfVehicles()
        {
            string[] dataFromVehiclesFile = System.IO.File.ReadAllLines("Vehicles.db");

            foreach (var line in dataFromVehiclesFile)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (line.StartsWith("*****"))
                    break;

                string[] partsOfLine = line.Split(',');

                string VehicleType = partsOfLine[0];
                string LicensePlate = partsOfLine[1];
                string ModelName = partsOfLine[2];
                int EnergyPercentage = int.Parse(partsOfLine[3]);
                string TierModel = partsOfLine[4];
                int CurrAirPressure = int.Parse(partsOfLine[5]);
                string OwnerName = partsOfLine[6];
                string OwnerPhone = partsOfLine[7];


                CreateAndInsertVehicle(VehicleType, LicensePlate, ModelName, OwnerName, OwnerPhone);
            }
        }

        public void UpdateVehicleStatus(string licensePlate, VehicleStatus newStatus)
        {
            /////////////////////////////////////////////////////////////////////////////
            if (!r_Vehicles.ContainsKey(licensePlate))
            {
                throw new KeyNotFoundException("Vehicle not found in garage.");
            }
            r_Vehicles[licensePlate].Status = newStatus;
        }


        public List<string> GetLicensePlates(VehicleStatus filter)
        {
            /////////////////////////////////////////////////////////////////////////////////////
            List<string> licensePlates = new List<string>();
            foreach (KeyValuePair<string, VehicleInGarage> entry in r_Vehicles)
            {
                if (string.IsNullOrEmpty(filter) || entry.Value.Vehicle.LicenseNumber.Contains(filter))
                {
                    licensePlates.Add(entry.Key);
                }
            }
            return licensePlates;
        }
        public void CreateAndInsertVehicle(string VehicleType, string LicensePlate, string ModelName, string OwnerName, string OwnerPhone)
        {
            Vehicle newVehicle = CreateVehicle(VehicleType, LicensePlate, ModelName);
            VehicleInGarage vehicleInGarage = new VehicleInGarage(newVehicle, OwnerName, OwnerPhone);
            if (r_Vehicles.ContainsKey(LicensePlate) == false)
            {
                r_Vehicles[LicensePlate] = vehicleInGarage;
            }
        }

        public void UpdateVehicleStatusToInRepair(string i_licensePlate)
        {
            if (!r_Vehicles.ContainsKey(i_licensePlate))
                throw new KeyNotFoundException("Vehicle not found in garage.");
            r_Vehicles[i_licensePlate].Status = VehicleStatus.InRepair;
        }
        public bool IsVehicleInGarage(string i_LicenseNumber)
        {
            return r_Vehicles.ContainsKey(i_LicenseNumber);
        }
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
