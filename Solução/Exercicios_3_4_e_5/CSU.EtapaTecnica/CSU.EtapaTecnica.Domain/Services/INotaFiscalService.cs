using CSU.EtapaTecnica.Domain.DTO;
using System.Collections.Generic;

namespace CSU.EtapaTecnica.Domain.Services
{
    public interface INotaFiscalService
    {
        IEnumerable<NotaFiscalDTO> GetNotasFiscais(int mes);
    }
}
