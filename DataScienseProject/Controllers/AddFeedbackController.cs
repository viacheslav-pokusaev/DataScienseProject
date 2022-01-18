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
        private readonly IAuthorizationService _authorizationService;
        public AddFeedbackController(IAddFeedbackService addFeedbackService, IAuthorizationService authorizationService)
        {
            _addFeedbackService = addFeedbackService;
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [Route("add")]
        public FeedbackModel Add([FromBody] FeedbackModel feedback)
        {
            _authorizationService.UpdateCookie(HttpContext);
            return _addFeedbackService.AddFeedback(feedback);
        }

    }
}
