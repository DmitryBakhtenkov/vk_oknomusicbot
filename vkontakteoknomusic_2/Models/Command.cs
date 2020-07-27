using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using VkNet.Model.Attachments;

namespace vkontakteoknomusic_2.Models
{
    public class Command
    {
        [BsonId]
        public ObjectId Id { get; set; }
        /// <summary>
        /// Текст, который запускает команду
        /// </summary>
        public string Trigger { get; set; }
        /// <summary>
        /// Ответ бота на команду
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// Список названий кнопок для ответа
        /// </summary>
        public IEnumerable<string> ButtonNames { get; set; }
        /// <summary>
        /// Список вложений для ответа
        /// </summary>
        public IEnumerable<MediaAttachment> Attachments { get; set; }
    }
}