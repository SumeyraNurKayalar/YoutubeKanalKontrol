using ChannelManagementApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChannelManagementApi.Services
{
    public interface IChannelService
    {
        Task<List<Channel>> GetAllChannelsAsync();
        Task<List<Channel>> SearchChannelsAsync(string searchTerm);
        Task<List<Channel>> FilterChannelsByCategoryAsync(string category);
        Task<List<Channel>> SortChannelsBySubscribersAsync(bool descending = false);
        Task<bool> ExportChannelsToFileAsync(string filePath, string format);
    }
}
