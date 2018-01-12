using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public partial class Form1 : Form
    {
        private Company company = new Company();
        private Validator Validation = new Validator();
        private DataTable dt;
        private List<Employee> Allemployees;
        private bool dataAdded = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            company.SetListOfEmployees();
            List<String> staff = company.GetListOfEmployees();
            String[] Columns = staff.ElementAt(0).Split(',').ToArray();
            staff.RemoveAt(0); // get rid of column headers
            dt = new DataTable();
            for(int i =0; i < Columns.Length; i++)
            {
                dt.Columns.Add(Columns[i]);
            }
            List<string[]> row = staff.Select(x => x.Split(',')).ToList();
            row.ForEach(x =>
            {
                dt.Rows.Add(x);
            });
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = false;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            Allemployees = company.GetEmployees();
            dataAdded = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Validation.SetTemp(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }
         private void WriteToCsv()
        {
            List<Employee> employees = company.GetEmployees();
            StreamWriter MrWritey = new StreamWriter("test.csv");
            for (int i = 0; i < employees.Count(); i++)
            {
                StringBuilder EmployeeChange = new StringBuilder();
                EmployeeChange.Append(employees.ElementAt(i).GetId());
                EmployeeChange.Append(',');
                EmployeeChange.Append(employees.ElementAt(i).GetFirstName());
                EmployeeChange.Append(',');
                EmployeeChange.Append(employees.ElementAt(i).GetLastName());
                EmployeeChange.Append(',');
                EmployeeChange.Append(employees.ElementAt(i).GetJoinDate());
                EmployeeChange.Append(',');
                EmployeeChange.Append(employees.ElementAt(i).GetDateOfBirth());
                EmployeeChange.Append(',');
                EmployeeChange.Append(employees.ElementAt(i).GetAge());
                EmployeeChange.Append(',');
                EmployeeChange.Append(employees.ElementAt(i).GetPhoneNumber());
                EmployeeChange.Append(',');
                EmployeeChange.Append(employees.ElementAt(i).GetEmailAddress());
                EmployeeChange.Append(',');
                EmployeeChange.Append(employees.ElementAt(i).Gettype());
                MrWritey.WriteLine(EmployeeChange.ToString());
            }
           MrWritey.Close();
            MessageBox.Show("Done something");
            //File.WriteAllLines("testing.csv", company.GetListOfEmployees());
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            List<Employee> employees = company.GetEmployees();
            if(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != Validation.GetTemp())
            {
                //value has changed
                int columnChanged = Convert.ToInt32(dataGridView1.Rows[e.ColumnIndex].Index);
                int changed = Convert.ToInt32(dataGridView1.Rows[e.RowIndex]);
                string value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (value == Validation.GetTemp())
                {
                    // didnt change

                }
                else // determine validator needed
                {
                    if (columnChanged == 1 || columnChanged == 2)
                    {
                        // name validator
                        NameValidator NameValidation = new NameValidator(Validation.GetTemp());
                        if (NameValidation.CheckName(value) == true)
                        {
                            // update value
                            if (columnChanged == 1)
                            {
                                employees[changed].SetFirstName(value);
                            } else
                            {
                                employees[changed].SetLastName(value);
                            }
                        }
                    }
                    else if (columnChanged == 3)
                    {
                        // Join date validator
                    }
                    else if (columnChanged == 4)
                    {
                        // DOB validator
                    }
                    else if (columnChanged == 6)
                    {
                        // Phone number validator
                    }
                    else if (columnChanged == 7)
                    {
                        // email validator
                        EmailValidator emailValid = new EmailValidator(Validation.GetTemp());
                        if (emailValid.CheckEmail() == true)
                        {
                            // valid email
                            employees[changed].SetEmailAddress(value);
                        }
                    }
                    else if (columnChanged == 8)
                    {
                        //type validator
                    }
                }
            }
        }

        private void testWriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteToCsv();
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataAdded == false)
            {

            }
            else
            {
                int id = (Convert.ToInt32((dataGridView1.Rows[e.RowIndex])) + 1); // increment id
                bool isValid = true;
                string firstName = (dataGridView1.Rows[e.RowIndex].Cells[1]).ToString(), lastName = (dataGridView1.Rows[e.RowIndex].Cells[2]).ToString(), joinDate = (dataGridView1.Rows[e.RowIndex].Cells[3]).ToString(), dob = (dataGridView1.Rows[e.RowIndex].Cells[4]).ToString(), phoneNumber = (dataGridView1.Rows[e.RowIndex].Cells[6]).ToString(), emailAddress = (dataGridView1.Rows[e.RowIndex].Cells[7]).ToString(), ttype = (dataGridView1.Rows[e.RowIndex].Cells[8]).ToString();
                byte eAge = Convert.ToByte(dataGridView1.Rows[e.RowIndex].Cells[5]);
                if (isValid == true)
                {
                    Employee newEmployee = new Employee(id, firstName, lastName, joinDate, dob, eAge, phoneNumber, emailAddress, ttype);
                    Allemployees.Add(newEmployee);
                    MessageBox.Show("Done");
                }
                else
                {

                }
            }
        }
    }
}
