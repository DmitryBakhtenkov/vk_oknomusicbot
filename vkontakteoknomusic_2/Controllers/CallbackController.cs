using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.GroupUpdate;
using VkNet.Utils;
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
        
        public CallbackController(IVkApi vkApi, IConfiguration configuration)
        {
            _vkApi = vkApi;
            _configuration = configuration;
        }
        /// <summary>
        /// Логика обработки запросов
        /// </summary>
        [HttpPost]
        public IActionResult Callback([FromBody] object param)
        {
            var updates = JsonConvert
                .DeserializeObject<Updates>(param.ToString() ?? throw new InvalidOperationException("Сервер не смог обработать запрос"));
            switch (updates.Type)
            {     
                case "confirmation":
                    return Ok(_configuration["Config:Confirmation"]);
                case "message_new":
                    //TODO: message logic
                    break;
                case "group_join":
                    //TODO: group join logic
                    break;
            }
            return Ok("ok");
        }
    }
    
}