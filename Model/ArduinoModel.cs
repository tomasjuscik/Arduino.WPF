namespace Arduino.WPF.Model
{
    using Arduino.WPF.Interfaces;
    using System;
    using System.IO.Ports;

    public class ArduinoModel : IArduinoModel
    {
        private readonly SerialPort ArduinoPort;
        public event EventHandler? dataChanged;

        public ArduinoModel()
        {
            ArduinoPort = new SerialPort();
        }

        ~ArduinoModel()
        {
            ArduinoPort.Close();
        }

        private string _response = string.Empty;

        public string Response
        {
            get
            {
                return _response;
            }
            set
            {
                _response = value;
            }
        }

        private string _status = string.Empty;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        public void ConfigurePort()
        {
            ArduinoPort.PortName = "COM3";
            ArduinoPort.BaudRate = 9600;
            ArduinoPort.DataReceived += ArduinoPort_DataReceived;
        }

        public void StartCommunication()
        {
            if (!ArduinoPort.IsOpen) 
            { 
                ArduinoPort.Open();
                Status = "Connected";
                this.dataChanged?.Invoke(this, new EventArgs());
            }
        }

        public void StopCommunication()
        {
            if (ArduinoPort.IsOpen) 
            { 
                ArduinoPort.Close();
                Status = "Disconnected";
                Response = string.Empty;
                this.dataChanged?.Invoke(this, new EventArgs());

            }
        }

        private void ArduinoPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var response = ArduinoPort.ReadLine();
            if (response != string.Empty)
            {
                Response = response;
            }
            this.dataChanged?.Invoke(this, e);
        }
    }
}
