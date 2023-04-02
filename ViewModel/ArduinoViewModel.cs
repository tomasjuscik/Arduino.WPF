namespace Arduino.WPF.ViewModel
{
    using Arduino.WPF.Interfaces;
    using Arduino.WPF.Model;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    public class ArduinoViewModel : INotifyPropertyChanged
    {
        private readonly IArduinoModel arduinoModel;
        public event PropertyChangedEventHandler? PropertyChanged;

        public ArduinoViewModel()
        {
            this.arduinoModel = new ArduinoModel();
            this.arduinoModel.ConfigurePort();
            this.arduinoModel.dataChanged += this.OnDataChanged;
        }

        private string _temperature = string.Empty;
        public string Temperature
        {
            get
            {
                return _temperature;
            }
            set
            {
                _temperature = value;
                this.OnPropertyChanged(nameof(Temperature));
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
                this.OnPropertyChanged(nameof(Status));
            }
        }

        private ICommand? _StartCommand;
        public ICommand StartCommand
        {
            get
            {
                if (_StartCommand == null)
                {
                    _StartCommand = new CommandHandler(() => this.StartCommunication(), () => true);
                }

                return _StartCommand;
            }
        }

        private ICommand? _StopCommand;
        public ICommand StopCommand
        {
            get
            {
                if (_StopCommand == null)
                {
                    _StopCommand = new CommandHandler(() => this.StopCommunication(), () => true);
                }

                return _StopCommand;
            }
        }

        private void StartCommunication()
        {
            this.arduinoModel.StartCommunication();
        }

        private void StopCommunication()
        {
            this.arduinoModel.StopCommunication();
        }

        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnDataChanged(object? sender, EventArgs e)
        {
            Temperature = this.arduinoModel.Response;
            Status = this.arduinoModel.Status;
        }
    }

    partial class CommandHandler : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;

        /// <summary>
        /// Creates instance of the command handler
        /// </summary>
        /// <param name="action">Action to be executed by the command</param>
        /// <param name="canExecute">A bolean property to containing current permissions to execute the command</param>
        public CommandHandler(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Wires CanExecuteChanged event 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Forcess checking if execute is allowed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
