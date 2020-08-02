using System;
using System.Collections.Generic;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;
using vkontakteoknomusic_2.Models;

namespace vkontakteoknomusic_2.Message
{
    public static class MessageSender
    {
        public static bool Send(IVkApi vkApi, Command command, long? peerId)
        {
            try
            {
                vkApi.Messages.Send(new MessagesSendParams
                {
                    RandomId = new DateTime().Millisecond,
                    PeerId = peerId,
                    Message = command.Answer,
                    Keyboard = GetMessageKeyboard(command.ButtonNames),
                    Attachments = command.Attachments
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static MessageKeyboard GetMessageKeyboard(IEnumerable<string> names)
        {
            var builder = new KeyboardBuilder();
            var i = 0;
            foreach(var n in names)
            {
                i++;
                if(n == "FAQ")
                    builder.AddButton(n, "", KeyboardButtonColor.Default);
                else if(n == "Назад")
                    builder.AddButton(n, "", KeyboardButtonColor.Negative);
                else
                    builder.AddButton(n, "", KeyboardButtonColor.Positive);
                if(i % 2 == 0)
                    builder.AddLine();
            }
            var button = builder.Build();
            return button;
        }
    }
}