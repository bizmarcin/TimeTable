namespace CourseData
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class WorkshopEntity : DbContext
    {
        // Your context has been configured to use a 'CourseDataModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CourseData.CourseDataModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CourseDataModel' 
        // connection string in the application configuration file.
        public WorkshopEntity()
            : base("name=CourseDataModel")
        {
        }

        protected virtual void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Courses)
                .WithMany(c => c.Orders);
                
                
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<Order> Order { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}