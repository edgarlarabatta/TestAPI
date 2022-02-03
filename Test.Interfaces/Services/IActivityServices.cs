using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Interfaces.Services;

namespace Test.Interfaces.Services
{
    public interface IActivityServices: IBaseService<Entities.Activity>
    {
        Task<Entities.Activity> Reschedule(Entities.Activity entity);
        Task<Entities.Activity> Cancel(Entities.Activity entity);
        Task<IEnumerable<Entities.Activity>> GetByFilters(Entities.Parameters parameters);

        Task<Entities.Activity> Terminate(Entities.Activity entity);

    }
}
