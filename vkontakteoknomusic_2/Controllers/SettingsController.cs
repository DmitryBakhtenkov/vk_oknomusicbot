using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
        public IActionResult CreateCommand([FromForm] Command command)
        {
            if (CommandContext.AddCommand(command))
                return Ok("Ok");
            else
                return BadRequest();
        }
        /// <summary>
        /// Метод get, возвращающий список всех команд
        /// </summary>
        [HttpGet]
        public IActionResult GetListCommands()
        {
            var commands = CommandContext.GetContext();
            return Ok(commands);
        }
        /// <summary>
        /// Метод get, возвращающий команду по триггеру
        /// </summary>
        [HttpGet("{trigger}")]
        public IActionResult GetCommandByTrigger(string trigger)
        {
            var command = CommandContext.GetContext().SingleOrDefault(c => c.Trigger == trigger);
            return Ok(command);
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
            var command = CommandContext.GetContext().SingleOrDefault(c => c.Trigger == trigger);
            if (CommandContext.DeleteCommand(command))
                return Ok();
            else
                return BadRequest();
        }
        
    }
}