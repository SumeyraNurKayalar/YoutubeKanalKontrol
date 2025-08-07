using Youtube_Kanal_Listesi_Yönetim.Models;
using CsvHelper;
using System.Globalization;
using ServiceStack.Text;

namespace Youtube_Kanal_Listesi_Yönetim.Services
{
    public class CsService
    {
        public async Task<List<Channel>> ReadChannelsFromCsv(IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);
            var channels = new List<Channel>();
            await foreach (var channel in csv.GetRecordsAsync<Channel>())
            {
                channels.Add(channel);
            }
            return channels;
        }

        public async Task<byte[]> ExportToCsv(List<Channel> channels)
        {
            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream);
            using var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture);

            await csv.WriteRecordsAsync(channels);
            await writer.FlushAsync();

            return memoryStream.ToArray();
        }
    }
}
