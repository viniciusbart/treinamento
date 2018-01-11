using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    public class Employee
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public decimal Bonus { get; set; }

        public override string ToString()
        {
            var total = Salary + Bonus;
            return $"{Name} - Salario: {Salary.ToString("C2")} - Bonus: {Bonus.ToString("C2")} - Total: {total.ToString("C2")}";
        }

    }
}
