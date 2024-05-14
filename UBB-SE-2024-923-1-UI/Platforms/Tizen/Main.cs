using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;

namespace UBB_SE_2024_923_1_UI
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
