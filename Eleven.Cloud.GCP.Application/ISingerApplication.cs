using Eleven.Cloud.GCP.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eleven.Cloud.GCP.Application
{
    public interface ISingerApplication
    {
        Task<IEnumerable<SingerDto>> GetAllSingers();
    }
}
