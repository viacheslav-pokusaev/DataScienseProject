using DataScienseProject.Models.Authorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Models.EmailSender
{
    public class EmailSendModel: AuthorizeModel
    {
        public DateTime EnterTime { get; set; }
    }
}
