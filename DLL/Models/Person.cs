namespace DLL.Models {
    public class Person {
        public int      Id           { get; set; }
        public string?  Name         { get; set; }
        public string?  Surname      { get; set; }
        public float    Salary       { get; set; }
        public DateTime BirthDate    { get; set; }
        public DateTime EmployeeDate { get; set; }
        public User?    User         { get; set; }
    }
}