using Wineshop.Models;

namespace WineShop.ViewModels
{
    public class OsatuViewModel
    {
        public string Bezeroa { get; set; }
        public string SaskiaId { get; set; }
        public IList<SaskiaAlea> SaskiaAleak { get; set; }
    }
}
