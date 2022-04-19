using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_CSharp_II_Project
{
    interface IMeasuringDevice
    {
        /// <summary>
        /// This method will return a decimal that represents the metric value of the most recent measurement that was captured.
        /// </summary>
        decimal MectricValue();

        /// <summary>
        /// This method will return a decimal that represents the imperial value of the most recent measurement that was captured.
        /// </summary>
        decimal ImperialValue();

        /// <summary>
        /// This method will start the device running. It will begin collecting measurements and record them.
        /// </summary>
        void StartCollecting();

        /// <summary>
        /// This method will stop the device. It will cease collecting measurements.
        /// </summary>
        void StopCollecting();

        /// <summary>
        /// This method will retrieve a copy of all of the recent data that the measuring device has captured. The data will be returned as an array of integer values.
        /// </summary>
        int[] GetRawData();
    }
}
