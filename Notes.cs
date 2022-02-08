namespace SharpToDo {
    public class Note
    {
        private int _id = 0;
        private string _todo = "";
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string ToDo
        {
            get { return _todo; }
            set { _todo = value; }
        }
    }
}