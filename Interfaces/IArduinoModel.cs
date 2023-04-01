using System;
using System.Threading.Tasks;

namespace Arduino.WPF.Interfaces
{
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
