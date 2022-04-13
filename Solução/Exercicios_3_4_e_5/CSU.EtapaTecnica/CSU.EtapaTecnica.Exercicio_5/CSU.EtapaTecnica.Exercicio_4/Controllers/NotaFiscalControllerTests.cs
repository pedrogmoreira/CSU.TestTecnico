using CSU.EtapaTecnica.Business;
using System;
using Xunit;
using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using CSU.EtapaTecnica.Exercicio_4;
using Microsoft.Extensions.DependencyInjection;
using CSU.EtapaTecnica.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;
using CSU.EtapaTecnica.Domain.Models;

namespace CSU.EtapaTecnica.Exercicio_5.CSU.EtapaTecnica.Exercicio_4.Controllers
{
    public class NotaFiscalControllerTests : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Program> _application;

        public NotaFiscalControllerTests()
        {
            _application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddControllers();
                        services.AddDbContext<CSUContext>(options =>
                          options
                            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CSU.EtapaTecnica.TESTS.PHGM;Trusted_Connection=True;MultipleActiveResultSets=true")
                            .UseUpperCaseNamingConvention());
                        services.ConfigureServices();
                    });
                });

            _httpClient = _application.CreateClient();
        }

        [Fact]
        public async void ShoudGetNoContent()
        {
            var result = await _httpClient.GetAsync("api/NotaFiscal/NotasFicais/1");
            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async void ShoudGetBadRequest()
        {
            var result = await _httpClient.GetAsync("api/NotaFiscal/NotasFicais/16");
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void ShoudGetData()
        {
            using var scope = _application.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<CSUContext>();

            //Arrange
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

            var result = await _httpClient.GetAsync("api/NotaFiscal/NotasFicais/10");
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public void Dispose()
        {
            _httpClient.Dispose();

            using var scope = _application.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<CSUContext>();
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
