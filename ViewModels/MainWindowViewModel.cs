using System;
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

      
        public static List<string> selectAll()
        {
            List<string> notes = new List<string>();

            try {
                using (var connection = new SqliteConnection("Data Source=db.db"))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                        SELECT note
                        FROM notes
                    ";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var note = reader.GetString(0);

                            notes.Add(note.ToString());
                        }
                    }

                    connection.Close();
                }
            } catch (SqliteException e) {
                Console.WriteLine(@"Error has occured");
            }
            

            return notes;
        }

        public static string selectById(int id)
        {
            string note = "";

            try {
                using (var connection = new SqliteConnection("Data Source=db.db"))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText =
                    @"
                        SELECT note
                        FROM notes
                        WHERE id = $id
                    ";

                    command.Parameters.AddWithValue("$id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = reader.GetString(0);

                            note = data.ToString();
                        }
                    }

                    connection.Close();
                }
            } catch (SqliteException e) {
                Console.WriteLine(@"Error has occured");
            }

            return note;
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

        public static int updateTable(int id, string newNote)
        {
            //if error occured 
            int result = -1;

            using (var connection = new SqliteConnection("Data Source=db.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    UPDATE notes
                    SET note = $note
                    WHERE id = $id
                ";

                command.Parameters.AddWithValue("$id", id);
                command.Parameters.AddWithValue("$note", newNote);
                
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
