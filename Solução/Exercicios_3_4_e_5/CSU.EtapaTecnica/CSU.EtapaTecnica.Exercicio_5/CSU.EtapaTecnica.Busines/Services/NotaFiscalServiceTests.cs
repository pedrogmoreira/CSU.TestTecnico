using AutoMapper;
using CSU.EtapaTecnica.Business.Services;
using CSU.EtapaTecnica.Data.Context;
using CSU.EtapaTecnica.Domain.DTO;
using CSU.EtapaTecnica.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;

namespace CSU.EtapaTecnica.Exercicio_5.CSU.EtapaTecnica.Busines.Tests.Services
{
    public class NotaFiscalServiceTests
    {
        
        [Fact]
        public async void ShouldReturnData()
        {
            CSUContext context = await InitializeDatabase();

            // adding elements to db
            var notaFiscal = new NotaFiscal
            {
                CodVenda = 652481,
                DestinatarioRemetente = "Luís Sebastião Martin Galvão",
                DtEmissao = new DateTime(2020, 10, 15),
                DtSaidaEntrada = new DateTime(2020, 10, 15),
                NumNota = 5451255
            };
            context.NotasFiscais.Add(notaFiscal);
            await context.SaveChangesAsync();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<NotaFiscal, NotaFiscalDTO>());
            var mapper = config.CreateMapper();

            // retrieving data
            var notaFiscalService = new NotaFiscalService(context, mapper);
            var result = notaFiscalService.GetNotasFiscais(10);

            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().CodVenda.Should().Be(notaFiscal.CodVenda);

            await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async void ShouldReturnEmpty()
        {
            CSUContext context = await InitializeDatabase();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<NotaFiscal, NotaFiscalDTO>());
            var mapper = config.CreateMapper();

            // retrieving data
            var notaFiscalService = new NotaFiscalService(context, mapper);
            var result = notaFiscalService.GetNotasFiscais(10);

            result.Should().NotBeNull();
            result.Should().BeEmpty();

            await context.Database.EnsureDeletedAsync();
        }

        private static async Task<CSUContext> InitializeDatabase()
        {
            // initializing db
            var testConnectionString = "Server=(localdb)\\mssqllocaldb;Database=CSU.EtapaTecnica.TESTS.PHGM;Trusted_Connection=True;MultipleActiveResultSets=true";
            var dbOptions = new DbContextOptionsBuilder<CSUContext>()
                .UseSqlServer(testConnectionString)
                .UseUpperCaseNamingConvention()
                .Options;
            var context = new CSUContext(dbOptions);
            await context.Database.EnsureCreatedAsync();
            await context.CreateProcedure();
            return context;
        }
    }
}
