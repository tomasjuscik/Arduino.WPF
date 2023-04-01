namespace Arduino.WPF.Interfaces
{
    using System;

    public interface IArduinoModel
    {
        void StartCommunication();
        void StopCommunication();
        void ConfigurePort();
        string Status { get;}
        string Response { get; set; }
        event EventHandler? dataChanged;

    }
}
