namespace DLL.Models {
    public class Seat {
        public int      Id      { get; set; }
        public int      Number  { get; set; }
        public int      Price   { get; set; }
        public int      Status  { get; set; }
        public Session? Session { get; set; }
    }
}