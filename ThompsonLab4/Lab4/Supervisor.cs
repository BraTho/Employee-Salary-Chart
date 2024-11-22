using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThompsonLab4
{
    public class Supervisor : Employee
    {
        public decimal Bonus{ get; set; }

        public Supervisor() { }

        //calls the base information from employees than adds Bonus
        public Supervisor(string name, DateTime startdate, decimal salary)
            : base(name, startdate, salary)
        {
            Bonus = 250;
        }

        // It similar to the raise in employees but now it raises the bonus for supervisors
        public override bool Raise(decimal amount) {  
            if(amount > 0)
            {
                Salary += Salary * amount;
                Bonus += Bonus * amount;
                return true;
            }else { return false; }
        }

        public override string ToString()
        {
            //this adds the Bonus information for supervisors in the listbox and combo box
            return base.ToString() + Bonus.ToString("c2");
        }

        //an operator that is used for the bonus
        public static Supervisor operator ++(Supervisor supervisor)
        {
            supervisor.Bonus += 100;
            return supervisor;
        }

        public static Supervisor operator --(Supervisor supervisor)
        {
            supervisor.Bonus = Math.Max(0, supervisor.Bonus - 100);
            return supervisor;
        }
    }
}
