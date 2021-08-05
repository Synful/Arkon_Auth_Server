using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands {
    public class auth : command {
        public string lic { get; set; }

        public auth(string lic) : base(cmdType.Auth) {
            this.lic = lic;
        }
    }
}
