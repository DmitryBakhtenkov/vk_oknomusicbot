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
    class TestCreateCommand
    {
        private Command _normalCommand, _existCommand, _nullCommand;
        private List<Command> _currentCommands;
        private Mock<Repository> _mock;

        [SetUp]
        public void Setup()
        {
            _normalCommand = new Command
            {
                Trigger = "Назад",
                Answer = "Выберите услугу",
                ButtonNames = new []{"Дистрибуция", "FAQ"}
            };
            _existCommand = new Command
            {
                Trigger = "Начать",
                Answer = "Выберите услугу",
                ButtonNames = new[] { "Дистрибуция", "FAQ" }
            };
            _nullCommand = new Command
            {
                Trigger = null,
                Answer = null,
                ButtonNames = null
            };
            _currentCommands = new List<Command>
            {
                new Command {Trigger = "Начать", Answer = "Выберите услугу", ButtonNames = new []{"Дистрибуция", "FAQ"}},
                new Command {Trigger = "FAQ", Answer = "[FAQ Link]", ButtonNames = new []{"Назад"}}
            };

            _mock = new Mock<Repository>();
        }

        [Test]
        public async Task TestNormalCreate()
        {
            _mock.Setup(r => r.CreateCommandAsync(_normalCommand)).ReturnsAsync((Command command) =>
            {
                var obj = _currentCommands.SingleOrDefault(c => c.Trigger == command.Trigger);
                return obj == null;
            });
            var controller = new SettingsController(_mock.Object);
            var result = await controller.CreateCommand(_normalCommand);
            Assert.IsInstanceOf<OkObjectResult>(result);
            _mock.Verify(r => r.CreateCommandAsync(_normalCommand));
        }

        [Test]
        public async Task TestExistsCreate()
        {
            _mock.Setup(r => r.CreateCommandAsync(_existCommand)).ReturnsAsync((Command command) =>
            {
                var obj = _currentCommands.SingleOrDefault(c => c.Trigger == command.Trigger);
                return obj == null;
            });
            var controller = new SettingsController(_mock.Object);
            var result = await controller.CreateCommand(_existCommand);
            Assert.IsInstanceOf<BadRequestResult>(result);
            _mock.Verify(r => r.CreateCommandAsync(_existCommand));
        }
        [Test]
        public async Task TestNullCreate()
        {
            _mock.Setup(r => r.CreateCommandAsync(_nullCommand)).ReturnsAsync((Command command) =>
            {
                if (command.Trigger == null) 
                    return false;
                var obj = _currentCommands.SingleOrDefault(c => c.Trigger == command.Trigger);
                return obj == null;
            });
            var controller = new SettingsController(_mock.Object);
            var result = await controller.CreateCommand(_nullCommand);
            Assert.IsInstanceOf<BadRequestResult>(result);
            _mock.Verify(r => r.CreateCommandAsync(_nullCommand));
        }

    }
}
