using Microsoft.AspNetCore.Mvc;
using Youtube_Kanal_Listesi_Yönetim.Models;
using Youtube_Kanal_Listesi_Yönetim.Services;

namespace Youtube_Kanal_Listesi_Yönetim.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChannelController : ControllerBase
    {
        private readonly Youtube_Kanal_Listesi_Yönetim.Services.IChannelService _channelService;

        public ChannelController(Youtube_Kanal_Listesi_Yönetim.Services.IChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Channel>>> GetAllChannels()
        {
            var channels = await _channelService.GetAllChannelsAsync();
            return Ok(channels);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Channel>>> SearchChannels([FromQuery] string term)
        {
            var channels = await _channelService.SearchChannelsAsync(term);
            return Ok(channels);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<Channel>>> FilterByCategory([FromQuery] string category)
        {
            var channels = await _channelService.FilterChannelsByCategoryAsync(category);
            return Ok(channels);
        }

        [HttpGet("sort")]
        public async Task<ActionResult<List<Channel>>> SortBySubscribers([FromQuery] bool descending = false)
        {
            var channels = await _channelService.SortChannelsBySubscribersAsync(descending);
            return Ok(channels);
        }
    }
}
