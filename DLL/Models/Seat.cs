using System.Data.SqlTypes;

namespace DLL.Models {
    public class Seat {
        public int      Id     { get; set; }
        public int      Number { get; set; }
        public SqlMoney Price  { get; set; }
        public int      Status { get; set; }
    }
}