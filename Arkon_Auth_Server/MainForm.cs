using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Server.Commands;
using Server.ViewModel;
using static Server.Commands.command;

namespace Server {
    public partial class MainForm : Form {
        #region Vars
        private List<Client> clients;
        private Client selected_client;
        private Socket listener;
        private IPAddress serverIP;
        private int serverPort;
        public enum MsgType {
            Info,
            Exception,
            Command
        }
        #endregion

        #region Main
        public MainForm() {
            InitializeComponent();
            clients = new List<Client>();
            if (!File.Exists("ServerLog.txt")) { FileStream f = File.Create("ServerLog.txt"); f.Close(); }
            ConsoleData.Text = File.ReadAllText("ServerLog.txt");
            ConsoleData.SelectionStart = ConsoleData.Text.Length;
            ConsoleData.ScrollToCaret();
            clientsData.AutoGenerateColumns = false;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
        }
        private void Console_TextChanged(object sender, EventArgs e) {
            ConsoleData.SelectionStart = ConsoleData.Text.Length;
            ConsoleData.ScrollToCaret();
        }
        private void serverBTN_Click(object sender, EventArgs e) {
            switch(serverBTN.Text) {
                case "Start Server":
                    ConsoleData.Text = "";
                    File.WriteAllText("ServerLog.txt", "");
                    serverBTN.Text = "Stop Server";
                    Text = "Server | Running";
                    serverIP = IPAddress.Parse("192.168.1.8");
                    serverPort = 10135;
                    listener_bw.RunWorkerAsync();
                    break;
                case "Stop Server":
                    DisconnectServer();
                    serverBTN.Text = "Start Server";
                    Text = "Server | Idle";
                    break;
            }
        }
        private void ConsoleWriter(MsgType type, string msg) {
            ConsoleData.Invoke((MethodInvoker)delegate {
                switch (type) {
                    case MsgType.Info:
                        string info = $"{DateTime.Now.ToString("MM/dd/yy hh:mm::ss tt")} [Info] {msg}\n";
                        File.AppendAllText("ServerLog.txt", info);
                        ConsoleData.AppendText(info);
                        break;
                    case MsgType.Command:
                        string command = $"{DateTime.Now.ToString("MM/dd/yy hh:mm::ss tt")} [Command] {msg}\n";
                        File.AppendAllText("ServerLog.txt", command);
                        ConsoleData.AppendText(command);
                        break;
                    case MsgType.Exception:
                        string exception = $"{DateTime.Now.ToString("MM/dd/yy hh:mm::ss tt")} [Exception] {msg}\n";
                        File.AppendAllText("ServerLog.txt", exception);
                        ConsoleData.AppendText(exception);
                        break;
                }
            });
        }
        #endregion

        #region Server Handling
        private void listener_bw_DoWork(object sender, DoWorkEventArgs e) {
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            listener.Bind(new IPEndPoint(serverIP, serverPort));
            listener.Listen(Convert.ToInt32(12));
            while(true) {
                try {
                    AddClient(listener.Accept());
                } catch { }
            }
        }

        private void AddClient(Socket socket) {
            Client client = new Client(socket);
            client.CommandReceived += new CommandReceivedEventHandler(CommandReceived);
            client.Disconnected += new DisconnectedEventHandler(ClientDisconnected);

            if(!AbnormalDC(client)) {
                clients.Add(client);
                ConsoleWriter(MsgType.Info, $"{client.IP}:{client.Port} Connected.");
                Invoke((MethodInvoker)delegate {
                    clientsData.DataSource = clients;
                });
            }
        }
        private bool RemoveClient(IPAddress ip) {
            lock(this) {
                Client c = clients.Find(x => x.IP == ip);
                if(c != null) {
                    clients.Remove(c);
                    Invoke((MethodInvoker)delegate {
                        if(clients.Count == 0) {
                            clientsData.DataSource = null;
                        } else {
                            clientsData.DataSource = clients;
                        }
                    });
                    return true;
                } else {
                    return false;
                }
            }
        }
        private bool AbnormalDC(Client mngr) {
            if(RemoveClient(mngr.IP)) {
                ConsoleWriter(MsgType.Info, $"{mngr.IP}:{mngr.Port} Abnormally Disconnected.");
                return true;
            } else {
                return false;
            }
        }
        public void DisconnectServer() {
            if (clients != null) {
                foreach (Client c in clients) {
                    c.Disconnect();
                }
                listener_bw.CancelAsync();
                listener_bw.Dispose();
                listener.Close();
                GC.Collect();
            }
        }
        #endregion

        #region Events
        private void CommandReceived(object sender, CommandEventArgs e) {
            switch(e.cmd.type) {
                case cmdType.Auth:
                    if(clients.Count == 0) {
                        clientsData.DataSource = null;
                    } else {
                        clientsData.DataSource = clients;
                    }
                    ConsoleWriter(MsgType.Command, $"[{e.cmd.client_ip}] Auth_Lic: {e.auth.lic}");
                    break;
                case cmdType.Ping:
                    break;
            }
        }
        private void ClientDisconnected(object sender, ClientEventArgs e) {
            if(RemoveClient(e.IP))
                ConsoleWriter(MsgType.Info, $"{e.IP}:{e.Port} Disconnected.");
        }
        #endregion

        private void send_test_btn_Click(object sender, EventArgs e) {
            if(selected_client == null) return;

            auth cmd = new auth(selected_client.IP, selected_client.License);
            selected_client.Send_Command(cmd);
        }

        private void clientsData_SelectionChanged(object sender, EventArgs e) {
            try {
                selected_client = clients[clientsData.SelectedCells[0].RowIndex];
            } catch { }
        }
    }
}
