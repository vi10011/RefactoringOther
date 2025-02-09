using System;
using System.Linq;
using System.Text;

class Worker
{
    public string Name { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public Company WorkPlace { get; set; }

    private const int MinYear = 1900;

    public Worker()
    {
        InitializeDefaults();
    }

    public Worker(string name, int year, int month, Company company)
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

    private void InitializeDefaults()
    {
        Name = string.Empty;
        Year = MinYear;
        Month = 1;
        WorkPlace = new Company();
    }

    public int GetWorkExperience()
    {
        DateTime currentDate = DateTime.Now;
        int totalMonths = (currentDate.Year - Year) * 12 + (currentDate.Month - Month);
        return totalMonths;
    }

    public double GetTotalMoney()
    {
        return WorkPlace.Salary * GetWorkExperience();
    }

}

class Company
{
    public string Name { get; set; }
    public string Position { get; set; }
    public double Salary { get; set; }

    public Company()
    {
        Name = string.Empty;
        Position = string.Empty;
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
}

class Program
{
    public static int GetValidYear() { /* ... */ }
    public static int GetValidMonth() { /* ... */ }
    public static double GetValidSalary() { /* ... */ }

    public static Worker[] ReadWorkersArray(int n)
    {
        Worker[] workers = new Worker[n];
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Введіть дані про працівника {i + 1}:");

            string name = Console.ReadLine();
            int year = GetValidYear();
            int month = GetValidMonth();
            string companyName = Console.ReadLine();
            string position = Console.ReadLine();
            double salary = GetValidSalary();

            Company company = new Company(companyName, position, salary);
            workers[i] = new Worker(name, year, month, company);
        }
        return workers;
    }

    public static void SortAndPrintWorkersBy(Func<Worker, object> sortingCriterion)
    {
        Array.Sort(workers, (x, y) => Comparer<object>.Default.Compare(sortingCriterion(x), sortingCriterion(y)));
        PrintWorkers(workers);
    }

    static void Main(string[] args)
    {
        Worker[] workers = ReadWorkersArray(3); // Наприклад
        SortAndPrintWorkersBy(w => w.GetWorkExperience());
    }
}
