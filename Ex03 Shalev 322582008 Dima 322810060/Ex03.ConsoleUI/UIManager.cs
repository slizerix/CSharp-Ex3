using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UIManager
    {
        private readonly GarageManager r_GarageManager = new GarageManager();
        public void Run()
        {
            ShowWelcomeMessage();
            MainLoop();
            ShowExitMessage();
        }

        private void MainLoop()
        {
            bool shouldExit = false;

            while (shouldExit == true)
            {
                ShowMainMenu();
                string userChoice = Console.ReadLine();
                shouldExit = HandleUserChoice(userChoice);
                Console.WriteLine();
            }
        }

        private bool HandleUserChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    r_GarageManager.LoadDataBaseOfVehicles();
                    Console.WriteLine("Vehicles loaded from file.");
                    break;
                case "2":
                    InsertNewVehicle();
                    break;
                case "3":
                    ShowLicensePlates();
                    break;
                case "4":
                    ChangeVehicleStatus();
                    break;
                case "5":
                    InflateTires();
                    break;
                case "6":
                    RefuelVehicle();
                    break;
                case "7":
                    ChargeBattery();
                    break;
                case "8":
                    ShowVehicleDetails();
                    break;
                case "9":
                    return true; // Exit
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1-9.");
                    break;
            }

            return false;
        }

        private void ShowWelcomeMessage()
        {
            Console.WriteLine("=== Welcome to the Garage Management System ===");
        }

        private void ShowExitMessage()
        {
            Console.WriteLine("Thank you for using the system. Goodbye!");
        }

        private void ShowMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Load vehicles from file");
            Console.WriteLine("2. Insert a new vehicle");
            Console.WriteLine("3. Show license plates");
            Console.WriteLine("4. Change vehicle status");
            Console.WriteLine("5. Inflate tires to max");
            Console.WriteLine("6. Refuel vehicle");
            Console.WriteLine("7. Charge electric vehicle");
            Console.WriteLine("8. Show vehicle details");
            Console.WriteLine("9. Exit");
            Console.Write("Your choice: ");
        }

        private void InsertNewVehicle()
        {
            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            if (r_GarageManager.IsVehicleInGarage(licensePlate))
            {
                r_GarageManager.UpdateVehicleStatusToInRepair(licensePlate);
                Console.WriteLine("Vehicle already in garage. Status updated to 'In Repair'.");
                return;
            }

            Console.Write("Enter vehicle type (ElectricMotorcycle, RegularCar): ");
            string type = Console.ReadLine();

            Console.Write("Enter model name: ");
            string model = Console.ReadLine();

            Console.Write("Enter owner's name: ");
            string owner = Console.ReadLine();

            Console.Write("Enter owner's phone: ");
            string phone = Console.ReadLine();

            // You can expand with additional inputs per type (fuel level, color, wheels, etc.)
            r_GarageManager.CreateAndInsertVehicle(type, licensePlate, model, owner, phone);

            Console.WriteLine("Vehicle inserted successfully.");
        }

        private void ShowLicensePlates()
        {
            Console.Write("Enter 0 for All, 1 for InReapair, 2 for Repaired, 3 for Paid");
            int filter = int.Parse(Console.ReadLine());
            //////////////////////////////////////////////////////////////////////////////////////
            List<string> plates = r_GarageManager.GetLicensePlates(filter);
            Console.WriteLine("--- License Plates ---");
            foreach (string plate in plates)
            {
                Console.WriteLine(plate);
            }
        }

        private void ChangeVehicleStatus()
        {
            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            Console.Write("Enter new status (InRepair, Repaired, Paid): ");
            string newStatus = Console.ReadLine();
            ///////////////////////////////////////////////////////////////////////////////////////
            r_GarageManager.UpdateVehicleStatus(licensePlate, newStatus);
            Console.WriteLine("Vehicle status updated.");
        }


        private void InflateTires()
        {
            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            r_GarageManager.InflateTiresToMax(licensePlate);
            //////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Tires inflated.");
        }

        private void RefuelVehicle()
        {
            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            Console.Write("Enter fuel type (Octan95, Octan96, Octan98, Soler): ");
            string fuelType = Console.ReadLine();

            Console.Write("Enter amount of liters: ");
            float liters = float.Parse(Console.ReadLine());

            r_GarageManager.RefuelVehicle(licensePlate, fuelType, liters);
            ///////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Vehicle refueled.");
        }

        private void ChargeBattery()
        {
            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            Console.Write("Enter charging time in minutes: ");
            float minutes = float.Parse(Console.ReadLine());

            r_GarageManager.RechargeBattery(licensePlate, minutes);
            ///////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Vehicle charged.");
        }

        private void ShowVehicleDetails()
        {
            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            string details = r_GarageManager.GetVehicleDetails(licensePlate);
            /////////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("--- Vehicle Details ---");
            Console.WriteLine(details);
        }

    }
}
