using System.Threading.Tasks;
using Core.Interface.Repository;

namespace Core.Interface.UnitOfWorks
{
    public interface IUnitOfWork
    {
        // IProductRepository Products { get; }

        Task CommitAsync();

        void Commit();
    }
}