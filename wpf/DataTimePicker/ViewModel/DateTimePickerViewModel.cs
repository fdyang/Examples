using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMBase;

namespace EnhancedUserControl.ViewModel
{
    public class DateTimePickerViewModel : ViewModelBase
    {
        private DateTime selectedDateTime; 

        public DateTimePickerViewModel()
        {
            this.selectedDateTime = DateTime.Now; 
        }

        public DateTime SelectedDateTime
        {
            get
            {
                return this.selectedDateTime;
            }
            set
            {
                if (this.selectedDateTime != value)
                {
                    this.selectedDateTime = value;
                    this.OnPropertyChanged("SelectedDateTime"); 
                }
            }
        }
    }
}
