using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThompsonLab4
{
    public class Employee
    {
        public decimal Amount;
        private DateTime date;
        private DateTime minDate = new DateTime(2020, 1, 1);
        private static int id = 3000;
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Salary {
            get { return Amount; }
            set { if (value > 50000)
                    Amount = value;
                else
                {
                    Amount = 50000;
                }
            }
        }
        public DateTime StartDate
        {
            get { return date; }
            set
            {
                if (value > minDate && value <= DateTime.Now)
                {
                    date = value;
                }
                else
                {
                    date = minDate;
                }
            }
        }

        public int NextID {
            get { return id; }
            set { id = value; }
        }

        private void SetID()
        {
            // takse the ID number and adds one to it every id
             ID = id++;
        }


        public Employee()
        {
            SetID();
        }

        public Employee(string name, DateTime startdate, decimal salary) : this()
        {
            Name = name;
            StartDate = startdate;
            Salary = salary;
        }


        public virtual bool Raise(decimal amount)
        {
            // if the amount is greater than 0 it will multiply the salary by the amount to give a raise
            if(amount > 0)
            {
                Salary += Salary * amount;
                return true;
            }else
            {
                return false;
            }
        }
            
        public override string ToString()
        {
            //This formats the information in the listbox and combo box
            return string.Format("{0,-11} {1,-18} {2,-18:dd-MMM-yyyy} {3,-16:C2} {4,-20:C2}", ID, Name, StartDate, Salary, GetType().Name);
        }

        // This is an operator we used for the compare button that is used to see what date is farther than the other
        public static bool operator <= (Employee emp1, Employee emp2) 
        {
            return emp1.StartDate <= emp2.StartDate;
        }
        public static bool operator >= (Employee emp1, Employee emp2)
        {
            return emp1.StartDate >= emp2.StartDate;
        }
    }
}

