using System.Collections.Generic;
using System.Linq;
using vkontakteoknomusic_2.Models;

namespace vkontakteoknomusic_2
{
    /// <summary>
    /// Контекст всех команд
    /// </summary>
    public class CommandContext
    {
        /// <summary>
        /// Список всех существующих команд
        /// </summary>
        private static List<Command> _commands;

        /// <summary>
        /// Синглтон, возвращающий список
        /// </summary>
        /// <returns></returns>
        public static List<Command> GetContext()
        {
            if (_commands == null)
            {
                _commands = new List<Command>();
            }
            return _commands;
        }
        /// <summary>
        /// Логика добавления команды
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Возвращает false, если команда с таким триггером уже есть</returns>
        public static bool AddCommand(Command command)
        {
            command.ButtonNames = command.ButtonNames.TakeWhile(c => c != null);
            if (_commands.SingleOrDefault(c => c.Trigger == command.Trigger) != null)
                return false;
            else
                _commands.Add(command);
            return true;
        }
        /// <summary>
        /// Удаляет выбранную команду
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static bool DeleteCommand(Command command)
        {
            if(_commands.Contains(command))
            {
                _commands.Remove(command);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}