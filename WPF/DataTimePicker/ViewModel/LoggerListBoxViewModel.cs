using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMBase;

namespace EnhancedUserControl.ViewModel
{
    public class LoggerListBoxViewModel : ViewModelBase
    {
        ObservableCollection<string> logger = new ObservableCollection<string>();

        public LoggerListBoxViewModel()
        {
        }

        public ObservableCollection<string> Logger
        {
            get
            {
                return this.logger; 
            }
            set
            {
                if (!this.logger.SequenceEqual(value))
                {
                    this.logger = value;
                    // this.OnPropertyChanged("Logger");   //ObservableCollection has already haven this trigger inside.

                    
                }
            }
        }
    }
}
