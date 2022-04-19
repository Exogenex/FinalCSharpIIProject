using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Final_CSharp_II_Project
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Declare a MeasureLengthDevice and initialize with default unit (Imperial).
        MeasureLengthDevice measureLengthDevice = new MeasureLengthDevice(Units.Imperial);
        Timer timer;

        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Runs after StartCollecting button has been clicked, starts a timer, and starts collecting data in measureLengthDevice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartCollectingClick(object sender, RoutedEventArgs e)
        {
            // Start collecting data in measureLengthDevice
            measureLengthDevice.StartCollecting();
            // Timer to display items
            timer = new Timer(TimerTick, null, 15000, 15000);
            StartCollectingButton.IsEnabled = false;
            StopCollectingButton.IsEnabled = true;
        }

        /// <summary>
        /// Runs after StopCollecting button has been clicked, stops the timer, and stops collecting data in measureLengthDevice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopCollectingClick(object sender, RoutedEventArgs e)
        {
            if (timer != null) { timer.Dispose(); }
            StartCollectingButton.IsEnabled = true;
            StopCollectingButton.IsEnabled = false;
        }

        /// <summary>
        /// Runs for every tick of the timer.
        /// </summary>
        /// <param name="StateInfo"></param>
        private void TimerTick(Object StateInfo)
        {
            // Get values
            decimal imperialValue = measureLengthDevice.ImperialValue();
            decimal metricValue = measureLengthDevice.MectricValue();
            // Variable to hold processed data.
            string processedData = "";
            // Get data
            int[] rawData = measureLengthDevice.GetRawData();
            // Write data to processedData
            foreach (int num in rawData)
            {
                processedData += $"{num}\n";
            }
            // Display data
            DisplayData(processedData, imperialValue, metricValue);
        }

        /// <summary>
        /// This method is supposed to run the display code in the UI thread but is not working for unknown reasons.
        /// </summary>
        /// <param name="processedData"></param>
        /// <param name="imperialValue"></param>
        /// <param name="metricValue"></param>
        private void DisplayData(string processedData, decimal imperialValue, decimal metricValue)
        {
            Thread displayDataThread = new Thread(new ThreadStart((Action)delegate () {
                // Display processedData
                RandNumOutputBox.Text = processedData;

                // Display random number data
                if (measureLengthDevice.GetUnitsToUse() == Units.Imperial)
                {
                    DataOutputBox.Text = $"{imperialValue}In.\n{DateTime.Now}";
                }
                if (measureLengthDevice.GetUnitsToUse() == Units.Metric)
                {
                    DataOutputBox.Text = $"{metricValue}In.\n{DateTime.Now}";
                }
            }));
        }

        /// <summary>
        /// Checks if InchesRadioButton is cecked and if it is updates data in measureLengthDevice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InchesChecked(object sender, RoutedEventArgs e) { measureLengthDevice.SetUnitsToUse(Units.Imperial); }

        /// <summary>
        /// Checks if Centimeters adioButton is cecked and if it is updates data in measureLengthDevice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CentimetersChecked(object sender, RoutedEventArgs e) { measureLengthDevice.SetUnitsToUse(Units.Metric);
        }
    }
}
