using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shamrock
{
    public class Player
    {
        public String name {get; set;}
        public double initialHcp { get; set; }
        public double playingHcp;
        public int startNumber { get; set; }
        public override string ToString() => name;
    }
}
