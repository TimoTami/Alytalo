using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÄlyTalo2
{
    public class Thermostat
    {
        public int Temperature { get; set; }

        public void SetTemperature(string GoalTemperature)
        {
            try
            {
                Temperature = int.Parse(GoalTemperature);

            }
            catch (Exception)
            {
                
            }
            
        }

    }
}