// Zadanie 5
var person = new Person("Ania", 30);
person.Introduce();

class Person
{
    public string Name { get; }
    public int Age { get; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void Introduce()
    {
        var status = Age >= 18 ? "an adult" : "a minor";
        Console.WriteLine($"Hi, I'm {Name}, {Age} years old ({status}).");
    }
}

// Zadanie 5
class BankAccount
{
    public double Balance { get; private set; }

    public BankAccount(double initialBalance)
    {
        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative");

        Balance = initialBalance;
    }

    public void Deposit(double amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit must be positive");

        Balance += amount;
    }

    public void Withdraw(double amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdraw must be positive");

        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds");

        Balance -= amount;
    }
}

// Zadanie 6
abstract class Animal
{
    public abstract void MakeSound();

    public void Eat() => Console.WriteLine("Animal is eating...");
}

class Dog : Animal
{
    public override void MakeSound() => Console.WriteLine("Woof!");
}

class Cat : Animal
{
    public override void MakeSound() => Console.WriteLine("Meow!");
}

// "Mini projekt"
abstract class Vehicle
{
    public abstract void Start();
}

class Car : Vehicle
{
    public override void Start() => Console.WriteLine("Car engine started.");
    public virtual void Drive() => Console.WriteLine("Car is driving.");
}

class ElectricCar : Car
{
    public override void Drive() => Console.WriteLine("Electric car is driving silently.");
    public void Charge() => Console.WriteLine("Battery charging...");
}
