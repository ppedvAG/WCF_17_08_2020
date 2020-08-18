using BLZ_Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLZ_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BLZServicePortTypeClient bankServiceClient = new BLZServicePortTypeClient("BLZServiceSOAP12port_http");

            var bankResult = bankServiceClient.getBank(textBox1.Text);

            label2.Text = $"{bankResult.bezeichnung} \nBIC:{bankResult.bic}\n{bankResult.plz} {bankResult.ort}";
        }
    }
}
