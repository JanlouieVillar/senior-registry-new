using System.ComponentModel.DataAnnotations;

namespace SeniorsRegistry
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public int Administrator { get; set; }
    }
}