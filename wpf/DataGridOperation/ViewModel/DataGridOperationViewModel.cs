using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMBase;

namespace DataGridOperation.ViewModel
{
    public class DataGridOperationViewModel : ViewModelBase
    {
        private ICommand runLoadCommand;

        public ICommand RunLoadCommand
        {
            get
            {
                if (this.runLoadCommand == null)
                {
                    this.runLoadCommand = new RelayCommand(
                        param => {
                            this.LoadData();
                        },
                        null); 
                }

                return this.runLoadCommand;
            }

        }

        private void LoadData()
        {
            // Load data from data table into data grid.
            this.Table = new DataTable();
            this.Table.Columns.Add("Col0");
            var row0 = this.Table.NewRow();
            row0["Col0"] = "Test0";
            this.Table.Rows.Add(row0);
        }

        public DataTable Table
        {
            get; 
            set; 
        }
    }
}

