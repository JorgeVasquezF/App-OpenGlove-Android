using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using WebSocketSharp; //Nuget: WebSocketSharp-netstandard (1.0.1)

namespace CSharpTest.OpenGloveAPI_C_Sharp_HL
{
    public class Communication
    {
        public WebSocket WebSocket { get; set; }
        public MessageGenerator MessageGenerator { get; set; }
        public string BluetoothDeviceName { get; set; }
        public string ConfigurationName { get; set; }

        public delegate void TimeTestServerLatencyActivateActuatorsReceived(long nanoSeconds);
        public delegate void TimeTestArduinoLatencyActivateActuatorsReceived(long microSeconds);
        public delegate void FlexorMovement(int region, int value);
        public delegate void AccelerometerValues(float ax, float ay, float az);
        public delegate void GyroscopeValues(float gx, float gy, float gz);
        public delegate void MagnometerValues(float mx, float my, float mz);
        public delegate void AttitudeValues(float pitch, float roll, float yaw);
        public delegate void AllIMUValues(float ax, float ay, float az, float gx, float gy, float gz, float mx, float my, float mz);
        public delegate void BluetoothDeviceConnectionState(bool isConnected);
        public delegate void WebSocketConnectionState(bool isConnected);
        public delegate void InfoMessage(string message);

        public event TimeTestServerLatencyActivateActuatorsReceived OnTimeTestServerLatencyActivateActuatorsReceived;
        public event TimeTestArduinoLatencyActivateActuatorsReceived OnTimeTestArduinoLatencyActivateActuatorsReceived;
        public event FlexorMovement OnFlexorValueReceived;
        public event AccelerometerValues OnAccelerometerValuesReceived;
        public event GyroscopeValues OnGyroscopeValuesReceived;
        public event MagnometerValues OnMagnometerValuesReceived;
        public event AttitudeValues OnAttitudeValuesReceived;
        public event AllIMUValues OnAllIMUValuesReceived;
        public event BluetoothDeviceConnectionState OnBluetoothDeviceConnectionStateChanged;
        public event WebSocketConnectionState OnWebSocketConnectionStateChangued;
        public event InfoMessage OnInfoMessagesReceived;

        public Communication(string bluetoothDeviceName, string configurationName, string url)
        {
            this.WebSocket = new WebSocket(url);
            this.WebSocket.OnOpen += OnOpen;         
            this.WebSocket.OnMessage += OnMessage;
            this.WebSocket.OnClose += OnClose;
            this.WebSocket.OnError += OnError;
            this.BluetoothDeviceName = bluetoothDeviceName;
            this.ConfigurationName = configurationName;

            MessageGenerator = new MessageGenerator(mainSeparator: ";", secondarySeparator: ",", empty: "");
        }

        private void OnOpen(object sender, EventArgs e)
        {
            Debug.WriteLine($"Websocket Connected to Server!");
            this.AddOpenGloveDeviceToServer(BluetoothDeviceName, ConfigurationName);
            Debug.WriteLine($"OpenGlove.Communication.AddOpenGloveToServer()");
            this.StartCaptureDataFromServer(BluetoothDeviceName);
            Debug.WriteLine($"Communication.StartCaptureDataFromServer()");
            OnWebSocketConnectionStateChangued?.Invoke(this.WebSocket.IsAlive);

        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            Debug.WriteLine($"Received from Server: {e.Data}");
            MessageHandler(e.Data);
        }

        private void MessageHandler(string message)
        {
            int mapping, value;
            float valueX, valueY, valueZ, pitch, roll, yaw;
            string[] words;

            if (message != null)
            {
                words = message.Split(',');
                try
                {
                    switch (words[0])
                    {
                        case "f":
                            mapping = Int32.Parse(words[1]);
                            value = Int32.Parse(words[2]);
                            OnFlexorValueReceived?.Invoke(mapping, value);
                            break;
                        case "a":
                            valueX = float.Parse(words[1], CultureInfo.InvariantCulture);
                            valueY = float.Parse(words[2], CultureInfo.InvariantCulture);
                            valueZ = float.Parse(words[3], CultureInfo.InvariantCulture);
                            OnAccelerometerValuesReceived?.Invoke(valueX, valueY, valueZ);
                            break;
                        case "g":
                            valueX = float.Parse(words[1], CultureInfo.InvariantCulture);
                            valueY = float.Parse(words[2], CultureInfo.InvariantCulture);
                            valueZ = float.Parse(words[3], CultureInfo.InvariantCulture);
                            OnGyroscopeValuesReceived?.Invoke(valueX, valueY, valueZ);
                            break;
                        case "m":
                            valueX = float.Parse(words[1], CultureInfo.InvariantCulture);
                            valueY = float.Parse(words[2], CultureInfo.InvariantCulture);
                            valueZ = float.Parse(words[3], CultureInfo.InvariantCulture);
                            OnMagnometerValuesReceived?.Invoke(valueX, valueY, valueZ);
                            break;
                        case "z":
                            OnAllIMUValuesReceived?.Invoke(float.Parse(words[1], CultureInfo.InvariantCulture), float.Parse(words[2], CultureInfo.InvariantCulture), float.Parse(words[3], CultureInfo.InvariantCulture), float.Parse(words[4], CultureInfo.InvariantCulture), float.Parse(words[5], CultureInfo.InvariantCulture), float.Parse(words[6], CultureInfo.InvariantCulture), float.Parse(words[7], CultureInfo.InvariantCulture), float.Parse(words[8], CultureInfo.InvariantCulture), float.Parse(words[9], CultureInfo.InvariantCulture));
                            break;
                        case "r":
                            pitch = float.Parse(words[1], CultureInfo.InvariantCulture);
                            roll = float.Parse(words[2], CultureInfo.InvariantCulture);
                            yaw = float.Parse(words[3], CultureInfo.InvariantCulture);
                            OnAttitudeValuesReceived?.Invoke(pitch, roll, yaw);
                            break;
                        case "b":
                            OnBluetoothDeviceConnectionStateChanged?.Invoke(bool.Parse(words[1]));
                            break;
                        case "us":
                            OnTimeTestArduinoLatencyActivateActuatorsReceived?.Invoke(long.Parse(words[1]));
                            break;
                        case "ns":
                            OnTimeTestServerLatencyActivateActuatorsReceived?.Invoke(long.Parse(words[1]));
                            break;
                            
                        default:
                            OnInfoMessagesReceived?.Invoke(message);
                            break;
                    }

                }
                catch
                {
                    Debug.WriteLine($"ERROR: BAD FORMAT on Communication.MessageHandler [message: {message}]");
                }
            }
        }

        private void OnClose(object sender, EventArgs e)
        {
            Debug.WriteLine($"WebSocket Closed: {e.ToString()}");
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            Debug.WriteLine($"WebSocket Error: {e.Exception}, {e.ToString()}");
        }

        public void StartOpenGlove(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.StartOpenGlove(bluetoothDeviceName));
        }

        public void StopOpenGlove(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.StopOpenGlove(bluetoothDeviceName));
        }

        public void AddOpenGloveDeviceToServer(string bluetoothDeviceName, string configurationName)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.AddOpenGloveDeviceToServer(bluetoothDeviceName, configurationName));
        }

        public void RemoveOpenGloveDeviceFromServer(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.RemoveOpenGloveDeviceFromServer(bluetoothDeviceName));
        }
        public void SaveOpenGloveConfiguration(string bluetoothDeviceName, string configurationName)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.SaveOpenGloveConfiguration(bluetoothDeviceName, configurationName));
        }

        public void ConnectToBluetoothDevice(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.ConnectToBluetoothDevice(bluetoothDeviceName));
        }

        public void DisconnectFromBluetoothDevice(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.DisconnectFromBluetoothDevice(bluetoothDeviceName));
        }

        public void StartCaptureDataFromServer(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.StartCaptureDataFromServer(bluetoothDeviceName));
        }

        public void StopCaptureDataFromServer(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.StopCaptureDataFromServer(bluetoothDeviceName));
        }

        public void AddActuator(string bluetoothDeviceName, int region, int positivePin, int negativePin)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.AddActuator(bluetoothDeviceName, region, positivePin, negativePin));
        }

        public void AddActuators(string bluetoothDeviceName, List<int> regions, List<int> positivePins, List<int> negativePins)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.AddActuators(bluetoothDeviceName, regions, positivePins, negativePins));
        }

        public void RemoveActuator(string bluetoothDeviceName, int region)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.RemoveActuator(bluetoothDeviceName, region));
        }

        public void RemoveActuators(string bluetoothDeviceName, List<int> regions)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.RemoveActuators(bluetoothDeviceName, regions));
        }

        public void ActivateActuators(string bluetoothDeviceName, List<int> regions, List<string> intensities)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.ActivateActuators(bluetoothDeviceName, regions, intensities));
        }

        public void ActivateActuatorsTimeTest(string bluetoothDeviceName, List<int> regions, List<string> intensities)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.ActivateActuatorsTimeTest(bluetoothDeviceName, regions, intensities));
        }

        public void TurnOnActuators(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.TurnOnActuators(bluetoothDeviceName));
        }

        public void TurnOffActuators(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.TurnOffActuators(bluetoothDeviceName));
        }

        public void ResetActuators(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.ResetActuators(bluetoothDeviceName));
        }

        public void AddFlexor(string bluetoothDeviceName, int region, int pin)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.AddFlexor(bluetoothDeviceName, region, pin));
        }

        public void AddFlexors(string bluetoothDeviceName, List<int> regions, List<int> pins)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.AddFlexors(bluetoothDeviceName, regions, pins));
        }

        public void RemoveFlexor(string bluetoothDeviceName, int region)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.RemoveFlexor(bluetoothDeviceName, region));
        }

        public void RemoveFlexors(string bluetoothDeviceName, List<int> regions)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.RemoveFlexors(bluetoothDeviceName, regions));
        }

        public void CalibrateFlexors(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.CalibrateFlexors(bluetoothDeviceName));
        }

        public void ConfirmCalibration(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.ConfirmCalibration(bluetoothDeviceName));
        }

        public void SetThreshold(string bluetoothDeviceName, int value)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.SetThreshold(bluetoothDeviceName, value));
        }

        public void TurnOnFlexors(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.TurnOnFlexors(bluetoothDeviceName));
        }

        public void TurnOffFlexors(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.TurnOffFlexors(bluetoothDeviceName));
        }

        public void ResetFlexors(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.ResetFlexors(bluetoothDeviceName));
        }

        public void StartIMU(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.StartIMU(bluetoothDeviceName));
        }

        public void SetIMUStatus(string bluetoothDeviceName, bool status)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.SetIMUStatus(bluetoothDeviceName, status));
        }

        public void SetRawData(string bluetoothDeviceName, bool status)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.SetRawData(bluetoothDeviceName, status));
        }

        public void SetIMUChoosingData(string bluetoothDeviceName, int value)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.SetIMUChoosingData(bluetoothDeviceName, value));
        }

        public void ReadOnlyAccelerometerFromIMU(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.ReadOnlyAccelerometerFromIMU(bluetoothDeviceName));
        }

        public void ReadOnlyGyroscopeFromIMU(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.ReadOnlyGyroscopeFromIMU(bluetoothDeviceName));
        }

        public void ReadOnlyMagnetometerFromIMU(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.ReadOnlyMagnetometerFromIMU(bluetoothDeviceName));
        }

        public void ReadOnlyAttitudeFromIMU(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.ReadOnlyAttitudeFromIMU(bluetoothDeviceName));
        }

        public void ReadAllDataFromIMU(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.ReadAllDataFromIMU(bluetoothDeviceName));
        }

        public void CalibrateIMU(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.CalibrateIMU(bluetoothDeviceName));
        }

        public void TurnOnIMU(string bluetoohDeviceName)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.TurnOnIMU(bluetoohDeviceName));
        }

        public void TurnOffIMU(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.TurnOffIMU(bluetoothDeviceName));
        }

        public void SetLoopDelay(string bluetoothDeviceName, int value)
        {
            if (this.WebSocket.IsAlive) 
                this.WebSocket.Send(MessageGenerator.SetLoopDelay(bluetoothDeviceName, value));
        }

        public void ConnectToWebSocketServer()
        {
            this.WebSocket.Connect();
            OnWebSocketConnectionStateChangued?.Invoke(this.WebSocket.IsAlive);
        }

        public void DisconnectFromWebSocketServer()
        {
            this.WebSocket.Close();
            OnWebSocketConnectionStateChangued?.Invoke(this.WebSocket.IsAlive);
        }

        public void GetOpenGloveArduinoVersionSoftware(string bluetoothDeviceName)
        {
            if (this.WebSocket.IsAlive)
                this.WebSocket.Send(MessageGenerator.GetOpenGloveArduinoVersionSoftware(bluetoothDeviceName));
        }
    }
}
