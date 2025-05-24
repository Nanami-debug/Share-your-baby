using HandyControl.Data;
using PropertyChanged;
using System;

namespace Form.Models
{
    [AddINotifyPropertyChangedInterface]
    public class ChatViewModel
    {
        public string Msg { get; set; }
        public ChatRoleType Role { get; set; }
        public DateTime SendTime { get; set; }
    }
}