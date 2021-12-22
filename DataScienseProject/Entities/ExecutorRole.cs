using System.Collections.Generic;

#nullable disable

namespace DataScienseProject.Entities
{
    public partial class ExecutorRole
    {
        public ExecutorRole()
        {
            ViewExecutors = new HashSet<ViewExecutor>();
        }

        public int ExecutorRoleKey { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<ViewExecutor> ViewExecutors { get; set; }
    }
}
