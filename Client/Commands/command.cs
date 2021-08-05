using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands {
    public class command {
        public cmdType type { get; set; }

        public enum cmdType {
            Auth = 1
        }

        public command(cmdType type) {
            this.type = type;
        }
    }
}
