using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using SharpToDo.ViewModels;
using SharpToDo.Views;
using Avalonia.Controls;
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
using SharpToDo;
using System.Windows.Input;
using ReactiveUI;

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

                loadNotes(desktop.MainWindow);
            }

            base.OnFrameworkInitializationCompleted();

            // Console.WriteLine(Application.Current);
        }

        //CRUD FUNCTIONS
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
                   CREATE TABLE IF NOT EXISTS notes ( id INTEGER PRIMARY KEY, note TEXT NOT NULL )
                ";
                
                try {
                    result = command.ExecuteNonQuery();

                 } catch (SqliteException e) {
                    Console.WriteLine($"Error has occured create table:{e}");
                }

                connection.Close();
            }

            //success
           return result;
        }

        public static List<Note> selectAll()
        {
            List<Note> notes = new List<Note>();

            try {
                using (var connection = new SqliteConnection("Data Source=db.db"))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                        SELECT *
                        FROM notes;
                    ";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var note = reader.GetString(0);
                            var todoNote = new Note();

                            todoNote.Id = Int32.Parse(reader["id"].ToString()) ;
                            todoNote.ToDo =  reader["note"].ToString() ?? "";

                            notes.Add(todoNote);

                        }
                    }

                    connection.Close();
                }
            } catch (SqliteException e) {
                Console.WriteLine(@"Error has occured");
            }
            

            return notes;
        }

         public static int deleteFomTable(int id)
        {
             //if error occured 
            int result = -1;

            using (var connection = new SqliteConnection("Data Source=db.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    DELETE FROM notes
                    WHERE id = $id
                ";

                command.Parameters.AddWithValue("$id", id);
                
                try {
                    result = command.ExecuteNonQuery();

                 } catch (SqliteException e) {
                    Console.WriteLine(@"Error has occured");
                }

                connection.Close();
            }

            //success
           return result;
        }

        //UI RELATIVE FUNCTIONS

        public static void addNote(Window window, Note newNote)
        {
            var wrapper = window.FindControl<StackPanel>("wrapper");
            
            var note = generateNote(newNote);

            wrapper.Children.Add(note);
        }
        

        public static DockPanel generateNote(Note note)
        {
            var panel = new DockPanel();
            var deleteBtn = new Button();
            var text = new TextBlock();
            var deleteCmd = ReactiveCommand.Create<string>(x => App.removeNote(x));

            //set text values on controls
            text.Text = note.ToDo;
            deleteBtn.Content = "Supprimer";
            deleteBtn.Command =  deleteCmd;
            deleteBtn.CommandParameter = note.Id.ToString();

            //add Controls to parent
            panel.Children.Add(text);
            panel.Children.Add(deleteBtn);
            panel.Name = note.Id.ToString();

            return panel;
        }


        public static void loadNotes(Window w)
        {
            var notes = selectAll();

            foreach (Note note in notes) {
                App.addNote(w, note);
            }            
        }

        public static void removeNote(string id)
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var w = desktop.MainWindow;
                var wrapper = w.FindControl<StackPanel>("wrapper");
                var note = w.FindControl<DockPanel>(id);
                
                wrapper.Children.Remove(note);

                App.deleteFomTable(Int32.Parse(id));
            }
        }


    }
}