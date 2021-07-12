using AutoMapper;
using MetricsManager.DAL.Models;
using MetricsManager.Responses;

namespace MetricsManager.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetricsApiResponse, CpuMetric>();
            CreateMap<CpuMetric, CpuMetricsApiResponse>();
        }
    }
}
