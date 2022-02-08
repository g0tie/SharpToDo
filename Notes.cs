namespace SharpToDo {
    public class Note
    {
        private int _id = 0;
        private string _todo = "";
        private bool _state = false;

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

        public bool State
        {
            get { return _state; }
            set { _state = value; }
        }
    }
}