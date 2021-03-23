using AutoMapper;
using Eleven.Cloud.GCP.Dto;
using Eleven.Cloud.GCP.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eleven.Cloud.GCP.Application
{
    public class SingerApplication : ISingerApplication
    {
        private readonly IMapper _mapper;
        private readonly ISingerRepository _singerRepository;

        public SingerApplication(IMapper mapper, ISingerRepository singerRepository)
        {
            _mapper = mapper;
            _singerRepository = singerRepository;
        }

        public async Task<IEnumerable<SingerDto>> GetAllSingers()
        {
            var singers = await _singerRepository.FindAllAsync();
            return _mapper.Map<IEnumerable<SingerDto>>(singers);
        }
    }
}
