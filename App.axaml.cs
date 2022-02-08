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
using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;

namespace SharpToDo
{
    public class App : Application
    {
        public override void Initialize()
        {
            App.createTable();
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

        public static int createTable()
        {
            //if error occured 
            int result = -1;

            using (var connection = new SqliteConnection("Data Source=db.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                   CREATE TABLE [IF NOT EXISTS] notes (
                        id INTEGER PRIMARY KEY,
                        note TEXT NOT NULL,
                    );
                ";
                
                try {
                    result = command.ExecuteNonQuery();

                 } catch (SqliteException e) {
                    Console.WriteLine($"Error has occured:{e}");
                }

                connection.Close();
            }

            //success
           return result;
        }
    }
}