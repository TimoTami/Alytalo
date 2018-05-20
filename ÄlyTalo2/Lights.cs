using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÄlyTalo2
{
    public class Lights
    {
        public bool Switched { get; set; }
        public string Dimmer { get; set; }

        public void LightsOn()
        {
            Switched = true;
        }
        public void LightsOff()
        {
            Switched = false;
        }

    }
}
