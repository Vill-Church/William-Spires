using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StockManagement
{
    public partial class Form1 : Form
    {
        private DataTable dt;
        private List<Products> ProductList = new List<Products>();
        private string[] Columns;
        private Products updated;
        private int selectedRcItem; // gloabal because its used across the context menu functions
       // private int startWidth, startHeight;
        private string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\stock.csv";
        public Form1()
        {
            InitializeComponent();
        }
        public void SetUpdatedProduct(int id, string name, double price, int quantity)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            /* startWidth = this.Width;
             startHeight = this.Height;
             this.WindowState = FormWindowState.Maximized;
             this.MaximumSize = this.Size;
             this.MinimumSize = this.Size;
             double rw = (Width - startWidth) / startWidth;
             double rh = (Height - startHeight) / startHeight;
             dataGridView1.Width += Convert.ToInt32(dataGridView1.Width * rw);
             dataGridView1.Height += Convert.ToInt32(dataGridView1.Height * rh);
             MessageBox.Show(this.Height.ToString());
             MessageBox.Show(this.Width.ToString()); */ // dynamic object sizing might be used
            tbPId.Enabled = false;
            tbName.Enabled = false;
            tbPrice.Enabled = false;
            tbQuantity.Enabled = false;
            if (!File.Exists(filePath)){ // check if file exits
                var file = File.Create(filePath); // if not create the file
                file.Close();
            } else if(new FileInfo(filePath).Length == 0) 
            { 
                // empty file therefore do not populate
            } else
            {
                Populate(ReadCSV()); // file has contents so populate. Calls ReadCSV() to return a list of strings for each line in the file
            }
        }
        private void Populate(List<string> contents)
        {
            Columns = contents[0].Split(',');
            contents.RemoveAt(0); // removing the headers from the list of contents as they are not products
            dt = new DataTable(); 
            for (int i=0; i < Columns.Length; i++)
            {
                dt.Columns.Add(Columns[i]); // adds all the columns from the headers provided by the csv
            }
            List<string[]> rows = contents.Select(x => x.Split(',')).ToList(); // Creates a list of string arrays storing one line of the file per array seperated by ,'s
            rows.ForEach(x =>
            {
                dt.Rows.Add(x); // x contains one lines worth of data in an array and adds it into the datatable
                Products product = new Products(Convert.ToInt32(x[0]), x[1].ToString(), Convert.ToDouble(x[2]), Convert.ToInt32(x[3])); // creates a new product dynamically
                ProductList.Add(product); // adds the product to the product list
            });
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = true; // disables the user from editing stuff in the datatable from the datatable
            dataGridView1.AllowUserToAddRows = false; // rows must be added using the add function not the datatable to ensure validation
            for(int i =0; i< dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // make sure the columns fill the size of the datatable
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex<0)
            {
                // header clicked as their index is -1
            } else // sets the textboxes to the values from the datagrid so they can be edited
            {
                int row = dataGridView1.Rows[e.RowIndex].Index;
                /*tbPId.Text = dataGridView1.Rows[row].Cells[0].Value.ToString();
                tbName.Text = dataGridView1.Rows[row].Cells[1].Value.ToString();
                tbPrice.Text = dataGridView1.Rows[row].Cells[2].Value.ToString();
                tbQuantity.Text = dataGridView1.Rows[row].Cells[3].Value.ToString();*/
            }
            
        }

        private List<string> ReadCSV()
        {
            List<string> contents = new List<string>();
            try
            {
                contents = File.ReadAllLines(filePath).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not read selected file. " + ex.Message);
            }
            return contents;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                selectedRcItem = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                RcContextMenu.Show(dataGridView1, new Point(e.X,e.Y));
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(selectedRcItem.ToString());
            //Editor edit = new Editor(ProductList.ElementAt(selectedRcItem).GetProductID(),ProductList.ElementAt(selectedRcItem).GetProductName(),ProductList.ElementAt(selectedRcItem).GetQuantity(),ProductList.ElementAt(selectedRcItem).GetPrice());
            //edit.Show();
            tbPId.Enabled = true;
            tbName.Enabled = true;
            tbPrice.Enabled = true;
            tbQuantity.Enabled = true;
            tbPId.Text = dataGridView1.Rows[selectedRcItem].Cells[0].Value.ToString();
            tbName.Text = dataGridView1.Rows[selectedRcItem].Cells[1].Value.ToString();
            tbPrice.Text = dataGridView1.Rows[selectedRcItem].Cells[2].Value.ToString();
            tbQuantity.Text = dataGridView1.Rows[selectedRcItem].Cells[3].Value.ToString();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(selectedRcItem.ToString());
            ProductList.RemoveAt(selectedRcItem);
            RefreshDataGrid();
            MessageBox.Show("Product Deleted.");
        }
        private void RefreshDataGrid()
        { 
            dt = new DataTable();
            for(int i=0; i<Columns.Length; i++)
            {
                dt.Columns.Add(Columns[i]);
            }
            ProductList.ForEach(x =>
            {
                string[] arr = { x.GetProductID().ToString(), x.GetProductName(), x.GetQuantity().ToString(), x.GetPrice().ToString() };
                dt.Rows.Add(arr);
            });
            dataGridView1.DataSource = dt;
        }
        private void WriteToCsv()
        {
            List<string> toCsv = new List<string>();
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i<Columns.Length; i++)
            {
                sb.Append(Columns[i]);
                if(i+1 == Columns.Length)
                {

                }else
                {
                    sb.Append(',');
                }
                
            }
            toCsv.Add(sb.ToString());
            sb.Clear();
            foreach(Products p in ProductList)
            {
                sb.Append(p.GetProductID());
                sb.Append(',');
                sb.Append(p.GetProductName());
                sb.Append(',');
                sb.Append(p.GetPrice());
                sb.Append(',');
                sb.Append(p.GetQuantity());
                toCsv.Add(sb.ToString());
                sb.Clear();
            }
            File.WriteAllLines(filePath, toCsv.ToList());
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteToCsv();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        { 
            ProductList.ElementAt(selectedRcItem).SetProductId(Convert.ToInt32(tbPId.Text));
            ProductList.ElementAt(selectedRcItem).SetProductName(tbName.Text);
            ProductList.ElementAt(selectedRcItem).SetPrice(Convert.ToDouble(tbPrice.Text));
            ProductList.ElementAt(selectedRcItem).SetQuantity(Convert.ToInt32(tbQuantity.Text));
            RefreshDataGrid();
            MessageBox.Show("Done");
        }
    }
}
