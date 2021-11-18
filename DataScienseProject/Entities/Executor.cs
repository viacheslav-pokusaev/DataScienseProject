using System;
using System.Collections.Generic;

#nullable disable

namespace DataScienseProject.Entities
{
    public partial class Executor
    {
        public Executor()
        {
            ViewExecutors = new HashSet<ViewExecutor>();
        }

        public int ExecutorKey { get; set; }
        public string ExecutorName { get; set; }
        public string ExecutorProfileLink { get; set; }

        public virtual ICollection<ViewExecutor> ViewExecutors { get; set; }
    }
}
