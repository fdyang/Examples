using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using MVVMBase;

namespace FolderBrowser.ViewModel
{
    public class FolderBrowserViewModel : ViewModelBase
    {
        private string logDirectory;

        private ICommand runFolderBrowserCommand;

        public string LogDirectory
        {
            get
            {
                return this.logDirectory;
            }

            set
            {
                if (this.logDirectory != value)
                {
                    this.logDirectory = value;
                    this.OnPropertyChanged("LogDirectory");
                }
            }
        }

        public ICommand RunFolderBrowserCommand
        {
            get
            {
                if (this.runFolderBrowserCommand == null)
                {
                    this.runFolderBrowserCommand = new RelayCommand(
                        param =>
                        {
                            this.StartFolderBrowser();
                        },
                        null);
                }

                return this.runFolderBrowserCommand;
            }
        }

        private void StartFolderBrowser()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                Thread t = new Thread(() => dialog.ShowDialog());
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                t.Join();

                var path = dialog.SelectedPath;
                if (!string.IsNullOrEmpty(path))
                {
                    this.LogDirectory = path;
                }
            }
        }
    }
}
