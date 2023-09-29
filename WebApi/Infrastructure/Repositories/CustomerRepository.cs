using WebApi.Abstractions;
using WebApi.Models;

namespace WebApi.Infrastructure.Repositories
{
    public class CustomerRepository:GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(EntityFrameworkCoreDbContext context):base(context) { }
    }
}
