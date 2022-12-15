using System.ComponentModel.DataAnnotations;

namespace Wineshop.Models
{
    public class SaskiaAlea
    {
        [Key]
        public int Id { get; set; }
        public string SaskiaId { get; set; }
        public int Kantitatea { get; set; }
        public System.DateTime Data { get; set; }
        public int ArdoaId { get; set; }
    }
}
