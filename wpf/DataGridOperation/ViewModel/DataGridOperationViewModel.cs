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
        private DataTable table; 

        private ICommand runLoadCommand;

        public DataGridOperationViewModel()
        {
          // this.LoadData(); 
        }

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

        /// <summary>
        /// Load data (*.csv) file and then pass to datatable and display it in datagrid.
        /// </summary>
        private void LoadData()
        {
            // Load data from data table into data grid.
            var t = new DataTable();
            t.Columns.Add("Col0");
            t.Columns.Add("Col1");
            var row0 = t.NewRow();
            row0["Col0"] = "Test0";
            row0["Col1"] = "Test1";
            t.Rows.Add(row0);
            this.Table = t; 
        }

        public DataTable Table
        {
            get
            {
                return this.table; 
            }

            set
            {
                this.table = value;
                this.OnPropertyChanged("Table"); 
            }
        }
    }
}

