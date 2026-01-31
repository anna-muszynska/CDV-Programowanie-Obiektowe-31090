using System.Text.Json;

namespace PasswordManager
{
    abstract class Credential
    {
        public string ServiceName { get; protected set; }
        public string Login { get; protected set; }

        protected string encryptedPassword;

        public void SetPassword(string password)
        {
            encryptedPassword = Encrypt(password);
        }

        public string GetPassword()
        {
            return Decrypt(encryptedPassword);
        }

        protected virtual string Encrypt(string input)
        {
            char[] arr = input.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        protected virtual string Decrypt(string input)
        {
            char[] arr = input.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        
        public abstract void Display();
    }
    
    class SocialMediaCredential : Credential
    {
        public SocialMediaCredential(string service, string login, string password)
        {
            ServiceName = service;
            Login = login;
            SetPassword(password);
        }

        public override void Display()
        {
            Console.WriteLine($"[Social Media] {ServiceName} | Login: {Login} | Hasło: {GetPassword()}");
        }
    }

    class BankCredential : Credential
    {
        public BankCredential(string service, string login, string password)
        {
            ServiceName = service;
            Login = login;
            SetPassword(password);
        }

        public override void Display()
        {
            Console.WriteLine($"[Bank] {ServiceName} | Login: {Login} | Hasło: {GetPassword()}");
        }
    }

    class PasswordRepository
    {
        private static readonly string FileName =
            Path.Combine(Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName,
                "passwords.json");        
        public List<CredentialDTO> Data { get; private set; } = new();
        
        public void Save()
        {
            string json = JsonSerializer.Serialize(Data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FileName, json);
        }

        public void Load()
        {
            if (File.Exists(FileName))
            {
                string json = File.ReadAllText(FileName);
                Data = JsonSerializer.Deserialize<List<CredentialDTO>>(json) ?? new();
            }
        }
    }

    class CredentialDTO
    {
        public string Type { get; set; }
        public string Service { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    class Program
    {
        static List<Credential> credentials = new();
        static PasswordRepository repo = new();

        static void Main()
        {
            repo.Load();
            LoadFromDTO();

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("MENEDŻER HASEŁ");
                Console.WriteLine("1. Dodaj hasło");
                Console.WriteLine("2. Wyświetl hasła");
                Console.WriteLine("3. Zapisz do pliku JSON");
                Console.WriteLine("4. Zakończ");
                Console.Write("Wybór: ");

                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1": AddCredential(); Pause(); break;
                    case "2": ShowCredentials(); Pause(); break;
                    case "3": Save(); Pause(); break;
                    case "4": running = false; break;
                    default: Console.WriteLine("Błędna opcja"); break;
                }
            }
        }

        static void AddCredential()
        {
            Console.Write("Nazwa serwisu: ");
            string service = Console.ReadLine();

            Console.Write("Login: ");
            string login = Console.ReadLine();

            Console.Write("Hasło: ");
            string password = Console.ReadLine();

            Console.Write("Typ (1 - SocialMedia, 2 - Bank): ");
            string type = Console.ReadLine();

            Credential cred;

            if (type == "2")
                cred = new BankCredential(service, login, password);
            else
                cred = new SocialMediaCredential(service, login, password);

            credentials.Add(cred);

            Console.WriteLine("Dodano wpis.");
        }

        static void ShowCredentials()
        {
            if (credentials.Count == 0)
            {
                Console.WriteLine("Brak zapisanych haseł.");
                return;
            }

            foreach (var cred in credentials)
            {
                cred.Display();
            }
        }

        static void Save()
        {
            repo.Data.Clear();

            foreach (var cred in credentials)
            {
                repo.Data.Add(new CredentialDTO
                {
                    Type = cred.GetType().Name,
                    Service = cred.ServiceName,
                    Login = cred.Login,
                    Password = cred.GetPassword()
                });
            }

            repo.Save();
            Console.WriteLine("Zapisano dane do JSON.");
        }

        static void LoadFromDTO()
        {
            foreach (var dto in repo.Data)
            {
                Credential cred = dto.Type switch
                {
                    nameof(BankCredential) => new BankCredential(dto.Service, dto.Login, dto.Password),
                    _ => new SocialMediaCredential(dto.Service, dto.Login, dto.Password)
                };

                credentials.Add(cred);
            }
        }
        
        static void Pause()
        {
            Console.WriteLine("\nNaciśnij dowolny klawisz by kontynuować...");
            Console.ReadLine();
        }
    }
}
