using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMBase;

namespace EnhancedUserControl.ViewModel
{
    public class AutoSuggestComboBoxViewModel : ViewModelBase
    {
        private string itemName;

        //private string itemValue; 

        private string selectedItem; 

        private List<string> items;

        public AutoSuggestComboBoxViewModel()
        {
            // For debug. 
            this.items = new List<string>()
            {
                "Apple", 
                "Microsoft",
                "Huawei",
                "Dell",
            };

            this.selectedItem = string.Empty;
            //this.itemName = string.Empty; 
            this.itemName = "Brand";            
        }

        public List<string> Items
        {
            get
            {
                return this.items;
            }
            set
            {
                if (!this.items.SequenceEqual(value))
                {
                    this.items = value;
                    base.OnPropertyChanged("Items");
                }
            }
        }

        public string ItemName
        {
            get
            {
                return this.itemName;
            }
            set
            {
                if (!this.itemName.Equals(value))
                {
                    this.itemName = value;
                    base.OnPropertyChanged("ItemName");
                }
            }
        }

        public string SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                if (this.selectedItem.Equals(value))
                {
                    this.selectedItem = value;
                    base.OnPropertyChanged("SelectedItem");
                }
            }
        }


    }
}
