using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    class Program
    {
        public static List<Employee> Employees = new List<Employee>()
        {
            new Employee() { Name = "Pavlov", Salary = 2850m, Bonus = 100m },
            new Employee() { Name = "Dimitri", Salary = 1940m, Bonus = 300m },
            new Employee() { Name = "Nikolai", Salary = 2780m, Bonus = 150m },
            new Employee() { Name = "Russell", Salary = 2400m, Bonus = 0m },
            new Employee() { Name = "Kriger", Salary = 6550m, Bonus = 200m},
            new Employee() { Name = "Yure", Salary = 4800m, Bonus = 300m },
            new Employee() { Name = "Mikhail", Salary = 4100m, Bonus = 300m },
        };

        public static decimal GetSalaryAverage()
        {
            ///Equivalenete ao return
            //var result = (from e in Employees
            //              select e.Salary).Average();

            return Employees.Average(e => e.Salary);
        }

        public static IEnumerable<Employee> GetSalaryAboveAverage()
        {
            var avg = GetSalaryAverage();
            var result = Employees.Where(x => x.Salary > avg).ToList();
            return result;
        }

        public static IEnumerable<Employee> GerOrderedEmployeesbyTotal()
        {
            var result = Employees.OrderBy(x => x.Salary + x.Bonus).ToList();

            return result;
        }

        public static Employee GetEmployeeWithLowestSalary()
        {
            //return Employees.OrderBy(x => x.Salary).FirstOrDefault();
            //return Employees.First(x => x.Salary == Employees.Min(y => y.Salary));

            return Employees.Where(x => x.Salary == Employees.Min(y => y.Salary)).First();
        }

        public static IEnumerable<Employee> AdjustSalaryAndBonus(decimal percent)
        {
            var avg = GetSalaryAverage();
            var lowerAvg = Employees.Where(x => x.Salary < avg).ToList();

            foreach (var item in lowerAvg)
            {
                item.Salary *= 1 + percent;
                item.Bonus *= 1 + percent;
            }

            return lowerAvg;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Employees");
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Average Salary: {0}", GetSalaryAverage().ToString("C2"));
            Console.WriteLine();

            Console.WriteLine("Employees with salary above average");
            var employeeAboveAvg = GetSalaryAboveAverage();

            foreach (var employee in employeeAboveAvg)
            {
                Console.WriteLine(employee);
            }

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Employees Ordered by total");

            var employeesWithSalaryAbove = GerOrderedEmployeesbyTotal();

            foreach (var employee in employeesWithSalaryAbove)
            {
                Console.WriteLine(employee);
            }

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Lowest Salary:");
            Console.WriteLine(GetEmployeeWithLowestSalary());

            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Employees with Adjusts ");
            var employeesWithAdjusts = AdjustSalaryAndBonus(0.15m);
            foreach (var employee in employeesWithAdjusts)
            {
                Console.WriteLine(employee);
            }


            /// EXEMPLO DE ALTERAÇÃO DE SAÍDA COM SELECT
            //Console.Clear();

            //var avg = GetSalaryAverage();

            //var employees = Employees.Where(x => x.Salary < avg)
            //                         .Select(x =>
            //                         {
            //                             x.Salary *= 1 + 0.15m;
            //                             x.Bonus *= 1 + 0.15m;

            //                             return x.Name;
            //                         });
            //foreach (var employee in employees)
            //{
            //    Console.WriteLine(employee);
            //}

            Console.ReadLine();
        }
    }
}
