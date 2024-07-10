using System;

namespace Messenger.UI.Schemas;

public class ChatInfoSchema
{
    public string? Id { get; set; }
    public string? ChatName { get; set; }
    public string? LastMessageId { get; set; }
    public MessageSchema? LastMessage{ get; set; } 
}
