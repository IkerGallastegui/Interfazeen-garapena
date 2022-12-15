using Wineshop.Models;
using WineShop.Models;

namespace Wineshop.Services
{
    public interface ISaskiaService
    {
        Task EskaeraBezeroaGehitu(BezeroaEskaera bezeroaEskaera);
        Task EskaeraSortu(BezeroaEskaera bezeroaEskaera, string saskiaId);
        Task SaskiaGehitu(int ardoaId, string saskiaId);
        Task SaskiaKendu(int id, string saskiaId);
        Task<List<SaskiaAlea>> SaskiaLortuAleak(string saskiaId);

    }
}
