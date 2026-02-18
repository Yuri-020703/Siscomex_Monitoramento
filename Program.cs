using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SiscomexMonitor
{
    class Program
    {
        // Configurações
        private static string localFolder = @"C:\SiscomexMonitor";

        // APIs para monitorar
        private static List<ApiMonitor> apis = new()
        {
            new ApiMonitor("CATP", "https://docs.portalunico.siscomex.gov.br/api/catp/catp.json"),
            new ApiMonitor("DUEX", "https://docs.portalunico.siscomex.gov.br/api/duex/duex.json"),
            new ApiMonitor("Autenticação", "https://docs.portalunico.siscomex.gov.br/api/plat/plat-auth.json")
        };

        // Configurações do e-mail
        private static string smtpHost = "-----";

        private static int smtpPort = 587;
        private static bool EnableSsl = true;
        private static string smtpUser = "yuri.aquino@e-it.net";
        private static string smtpPass = "senhasegura";
        private static string emailFrom = "yuri.aquino@e-it.net";
        private static string emailTo = "yuri.aquino@e-it.net";


        static async Task Main(string[] args)
        {
            try
            {
                Directory.CreateDirectory(localFolder);
                using HttpClient client = new HttpClient();

                List<string> changedApis = new List<string>();

                foreach (var api in apis)
                {
                    string localFile = Path.Combine(localFolder, api.Name + ".json");
                    Console.WriteLine($"Baixando {api.Name}...");
                    string content = await client.GetStringAsync(api.Url);
                    string newHash = ComputeHash(content);
                    string oldHash = null;

                    if (File.Exists(localFile))
                    {
                        oldHash = ComputeHash(File.ReadAllText(localFile));
                    }

                    if (oldHash != newHash)
                    {
                        changedApis.Add(api.Name);
                        File.WriteAllText(localFile, content);
                        Console.WriteLine($"Alteração detectada em {api.Name} e arquivo local atualizado.");
                    }
                    else
                    {
                        Console.WriteLine($"Nenhuma alteração em {api.Name}.");
                    }
                }

                if (changedApis.Count > 0)
                {
                    string subject = "Atualizações detectadas nas APIs Siscomex";
                    string body = "As seguintes APIs tiveram alterações:\n\n" + string.Join("\n", changedApis);
                    bool emailSent = SendEmail(subject, body);
                    if (emailSent) Console.WriteLine("E-mail enviado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Nenhuma alteração detectada em nenhuma API.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }


        }

        static string ComputeHash(string content)
        {
            using MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            byte[] hash = md5.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "");
        }

        static bool SendEmail(string subject, string body)
        {
            try
            {
                using MailMessage mail = new MailMessage(emailFrom, emailTo, subject, body);
                using SmtpClient client = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = true
                };
                client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
                return false;
            }
        }
    }
}

