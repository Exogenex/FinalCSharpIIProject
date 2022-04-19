using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_CSharp_II_Project
{
    public class Device
    {
        /// <summary>
        /// This method will return a random integer between 1 and 10 as a measurement of some imaginary object.
        /// </summary>
        public int GetMeasurement()
        {
            // Create random object.
            Random random = new Random();
            // Return rantom number between 1 and 10 (inclusive).
            return random.Next(1,11);
        }
    }
}
