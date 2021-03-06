#nullable disable

namespace DataScienseProject.Entities
{
    public partial class Feedback
    {
        public long FeedbackKey { get; set; }
        public long? VisitKey { get; set; }
        public long? ViewKey { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }

        public virtual View ViewKeyNavigation { get; set; }
        public virtual VisitLog VisitKeyNavigation { get; set; }
    }
}
