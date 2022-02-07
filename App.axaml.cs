using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SharpToDo.ViewModels;
using SharpToDo.Views;
using System.IO;
using System;
using System.Text;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace SharpToDo
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}