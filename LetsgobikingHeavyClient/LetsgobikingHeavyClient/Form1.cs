using LetsgobikingHeavyClient.LetsGoBiking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LetsgobikingHeavyClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void AddressInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void fetch_Click(object sender, EventArgs e)
        {
            RoutingGBClient routingGBClient = new RoutingGBClient();
            double[] coordinates = routingGBClient.convert(AddressInput.Text)[0].geometry.coordinates;
            string station = routingGBClient.GetClosestStation(coordinates[1], coordinates[0]).name;
            label7.Text = station;
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
