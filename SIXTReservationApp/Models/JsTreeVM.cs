using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIXTReservationApp.Models
{
    public class JsTreeVM
    {
        public string Id { get; set; }

        public string Parent { get; set; }

        public string Text { get; set; }

        public string Icon { get; set; }
        public int Order { get; set; }
        public bool CanBeLanding { get; set; }

        public TreeNodeStatus State { get; set; }
    }

    public class TreeNodeStatus
    {
        public bool Opened { get; set; }

        public bool Selected { get; set; }

        public bool Disabled { get; set; }
    }
}
