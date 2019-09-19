using System;
using Models.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notex.Views;
using NotexServerCallFromApp;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Notex
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            //var account = new Account()
            //{
            //    Name = "Tomas",
            //    Guid = Guid.NewGuid().ToString()
            //};
            //DataManipulations.PutDataToServer("account", JsonConvert.SerializeObject(account));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
