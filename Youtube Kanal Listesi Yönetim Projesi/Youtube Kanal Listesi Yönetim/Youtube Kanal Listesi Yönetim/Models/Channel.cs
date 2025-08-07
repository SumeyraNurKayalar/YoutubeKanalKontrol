using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Youtube_Kanal_Listesi_Yönetim.Models
{
    public class Channel
    {
        public bool IsActive { get; set; }
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string Category { get; set; }
        public string Subscribers { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
