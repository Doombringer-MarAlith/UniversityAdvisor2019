using Microsoft.Extensions.DependencyInjection;
using ServerCallFromApp;
using System;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Windows.Forms;
using Objektinis;

namespace App
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        { 
            var container = new ContainerBuilder().Build();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run((Form) container.GetService<IUniversitySearchForm>());
        }
    }
}