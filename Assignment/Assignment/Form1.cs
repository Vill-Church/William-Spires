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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<String> staff = ReadCsv();
            company.SetListOfEmployees(CreateEmployeeList(staff));
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
            dataGridView1.Columns[0].ReadOnly = true; // disable id edit 
            dataGridView1.Columns[5].ReadOnly = true; // disable age edit, age will be determined from DOB
            dataGridView1.AllowUserToAddRows = false;
            Allemployees = new List<Employee>();
            Allemployees = company.GetEmployees();
        }

        private List<Employee> CreateEmployeeList(List<string> ListOfEmployees)
        {
            List<Employee> employees = new List<Employee>();
            List<string[]> row = ListOfEmployees.Select(x => x.Split(',')).ToList();
            for (int i = 1; i < ListOfEmployees.Count(); i++) // first line is column headings
            {
                Employee employee = new Employee(Convert.ToInt32(row[i][0]), row[i][1], row[i][2], row[i][3], row[i][4], Convert.ToByte(row[i][5]), row[i][6], row[i][7], row[i][8]);
                employees.Add(employee);
            }
            return employees;
        }
        private List<String> ReadCsv()
        {
            List<String> Contents = new List<String>();
            Stream FilePath;
            bool isEmpty = true;
            while (isEmpty == true)
            {
                OpenFileDialog openFile = new OpenFileDialog
                {
                    Title = "Select Members CSV file",
                    Filter = "CSV Files|*.csv",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if ((FilePath = openFile.OpenFile()) != null)
                        {
                            if (new FileInfo(openFile.FileName).Length == 0)
                            {
                                isEmpty = true;// Empty
                                MessageBox.Show("This file is empty");
                            }
                            else
                            {
                                isEmpty = false;
                                company.SetFilePath(openFile.FileName);
                                Contents = File.ReadAllLines(openFile.FileName).ToList(); // Read 
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not read selected file: " + ex.Message);
                    }
                }
            }
            return Contents;
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
            StreamWriter MrWritey = new StreamWriter("test.csv");
            for (int i = 0; i < Allemployees.Count(); i++)
            {
                StringBuilder EmployeeChange = new StringBuilder();
                EmployeeChange.Append(Allemployees.ElementAt(i).GetId());
                EmployeeChange.Append(',');
                EmployeeChange.Append(Allemployees.ElementAt(i).GetFirstName());
                EmployeeChange.Append(',');
                EmployeeChange.Append(Allemployees.ElementAt(i).GetLastName());
                EmployeeChange.Append(',');
                EmployeeChange.Append(Allemployees.ElementAt(i).GetJoinDate());
                EmployeeChange.Append(',');
                EmployeeChange.Append(Allemployees.ElementAt(i).GetDateOfBirth());
                EmployeeChange.Append(',');
                EmployeeChange.Append(Allemployees.ElementAt(i).GetAge());
                EmployeeChange.Append(',');
                EmployeeChange.Append(Allemployees.ElementAt(i).GetPhoneNumber());
                EmployeeChange.Append(',');
                EmployeeChange.Append(Allemployees.ElementAt(i).GetEmailAddress());
                EmployeeChange.Append(',');
                EmployeeChange.Append(Allemployees.ElementAt(i).Gettype());
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
    }
}
