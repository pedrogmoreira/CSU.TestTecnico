using AutoMapper;
using CSU.EtapaTecnica.Domain.DTO;
using CSU.EtapaTecnica.Domain.Models;

namespace CSU.EtapaTecnica.Exercicio_4.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<NotaFiscal, NotaFiscalDTO>()
                .ReverseMap();
        }
    }
}
