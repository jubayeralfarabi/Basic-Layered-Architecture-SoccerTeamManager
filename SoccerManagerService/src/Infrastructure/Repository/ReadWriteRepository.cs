namespace Soccer.Infrastructure.Repository.RDBRepository
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public class ReadWriteRepository<TContext> : ReadOnlyRepository<TContext>, IReadWriteRepository
     where TContext : DbContext
    {
        public ReadWriteRepository(TContext context)
            : base(context)
        {

        }

        public ChangeTracker ChangeTracker { get; set; }

        public virtual void Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class
        {
            this.context.Set<TEntity>().Add(entity);
            this.context.SaveChanges();
        }

        public virtual void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class
        {
            this.context.Set<TEntity>().Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
            this.context.SaveChanges();
        }
    }
}
