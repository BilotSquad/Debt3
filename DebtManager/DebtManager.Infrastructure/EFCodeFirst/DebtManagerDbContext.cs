using DebtManager.Domain.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace DebtManager.Infrastructure.EFCodeFirst
{
    public class DebtManagerDbContext : DbContext, IRepository
    {
        public IDbSet<Debt> Debts { get; set; }
        public IDbSet<Payment> Payments { get; set; }
        public IDbSet<User> Users { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DebtManagerDbContext"/> class.
        /// </summary>
        public DebtManagerDbContext()
            : base("DebtManagerDbContext")
        {
            System.Data.Entity.Database.SetInitializer<DebtManagerDbContext>(null);//new DropCreateDatabaseAlways<DebtManagerDbContext>();
        }

        //static DebtManagerDbContext()
        //{
        //    Database.SetInitializer<DebtManagerDbContext>(new CreateDatabaseIfNotExists<DebtManagerDbContext>());
        //}

        public static DebtManagerDbContext Create()
        {
            //var dbContext = context.Get<DebtManagerDbContext>();//IOwinContext context
            return new DebtManagerDbContext();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DebtManagerDbContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">The name or connection string.</param>
        //public DebtManagerDbContext(string nameOrConnectionString)
        //    : base(nameOrConnectionString)
        //{
        //    System.Data.Entity.Database.SetInitializer<DebtManagerDbContext>(null); 
        //}

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            var typesToRegister =
                Assembly.GetExecutingAssembly().GetTypes().Where(
                    type =>
                    type.BaseType != null &&
                    type.BaseType.IsGenericType &&
                    type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var configurationInstance in typesToRegister.Select(Activator.CreateInstance))
            {
                modelBuilder.Configurations.Add((dynamic)configurationInstance);
            }

            FixRelationships(modelBuilder);

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<User>()
            //    .ToTable("Users", "dbo")
            //    .HasKey<int>(l => l.Id);

            //modelBuilder.Entity<UserLogin>().ToTable("UserLogin", "dbo");//.HasKey<int>(l => l.UserId);
            //modelBuilder.Entity<Role>().ToTable("Roles", "dbo");//.HasKey<int>(r => r.Id);
            //modelBuilder.Entity<UserRole>().ToTable("UserRoles");//.HasKey(r => new { r.RoleId, r.UserId });
            //modelBuilder.Entity<UserClaim>().ToTable("UserClaim");

            //modelBuilder.Entity<Case>()
            //.HasMany(x => x.Participants)
            //.WithMany(x => x.CasesParticipants)
            //.Map(x =>
            //{
            //    x.ToTable("CaseParticipants");
            //    x.MapLeftKey("Case_Id");
            //    x.MapRightKey("User_Id");
            //});
        }

        /// <summary>
        /// This method is called to fix some issues with related entities.
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void FixRelationships(System.Data.Entity.DbModelBuilder modelBuilder)
        {
        }

        //public T Insert<T>(T entity)
        //    where T : class
        //{
        //    if (entity == null)
        //        throw new ArgumentNullException("entity");

        //    return this.Set<T>().Add(entity);
        //new DropCreateDatabaseAlways<DebtManagerDbContext>()}

    //    public IQueryable<T> GetAll<T>()
    //where T : class
    //    {
    //        return this.Set<T>();
    //    }

        /// <summary>
        /// Persists all.
        /// </summary>
        //public void PersistAll()
        //{
        //    try
        //    {
        //        this.SaveChanges();
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        // Convert DbEntityValidationException to ValidationException
        //        throw new ValidationException(ex.Message, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.InnerException is OptimisticConcurrencyException)
        //        {
        //            //throw new ValidationException(ex.Message, ex);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}


        public T Get<T>(int id) where T : class
        {
            return this.Set<T>().Find(id);
        }
    }
}
