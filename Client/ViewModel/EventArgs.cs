using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Client.Commands;

namespace Client.ViewModel {
    public delegate void CommandReceivedEventHandler(object sender, CommandEventArgs e);
    public delegate void CommandSentEventHandler(object sender, EventArgs e);
    public delegate void CommandSendingFailedEventHandler(object sender, EventArgs e);

    public delegate void ServerDisconnectedEventHandler(object sender, ServerEventArgs e);
    public delegate void DisconnectedEventHandler(object sender, EventArgs e);

    public delegate void ConnectingSuccessedEventHandler(object sender, EventArgs e);
    public delegate void ConnectingFailedEventHandler(object sender, EventArgs e);

    public delegate void NetworkDeadEventHandler(object sender, EventArgs e);
    public delegate void NetworkAlivedEventHandler(object sender, EventArgs e);

    public class CommandEventArgs : EventArgs {
        private command _cmd;
        public command cmd {
            get {
                return _cmd;
            }
        }

        private auth _auth;
        public auth auth {
            get {
                return _auth;
            }
        }

        public CommandEventArgs(auth a) {
            this._auth = a;
            this._cmd = a;
        }
    }

    public class ServerEventArgs : EventArgs {
        private Socket socket;
        public IPAddress IP {
            get { return ((IPEndPoint)this.socket.RemoteEndPoint).Address; }
        }
        public int Port {
            get { return ((IPEndPoint)this.socket.RemoteEndPoint).Port; }
        }

        public ServerEventArgs(Socket clientSocket) {
            this.socket = clientSocket;
        }
    }
}
