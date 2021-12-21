using DataScienseProject.Interfaces;
using DataScienseProject.Models.Feedback;
using Microsoft.AspNetCore.Mvc;


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
        public FeedbackModel Add([FromBody]FeedbackModel feedback)
        {
            return _addFeedbackService.AddFeedback(feedback);
        }

    }
}
