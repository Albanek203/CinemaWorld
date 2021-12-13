namespace DLL.Models {
    public class ActionsData {
        public int      Id             { get; set; }
        public int      UserId         { get; set; }
        public User?    User           { get; set; }
        public int      TypeOperation  { get; set; }
        public int      ObjectDataType { get; set; }
        public int      Cost           { get; set; }
        public DateTime Time           { get; set; }
    }
}