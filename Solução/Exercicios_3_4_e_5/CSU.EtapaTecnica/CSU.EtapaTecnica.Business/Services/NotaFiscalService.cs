using AutoMapper;
using CSU.EtapaTecnica.Data.Context;
using CSU.EtapaTecnica.Domain.DTO;
using CSU.EtapaTecnica.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CSU.EtapaTecnica.Business.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly CSUContext _context;
        private readonly IMapper _mapper;   

        public NotaFiscalService(CSUContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<NotaFiscalDTO> GetNotasFiscais(int mes)
        {
            return _mapper.Map<IEnumerable<NotaFiscalDTO>>(_context.NotasFiscais
                .FromSqlRaw($"EXEC GetNotasFiscaisPorMes {mes}")
                .AsNoTracking()
                .AsEnumerable());
        }
    }
}
