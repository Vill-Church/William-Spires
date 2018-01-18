using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagement
{
    public partial class Editor : Form
    {
        //private Products editProduct;
        private int ProductID, quantity;
        private string productName;
        private double price;
        public Editor(int id, string name, int amount, double price)
        {
            ProductID = id;
            productName = name;
            quantity = amount;
            this.price = price;
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            
        }

        private void txtProductId_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && !Char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && !Char.IsControl(ch))
            {
                e.Handled = true;
            }
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            txtProductId.Text = ProductID.ToString();
            txtProductName.Text = productName.ToString();
            txtAmount.Text = price.ToString();
            txtQuantity.Text = quantity.ToString();
        }
    }
}
