using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using vkontakteoknomusic_2;
using vkontakteoknomusic_2.Controllers;
using vkontakteoknomusic_2.Models;

namespace NUnitTestProject
{
    public class TestGetAllCommands
    {
        private List<Command> _commands;
        [SetUp]
        public void Setup()
        {
            _commands = new List<Command>
            {
                new Command {Trigger = "Начать", Answer = "Выберите услугу", ButtonNames = new []{"Дистрибуция", "FAQ"}},
                new Command {Trigger = "FAQ", Answer = "[FAQ Link]", ButtonNames = new []{"Назад"}}
            };
        }

        [Test]
        public async Task TestGetAll()
        {
            var mock = new Mock<Repository>();
            mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_commands);
            var controller = new SettingsController(mock.Object);

            var okObject = (OkObjectResult) await controller.GetListCommands();
            var result = (List<Command>) okObject.Value;
            Assert.AreEqual(_commands.Count, result.Count);
        }
    }
}