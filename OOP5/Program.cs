using System;
using System.Linq;
using System.Text;

class Worker
{
    protected string Name;
    protected string Year;
    protected string Month;
    protected Company WorkPlace;

    public Worker()
    {
        Name = "";
        Year = "";
        Month = "";
        WorkPlace = new Company();
    }

    public Worker(string name, string year, string month, Company company)
    {
        Name = name;
        Year = year;
        Month = month;
        WorkPlace = company;
    }

    public Worker(Worker other)
    {
        Name = other.Name;
        Year = other.Year;
        Month = other.Month;
        WorkPlace = new Company(other.WorkPlace);
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetYear(string year)
    {
        Year = year;
    }

    public void SetMonth(string month)
    {
        Month = month;
    }

    public void SetWorkPlace(Company company)
    {
        WorkPlace = company;
    }

    public string GetName()
    {
        return Name;
    }

    public string GetYear()
    {
        return Year;
    }

    public string GetMonth()
    {
        return Month;
    }

    public Company GetWorkPlace()
    {
        return WorkPlace;
    }

    public int GetWorkExperience()
    {
        DateTime currentDate = DateTime.Now;
        int totalMonths = (currentDate.Year - Convert.ToInt32(Year)) * 12 + (currentDate.Month - Convert.ToInt32(Month));
        return totalMonths;
    }

    public double GetTotalMoney()
    {
        return WorkPlace.GetSalary() * GetWorkExperience();
    }
}

class Company
{
    protected string Name;
    protected string Position;
    protected double Salary;

    public Company()
    {
        Name = "";
        Position = "";
        Salary = 0.0;
    }

    public Company(string name, string position, double salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }

    public Company(Company other)
    {
        Name = other.Name;
        Position = other.Position;
        Salary = other.Salary;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetPosition(string position)
    {
        Position = position;
    }

    public void SetSalary(double salary)
    {
        Salary = salary;
    }

    public string GetName()
    {
        return Name;
    }

    public string GetPosition()
    {
        return Position;
    }

    public double GetSalary()
    {
        return Salary;
    }
}

class Program
{
    public static Worker[] ReadWorkersArray(int n)
    {
        Worker[] workers = new Worker[n];
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Введіть дані про працівника {0}:", i + 1);
            Console.Write("Прізвище та ініціали: ");
            string name = Console.ReadLine();
            Console.Write("Рік початку роботи: ");
            string year = Console.ReadLine();
            Console.Write("Місяць початку роботи: ");
            string month = Console.ReadLine();
            Console.Write("Назва компанії: ");
            string companyName = Console.ReadLine();
            Console.Write("Посада: ");
            string position = Console.ReadLine();
            Console.Write("Зарплата: ");
            double salary = Convert.ToDouble(Console.ReadLine());

            Company company = new Company(companyName, position, salary);
            workers[i] = new Worker(name, year, month, company);
        }
        return workers;
    }

    public static void PrintWorker(Worker worker)
    {
        Console.WriteLine("Прізвище та ініціали: {0}", worker.GetName());
        Console.WriteLine("Стаж роботи: {0} місяців", worker.GetWorkExperience());
        Console.WriteLine("Загальна сума зароблених коштів: {0}", worker.GetTotalMoney());
        Console.WriteLine("Назва компанії: {0}", worker.GetWorkPlace().GetName());
        Console.WriteLine("Посада: {0}", worker.GetWorkPlace().GetPosition());
        Console.WriteLine("Зарплата: {0}", worker.GetWorkPlace().GetSalary());
    }

    public static void PrintWorkers(Worker[] workers)
    {
        foreach (var worker in workers)
        {
            PrintWorker(worker);
            Console.WriteLine();
        }
    }

    public static void GetWorkersInfo(Worker[] workers, out double maxSalary, out double minSalary)
    {
        maxSalary = workers.Max(w => w.GetWorkPlace().GetSalary());
        minSalary = workers.Min(w => w.GetWorkPlace().GetSalary());
    }

    public static void SortWorkerBySalary(ref Worker[] workers)
    {
        Array.Sort(workers, (x, y) => y.GetWorkPlace().GetSalary().CompareTo(x.GetWorkPlace().GetSalary()));
    }

    public static void SortWorkerByWorkExperience(ref Worker[] workers)
    {
        Array.Sort(workers, (x, y) => x.GetWorkExperience().CompareTo(y.GetWorkExperience()));
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;

        Console.Write("Введіть кількість працівників: ");
        int n = Convert.ToInt32(Console.ReadLine());
        Worker[] workers = ReadWorkersArray(n);

        Console.WriteLine("\nІнформація про працівників:");
        PrintWorkers(workers);

        double maxSalary, minSalary;
        GetWorkersInfo(workers, out maxSalary, out minSalary);
        Console.WriteLine("\nМаксимальна зарплата: {0}", maxSalary);
        Console.WriteLine("Мінімальна зарплата: {0}", minSalary);

        SortWorkerBySalary(ref workers);
        Console.WriteLine("\nСортування працівників за зарплатою:");
        PrintWorkers(workers);

        SortWorkerByWorkExperience(ref workers);
        Console.WriteLine("\nСортування працівників за стажем роботи:");
        PrintWorkers(workers);
    }
}
