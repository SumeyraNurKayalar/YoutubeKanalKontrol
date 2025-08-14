using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Filters;
using Youtube_Kanal_Listesi_Yönetim.Models;

namespace Youtube_Kanal_Listesi_Yönetim.Examples
{
    public class ChannelListExample : IExamplesProvider<List<Channel>>
    {
        public List<Channel> GetExamples()
        {
            return new List<Channel>
            {
                new Channel
                {
                    IsActive = true,
                    ChannelId = "UC999999",
                    ChannelName = "Deneme 1",
                    Category = "Yemek",
                    Subscribers = "5000",
                    CreatedAt = DateTime.UtcNow
                }
            };
        }
    }
}
