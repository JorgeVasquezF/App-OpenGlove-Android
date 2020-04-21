using System;
using System.Collections.Generic;

namespace CSharpTest.OpenGloveAPI_C_Sharp_HL
{
    public class OpenGlove
    {
        public string Name { get; set; }
        public string BluetoothDeviceName { get; set; }
        public string ConfigurationName { get; set; }
        public string WebsocketEndpointUrl { get { return Communication.WebSocket.Url.ToString(); } }
        public Communication Communication { get; set; }
        private bool _IsConnectedToWebSocketServer { get { return IsWebSocketConnected(); } set { _IsConnectedToWebSocketServer = value; } }
        private bool _IsConnectedToBluetoothDevice { get; set; }
        public bool IsConnectedToWebSocketServer { get { return _IsConnectedToWebSocketServer; } }
        public bool IsConnectedToBluetoohDevice { get { return _IsConnectedToBluetoothDevice; } }


        public OpenGlove(string name, string bluetoothDeviceName, string configurationName, string webSocketEndPointUrl)
        {
            this.Name = name;
            this.BluetoothDeviceName = bluetoothDeviceName;
            this.ConfigurationName = configurationName;
            this.Communication = new Communication(bluetoothDeviceName, configurationName, webSocketEndPointUrl);
            this.Communication.OnBluetoothDeviceConnectionStateChanged += OnBluetoothDeviceConnectionStateChanged;
        }

        private void OnBluetoothDeviceConnectionStateChanged(bool isConnected)
        {
            this._IsConnectedToBluetoothDevice = isConnected;
        }

        //Modify for different validation of IsConnected to WebSocket server in other languages of programing and libraries
        private bool IsWebSocketConnected()
        {
            if (this.Communication.WebSocket.ReadyState == WebSocketSharp.WebSocketState.Open)
                return true;
            else
                return false;
        }

        public void Start()
        {
            this.Communication.StartOpenGlove(BluetoothDeviceName);
        }

        public void Stop()
        {
            this.Communication.StopOpenGlove(BluetoothDeviceName);
        }

        public void AddOpenGloveDeviceToServer()
        {
            this.Communication.AddOpenGloveDeviceToServer(BluetoothDeviceName, ConfigurationName);
        }

        public void RemoveOpenGloveDeviceFromServer()
        {
            this.Communication.RemoveOpenGloveDeviceFromServer(BluetoothDeviceName);
        }

        public void SaveOpenGloveConfiguration()
        {
            this.Communication.SaveOpenGloveConfiguration(BluetoothDeviceName, ConfigurationName);
        }

        public void ConnectToBluetoothDevice()
        {
            this.Communication.ConnectToBluetoothDevice(BluetoothDeviceName);
        }

        public void DisconnectFromBluetoothDevice()
        {
            this.Communication.DisconnectFromBluetoothDevice(BluetoothDeviceName);
        }

        public void StartCaptureDataFromServer()
        {
            this.Communication.StartCaptureDataFromServer(BluetoothDeviceName);
        }

        public void StopCaptureDataFromServer()
        {
            this.Communication.StopCaptureDataFromServer(BluetoothDeviceName);
        }

        public void AddActuator(int region, int positivePin, int negativePin)
        {
            this.Communication.AddActuator(BluetoothDeviceName ,region, positivePin, negativePin);
        }

        public void AddActuators(List<int> regions, List<int> positivePins, List<int> negativePins)
        {
            this.Communication.AddActuators(BluetoothDeviceName, regions, positivePins, negativePins);
        }

        public void RemoveActuator(int region)
        {
            this.Communication.RemoveActuator(BluetoothDeviceName, region);
        }

        public void RemoveActuators(List<int> regions)
        {
            this.Communication.RemoveActuators(BluetoothDeviceName, regions);
        }

        public void ActivateActuators(List<int> regions, List<string> intensities)
        {
            this.Communication.ActivateActuators(BluetoothDeviceName, regions, intensities);
        }

        public void ActivateActuatorsTimeTest(List<int> regions, List<string> intensities)
        {
            this.Communication.ActivateActuatorsTimeTest(BluetoothDeviceName, regions, intensities);
        }

        public void TurnOnActuators()
        {
            this.Communication.TurnOnActuators(BluetoothDeviceName);
        }

        public void TurnOffActuators()
        {
            this.Communication.TurnOffActuators(BluetoothDeviceName);
        }

        public void TurnOnFlexors()
        {
            this.Communication.TurnOnFlexors(BluetoothDeviceName);
        }

        public void TurnOffFlexors()
        {
            this.Communication.TurnOffFlexors(BluetoothDeviceName);
        }

        public void ResetActuators()
        {
            this.Communication.ResetActuators(BluetoothDeviceName);
        }

        public void AddFlexor(int region, int pin)
        {
            this.Communication.AddFlexor(BluetoothDeviceName, region, pin);
        }

        public void AddFlexors(List<int> regions, List<int> pins)
        {
            this.Communication.AddFlexors(BluetoothDeviceName, regions, pins);
        }

        public void RemoveFlexor(int region)
        {
            this.Communication.RemoveFlexor(BluetoothDeviceName, region);
        }

        public void RemoveFlexors(List<int> regions)
        {
            this.Communication.RemoveFlexors(BluetoothDeviceName, regions);
        }

        public void CalibrateFlexors()
        {
            this.Communication.CalibrateFlexors(BluetoothDeviceName);
        }

        public void ConfirmCalibration()
        {
            this.Communication.ConfirmCalibration(BluetoothDeviceName);
        }

        public void SetThreshold(int value)
        {
            this.Communication.SetThreshold(BluetoothDeviceName, value);
        }

        public void ResetFlexors()
        {
            this.Communication.ResetFlexors(BluetoothDeviceName);
        }

        public void StartIMU()
        {
            this.Communication.StartIMU(BluetoothDeviceName);
        }

        public void SetIMUStatus(bool status)
        {
            this.Communication.SetIMUStatus(BluetoothDeviceName, status);
        }

        public void SetRawData(bool status)
        {
            this.Communication.SetRawData(BluetoothDeviceName, status);
        }


        /* integer command      inside arduino code        IMU component
         * 0                 :          a            :      Accelerometer
         * 1                 :          g            :      Gyroscope
         * 2                 :          m            :      Magnetometer
         * 3                 :          r            :      Attitude
         * default (other)   :          z            :      Accelerometer, Gyroscope and Magnetometer
        */

        public void SetIMUChoosingData(int value)
        {
            this.Communication.SetIMUChoosingData(BluetoothDeviceName, value);
        }

        public void ReadOnlyAccelerometerFromIMU()
        {
            this.Communication.ReadOnlyAccelerometerFromIMU(BluetoothDeviceName);
        }

        public void ReadOnlyGyroscopeFromIMU()
        {
            this.Communication.ReadOnlyGyroscopeFromIMU(BluetoothDeviceName);
        }

        public void ReadOnlyMagnetometerFromIMU()
        {
            this.Communication.ReadOnlyMagnetometerFromIMU(BluetoothDeviceName);
        }

        public void ReadOnlyAttitudeFromIMU()
        {
            this.Communication.ReadOnlyAttitudeFromIMU(BluetoothDeviceName);
        }

        public void ReadAllDataFromIMU()
        {
            this.Communication.ReadAllDataFromIMU(BluetoothDeviceName);
        }

        public void CalibrateIMU()
        {
            throw new NotImplementedException();
            // Need Implement this on OpenGlove Aplication, see SwitchOpenGloveServer in OpenGloveServer class
            // Communication and MessageGenerator methods is OK
            //this.Communication.CalibrateIMU();
        }

        public void TurnOnIMU()
        {
            this.Communication.TurnOnIMU(BluetoothDeviceName);
        }

        public void TurnOffIMU()
        {
            this.Communication.TurnOffIMU(BluetoothDeviceName);
        }

        public void SetLoopDelay(int value)
        {
            this.Communication.SetLoopDelay(BluetoothDeviceName, value);
        }

        public void ConnectToWebSocketServer()
        {
            this.Communication.ConnectToWebSocketServer();
        }

        public void DisconnectFromWebSocketServer()
        {
            this.Communication.DisconnectFromWebSocketServer();
        }

        public void GetOpenGloveArduinoVersionSoftware()
        {
            this.Communication.GetOpenGloveArduinoVersionSoftware(BluetoothDeviceName);
        }

        //TODO Future work
        //public List<string> GetAllPairedDevices();

        //public List<string> GetOpenGloveDevicesOnServer();
    }

    /*
     * Create your custom Regions for other types of Bluetooth wereables 
     * how helmets, general joints capture and actuators zones.
     * 
     * Palmar, Dorsal and Flexors Regions is for hands Mappings.
     * Enjoy!
    */

    public enum PalmarRegion
    {
        FingerSmallDistal,
        FingerRingDistal,
        FingerMiddleDistal,
        FingerIndexDistal,

        FingerSmallMiddle,
        FingerRingMiddle,
        FingerMiddleMiddle,
        FingerIndexMiddle,

        FingerSmallProximal,
        FingerRingProximal,
        FingerMiddleProximal,
        FingerIndexProximal,

        PalmSmallDistal,
        PalmRingDistal,
        PalmMiddleDistal,
        PalmIndexDistal,

        PalmSmallProximal,
        PalmRingProximal,
        PalmMiddleProximal,
        PalmIndexProximal,

        HypoThenarSmall,
        HypoThenarRing,
        ThenarMiddle,
        ThenarIndex,

        FingerThumbProximal,
        FingerThumbDistal,

        HypoThenarDistal,
        Thenar,

        HypoThenarProximal
    }

    public enum DorsalRegion
    {
        FingerSmallDistal = 29,
        FingerRingDistal,
        FingerMiddleDistal,
        FingerIndexDistal,

        FingerSmallMiddle,
        FingerRingMiddle,
        FingerMiddleMiddle,
        FingerIndexMiddle,

        FingerSmallProximal,
        FingerRingProximal,
        FingerMiddleProximal,
        FingerIndexProximal,

        PalmSmallDistal,
        PalmRingDistal,
        PalmMiddleDistal,
        PalmIndexDistal,

        PalmSmallProximal,
        PalmRingProximal,
        PalmMiddleProximal,
        PalmIndexProximal,

        HypoThenarSmall,
        HypoThenarRing,
        ThenarMiddle,
        ThenarIndex,

        FingerThumbProximal,
        FingerThumbDistal,

        HypoThenarDistal,
        Thenar,

        HypoThenarProximal
    }

    public enum FlexorsRegion
    {
        ThumbInterphalangealJoint = 0,
        IndexInterphalangealJoint,
        MiddleInterphalangealJoint,
        RingInterphalangealJoint,
        SmallInterphalangealJoint,

        ThumbMetacarpophalangealJoint,
        IndexMetacarpophalangealJoint,
        MiddleMetacarpophalangealJoint,
        RingMetacarpophalangealJoint,
        SmallMetacarpophalangealJoint
    }
}
