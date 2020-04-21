using System;
using System.Collections.Generic;

namespace CSharpTest.OpenGloveAPI_C_Sharp_HL
{
    public class MessageGenerator
    {
        public enum OpenGloveActions
        {
            StartOpenGlove = 0,
            StopOpenGlove,
            AddOpenGloveDeviceToServer,
            RemoveOpenGloveDeviceFromServer,
            SaveOpenGloveConfiguration,
            ConnectToBluetoothDevice,
            DisconnectFromBluetoothDevice,
            ConnectToWebSocketServer,
            DisconnecFromWebSocketServer,
            StartCaptureDataFromServer,
            StopCaptureDataFromServer,

            AddActuator = 11,
            AddActuators,
            RemoveActuator,
            RemoveActuators,
            ActivateActuators,
            ActivateActuatorsTimeTest,
            TurnOnActuators,
            TurnOffActuators,
            ResetActuators,

            AddFlexor = 20,
            AddFlexors,
            RemoveFlexor,
            RemoveFlexors,
            CalibrateFlexors,
            ConfirmCalibration,
            SetThreshold,
            TurnOnFlexors,
            TurnOffFlexors,
            ResetFlexors,

            StartIMU = 30,
            SetIMUStatus,
            SetRawData,
            SetIMUChoosingData,
            ReadOnlyAccelerometerFromIMU,
            ReadOnlyGyroscopeFromIMU,
            ReadOnlyMagnetometerFromIMU,
            ReadOnlyAttitudeFromIMU,
            ReadAllDataFromIMU,
            CalibrateIMU,
            TurnOnIMU,
            TurnOffIMU,

            SetLoopDelay = 42,
            GetOpenGloveArduinoSoftwareVersion,
        }
        public string MainSeparator { get; set; }
        public string SecondarySeparator { get; set; }
        public string Empty { get; set; }

        public MessageGenerator(string mainSeparator, string secondarySeparator, string empty)
        {
            this.MainSeparator = mainSeparator;
            this.SecondarySeparator = secondarySeparator;
            this.Empty = empty;
        }

        /* For implement this Join to other specific languages (C# String.Join using params object[] and Java String.Join using Object ...)
         * 
         * C#: String.Join https://docs.microsoft.com/en-us/dotnet/api/system.string.join?view=netframework-4.7.2#System_String_Join_System_String_System_Object___
         * You can test your C# code or other in:
         *  https://www.tutorialspoint.com/codingground.htm
         *  https://www.tutorialspoint.com/compile_csharp_online.php
        */
        private string Join(string separator, params object[] list)
        {
            return String.Join(separator, list);
        }

        private string JoinList(string separator, List<string> list)
        {
            return String.Join(separator, list);
        }

        private string JoinList(string separator, List<int> list)
        {
            return String.Join(separator, list);
        }

        private string BooleanString(bool value)
        {
            string booleanString = (value) ? "True" : "False";
            return booleanString;
        }

        public string StartOpenGlove(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.StartOpenGlove, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string StopOpenGlove(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.StopOpenGlove, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string AddOpenGloveDeviceToServer(string bluetoothDeviceName, string configurationName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.AddOpenGloveDeviceToServer, bluetoothDeviceName, Empty, configurationName, Empty);
        }

        public string RemoveOpenGloveDeviceFromServer(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.RemoveOpenGloveDeviceFromServer, bluetoothDeviceName, Empty, Empty, Empty);
        }
        public string SaveOpenGloveConfiguration(string bluetoothDeviceName, string configurationName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.SaveOpenGloveConfiguration, bluetoothDeviceName, Empty, configurationName, Empty);
        }

        public string ConnectToBluetoothDevice(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.ConnectToBluetoothDevice, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string DisconnectFromBluetoothDevice(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.DisconnectFromBluetoothDevice, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string StartCaptureDataFromServer(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.StartCaptureDataFromServer, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string StopCaptureDataFromServer(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.StopCaptureDataFromServer, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string AddActuator(string bluetoothDeviceName, int region, int positivePin, int negativePin)
        {
            return Join(MainSeparator, (int)OpenGloveActions.AddActuator, bluetoothDeviceName, region, positivePin, negativePin);
        }

        public string AddActuators(string bluetoothDeviceName, List<int> regions, List<int> positivePins, List<int> negativePins)
        {
            return Join(MainSeparator, (int)OpenGloveActions.AddActuators, bluetoothDeviceName, JoinList(SecondarySeparator, regions), JoinList(SecondarySeparator, positivePins), JoinList(SecondarySeparator, negativePins));
        }

        public string RemoveActuator(string bluetoothDeviceName, int region)
        {
            return Join(MainSeparator, (int)OpenGloveActions.RemoveActuator, bluetoothDeviceName, region, Empty, Empty);
        }

        public string RemoveActuators(string bluetoothDeviceName, List<int> regions)
        {
            return Join(MainSeparator, (int)OpenGloveActions.RemoveActuators, bluetoothDeviceName, JoinList(SecondarySeparator, regions), Empty, Empty);
        }

        public string ActivateActuators(string bluetoothDeviceName, List<int> regions, List<string> intensities)
        {
            return Join(MainSeparator, (int)OpenGloveActions.ActivateActuators, bluetoothDeviceName, JoinList(SecondarySeparator, regions), JoinList(SecondarySeparator, intensities), Empty);
        }

        public string ActivateActuatorsTimeTest(string bluetoothDeviceName, List<int> regions, List<string> intensities)
        {
            return Join(MainSeparator, (int)OpenGloveActions.ActivateActuatorsTimeTest, bluetoothDeviceName, JoinList(SecondarySeparator, regions), JoinList(SecondarySeparator, intensities), Empty);
        }

        public string TurnOnActuators(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.TurnOnActuators, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string TurnOffActuators(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.TurnOffActuators, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string ResetActuators(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.ResetActuators, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string AddFlexor(string bluetoothDeviceName, int region, int pin)
        {
            return Join(MainSeparator, (int)OpenGloveActions.AddFlexor, bluetoothDeviceName, region, pin, Empty);
        }

        public string AddFlexors(string bluetoothDeviceName, List<int> regions, List<int> pins)
        {
            return Join(MainSeparator, (int)OpenGloveActions.AddFlexors, bluetoothDeviceName, JoinList(SecondarySeparator, regions), JoinList(SecondarySeparator, pins), Empty);
        }

        public string RemoveFlexor(string bluetoothDeviceName, int region)
        {
            return Join(MainSeparator, (int)OpenGloveActions.RemoveFlexor, bluetoothDeviceName, region, Empty, Empty);
        }

        public string RemoveFlexors(string bluetoothDeviceName, List<int> regions)
        {
            return Join(MainSeparator, (int)OpenGloveActions.RemoveFlexors, bluetoothDeviceName, JoinList(SecondarySeparator, regions), Empty, Empty);
        }

        public string CalibrateFlexors(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.CalibrateFlexors, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string ConfirmCalibration(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.ConfirmCalibration, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string SetThreshold(string bluetoothDeviceName, int value)
        {
            return Join(MainSeparator, (int)OpenGloveActions.SetThreshold, bluetoothDeviceName, Empty, value, Empty);
        }

        public string TurnOnFlexors(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.TurnOnFlexors, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string TurnOffFlexors(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.TurnOffFlexors, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string ResetFlexors(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.ResetFlexors, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string StartIMU(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.StartIMU, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string SetIMUStatus(string bluetoothDeviceName, bool status)
        {
            return Join(MainSeparator, (int)OpenGloveActions.SetIMUStatus, bluetoothDeviceName, Empty, BooleanString(status), Empty);
        }

        public string SetRawData(string bluetoothDeviceName, bool status)
        {
            return Join(MainSeparator, (int)OpenGloveActions.SetRawData, bluetoothDeviceName, Empty, BooleanString(status), Empty);
        }

        public string SetIMUChoosingData(string bluetoothDeviceName, int value)
        {
            return Join(MainSeparator, (int)OpenGloveActions.SetIMUChoosingData, bluetoothDeviceName, Empty, value, Empty);
        }

        public string ReadOnlyAccelerometerFromIMU(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.ReadOnlyAccelerometerFromIMU, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string ReadOnlyGyroscopeFromIMU(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.ReadOnlyGyroscopeFromIMU, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string ReadOnlyMagnetometerFromIMU(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.ReadOnlyMagnetometerFromIMU, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string ReadOnlyAttitudeFromIMU(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.ReadOnlyAttitudeFromIMU, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string ReadAllDataFromIMU(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.ReadAllDataFromIMU, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string CalibrateIMU(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.CalibrateIMU, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string TurnOnIMU(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.TurnOnIMU, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string TurnOffIMU(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.TurnOffIMU, bluetoothDeviceName, Empty, Empty, Empty);
        }

        public string SetLoopDelay(string bluetoothDeviceName, int value)
        {
            return Join(MainSeparator, (int)OpenGloveActions.SetLoopDelay, bluetoothDeviceName, Empty, value, Empty);
        }

        public string GetOpenGloveArduinoVersionSoftware(string bluetoothDeviceName)
        {
            return Join(MainSeparator, (int)OpenGloveActions.GetOpenGloveArduinoSoftwareVersion, bluetoothDeviceName, Empty, Empty, Empty);
        }
    }
}
