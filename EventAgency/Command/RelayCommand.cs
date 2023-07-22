﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EventAgency.Command
{
    public class RelayCommand : ICommand
    {
        private ICommand showWindowCommand;
        private object canShowWindow;

        public event EventHandler CanExecuteChanged;

        public Action<object> _Execute { get; set; }
        public Predicate<object> _CanExecute { get; set; }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _Execute = execute;
            _CanExecute = canExecute;
        }

        public RelayCommand(ICommand showWindowCommand, object canShowWindow)
        {
            this.showWindowCommand = showWindowCommand;
            this.canShowWindow = canShowWindow;
        }

        public bool CanExecute(object parameter)
        {
            return _CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _Execute(parameter);
        }
    }
}
