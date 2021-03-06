﻿using System;
using System.Diagnostics;
using System.Windows.Input;

namespace MedienBibliothek.Model
{
    class DelegateCommand : ICommand
    {
        #region Fields

        readonly Action _execute;
        readonly Predicate<object> _canExecute;

        #endregion // Fields

        #region Constructors

        public DelegateCommand(Action execute)
            : this(execute, null)
        {
        }

        public DelegateCommand(Action execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion // Constructors

        #region ICommand Members

        public void Execute(object parameter)
        {
            _execute();
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion // ICommand Members
    }


}
