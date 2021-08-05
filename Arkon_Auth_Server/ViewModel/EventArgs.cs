using Server.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.ViewModel {
    public delegate void CommandReceivedEventHandler(object sender, CommandEventArgs e);
    public delegate void CommandSentEventHandler(object sender, EventArgs e);
    public delegate void CommandSendingFailedEventHandler(object sender, EventArgs e);
    public delegate void DisconnectedEventHandler(object sender, ClientEventArgs e);

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

        private ping _ping;
        public ping ping {
            get {
                return _ping;
            }
        }

        public CommandEventArgs(auth a) {
            this._auth = a;
            this._cmd = a;
        }
        public CommandEventArgs(ping p) {
            this._ping = p;
            this._cmd = p;
        }
    }

    public class ClientEventArgs : EventArgs {
        private Socket socket;
        public IPAddress IP {
            get { return ((IPEndPoint)this.socket.RemoteEndPoint).Address; }
        }
        public int Port {
            get { return ((IPEndPoint)this.socket.RemoteEndPoint).Port; }
        }
        public ClientEventArgs(Socket clientSocket) {
            this.socket = clientSocket;
        }
    }
}
