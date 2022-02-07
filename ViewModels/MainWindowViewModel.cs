using System;
using ReactiveUI;
using Avalonia.Controls;
using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;

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

        public void createSqlConnection()
        {
           
        }

        public void removeNote(ReadOnlyCollection<Object> parameters)
        {
            var window = parameters[0];
            var id = parameters[1];
            Console.WriteLine(window);
            // var note = window.FindControl<DockPanel>(id);
            // var wrapper = window.FindControl<StackPanel>("wrapper");

            // wrapper.Children.Remove(note);
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
            panel.Name = generateId();

            return panel;
        }

        public string generateId()
        {
            return Guid.NewGuid().ToString();
        }

    }
       
}
