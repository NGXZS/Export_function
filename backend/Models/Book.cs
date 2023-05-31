using System.ComponentModel.DataAnnotations;

namespace PWAY_ASPNetCore_WebAPI.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public int StartDis { get; set; }
        public int EndDis { get; set; }
        public string string1 { get; set; }
        public string string2 { get; set; }
    }
}
