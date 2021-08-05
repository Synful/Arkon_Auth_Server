using Server.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Server.Commands.command;

namespace Server.ViewModel {
    public class Client {
        #region Vars
        public string Username { get; set; }
        public string License { get; set; }

        public IPAddress IP {
            get {
                if(this.socket != null)
                    return ((IPEndPoint)this.socket.RemoteEndPoint).Address;
                else
                    return IPAddress.None;
            }
            set { }
        }
        public int Port {
            get {
                if(this.socket != null)
                    return ((IPEndPoint)this.socket.RemoteEndPoint).Port;
                else
                    return -1;
            }
            set { }
        }

        private Socket socket;
        private NetworkStream stream;
        private BackgroundWorker recv_bw;
        private BackgroundWorker send_bw;
        private Semaphore sema = new Semaphore(1, 1);
        #endregion

        #region Constructor
        public Client(Socket soc) {
            this.socket = soc;

            this.stream = new NetworkStream(this.socket);

            this.recv_bw = new BackgroundWorker();
            this.recv_bw.DoWork += new DoWorkEventHandler(Recv);
            this.recv_bw.RunWorkerAsync();

            this.send_bw = new BackgroundWorker();
            this.send_bw.DoWork += new DoWorkEventHandler(Send);
            this.send_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Send_Completed);
        }
        #endregion

        #region Private Methods
        private void Recv(object sender, DoWorkEventArgs e) {
            while(this.socket.Connected) {
                try {
                    //Read the command's Type.
                    byte[] buffer = new byte[1];
                    int readBytes = stream.Read(buffer, 0, 1);
                    if(readBytes == 0)
                        break;
                    cmdType cType = (cmdType)buffer[0];

                    switch(cType) {
                        case cmdType.Auth:
                            buffer = new byte[32];
                            readBytes = stream.Read(buffer, 0, 32);
                            if(readBytes == 0)
                                break;
                            string lic = Encoding.ASCII.GetString(buffer);

                            bool validLic = Database.instance.ValidKey(lic);
                            if(validLic) {
                                this.Username = Database.instance.GetUsername(lic);
                                this.License = lic;
                            }

                            this.OnCommandReceived(new CommandEventArgs(new auth(IP, lic)));
                            break;
                        case cmdType.Ping:
                            break;
                    }

                } catch(Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }
            this.OnDisconnected(new ClientEventArgs(this.socket));
            this.Disconnect();
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
                        break;
                    case cmdType.Ping:
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
                this.OnCommandSent(new EventArgs());
            else
                this.OnCommandFailed(new EventArgs());

            ((BackgroundWorker)sender).Dispose();
            GC.Collect();
        }
        #endregion

        #region Public Methods
        public void Send_Command(object o) {
            if(this.socket != null && this.socket.Connected) {
                send_bw.RunWorkerAsync(o);
            } else {
                this.OnCommandFailed(new EventArgs());
            }
        }
        public bool Disconnect() {
            if(this.socket != null && this.socket.Connected) {
                try {
                    this.socket.Shutdown(SocketShutdown.Both);
                    this.socket.Close();
                    return true;
                } catch {
                    return false;
                }
            } else {
                return true;
            }
        }
        #endregion

        #region Events
        public event CommandReceivedEventHandler CommandReceived;
        protected virtual void OnCommandReceived(CommandEventArgs e) {
            if(CommandReceived != null)
                CommandReceived(this, e);
        }

        public event CommandSentEventHandler CommandSent;
        protected virtual void OnCommandSent(EventArgs e) {
            if(CommandSent != null)
                CommandSent(this, e);
        }

        public event CommandSendingFailedEventHandler CommandFailed;
        protected virtual void OnCommandFailed(EventArgs e) {
            if(CommandFailed != null)
                CommandFailed(this, e);
        }

        public event DisconnectedEventHandler Disconnected;
        protected virtual void OnDisconnected(ClientEventArgs e) {
            if(Disconnected != null)
                Disconnected(this, e);
        }
        #endregion
    }
}
