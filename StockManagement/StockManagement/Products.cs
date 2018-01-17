using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagement
{
    class Products
    {
        private int ProductID;
        private string ProductName;
        private double price;
        private int quantity;
        public Products(int id, string name, double price, int quantity)
        {
            SetProductId(id);
            SetProductName(name);
            SetPrice(price);
            SetQuantity(quantity);
        }
        public int GetProductID()
        {
            return ProductID;
        }
        public void SetProductId(int id)
        {
            ProductID = id;
        }
        public string GetProductName()
        {
            return ProductName;
        }
        public void SetProductName(string name)
        {
            ProductName = name;
        }
        public double GetPrice()
        {
            return price;
        }
        public void SetPrice(double price)
        {
            this.price = price;
        }
        public int GetQuantity()
        {
            return quantity;
        }
        public void SetQuantity(int quantity)
        {
            this.quantity = quantity;
        }
    }
}
