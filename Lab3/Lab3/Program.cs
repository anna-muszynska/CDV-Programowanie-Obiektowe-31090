using System.Text.Json;
using Lab3.Models;

var bikes = new List<Bike>();

var carsJson = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "cars.json"));
var cars = JsonSerializer.Deserialize<List<Car>>(carsJson);

bool continueApp = true;

do
{
    Console.WriteLine("--- MENU ---");
    Console.WriteLine("1. Vehicle list");
    Console.WriteLine("2. New vehicle");
    Console.WriteLine("3. Remove vehicle");
    Console.WriteLine("4. Update vehicle");
    Console.WriteLine("0. Exit");
    
    var option = Console.ReadKey().KeyChar;

    switch (option)
    {
        case '0':
            Console.WriteLine("Bye bye...");
            continueApp = false;
            break;
        case '1':
            Console.WriteLine("\nCars:");
            foreach (var car in cars)
            {
                car.ShowMe();
            }

            Console.WriteLine("\nBikes:");
            foreach (var bike in bikes)
            {
                Console.WriteLine($"Bike Type: {bike.BikeType}, Engine: {bike.Engine}");
            }
            break;
        
        case '2':
            AddNewVehicle();
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "cars.json"), JsonSerializer.Serialize(cars));
            break;
        case '3':
            RemoveVehicle();
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "cars.json"), JsonSerializer.Serialize(cars));
            break;
        case '4':
            UpdateVehicle();
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "cars.json"), JsonSerializer.Serialize(cars));
            break;
        default:
            Console.WriteLine("Unknown option");
            break;
    }
} while (continueApp);

void AddNewVehicle()
{
    Console.WriteLine("\n1 for car, 0 for bike:");
    
    var option = Console.ReadKey().KeyChar;
    if (option == '0')
    {
        AddNewBike();
    } else if (option == '1')
    {
        AddNewCar();
    }
    else
    {
        Console.WriteLine("Unknown option");
    }
}

void AddNewBike()
{
    Console.WriteLine("\nBike Type: ");
    var bikeType = Console.ReadLine();
    
    Console.WriteLine("\nEngine: ");
    var engine = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(bikeType) || string.IsNullOrWhiteSpace(engine))
    {
        Console.WriteLine("Invalid bike type or engine");
        return;
    }

    var bike = new Bike(engine, bikeType);
    bikes.Add(bike);

    Console.WriteLine("Bike added successfully!");
}

void AddNewCar()
{
    Console.WriteLine("\nModel: ");
    var model = Console.ReadLine();
    
    Console.WriteLine("\nEngine: ");
    var engine = Console.ReadLine();
    
    Console.WriteLine("\nYear: ");
    var success = int.TryParse(Console.ReadLine(), out int year);

    if (string.IsNullOrWhiteSpace(model) || string.IsNullOrWhiteSpace(engine) || !success)
    {
        Console.WriteLine("Invalid model, engine or year");
        return;
    }
    
    var car = new Car(model, engine, year);
    cars.Add(car);
}

void RemoveVehicle()
{
    Console.WriteLine("\nRemove car or bike? (1 = car, 0 = bike)");
    var option = Console.ReadKey().KeyChar;
    Console.WriteLine();

    if (option == '1')
    {
        Console.WriteLine("Enter car model to remove:");
        var model = Console.ReadLine();
        var carToRemove = cars.Find(c => c.Model.Equals(model, StringComparison.OrdinalIgnoreCase));
        if (carToRemove != null)
        {
            cars.Remove(carToRemove);
            Console.WriteLine("Car removed!");
        }
        else
        {
            Console.WriteLine("Car not found!");
        }
    }
    else if (option == '0')
    {
        Console.WriteLine("Enter bike type to remove:");
        var type = Console.ReadLine();
        var bikeToRemove = bikes.Find(b => b.BikeType.Equals(type, StringComparison.OrdinalIgnoreCase));
        if (bikeToRemove != null)
        {
            bikes.Remove(bikeToRemove);
            Console.WriteLine("Bike removed!");
        }
        else
        {
            Console.WriteLine("Bike not found!");
        }
    }
    else
    {
        Console.WriteLine("Unknown option");
    }
}

void UpdateVehicle()
{
    Console.WriteLine("\nUpdate car or bike? (1 = car, 0 = bike)");
    var option = Console.ReadKey().KeyChar;
    Console.WriteLine();

    if (option == '1')
    {
        Console.WriteLine("Enter car model to update:");
        var model = Console.ReadLine();
        var carToUpdate = cars.Find(c => c.Model.Equals(model, StringComparison.OrdinalIgnoreCase));
        if (carToUpdate != null)
        {
            Console.WriteLine("New Model:");
            var newModel = Console.ReadLine();
            Console.WriteLine("New Engine:");
            var newEngine = Console.ReadLine();
            Console.WriteLine("New Year:");
            var success = int.TryParse(Console.ReadLine(), out int newYear);

            if (!string.IsNullOrWhiteSpace(newModel)) carToUpdate.Model = newModel;
            if (!string.IsNullOrWhiteSpace(newEngine)) carToUpdate.Engine = newEngine;
            if (success)
            {
                try
                {
                    carToUpdate.Year = newYear;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Car updated successfully!");
        }
        else
        {
            Console.WriteLine("Car not found!");
        }
    }
    else if (option == '0')
    {
        Console.WriteLine("Enter bike type to update:");
        var type = Console.ReadLine();
        var bikeToUpdate = bikes.Find(b => b.BikeType.Equals(type, StringComparison.OrdinalIgnoreCase));
        if (bikeToUpdate != null)
        {
            Console.WriteLine("New Bike Type:");
            var newType = Console.ReadLine();
            Console.WriteLine("New Engine:");
            var newEngine = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newType)) bikeToUpdate.BikeType = newType;
            if (!string.IsNullOrWhiteSpace(newEngine)) bikeToUpdate.Engine = newEngine;

            Console.WriteLine("Bike updated successfully!");
        }
        else
        {
            Console.WriteLine("Bike not found!");
        }
    }
    else
    {
        Console.WriteLine("Unknown option");
    }
}
