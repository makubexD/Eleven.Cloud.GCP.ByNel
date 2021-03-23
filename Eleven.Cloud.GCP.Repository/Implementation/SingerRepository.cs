using Eleven.Cloud.GCP.Entity;
using Eleven.Cloud.GCP.Repository.Base;
using Eleven.Cloud.GCP.Repository.Interface;

namespace Eleven.Cloud.GCP.Repository.Implementation
{
    public class SingerRepository : BaseRepository<Singer>, ISingerRepository
    {
        public SingerRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
