using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands {
    public class auth : command {
        public string lic { get; set; }

        public auth(IPAddress ip, string lic) : base(cmdType.Auth, ip) {
            this.lic = lic;
        }
    }
}
