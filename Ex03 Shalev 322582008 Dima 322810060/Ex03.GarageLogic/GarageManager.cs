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

                string vehicleType = partsOfLine[0];
                string licensePlate = partsOfLine[1];
                string modelName = partsOfLine[2];
                float energyAmount = float.Parse(partsOfLine[3]);
                string wheelsManufacturer = partsOfLine[4];
                float currentAirPressure = float.Parse(partsOfLine[5]);
                string ownerName = partsOfLine[6];
                string ownerPhone = partsOfLine[7];

                // Create and insert the basic vehicle
                CreateAndInsertVehicle(vehicleType, licensePlate, modelName, ownerName, ownerPhone);

                // Fill in the rest of the details
                switch (vehicleType)
                {
                    case "FuelCar":
                    case "ElectricCar":
                        string color = partsOfLine[8];
                        int numOfDoors = int.Parse(partsOfLine[9]);
                        InitializeVehicleDetails(licensePlate, currentAirPressure, wheelsManufacturer,
                            energyAmount, color, numOfDoors);
                        break;

                    case "FuelMotorcycle":
                    case "ElectricMotorcycle":
                        string licenseType = partsOfLine[8];
                        int engineCapacity = int.Parse(partsOfLine[9]);
                        InitializeVehicleDetails(licensePlate, currentAirPressure, wheelsManufacturer,
                            energyAmount, licenseType, engineCapacity);
                        break;

                    case "Truck":
                        bool isHazardous = partsOfLine[8] == "1";
                        string cargoVolume = partsOfLine[9];
                        InitializeVehicleDetails(licensePlate, currentAirPressure, wheelsManufacturer,
                            energyAmount, cargoVolume, isHazardous ? 1 : 0);
                        break;
                }
            }
        }

        public void InitializeVehicleDetails(string licensePlate, float currentAirPressure, string wheelsManufacturer,
                                     float energyAmount, string colorOrCargoOrLicenseType, int doorsOrEngineCapacityOrIsHazardous)
        {
            if (!r_Vehicles.ContainsKey(licensePlate))
            {
                throw new ArgumentException("Vehicle does not exist in the garage.");
            }

            VehicleInGarage vehicleInGarage = r_Vehicles[licensePlate];
            Vehicle vehicle = vehicleInGarage.Vehicle;

            
            foreach (Wheel wheel in vehicle.Wheels)
            {
                wheel.CurrentAirPressure = currentAirPressure;
                wheel.Manufacturer = wheelsManufacturer;
            }
  
        }


        public void UpdateVehicleStatus(string licensePlate, VehicleStatus newStatus)
        {
            if (!r_Vehicles.ContainsKey(licensePlate))
            {
                throw new KeyNotFoundException("Vehicle not found in garage.");
            }
            r_Vehicles[licensePlate].Status = newStatus;
        }


        public List<string> GetLicensePlates(VehicleStatus? filter)
        {
            List<string> licensePlates = new List<string>();

            foreach (KeyValuePair<string, VehicleInGarage> entry in r_Vehicles)
            {
                if (!filter.HasValue || entry.Value.Status == filter.Value)
                {
                    licensePlates.Add(entry.Key);
                }
            }

            return licensePlates;
        }

        public void CreateAndInsertVehicle(string vehicleType, string licensePlate, string modelName, string ownerName, string ownerPhone)
        {
            Vehicle newVehicle = CreateVehicle(vehicleType, licensePlate, modelName);

            // Determine number of wheels
            int numOfWheels = 0;
            float maxAirPressure = 0;

            switch (vehicleType)
            {
                case "FuelCar":
                case "ElectricCar":
                    numOfWheels = 5;
                    maxAirPressure = 32;
                    break;
                case "FuelMotorcycle":
                case "ElectricMotorcycle":
                    numOfWheels = 2;
                    maxAirPressure = 30;
                    break;
                case "Truck":
                    numOfWheels = 12;
                    maxAirPressure = 27;
                    break;
            }

            // Initialize wheels with default values (will be updated later)
            List<Wheel> wheels = new List<Wheel>();
            for (int i = 0; i < numOfWheels; i++)
            {
                wheels.Add(new Wheel("Unknown", 0, maxAirPressure));
            }

            newVehicle.Wheels = wheels;

            VehicleInGarage vehicleInGarage = new VehicleInGarage(newVehicle, ownerName, ownerPhone);
            if (!r_Vehicles.ContainsKey(licensePlate))
            {
                r_Vehicles[licensePlate] = vehicleInGarage;
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

        public void InflateTiresToMax(string licensePlate)
        {
            if (!r_Vehicles.ContainsKey(licensePlate))
                throw new KeyNotFoundException("Vehicle not found.");

            Vehicle vehicle = r_Vehicles[licensePlate].Vehicle;
            foreach (Wheel wheel in vehicle.Wheels)
            {
                wheel.CurrentAirPressure = wheel.MaxAirPressure;
            }
        }

        public string GetVehicleDetails(string licensePlate)
        {
            if (!r_Vehicles.ContainsKey(licensePlate))
                throw new KeyNotFoundException("Vehicle not found.");

            VehicleInGarage vehicleInGarage = r_Vehicles[licensePlate];
            StringBuilder detailsBuilder = new StringBuilder();
            detailsBuilder.AppendLine($"Owner: {vehicleInGarage.OwnerName}");
            detailsBuilder.AppendLine($"Phone: {vehicleInGarage.OwnerPhone}");
            detailsBuilder.AppendLine($"Status: {vehicleInGarage.Status}");
            detailsBuilder.AppendLine(vehicleInGarage.Vehicle.ToString());
            return detailsBuilder.ToString();
        }

        public void RefuelVehicle(string licensePlate, FuelType fuelType, float litersToAdd)
        {
            if (!r_Vehicles.ContainsKey(licensePlate))
                throw new KeyNotFoundException("Vehicle not found.");

            Vehicle vehicle = r_Vehicles[licensePlate].Vehicle;

            if (vehicle is FuelCar fuelCar)
            {
                fuelCar.Refuel(litersToAdd, fuelType);
            }
            else if (vehicle is FuelMotorcycle fuelMotorcycle)
            {
                fuelMotorcycle.Refuel(litersToAdd, fuelType);
            }
            else if (vehicle is Truck truck)
            {
                truck.Refuel(litersToAdd, fuelType);
            }
            else
            {
                throw new InvalidOperationException("This vehicle does not support refueling.");
            }
        }

        public void RechargeBattery(string licensePlate, float minutesToAdd)
        {
            if (!r_Vehicles.ContainsKey(licensePlate))
                throw new KeyNotFoundException("Vehicle not found.");

            Vehicle vehicle = r_Vehicles[licensePlate].Vehicle;

            float hoursToAdd = minutesToAdd / 60f;

            if (vehicle is ElectricCar electricCar)
            {
                electricCar.Recharge(hoursToAdd);
            }
            else if (vehicle is ElectricMotorcycle electricMotorcycle)
            {
                electricMotorcycle.Recharge(hoursToAdd);
            }
            else
            {
                throw new InvalidOperationException("This vehicle does not have a battery to recharge.");
            }
        }


    }
}
