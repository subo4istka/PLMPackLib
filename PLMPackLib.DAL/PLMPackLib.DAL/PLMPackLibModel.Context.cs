﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PLMPackLibDb : DbContext
    {
        public PLMPackLibDb()
            : base("name=PLMPackLibDb")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CardboardFormat> CardboardFormats { get; set; }
        public DbSet<CardboardProfile> CardboardProfiles { get; set; }
        public DbSet<CardboardQuality> CardboardQualities { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Majoration> Majorations { get; set; }
        public DbSet<MajorationSet> MajorationSets { get; set; }
        public DbSet<ParamDefaultComponent> ParamDefaultComponents { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<TreeNode> TreeNodes { get; set; }
        public DbSet<TreeNodeDocument> TreeNodeDocuments { get; set; }
    }
}