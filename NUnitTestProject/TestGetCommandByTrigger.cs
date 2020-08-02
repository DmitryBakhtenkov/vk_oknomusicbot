using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using vkontakteoknomusic_2;
using vkontakteoknomusic_2.Controllers;
using vkontakteoknomusic_2.Models;

namespace NUnitTestProject
{
    class TestGetCommandByTrigger
    {
        private List<Command> _currentCommands;
        private Mock<Repository> _mock;
        private Command _normalCommand;
        private Command _notExistCommand;

        [SetUp]
        public void Setup()
        {
            _normalCommand = new Command
            {
                Trigger = "Начать",
                Answer = "Выберите услугу",
                ButtonNames = new[] { "Дистрибуция", "FAQ" }
            };
            _notExistCommand = new Command
            {
                Trigger = "Назад",
                Answer = "Выберите услугу",
                ButtonNames = new[] { "Дистрибуция", "FAQ" }
            };
            _currentCommands = new List<Command>
            {
                new Command {Trigger = "Начать", Answer = "Выберите услугу", ButtonNames = new []{"Дистрибуция", "FAQ"}},
                new Command {Trigger = "FAQ", Answer = "[FAQ Link]", ButtonNames = new []{"Назад"}}
            };

            _mock = new Mock<Repository>();
        }

        [Test]
        public async Task TestGetByTriggerExist()
        {
            _mock.Setup(repository => repository.GetByTriggerAsync(_normalCommand.Trigger)).ReturnsAsync(
                (string trigger) =>
                {
                    var obj = _currentCommands.SingleOrDefault(c => c.Trigger == trigger);
                    return obj;
                });
            var controller = new SettingsController(_mock.Object);
            var okObject = (OkObjectResult) await controller.GetCommandByTrigger(_normalCommand.Trigger);
            var result = (Command) okObject.Value;
            Assert.AreEqual(result.Trigger, _normalCommand.Trigger);
        }

        [Test]
        public async Task TestGetByTriggerNotExist()
        {
            _mock.Setup(repository => repository.GetByTriggerAsync(_notExistCommand.Trigger)).ReturnsAsync(
                (string trigger) =>
                {
                    var obj = _currentCommands.SingleOrDefault(c => c.Trigger == trigger);
                    return obj;
                });
            var controller = new SettingsController(_mock.Object);
            var okObject = (OkObjectResult)await controller.GetCommandByTrigger(_normalCommand.Trigger);
            var result = (Command)okObject.Value;
            Assert.Null(result);
        }
    }
}
