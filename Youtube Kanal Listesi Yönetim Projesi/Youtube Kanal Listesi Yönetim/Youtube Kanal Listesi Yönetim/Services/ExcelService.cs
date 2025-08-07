using Youtube_Kanal_Listesi_Yönetim.Models;
using OfficeOpenXml;

namespace Youtube_Kanal_Listesi_Yönetim.Services
{
    public class ExcelService
    {
        public async Task<List<Channel>> ReadChannelsFromExcel(IFormFile file)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets[0];

            var channels = new List<Channel>();
            var rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                channels.Add(new Channel
                {
                    ChannelId = worksheet.Cells[row, 1].Text,
                    ChannelName = worksheet.Cells[row, 2].Text,
                    Category = worksheet.Cells[row, 3].Text,
                    Subscribers = worksheet.Cells[row, 4].Text,
                    IsActive = bool.Parse(worksheet.Cells[row, 5].Text)
                });
            }
                return channels;
            }
        public async Task<byte[]> WriteChannelsToExcel(List<Channel> channels)
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Channels");

            worksheet.Cells[1, 1].Value = "ChannelId";
            worksheet.Cells[1, 2].Value = "ChannelName";
            worksheet.Cells[1, 3].Value = "Category";
            worksheet.Cells[1, 4].Value = "Subscribers";
            worksheet.Cells[1, 5].Value = "IsActive";

            int row = 2;
            foreach (var channel in channels)
            {
                worksheet.Cells[row, 1].Value = channel.ChannelId;
                worksheet.Cells[row, 2].Value = channel.ChannelName;
                worksheet.Cells[row, 3].Value = channel.Category;
                worksheet.Cells[row, 4].Value = channel.Subscribers;
                worksheet.Cells[row, 5].Value = channel.IsActive;
                row++;
            }

            return await package.GetAsByteArrayAsync();
        }

    }
}

