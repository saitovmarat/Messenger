using System;

namespace Messenger.UI.Schemas;

public class MessageSchema
{
    public string? Id { get; set; }
    public string? SenderId { get; set; }
    public UserSchema? Sender { get; set; }  
    public string? Text { get; set; }
    public DateTime? MessageSentTime { get; set; }
}
