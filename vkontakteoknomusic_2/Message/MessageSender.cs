using System;
using System.Collections.Generic;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Attachments;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace vkontakteoknomusic_2.Message
{
    public static class MessageSender
    {
        public static bool Send(IVkApi vkApi, string text, long peerId, MessageKeyboard keyboard = null, IEnumerable<MediaAttachment> attachments = null)
        {
            try
            {
                vkApi.Messages.Send(new MessagesSendParams
                {
                    RandomId = new DateTime().Millisecond,
                    PeerId = peerId,
                    Message = text,
                    Keyboard = keyboard,
                    Attachments = attachments
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