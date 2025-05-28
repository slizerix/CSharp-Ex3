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
                CreateAndInsertVehicle(vehicleType, licensePlate, modelName, ownerName, ownerPhone, currentAirPressure, wheelsManufacturer, energyAmount);

                // Fill in the rest of the details
                switch (vehicleType)
                {
                    case "FuelCar":
                    case "ElectricCar":
                        CarColor color = (CarColor)Enum.Parse(typeof(CarColor), partsOfLine[8]);
                        int numOfDoors = int.Parse(partsOfLine[9]);
                        

                        break;

                    case "FuelMotorcycle":
                    case "ElectricMotorcycle":
                        LicenseType licenseType = (LicenseType)Enum.Parse(typeof(LicenseType), partsOfLine[8]);
                        int engineCapacity = int.Parse(partsOfLine[9]);
                        

                        break;

                    case "Truck":
                        bool isHazardous = partsOfLine[8] == "1";
                        string cargoVolume = partsOfLine[9];
                        

                        break;
                }
            }
        }

        public void CreateAndInsertVehicle(string vehicleType, string licensePlate, string modelName, string ownerName, string ownerPhone, float currentAirPressure, string wheelsManufacturer, float energyAmount)
        {
            if (vehicleType != "FuelCar" && vehicleType != "ElectricCar" && vehicleType != "FuelMotorcycle" && vehicleType != "Truck" && vehicleType != "ElectricMotorcycle")
            {
                throw new KeyNotFoundException("Vehicle type not found.");
            }

            Vehicle newVehicle = CreateVehicle(vehicleType, licensePlate, modelName);
            newVehicle.CurrentAmount = energyAmount;

            // Determine number of wheels
            int numOfWheels = 0;
            float maxAirPressure = 0;

            switch (vehicleType)
            {
                case "FuelCar":  
                    numOfWheels = 5;
                    maxAirPressure = 32;
                    break;
                case "ElectricCar":
                    numOfWheels = 5;
                    maxAirPressure = 32;
                    break;
                case "FuelMotorcycle":
                    numOfWheels = 5;
                    maxAirPressure = 32;

                    break;
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
                wheels.Add(new Wheel(wheelsManufacturer, currentAirPressure, maxAirPressure));
            }

            newVehicle.Wheels = wheels;

            VehicleInGarage vehicleInGarage = new VehicleInGarage(newVehicle, ownerName, ownerPhone);
            if (!r_Vehicles.ContainsKey(licensePlate))
            {
                r_Vehicles[licensePlate] = vehicleInGarage;
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
            Vehicle vehicle = vehicleInGarage.Vehicle;
            StringBuilder detailsBuilder = new StringBuilder();

            detailsBuilder.AppendLine($"Owner: {vehicleInGarage.OwnerName}");
            detailsBuilder.AppendLine($"Phone: {vehicleInGarage.OwnerPhone}");
            detailsBuilder.AppendLine($"Status: {vehicleInGarage.Status}");

            detailsBuilder.AppendLine(vehicle.GetVhicleInfo());

            return detailsBuilder.ToString();
        }

        public void SetElectricMotorcycleDetails(string licensePlate, string licenseTypeInput, float engineVolume)
        {
            if (!r_Vehicles.ContainsKey(licensePlate))
                throw new KeyNotFoundException("Vehicle not found.");

            Vehicle vehicle = r_Vehicles[licensePlate].Vehicle;

            if (vehicle is ElectricMotorcycle motorcycle)
            {
                motorcycle.LicenseType = (LicenseType)Enum.Parse(typeof(LicenseType), licenseTypeInput);
                motorcycle.EngineVolume = engineVolume;
            }
            else
            {
                throw new InvalidOperationException("Vehicle is not an electric motorcycle.");
            }
        }

        public void SetFuelCarDetails(string licensePlate, string fuelTypeInput, string colorInput, int numOfDoors)
        {
            if (!r_Vehicles.ContainsKey(licensePlate))
                throw new KeyNotFoundException("Vehicle not found.");

            Vehicle vehicle = r_Vehicles[licensePlate].Vehicle;

            if (vehicle is FuelCar car)
            {
                car.Color = (CarColor)Enum.Parse(typeof(CarColor), colorInput);
                car.NumOfDoors = numOfDoors;
                car.FuelType = (FuelType)Enum.Parse(typeof(FuelType), fuelTypeInput);
            }
            else
            {
                throw new InvalidOperationException("Vehicle is not a fuel car.");
            }
        }

        public void SetFuelMotorcycleDetails(string licensePlate, string fuelTypeInput, string licenseTypeInput, float engineVolume)
        {
            if (!r_Vehicles.ContainsKey(licensePlate))
                throw new KeyNotFoundException("Vehicle not found.");

            Vehicle vehicle = r_Vehicles[licensePlate].Vehicle;

            if (vehicle is FuelMotorcycle motorcycle)
            {
                motorcycle.LicenseType = (LicenseType)Enum.Parse(typeof(LicenseType), licenseTypeInput);
                motorcycle.EngineVolume = engineVolume;
                motorcycle.FuelType = (FuelType)Enum.Parse(typeof(FuelType), fuelTypeInput);
            }
            else
            {
                throw new InvalidOperationException("Vehicle is not a fuel motorcycle.");
            }
        }

        public void SetTruckDetails(string licensePlate, bool isHazardous, float cargoVolume)
        {
            if (!r_Vehicles.ContainsKey(licensePlate))
                throw new KeyNotFoundException("Vehicle not found.");

            Vehicle vehicle = r_Vehicles[licensePlate].Vehicle;

            if (vehicle is Truck truck)
            {
                truck.CarriesHazardousMaterials = isHazardous;
                truck.CargoVolume = cargoVolume;
            }
            else
            {
                throw new InvalidOperationException("Vehicle is not a truck.");
            }
        }

        public void SetElectricCarDetails(string licensePlate, string colorInput, int numOfDoors)
        {
            if (!r_Vehicles.ContainsKey(licensePlate))
                throw new KeyNotFoundException("Vehicle not found.");

            Vehicle vehicle = r_Vehicles[licensePlate].Vehicle;

            if (vehicle is ElectricCar car)
            {
                car.Color = (CarColor)Enum.Parse(typeof(CarColor), colorInput);
                car.NumOfDoors = numOfDoors;
            }
            else
            {
                throw new InvalidOperationException("Vehicle is not an electric car.");
            }
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
