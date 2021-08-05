using Client.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Client.Commands.command;

namespace Client.ViewModel {
    public class Server {
        #region Vars
        public bool Connected {
            get {
                if(socket != null)
                    return socket.Connected;
                else
                    return false;
            }
        }

        public IPAddress ServerIP {
            get {
                if(Connected)
                    return server_ep.Address;
                else
                    return IPAddress.None;
            }
        }
        public int ServerPort {
            get {
                if(Connected)
                    return server_ep.Port;
                else
                    return -1;
            }
        }

        public IPAddress IP {
            get {
                if(Connected)
                    return ((IPEndPoint)socket.LocalEndPoint).Address;
                else
                    return IPAddress.None;
            }
        }
        public int Port {
            get {
                if(Connected)
                    return ((IPEndPoint)socket.LocalEndPoint).Port;
                else
                    return -1;
            }
        }

        public Socket socket;

        private NetworkStream stream;
        private BackgroundWorker recv_bw;
        private BackgroundWorker send_bw;
        private BackgroundWorker connector_bw;
        private IPEndPoint server_ep;
        private Semaphore sema = new Semaphore(1, 1);
        #endregion

        #region Contsructors
        public Server(IPEndPoint server) {
            server_ep = server;
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);

            connector_bw = new BackgroundWorker();
            connector_bw.DoWork += new DoWorkEventHandler(Connect);
            connector_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Connect_Completed);
        }

        public Server(IPAddress serverIP, int port) {
            server_ep = new IPEndPoint(serverIP, port);
            NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(NetworkChange_NetworkAvailabilityChanged);

            connector_bw = new BackgroundWorker();
            connector_bw.DoWork += new DoWorkEventHandler(Connect);
            connector_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Connect_Completed);
        }
        #endregion

        #region Private Methods
        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e) {
            if(!e.IsAvailable) {
                OnNetworkDead(new EventArgs());
                OnDisconnectedFromServer(new EventArgs());
            } else
                OnNetworkAlived(new EventArgs());
        }

        private void Recv(object sender, DoWorkEventArgs e) {
            while(socket.Connected) {
                try {
                    //Read the command's Type.
                    byte[] buffer = new byte[4];
                    int readBytes = stream.Read(buffer, 0, 4);
                    if(readBytes == 0)
                        break;
                    cmdType cType = (cmdType)BitConverter.ToInt32(buffer, 0);

                    switch(cType) {
                        case cmdType.Auth:
                            buffer = new byte[4];
                            readBytes = stream.Read(buffer, 0, 4);
                            if(readBytes == 0)
                                break;

                            Console.WriteLine($"Recv'ed Auth {BitConverter.ToInt32(buffer, 0)}");
                            break;
                    }

                } catch(Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }
            OnServerDisconnected(new ServerEventArgs(socket));
            Disconnect();
        }

        private void Send(object sender, DoWorkEventArgs e) {
            try {
                sema.WaitOne();
                object o = e.Argument;
                //Type
                byte[] buffer = new byte[4];
                buffer = BitConverter.GetBytes((int)((command)o).type);
                stream.Write(buffer, 0, 4);
                stream.Flush();

                switch(((command)o).type) {
                    case cmdType.Auth:
                        buffer = new byte[32];
                        buffer = Encoding.ASCII.GetBytes("zQhgcj8lso-uNJ1tVTgLv-NQXpyyRTrs");
                        stream.Write(buffer, 0, 32);
                        stream.Flush();

                        buffer = new byte[12];
                        buffer = Encoding.ASCII.GetBytes("00248D266789");
                        stream.Write(buffer, 0, 12);
                        stream.Flush();

                        buffer = new byte[32];
                        buffer = Encoding.ASCII.GetBytes("75DF0943FB828FC16907BFE5E87EFBE0");
                        stream.Write(buffer, 0, 32);
                        stream.Flush();

                        buffer = new byte[20];
                        buffer = Encoding.ASCII.GetBytes("11111111111111111111");
                        stream.Write(buffer, 0, 20);
                        stream.Flush();

                        buffer = new byte[3];
                        buffer = Encoding.ASCII.GetBytes("3.3");
                        stream.Write(buffer, 0, 20);
                        stream.Flush();

                        buffer = new byte[1];
                        buffer = BitConverter.GetBytes(0);
                        stream.Write(buffer, 0, 1);
                        stream.Flush();
                        break;
                }

                sema.Release();
                e.Result = true;
            } catch {
                sema.Release();
                e.Result = false;
            }
        }
        private void Send_Completed(object sender, RunWorkerCompletedEventArgs e) {
            if(!e.Cancelled && e.Error == null && ((bool)e.Result))
                OnCommandSent(new EventArgs());
            else
                OnCommandFailed(new EventArgs());

            ((BackgroundWorker)sender).Dispose();
            GC.Collect();
        }

        private void Connect(object sender, DoWorkEventArgs e) {
            try {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                socket.Connect(server_ep);

                stream = new NetworkStream(socket);

                recv_bw = new BackgroundWorker();
                recv_bw.WorkerSupportsCancellation = true;
                recv_bw.DoWork += new DoWorkEventHandler(Recv);
                recv_bw.RunWorkerAsync();

                send_bw = new BackgroundWorker();
                send_bw.WorkerSupportsCancellation = true;
                send_bw.DoWork += new DoWorkEventHandler(Send);
                send_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Send_Completed);

                e.Result = true;
            } catch {
                e.Result = false;
            }
        }
        private void Connect_Completed(object sender, RunWorkerCompletedEventArgs e) {
            if(!((bool)e.Result))
                OnConnectingFailed(new EventArgs());
            else
                OnConnectingSuccessed(new EventArgs());

            ((BackgroundWorker)sender).Dispose();
        }
        #endregion

        #region Public Methods
        public void Connect() {
            connector_bw.RunWorkerAsync();
        }

        public void Send_Command(object o) {
            if (socket != null && socket.Connected) {
                send_bw.RunWorkerAsync(o);
            } else {
                OnCommandFailed(new EventArgs());
            }
        }

        public bool Disconnect() {
            if(socket != null && socket.Connected) {
                try {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    recv_bw.CancelAsync();
                    OnDisconnectedFromServer(new EventArgs());
                    return true;
                } catch {
                    return false;
                }

            } else
                return true;
        }
        #endregion

        #region Events
        public event CommandReceivedEventHandler CommandReceived;
        protected virtual void OnCommandReceived(CommandEventArgs e) {
            if(CommandReceived != null) {
                Control target = CommandReceived.Target as Control;
                if(target != null && target.InvokeRequired)
                    target.Invoke(CommandReceived, new object[] { this, e });
                else
                    CommandReceived(this, e);
            }
        }

        public event CommandSentEventHandler CommandSent;
        protected virtual void OnCommandSent(EventArgs e) {
            if(CommandSent != null) {
                Control target = CommandSent.Target as Control;
                if(target != null && target.InvokeRequired)
                    target.Invoke(CommandSent, new object[] { this, e });
                else
                    CommandSent(this, e);
            }
        }

        public event CommandSendingFailedEventHandler CommandFailed;
        protected virtual void OnCommandFailed(EventArgs e) {
            if(CommandFailed != null) {
                Control target = CommandFailed.Target as Control;
                if(target != null && target.InvokeRequired)
                    target.Invoke(CommandFailed, new object[] { this, e });
                else
                    CommandFailed(this, e);
            }
        }

        public event ServerDisconnectedEventHandler ServerDisconnected;
        protected virtual void OnServerDisconnected(ServerEventArgs e) {
            if(ServerDisconnected != null) {
                Control target = ServerDisconnected.Target as Control;
                if(target != null && target.InvokeRequired)
                    target.Invoke(ServerDisconnected, new object[] { this, e });
                else
                    ServerDisconnected(this, e);
            }
        }

        public event DisconnectedEventHandler DisconnectedFromServer;
        protected virtual void OnDisconnectedFromServer(EventArgs e) {
            if(DisconnectedFromServer != null) {
                Control target = DisconnectedFromServer.Target as Control;
                if(target != null && target.InvokeRequired)
                    target.Invoke(DisconnectedFromServer, new object[] { this, e });
                else
                    DisconnectedFromServer(this, e);
            }
        }

        public event ConnectingSuccessedEventHandler ConnectingSuccessed;
        protected virtual void OnConnectingSuccessed(EventArgs e) {
            if(ConnectingSuccessed != null) {
                Control target = ConnectingSuccessed.Target as Control;
                if(target != null && target.InvokeRequired)
                    target.Invoke(ConnectingSuccessed, new object[] { this, e });
                else
                    ConnectingSuccessed(this, e);
            }
        }

        public event ConnectingFailedEventHandler ConnectingFailed;
        protected virtual void OnConnectingFailed(EventArgs e) {
            if(ConnectingFailed != null) {
                Control target = ConnectingFailed.Target as Control;
                if(target != null && target.InvokeRequired)
                    target.Invoke(ConnectingFailed, new object[] { this, e });
                else
                    ConnectingFailed(this, e);
            }
        }

        public event NetworkDeadEventHandler NetworkDead;
        protected virtual void OnNetworkDead(EventArgs e) {
            if(NetworkDead != null) {
                Control target = NetworkDead.Target as Control;
                if(target != null && target.InvokeRequired)
                    target.Invoke(NetworkDead, new object[] { this, e });
                else
                    NetworkDead(this, e);
            }
        }

        public event NetworkAlivedEventHandler NetworkAlived;
        protected virtual void OnNetworkAlived(EventArgs e) {
            if(NetworkAlived != null) {
                Control target = NetworkAlived.Target as Control;
                if(target != null && target.InvokeRequired)
                    target.Invoke(NetworkAlived, new object[] { this, e });
                else
                    NetworkAlived(this, e);
            }
        }
        #endregion
    }
}
