using System.Threading.Tasks;
using NUnit.Framework;
using vkontakteoknomusic_2;

namespace NUnitTestProject
{
    public class Tests
    {
        private Repository _repo;
        [SetUp]
        public void Setup()
        {
            _repo = new Repository();
        }

        [Test]
        public async Task GetAllNotesTest()
        {
            var notes = await _repo.GetAllAsync();
            Assert.NotNull(notes);
        }

        [Test]
        public async Task GetNoteByTriggerTest()
        {
            var note = await _repo.GetCommandByTriggerAsync("FAQ");
            Assert.NotNull(note);
        }
    }
}