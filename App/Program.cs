using Microsoft.Extensions.DependencyInjection;
using Objektinis;
using System;
using System.Windows.Forms;

namespace App
{
    internal static class Program
    {
        internal static IServiceProvider Container = new ContainerBuilder().Build();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run((Form)Container.GetService<ILoginForm>()); // IUniversitySearchForm
        }
    }
}