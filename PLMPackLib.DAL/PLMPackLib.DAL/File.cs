//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PLMPackLib.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class File
    {
        public File()
        {
            this.Documents = new HashSet<Document>();
            this.Thumbnails = new HashSet<Thumbnail>();
        }
    
        public int ID { get; set; }
        public System.Guid Guid { get; set; }
        public string Extension { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Thumbnail> Thumbnails { get; set; }
    }
}