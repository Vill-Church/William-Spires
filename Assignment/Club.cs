using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class Class1
{
    private List<string> Employees = new List<string>();
	public Class1()
	{

	}
    public void ReadCSV()
    {
        Stream filePath = null;
        OpenFileDialog openFile = new OpenFileDialog();
        openFile.Title = "Select CSV File";
        openFile.Filter = "CSV File|*.csv";
        openFile.Show();

    }
}
