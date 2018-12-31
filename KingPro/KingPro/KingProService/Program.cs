using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingProService
{
    using System.ServiceProcess;

    /// <summary>
    /// Represents the main application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        internal static void Main()
        {
            ServiceBase[] servicesToRun;
            servicesToRun = new ServiceBase[]
            {
                new KingProService()
            };

            ServiceBase.Run(servicesToRun);
        }
    }
}
