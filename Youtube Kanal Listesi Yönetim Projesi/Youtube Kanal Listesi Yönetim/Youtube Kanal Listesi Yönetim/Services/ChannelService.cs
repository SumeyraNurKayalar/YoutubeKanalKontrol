using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Youtube_Kanal_Listesi_Yönetim.Data;
using Youtube_Kanal_Listesi_Yönetim.Models;
using Channel = Youtube_Kanal_Listesi_Yönetim.Models.Channel;

namespace Youtube_Kanal_Listesi_Yönetim.Services
{
    public class ChannelService : IChannelService
    {
        private readonly IAppDbContext _context;

        public ChannelService(IAppDbContext context)
        {
            _context = context;
        }

        public Task<List<Channel>> GetAllChannelsAsync()
        {
            return Task.FromResult(_context.GetAllChannels().Cast<Channel>().ToList());
        }

        public Task<List<Channel>> SearchChannelsAsync(string searchTerm)
        {
            var filtered = _context.GetAllChannels()
                .Cast<Channel>()
                .Where(c => c.ChannelName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Task.FromResult(filtered);
        }

        public Task<List<Channel>> FilterChannelsByCategoryAsync(string category)
        {
            var filtered = _context.GetAllChannels()
                .Cast<Channel>()
                .Where(c => c.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Task.FromResult(filtered);
        }

        public Task<List<Channel>> SortChannelsBySubscribersAsync(bool descending = false)
        {
            var channels = _context.GetAllChannels().Cast<Channel>().ToList();

            var sorted = descending
                ? channels.OrderByDescending(c => int.Parse(c.Subscribers)).ToList()
                : channels.OrderBy(c => int.Parse(c.Subscribers)).ToList();

            return Task.FromResult(sorted);
        }

        public Task<bool> ExportChannelsToFileAsync(string filePath, string format)
        {
            try
            {
                var channels = _context.GetAllChannels().Cast<Channel>().ToList();

                if (format.ToLower() == "csv")
                {
                    var lines = new List<string> { "ChannelId,ChannelName,Category,Subscribers,CreatedAt,IsActive" };
                    lines.AddRange(channels.Select(c =>
                        $"{c.ChannelId},{c.ChannelName},{c.Category},{c.Subscribers},{c.CreatedAt:yyyy-MM-dd},{c.IsActive}"));

                    File.WriteAllLines(filePath, lines);
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }

    public interface IChannelService
    {
        Task<List<Channel>> GetAllChannelsAsync();
        Task<List<Channel>> SearchChannelsAsync(string searchTerm);
        Task<List<Channel>> FilterChannelsByCategoryAsync(string category);
        Task<List<Channel>> SortChannelsBySubscribersAsync(bool descending);
        Task<bool> ExportChannelsToFileAsync(string filePath, string format);
    }
}
