using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Xpedia.ASV.Common.BusinessObjects;
using Xpedia.ASV.Wpf.Interfaces;

namespace Haldan.Wpf.Helper
{
   public class DeviceHelper : IDeviceHelper
    {
        private HidLibrary.HidDevice currentDevice;

        #region ASCII Command	Description
        private const string RESETMACHINE = "e";
        public bool IsConnected { get; set; }
        #endregion

        public void SendDeviceCommand(List<string> commands, VisionTestType visionTestType)
        {

            if (this.currentDevice == null || !this.currentDevice.IsConnected || !this.currentDevice.IsOpen)
            {
                this.ConnectToDevice();
                if (this.currentDevice == null)
                {
                    return;
                }
            }

            foreach (string command in commands)
            {
                if (visionTestType != VisionTestType.Default || (visionTestType == VisionTestType.Default && command == DeviceHelper.RESETMACHINE))
                {
                    byte[] commnds = new byte[] { ASCIIEncoding.ASCII.GetBytes(command).First(), ASCIIEncoding.ASCII.GetBytes(command).First(), default(int) };
                    this.currentDevice.Write(commnds);
                }
            }
        }

        public void ConnectToDevice()
        {
            int[] ids = new int[] { 4761 };

            this.currentDevice = HidLibrary.HidDevices.Enumerate(2341, ids).ToList().FirstOrDefault();

            if (this.currentDevice == null)
            {
                this.IsConnected = false;
                MessageBox.Show("Selected Device could not be found. Please check if the device is On and  plugged in", "Device not found", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            this.currentDevice.OpenDevice();
            this.IsConnected = true;
            var commands = new List<string> { DeviceHelper.RESETMACHINE };
            this.SendDeviceCommand(commands, VisionTestType.Default);
        }

        public void CloseDeviceConnection()
        {
            //this.SendDeviceCommand(DeviceHelper.RESETMACHINE, VisionTestType.Default);
            if (this.IsConnected)
            {
                this.currentDevice.CloseDevice();
            }
        }
    }
}
