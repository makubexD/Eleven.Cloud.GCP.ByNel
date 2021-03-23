using AutoMapper;
using Eleven.Cloud.GCP.Dto;
using Eleven.Cloud.GCP.Entity;

namespace Eleven.Cloud.GCP.Mapping
{
    public class SingerProfile : Profile
    {
        public SingerProfile()
        {
            CreateMap<Singer, SingerDto>()
                    .ReverseMap();
        }
    }
}
