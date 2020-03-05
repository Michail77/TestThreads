using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Image.ViewModel
{
    // http://msdn.microsoft.com/cs-cz/library/system.windows.input.icommand(v=vs.110).aspx
    // http://www.codeproject.com/Articles/274982/Commands-in-MVVM
        
    abstract class BaseCommand : ICommand
    {
        protected Func<bool> canExecute;

        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }
            else
            {
                return this.canExecute();
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        virtual public void Execute(object parameter = null) { }
    }

    class Command : BaseCommand 
    {
        private Action akce;            
      
        public Command(Action akce, Func<bool> canExecute)
        {
            if (akce == null)
                throw new ArgumentNullException("Akce nemá žádnou obsluhu.");

            this.akce = akce;
            this.canExecute = canExecute;            
        }

        public Command(Action akce) : this(akce, null) { }

        override public void Execute(object parameter = null)
        {
            this.akce();
        }
    }

    class ParametrizedCommand : BaseCommand
    {
        private Action<object> akce;

        public ParametrizedCommand(Action<object> akce, Func<bool> canExecute)
        {
            if (akce == null)
                throw new ArgumentNullException("Akce nemá žádnou obsluhu.");

            this.akce = akce;
            this.canExecute = canExecute;
        }

        public ParametrizedCommand(Action<object> akce) : this(akce, null) { }

        override public void Execute(object parameter)
        {
            this.akce(parameter);
        }
    }
}
