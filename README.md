# SharpToDo

## A Simple ToDo APP written in c# with AvaloniaUI .NET Core and SQLite on Linux (Application de ToDo en c#)
![image](https://user-images.githubusercontent.com/56622131/152974016-7557266a-8cf5-4a7a-b7fc-1c0f9897fcd2.png)

## Usage 
Click on the text box, write a note add it by clicking on the green button.
![image](https://user-images.githubusercontent.com/56622131/152974445-d1f76625-9878-424c-b0df-335b2fd4c252.png)

You can click on checkboxes to mark a todo as finished
![image](https://user-images.githubusercontent.com/56622131/152974512-60592528-72a8-43a2-930f-b63035d7290e.png)

To remove a note, simply click on the red button
![image](https://user-images.githubusercontent.com/56622131/152974610-dc951a25-94b7-4cf1-9dc0-27e7d3dcfbf2.png)


## Code

### CRUD (SQLite)

App.cs
```csharp
public static int createTable() \\create the table if not exist
public static List<Note> selectAll() \\retireve all notes
public static int deleteFomTable(int id) \\delete by id
public static int updateTable(int id, int state) \\update the state of an element

```
MainWindowViewModel.cs
```csharp
public static int insertIntoTable(string note) \\insert note into table
```

### Actions
```csharp
public static void loadNotes(Window w) \\get all notes and generate them with addNote
public void addNote(Window window) \\add note on the main UI component with note generate by generateNote
public static DockPanel generateNote(Note note) \\create UI for the note (deletebtn, checkbox state and text)
public static void removeNote(string id) \\remove on UI and database then refresh window
public static void updateNote(Note note) \\update the state of a note
```
## Run & Compile

```bash
dotnet run \\ run the project 
dotnet build \\build an executable for your machine then click on it to start the app
```

## Wirerame
![image](https://user-images.githubusercontent.com/56622131/152977736-2baaa715-e2a5-485e-9389-17793752b093.png)
