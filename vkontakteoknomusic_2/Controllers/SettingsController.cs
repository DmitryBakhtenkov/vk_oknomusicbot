using Microsoft.AspNetCore.Mvc;
using vkontakteoknomusic_2.Models;

namespace vkontakteoknomusic_2.Controllers
{
    /// <summary>
    /// Контроллер для настройки бота
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        /// <summary>
        /// Метод post, создающий команду
        /// </summary>
        [HttpPost]
        public IActionResult CreateCommand([FromBody] Command command)
        {
            //TODO: Creating command logic
            return Ok();
        }
        /// <summary>
        /// Метод get, возвращающий список всех команд
        /// </summary>
        [HttpGet]
        public IActionResult GetListCommands()
        {
            //TODO: Creating GET command
            return Ok();
        }
        /// <summary>
        /// Метод get, возвращающий команду по триггеру
        /// </summary>
        [HttpGet("{trigger}")]
        public IActionResult GetCommandGyTrigger(string trigger)
        {
            //TODO: logic Get by trigger
            return Ok();
        }
        /// <summary>
        /// Метод put, изменяющий команду в списке существующих команд
        /// </summary>
        [HttpPut]
        public IActionResult UpdateCommand(Command command)
        {
            //TODO: logic for update method
            return Ok();
        }
        /// <summary>
        /// Метод delete, удаляющий команду с определённым триггером
        /// </summary>>
        [HttpDelete("{trigger}")]
        public IActionResult DeleteCommand(string trigger)
        {
            //TODO: delete logic
            return Ok();
        }
        
    }
}