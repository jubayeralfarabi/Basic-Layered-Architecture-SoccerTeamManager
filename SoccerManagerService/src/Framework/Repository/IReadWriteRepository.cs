namespace Soccer.Infrastructure.Repository.RDBRepository
{
    using System.Threading.Tasks;

    public interface IReadWriteRepository : IReadOnlyRepository
    {
        void Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class;

        void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class;
    }
}
