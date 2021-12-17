using DataScienseProject.Models.Feedback;

namespace DataScienseProject.Interfaces
{
    public interface IAddFeedbackService
    {
        FeedbackModel AddFeedback(FeedbackModel feedback);
    }
}
