using Client.Commands;
using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client {
    public partial class MainForm : Form {
        Server server; 

        public MainForm() {
            InitializeComponent();
            server = new Server(IPAddress.Parse("192.168.1.8"), 666);
            server.Connect();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            server.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e) {
            auth a = new auth("fsdf");
            server.Send_Command(a);
        }
    }
}
