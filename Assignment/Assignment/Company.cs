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
        private String filePath;
        
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
            ListOfEmployees = ReadCsv(this.filePath);
        }
        private List<String> ReadCsv(String filePath)
        {
            List<String> Contents = new List<String>();
            Stream FilePath;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Select Members CSV file";
            openFile.Filter = "CSV Files|*.csv";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if(openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if((FilePath = openFile.OpenFile()) != null)
                    { 
                        Contents = File.ReadAllLines(openFile.FileName).ToList();
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("Could not read selected file: " + ex.Message);
                }
            }
           
            return Contents;
        }
    }
}
