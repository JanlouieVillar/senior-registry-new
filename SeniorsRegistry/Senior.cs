using System.ComponentModel.DataAnnotations;

namespace SeniorsRegistry
{
    public class Senior
    {
        [Key]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Birthday { get; set; }
        public string Zone { get; set; }
        public string Contact { get; set; }
        public string Guardian { get; set; }
        public string Credential { get; set; }        
        public string Benefit1 { get; set; }
        public string Benefit2 { get; set; }
        public string Benefit3 { get; set; }
        public string Benefit4 { get; set; }


    }
}
