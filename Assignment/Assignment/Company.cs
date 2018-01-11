using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    class Company
    {
        private List<String> ListOfEmployees = new List<String>();
        private List<Employee> employees = new List<Employee>();
        private String filePath;
        
        public List<Employee> GetEmployees()
        {
            return employees;
        }
        public void SetFilePath(String filePath)
        {
            this.filePath = filePath;
        }
        public String GetFilePath()
        {
            return filePath;
        }

        public List<String> GetListOfEmployees()
        { 
            return ListOfEmployees;
        }

        public void SetListOfEmployees()
        {
            ListOfEmployees = ReadCsv(filePath);
            List<string[]> row = ListOfEmployees.Select(x => x.Split(',')).ToList();
            for (int i=1; i<ListOfEmployees.Count(); i++)
            {
                Employee employee = new Employee(Convert.ToInt32(row[i][0]), row[i][1], row[i][2], row[i][3], row[i][4], Convert.ToByte(row[i][5]), row[i][6], row[i][7], row[i][8]);
                employees.Add(employee);
            }
        }
        private List<String> ReadCsv(String filePath)
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
                                SetFilePath(openFile.FileName);
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
    }
}
