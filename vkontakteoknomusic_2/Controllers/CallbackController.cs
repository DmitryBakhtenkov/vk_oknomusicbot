using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VkNet.Abstractions;
using VkNet.Utils;
using vkontakteoknomusic_2.Message;
using vkontakteoknomusic_2.Models;

namespace vkontakteoknomusic_2.Controllers
{
    /// <summary>
    /// Контроллер для обработки запросов от вк
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CallbackController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IVkApi _vkApi;
        private readonly Repository _repo = new Repository();
        
        public CallbackController(IVkApi vkApi, IConfiguration configuration)
        {
            _vkApi = vkApi;
            _configuration = configuration;
        }
        /// <summary>
        /// Логика обработки запросов
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Callback([FromBody] object param)
        {
            var updates = JsonConvert
                .DeserializeObject<Updates>(param.ToString() ?? throw new InvalidOperationException("Сервер не смог обработать запрос"));
            switch (updates.Type)
            {     
                case "confirmation":
                    return Ok(_configuration["Config:Confirmation"]);
                case "message_new":
                    var message = VkNet.Model.Message.FromJson(new VkResponse(updates.Object));
                    var command = await _repo.GetCommandByTriggerAsync(message.Text);
                    if (command != null)
                    {
                        MessageSender.Send(_vkApi, command, message.PeerId);
                    }
                    break;
                case "group_join":
                    //TODO: group join logic
                    break;
            }
            return Ok("ok");
        }
    }
    
}