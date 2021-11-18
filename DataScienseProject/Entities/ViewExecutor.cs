using System;
using System.Collections.Generic;

#nullable disable

namespace DataScienseProject
{
    public partial class ViewExecutor
    {
        public long ViewExecutorKey { get; set; }
        public long? ViewKey { get; set; }
        public int? ExecutorKey { get; set; }
        public int? ExecutorRoleKey { get; set; }
        public int? OrderNumber { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Executor ExecutorKeyNavigation { get; set; }
        public virtual ExecutorRole ExecutorRoleKeyNavigation { get; set; }
        public virtual View ViewKeyNavigation { get; set; }
    }
}
