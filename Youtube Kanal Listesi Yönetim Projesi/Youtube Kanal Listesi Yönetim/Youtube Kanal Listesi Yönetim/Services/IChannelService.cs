using System.Collections.Generic;
using System.Threading.Tasks;

namespace Youtube_Kanal_Listesi_Yönetim.Models
{
    public interface IChannelService
    {
        List<Channel> GetAllChannels();
        void AddChannel(Channel channel);
        Task<List<Channel>> GetAllChannelsAsync();
        Task<List<Channel>> SearchChannelsAsync(string searchTerm);
        Task<List<Channel>> FilterChannelsByCategoryAsync(string category);
        Task<List<Channel>> SortChannelsBySubscribersAsync(bool descending = false);
        Task<bool> ExportChannelsToFileAsync(string filePath, string format);
    }
}
