
using DataScienseProject.Models.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Interfaces
{
    public interface IAddFeedbackService
    {
        Feedback AddFeedback(Feedback feedback);
    }
}
