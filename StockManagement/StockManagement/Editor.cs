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

        private void Editor_Load(object sender, EventArgs e)
        {
            txtProductId.Text = ProductID.ToString();
            txtProductName.Text = productName.ToString();
            txtAmount.Text = price.ToString();
            txtQuantity.Text = quantity.ToString();
        }
    }
}
