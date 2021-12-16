using DataScienseProject.Context;
using DataScienseProject.Interfaces;
using DataScienseProject.Models;
using DataScienseProject.Models.Feedback;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataScienseProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddFeedbackController : ControllerBase
    {
        private readonly IAddFeedbackService _addFeedbackService;        
        public AddFeedbackController(IAddFeedbackService addFeedbackService)
        {
            _addFeedbackService = addFeedbackService;
        }

        [HttpPost]
        [Route("add")]
        public Feedback Add(Feedback feedback)
        {
            return _addFeedbackService.AddFeedback(feedback);
        }

    }
}
