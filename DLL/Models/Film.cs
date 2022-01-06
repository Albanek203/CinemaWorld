namespace DLL.Models {
    public class Film {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Genre { get; set; }
        public string? Duration { get; set; }
        public bool Is3D { get; set; }
        public float Rating { get; set; }
        public float Price { get; set; }
        public List<Session>? Sessions { get; set; }
    }
}