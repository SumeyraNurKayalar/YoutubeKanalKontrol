using Youtube_Kanal_Listesi_Yönetim.Services;
using Youtube_Kanal_Listesi_Yönetim.Data;
namespace Youtube_Kanal_Listesi_Yönetim.Services
{
    public class ChannelService : IChannelService
    {
        private readonly AppDbContext _context;

        public ChannelService(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Channel>> GetAllChannelsAsync()
        {
            return Task.FromResult(_context.GetAllChannels());
        }

        public Task<List<Channel>> SearchChannelsAsync(string searchTerm)
        {
            var filtered = _context.GetAllChannels()
                .Where(c => c.ChannelName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Task.FromResult(filtered);
        }

        public Task<List<Channel>> FilterChannelsByCategoryAsync(string category)
        {
            var filtered = _context.GetAllChannels()
                .Where(c => c.Category == category)
                .ToList();

            return Task.FromResult(filtered);
        }

        public Task<List<Channel>> SortChannelsBySubscribersAsync(bool descending = false)
        {
            var sorted = descending
                ? _context.GetAllChannels().OrderByDescending(c => c.Subscribers).ToList()
                : _context.GetAllChannels().OrderBy(c => c.Subscribers).ToList();

            return Task.FromResult(sorted);
        }

        public Task<bool> ExportChannelsToFileAsync(string filePath, string format)
        {
            var channels = _context.GetAllChannels();

            if (format.ToLower() == "csv")
            {
                using (var writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("ChannelId,ChannelName,Category,Subscribers,CreatedAt,IsActive");

                    foreach (var c in channels)
                    {
                        writer.WriteLine($"{c.ChannelId},{c.ChannelName},{c.Category},{c.Subscribers},{c.CreatedAt},{c.IsActive}");
                    }
                }
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

    }
}


