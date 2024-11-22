using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ThompsonLab4;

namespace Lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            // A list of Employees
            workers = new List<Employee>();
            InitializeComponent();

            lstEmployees.DisplayMember = "Name";
            cboNames.DisplayMember = "Name";

            ListData();
        }

        private List<Employee> workers;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Checks 1st rad button and turns off the compare button
            radEmployee.Checked = true;
            btnCompare.Enabled = false;
            ResetInputs();
            //These are arrays for the new employees
            string[] names = { "Carter Reid", "Nathan Natidi", "Shane White" };
            string[] startDates = { "2022/05/09", "2023/08/18", "2021/11/24" };
            decimal[] salarys = { 36900, 55900, 73500 };
            //A for loop for making three new employees
            for (int i = 0; i < names.Length; i++)
            {
                Employee employee = new Employee(names[i], DateTime.Parse(startDates[i]), salarys[i]);
                workers.Add(employee);
            }
            //Creates a new supervisor
            Supervisor supervisor = new Supervisor("Brady Thompson", DateTime.Now, 3);
            workers.Add(supervisor);

            ListData();
        }

        private void ListData()
        {
            // This displays the employes in the listbox and combo box
            this.lstEmployees.DataSource = null;
            this.lstEmployees.DataSource = workers;
            this.cboNames.DataSource = null;
            this.cboNames.DataSource = workers;
        }

        public void ResetInputs()
        {
            //it resets all the text boxes, set the date back to the current date and focuses on the name text box
            txtName.Text = "";
            txtSalary.Text = "";
            txtName.Focus();
            dtpStarted.Value = DateTime.Now;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // it validates if the there is anything in the name textbox and that the stuff in the salary text box is a decimal
            if (!Validator.IsPresent(txtName))
                return;
            if (!Validator.IsDecimal(txtSalary))
                return;
            // This shows that if the employee radio button is checked it makes an employee instance and make a supervisor instance if it isnt 
            if (radEmployee.Checked)
            {
                Employee employee = new Employee(txtName.Text, DateTime.Parse(dtpStarted.Text), decimal.Parse(txtSalary.Text));
                workers.Add(employee);
            }
            else
            {
                Supervisor supervisor = new Supervisor(txtName.Text, DateTime.Parse(dtpStarted.Text), decimal.Parse(txtSalary.Text));
                workers.Add(supervisor);
            }
            ListData();
            ResetInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // deletes the employee or supervisor that is hightlighted
            if(lstEmployees.SelectedIndex == -1) return;    
            if (MessageBox.Show("Are you sure?", "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            workers.RemoveAt(lstEmployees.SelectedIndex);
            ListData();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            // takes two employees and campares the dates to see which one was hired first
            Employee employee1 = (Employee)lstEmployees.SelectedItem;
            Employee employee2 = (Employee)cboNames.SelectedItem;

            bool result = employee1 <= employee2;



            MessageBox.Show(result ? $"{employee1.Name} started before {employee2.Name}" : $"{employee1.Name} started after {employee2.Name}");
            ListData();
            ResetInputs();
        }

        private void btnRaise_Click(object sender, EventArgs e)
        {
            //gives a 1.75% raise if you press the raise button and press ok
            if (MessageBox.Show("Are you sure?", "Give Raise", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;


            for(int i = 0; i<workers.Count; i++)
            {
                workers[i].Raise(0.0175m);
            }

            ListData();
            lstEmployees.SelectedIndex = 0;
        }




        private void btnBonus_Click(object sender, EventArgs e)
        {
            // if you have a supervisor selected and press the bonus button it will either add $100 or take it away depending on if you press yes or no
            if(lstEmployees.SelectedItem != null && lstEmployees.SelectedItem is Supervisor supervisor)
            {
                DialogResult dialogResult = MessageBox.Show("Unit of change is $100: \n Select yes to add bonus \n Select no to subtract from bonus \n Select cancel or close to cancel change", "Change Bonus", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    supervisor++;
                }else if(dialogResult == DialogResult.No) 
                {
                    supervisor--;
                }else
                {
                    return;
                }
            }
            ListData();
        }

        private void lstEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleCompare();
        }

        private void cboNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleCompare();
        }

        public void ToggleCompare()
        {
            // if the same instance is selected in the listbox and commbo box in will disable and if they arent in will be enabled
            if (lstEmployees.SelectedIndex == -1 || cboNames.SelectedIndex == -1)
            {
                btnCompare.Enabled = false;
                return;
            }

            Employee employee1 = (Employee)lstEmployees.SelectedItem;
            Employee employee2 = (Employee)cboNames.SelectedItem;

            if (employee1 == employee2)
            {
                btnCompare.Enabled = false;
                return;
            }

            btnCompare.Enabled = true;

        }
    }
}
