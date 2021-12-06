using System.Collections.Generic;

namespace DLL.Models {
    public class User {
        public int                Id          { get; set; }
        public int                PersonId    { get; set; }
        public Person?            Person      { get; set; }
        public int                LoginDataId { get; set; }
        public LoginData?         LoginData   { get; set; }
        public int                Role        { get; set; }
        public List<ActionsData>? ActionsData { get; set; }
    }
}