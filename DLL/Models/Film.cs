﻿using System;
using System.Data.SqlTypes;

namespace DLL.Models {
    public class Film {
        public int      Id       { get; set; }
        public string?  Name     { get; set; }
        public int      Genre    { get; set; }
        public string?  Duration { get; set; }
        public DateTime Date     { get; set; }
        public bool     Is3D     { get; set; }
        public float    Rating   { get; set; }
        public SqlMoney Price    { get; set; }
    }
}