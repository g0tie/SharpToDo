﻿using System;
using ReactiveUI;
using Avalonia.Controls;
using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SharpToDo.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private string _newNote = "";

        public string NewNote
        {
            get => _newNote;
            set {
                _newNote = value;
            }
        }

        public static int insertIntoTable(string note)
        {
         //if error occured 
            int result = -1;

            using (var connection = new SqliteConnection("Data Source=db.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO notes(note)
                    VALUES ($note)
                ";

                command.Parameters.AddWithValue("$note", note);
                
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

      
        public void addNote(Window window)
        {
            var wrapper = window.FindControl<StackPanel>("wrapper");
            
            var note = generateNote(NewNote);

            wrapper.Children.Add(note);
        }

        public DockPanel generateNote(string note)
        {
            var panel = new DockPanel();
            var deleteBtn = new Button();
            var text = new TextBlock();

            //set text values on controls
            text.Text = note;
            deleteBtn.Content = "Supprimer";

            //add Controls to parent
            panel.Children.Add(text);
            panel.Children.Add(deleteBtn);

            MainWindowViewModel.insertIntoTable(note);

            return panel;
        }

    }
       
}
