using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using EnhancedUserControl.ViewModel;

namespace EnhancedUserControl.View
{
    /// <summary>
    /// Interaction logic for LoggerListBox.xaml
    /// </summary>
    public partial class LoggerListBox : UserControl
    {
        public LoggerListBox()
        {
            InitializeComponent();
            this.DataContext = new LoggerListBoxViewModel();
            //Task.Factory.StartNew(() =>
            //{
            //    this.Dispatcher.Invoke(() =>
            //    {
            //        for (int i = 0; i < 5; i++)
            //        {
            //            ((LoggerListBoxViewModel)(this.DataContext)).Logger.Add("Log~~~~.." + i);
            //            Thread.Sleep(2000);
            //        }
            //    }); 
            //}); 

            // Start a non-UI thread to do some works without blocking UI thread.
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    // Put UI change enter dispatcher queue.
                    this.Dispatcher.Invoke(() =>
                    {
                        ((LoggerListBoxViewModel)(this.DataContext)).Logger.Add("Log~~~~.." + i);
                    });

                    // Other works.
                    Thread.Sleep(1000); 
                }
            });

        }
    }
}
