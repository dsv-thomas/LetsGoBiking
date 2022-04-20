using LetsgobikingHeavyClient.RoutingService;
using System;
using System.Collections.Generic;
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
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            RoutingGBClient routingGBClient = new RoutingGBClient();
            double[] coordinates = routingGBClient.convert(AddressInput.Text)[0].geometry.coordinates;
            Station station = routingGBClient.GetClosestStation(coordinates[1], coordinates[0], true);
            watch.Stop();
            label7.Text = station.name;
            label9.Text = watch.ElapsedMilliseconds + " ms";
        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            RoutingGBClient routingGBClient = new RoutingGBClient();
            Travel travel = routingGBClient.travel(startpoint.Text, endpoint.Text);
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            label12.Text = travel.stationStart.name;
            label14.Text = travel.stationEnd.name;
            label10.Text = watch.ElapsedMilliseconds + " ms";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RoutingGBClient client = new RoutingGBClient();
            Dictionary<String, String> dict = client.getStatistics();
            label17.Text = dict["TotalRoute"];
            label18.Text = dict["TopStation"];
            label20.Text = dict["MoyResponseTime"] + " ms";

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}
