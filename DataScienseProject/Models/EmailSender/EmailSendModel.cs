using DataScienseProject.Models.Authorize;
using System;

namespace DataScienseProject.Models.EmailSender
{
    public class EmailSendModel : AuthorizeModel
    {
        public DateTime EnterTime { get; set; }
    }
}
