using System.Collections.Generic;

#nullable disable

namespace DataScienseProject.Entities
{
    public partial class Element
    {
        public Element()
        {
            ElementParameters = new HashSet<ElementParameter>();
            ViewElements = new HashSet<ViewElement>();
        }

        public long ElementKey { get; set; }
        public int ElementTypeKey { get; set; }
        public int? LinkTypeKey { get; set; }
        public string ElementName { get; set; }
        public string Path { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public bool? IsShowElementName { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ElementType ElementTypeKeyNavigation { get; set; }
        public virtual LinkType LinkTypeKeyNavigation { get; set; }
        public virtual ICollection<ElementParameter> ElementParameters { get; set; }
        public virtual ICollection<ViewElement> ViewElements { get; set; }
    }
}
