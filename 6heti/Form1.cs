﻿using _6heti.Entities;
using _6heti.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace _6heti
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();

        public Form1()
        {
            InitializeComponent();
            GetCurrencies();
            comboBox1.DataSource = Currencies;
            NewMethod();

        }

        private void NewMethod()
        {
            Rates.Clear();

            GetExchangeRates();
            dataGridView1.DataSource = Rates;
            otosfeladatfuggveny();
            hatosfeladatfuggveny();
        }

        void GetExchangeRates()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {

                currencyNames = Convert.ToString(comboBox1.SelectedItem),
                startDate = Convert.ToString(dateTimePicker1.Value),
                endDate = Convert.ToString(dateTimePicker2.Value)
            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;

        }
        void otosfeladatfuggveny()
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);                    // ???????????????????

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);

                // Dátum
                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                // Valuta
                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                // Érték
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }
        }

        void hatosfeladatfuggveny() //refresh data fgv
        {
            chartRateData.DataSource = Rates;
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            NewMethod();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewMethod();
        }

        GetCurrencies() 
        {
            //nem tudtam
        }
    }
}
