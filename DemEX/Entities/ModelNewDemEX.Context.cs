﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemEX.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DemEXNEWEntities : DbContext
    {
        public DemEXNEWEntities()
            : base("name=DemEXNEWEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Jury> Jury { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Moderators> Moderators { get; set; }
        public virtual DbSet<Organizer> Organizer { get; set; }
    }
}
