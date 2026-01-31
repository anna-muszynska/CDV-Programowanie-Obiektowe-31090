void ZadWiek()
{
    const int minEntryAge = 14;
    const int minSimAge = 18;

    int age;

    while (true)
    {
        Console.Write("Podaj swój wiek: ");
        if (int.TryParse(Console.ReadLine(), out age) && age > 0)
            break;

        Console.WriteLine("Błąd! Podaj poprawną liczbę.");
    }

    if (age < minEntryAge)
        Console.WriteLine("Nie możesz wejść do sklepu.");
    else if (age < minSimAge)
        Console.WriteLine("Możesz wejść, ale nie kupisz karty SIM.");
    else
        Console.WriteLine("Witamy w sklepie!");
}

void Zad1()
{
    const string password = "admin123";
    string? input;

    do
    {
        Console.Write("Podaj hasło: ");
        input = Console.ReadLine();

        if (input != password)
            Console.WriteLine("Nieprawidłowe hasło!");
    }
    while (input != password);

    Console.WriteLine("Zalogowano poprawnie.");
}

void Zad2()
{
    int number;

    while (true)
    {
        Console.Write("Podaj liczbę dodatnią: ");
        if (int.TryParse(Console.ReadLine(), out number) && number > 0)
            break;

        Console.WriteLine("Błąd! To nie jest liczba dodatnia.");
    }

    Console.WriteLine($"Podałeś liczbę: {number}");
}

void Zad3()
{
    var cities = new[] { "Warszawa", "Poznań", "Gdańsk", "Wrocław" };

    foreach (var city in cities)
        Console.WriteLine(city);
}
