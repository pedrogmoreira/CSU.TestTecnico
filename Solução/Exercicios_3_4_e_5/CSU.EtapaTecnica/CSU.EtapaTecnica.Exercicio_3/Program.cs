using CSU.EtapaTecnica.Data.Context;
using CSU.EtapaTecnica.Domain.DTO;
using CSU.EtapaTecnica.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Exercicio_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            Console.Write("Digite o numero do mes a ser consultado: ");
            var rawValue = Console.ReadLine();
            int mes = 0;

            if (int.TryParse(rawValue, out mes))
            {
                if (mes < 0 || mes > 12)
                {
                    Console.WriteLine("Número fornecido é inválido.");
                }
                else
                {
                    using CSUContext context = await GetContext();

                    var result = context.NotasFiscais
                        .FromSqlRaw($"EXEC GetNotasFiscaisPorMes {mes}");

                    var resultXmlContent = ToXML(GetNotaFiscalDTO(result));

                    var xmlFileDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"OUTPUT");
                    var fileName = $"{ DateTime.UtcNow.ToString("yyyyMMdd_hhmmss")}_notas_ficais.xml";
                    var xmlFilePath = Path.Combine(xmlFileDirectory, fileName);

                    if (!Directory.Exists(xmlFileDirectory))
                    {
                        Directory.CreateDirectory(xmlFileDirectory);
                    }

                    await File.WriteAllTextAsync(xmlFilePath, resultXmlContent);

                    Console.WriteLine("Arquivo criado com sucesso!");
                    Console.WriteLine($"\nFilename: {fileName}\nFile Location: {xmlFileDirectory}");
                }
            }
            else
            {
                Console.WriteLine("Insira apenas numeros.");
            }
        }

        private async static Task<CSUContext> GetContext()
        {
            var connectionstring = "Server=(localdb)\\mssqllocaldb;Database=CSU.EtapaTecnica.PHGM;Trusted_Connection=True;MultipleActiveResultSets=true";

            var optionsBuilder = new DbContextOptionsBuilder<CSUContext>();
            optionsBuilder.UseSqlServer(connectionstring);
            CSUContext context = new CSUContext(optionsBuilder.Options);

            await context.CreateDBMigrateAndSeedData();

            return context;
        }

        private static string ToXML(IEnumerable<NotaFiscalDTO> obj)
        {
            using StringWriter stringWriter = new StringWriter(new StringBuilder());
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<NotaFiscalDTO>), new XmlRootAttribute("NotasFiscais"));
            xmlSerializer.Serialize(stringWriter, obj);
            return stringWriter.ToString();
        }

        private static IEnumerable<NotaFiscalDTO> GetNotaFiscalDTO(IEnumerable<NotaFiscal> nfs)
        {
            var result = new List<NotaFiscalDTO>();

            foreach (var item in nfs)
            {
                result.Add(new NotaFiscalDTO
                {
                    CodNota = item.CodNota,
                    CodVenda = item.CodVenda,
                    DestinatarioRemetente = item.DestinatarioRemetente,
                    DtEmissao = item.DtEmissao,
                    DtSaidaEntrada = item.DtSaidaEntrada,
                    NumeroRecibo = item.NumeroRecibo,
                    NumNota = item.NumNota,
                    TransFrete = item.TransFrete,
                    ValorTotalNota = item.ValorTotalNota,
                    ValorTotalProdutos = item.ValorTotalProdutos
                });
            }

            return result;
        }
    }
}
