namespace DLL.Models {
    public class LoginData {
        public int     Id       { get; set; }
        public string? Email    { get; set; }
        public string? Login    { get; set; }
        public string? Password { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public User?   User     { get; set; }
    }
}