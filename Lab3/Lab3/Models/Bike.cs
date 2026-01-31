namespace Lab3.Models;

public class Bike : Vehicle
{
    public string BikeType;
    
    public Bike (string engine, string bikeType) : base(engine)
    {
        this.BikeType = bikeType;
    }
}