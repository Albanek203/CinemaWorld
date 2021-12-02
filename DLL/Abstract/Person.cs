using System;

namespace DLL.Abstract {
    public abstract class Person {
        public string?  Name         { get; set; }
        public string?  Surname      { get; set; }
        public float    Salary       { get; set; }
        public DateTime BirthDate    { get; set; }
        public DateTime EmployeeDate { get; set; }
    }
}