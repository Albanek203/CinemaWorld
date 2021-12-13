namespace DLL.Models {
    public class Session {
        public int         Id          { get; set; }
        public int         HallNumber  { get; set; }
        public int         FilmId      { get; set; }
        public Film?       Film        { get; set; }
        public List<Seat>? Seats       { get; set; }
        public DateTime    DateSession { get; set; }
    }
}