﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public partial class Form1 : Form
    {
        private Validator Validation = new Validator();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Company company = new Company();
            company.SetListOfEmployees();
            List<String> staff = company.GetListOfEmployees();
            String[] Columns = staff.ElementAt(0).Split(',').ToArray();
            staff.RemoveAt(0);
            DataTable dt = new DataTable();
            //dt.Columns.Add("column anme");
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
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Validation.SetTemp(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != Validation.GetTemp())
            {
                //value has changed
                int columnChanged = Convert.ToInt32(dataGridView1.Rows[e.ColumnIndex].Index);
                int rowChanged = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                String value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
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
                    }
                    else if (columnChanged == 8)
                    {
                        //type validator
                    }
                }
            }
        }
    }
}
