using Wineshop.Models;
using Wineshop.ViewModels;

namespace WineShop.ViewModels
{
    public class OsatuViewModel
    {
        public string Bezeroa { get; set; }
        public string SaskiaId { get; set; }
        public IList<SaskiaAleaViewModel> SaskiaAleaVMList { get; set; }
        public List<SaskiaAlea> SaskiaAleak { get; internal set; }
    }
}
