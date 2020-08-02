using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        private readonly Repository _repo;

        public SettingsController(Repository repo)
        {
            if (repo != null)
                _repo = repo;
        }

        /// <summary>
        /// Метод post, создающий команду
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateCommand([FromForm] Command command)
        {
            if (await _repo.CreateCommandAsync(command))
                return Ok("Ok");
            else
                return BadRequest();
        }
        /// <summary>
        /// Метод get, возвращающий список всех команд
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetListCommands()
        {
            var result = await _repo.GetAllAsync();
            return Ok(result);
        }
        /// <summary>
        /// Метод get, возвращающий команду по триггеру
        /// </summary>
        [HttpGet("{trigger}")]
        public async Task<IActionResult> GetCommandByTrigger(string trigger)
        {
            return Ok(await _repo.GetByTriggerAsync(trigger));
        }
        /// <summary>
        /// Метод put, изменяющий команду в списке существующих команд
        /// </summary>
        [HttpPut("{trigger}")]
        public async Task<IActionResult> UpdateCommand(string trigger, [FromBody] Command command)
        {
            if(await _repo.UpdateCommandAsync(trigger, command))
                return Ok("Ok");
            else
                return BadRequest();
        }
        /// <summary>
        /// Метод delete, удаляющий команду с определённым триггером
        /// </summary>>
        [HttpDelete("{trigger}")]
        public async Task<IActionResult> DeleteCommand(string trigger)
        {
            var command = await _repo.GetByTriggerAsync(trigger);
            if (command != null)
            {
                if (await _repo.DeleteCommandAsync(command))
                    return Ok("Ok");
            }
            return BadRequest();
        }
        
    }
}