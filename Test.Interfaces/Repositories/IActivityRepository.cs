using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Test.Interfaces.Repositories
{
    public interface IActivityRepository:IBaseRepository<Entities.Activity>
    {
        Task<Entities.Activity> Exists(int property_id, DateTime schedule);
        Task<Entities.Activity> Reschedule(Entities.Activity entity);
        Task<Entities.Activity> Cancel(Entities.Activity entity);

        Task<IEnumerable<Entities.Activity>> GetByFilters(Entities.Parameters parameters);
        Task<Entities.Activity> Terminate(Entities.Activity entity);


    }
}
