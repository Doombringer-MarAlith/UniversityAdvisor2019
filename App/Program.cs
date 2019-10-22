﻿using Microsoft.Extensions.DependencyInjection;
using ServerCallFromApp;
using System;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Windows.Forms;
using ExternalDependencies;
using Objektinis;
using Objektinis.FormManagers;

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

            var a = Container.GetService<FormManagerData>();
            var aa = Container.GetService<FormManagerData>();
            a.CurrentReviewIndex = 5;
            var g = a.CurrentReviewIndex;
            var ga = aa.CurrentReviewIndex;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run((Form) Container.GetService<ILoginForm>()); // IUniversitySearchForm
        }
    }
}