namespace DataScienseProject.Models.Gallery
{
    public class GroupDataSelectModel
    {
        public string ViewName  { get; set; }
        public int? OrderNumber { get; set; }
        public long ViewKey { get; set; }
        public string TagName { get; set; }
        public bool? IsViewDeleted { get; set; }
        public int? ExecutorKey { get; set; }
        public string ExecutorName { get; set; }

        //gv start
        public string GroupName { get; set; }
        public bool? IsGroupDeleted { get; set; }
        //gv end
}
}
