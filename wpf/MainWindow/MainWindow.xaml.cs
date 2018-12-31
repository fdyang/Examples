using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EnhancedUserControl.ViewModel;

namespace MainWillWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

           // this.UpdateUI(); 
        }

        private void UpdateUI()
        {
            Task.Factory.StartNew(() =>
            {
                this.Logger.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new Action(()
                =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        ((LoggerListBoxViewModel)this.Logger.DataContext).Logger.Add("log..." + i);
                        Thread.Sleep(1000);

                    }

                }));
            });

            //Task.Factory.StartNew(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        ((LoggerListBoxViewModel)this.Logger.DataContext).Logger.Add("log..." + i);
            //        Thread.Sleep(1000);

            //    }
            //});

            //this.Logger.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new Action(()
            //    =>
            //    {
            //        for (int i = 0; i < 10; i++)
            //        {
            //            ((LoggerListBoxViewModel)this.Logger.DataContext).Logger.Add("log..." + i);
            //            Thread.Sleep(1000);

            //        }

            //    }));

        }
    }
}
