using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Final_CSharp_II_Project
{
    class MeasureLengthDevice : Device, IMeasuringDevice
    {
        Queue<int> dataQueue = new Queue<int>();
        Timer timer;

        /// <summary>
        /// Initializes fields and timer event.
        /// </summary>
        /// <param name="unitType">The starting unit type to be used.</param>
        public MeasureLengthDevice(Units unitType)
        {
            this.unitsToUse = unitType;
            this.dataCaptured = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            this.mostRecentMeasure = 0;
            // Add TimerDoWork method to TimerEvent
            this.TickEvent += TimerTick;
        }

        /// <summary>
        /// This method will return a decimal that represents the metric value of the most recent measurement that was captured.
        /// </summary>
        public decimal MectricValue() { return (decimal)this.mostRecentMeasure; }

        /// <summary>
        /// This method will return a decimal that represents the imperial value of the most recent measurement that was captured.
        /// </summary>
        public decimal ImperialValue() { return (decimal)(this.mostRecentMeasure * 2.54); }
        // 2.54 is the number used to convert inches to centimeters

        /// <summary>
        /// This method will start the device running. It will begin collecting measurements and record them.
        /// </summary>
        public void StartCollecting() { timer = new Timer(Timer15Sec, null, 0, 15000); }
        // First prameter in Timer() is the method that will be called, second is a parameter passed to timer method (none here),
        // third is time in miliseconds before first call, fourth is time inbetween calls.

        // Timer delegate and event
        public delegate void TimerDelegate();
        public event TimerDelegate TickEvent;

        /// <summary>
        /// Intermideiate for timer event.
        /// </summary>
        /// <param name="StateInfo">Information to be passed to the method (not used).</param>
        private void Timer15Sec(Object StateInfo)
        {
            this.TickEvent(); // Calls TimerDoWork
        }

        /// <summary>
        /// Runs every 15 seconds while timer in StartCollecting is active.
        /// </summary>
        private void TimerTick()
        {
            // Create device object
            Device device = new Device();
            // Set mostRecentMeasure with a new value from Device.GetMeasurement()
            mostRecentMeasure = device.GetMeasurement();
            // Add that value to the dataCaptured history array
            AddDataToDataCaptured(mostRecentMeasure);
        }

        /// <summary>
        /// This method will stop the device. It will cease collecting measurements.
        /// </summary>
        public void StopCollecting()
        {
            if (timer != null) { timer.Dispose(); }
        }

        /// <summary>
        /// This method will retrieve a copy of all of the recent data that the measuring device has captured. The data will be returned as an array of integer values.
        /// </summary>
        public int[] GetRawData() { return dataCaptured; }

        /// <summary>
        /// Units - This field will determine whether the generated measurements are interpreted in metric (e.g. centimeters) or imperial (e.g. inches) units. Its value will be determined from user input.
        /// </summary>
        private Units unitsToUse;

        /// <summary>
        /// This method takes one Units parameter and sets the field unitsToUse accordingly.
        /// </summary>
        public void SetUnitsToUse(Units unit) { this.unitsToUse = unit; }

        /// <summary>
        /// Returns the state of unitsToUse.
        /// </summary>
        /// <returns></returns>
        public Units GetUnitsToUse() { return this.unitsToUse; }

        /// <summary>
        /// This method uses a queue to add data to dataCaptured.
        /// </summary>
        /// <param name="dataToAdd"></param>
        private void AddDataToDataCaptured(int dataToAdd)
        {
            if (dataQueue.Count > 10)
            {
                dataQueue.Enqueue(dataToAdd);
            }
            if (dataQueue.Count == 10)
            {
                dataQueue.Dequeue();
                dataQueue.Enqueue(dataToAdd);
            }
            dataQueue.CopyTo(dataCaptured, 0);
        }

        /// <summary>
        /// integer array - This field will store a history of a limited set of recently captured measurements. Once the array is full, the class should start overwriting the oldest elements while continuing to record the newest captures. (You may need some helper fields/variables to go with this one).
        /// </summary>
        private int[] dataCaptured;

        /// <summary>
        /// integer - This field will store the most recent measurement captured for convenience of display.
        /// </summary>
        private int mostRecentMeasure;
    }
}
