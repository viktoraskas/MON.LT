using System;

namespace MON.LT.Models
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public int imageId { get; set; }
        public byte[] image { get; set; }
        public byte[] imagehumbnail { get; set; }
    }
}