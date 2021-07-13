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

            CreateMap<DotNetMetricsApiResponse, DotNetMetric>();
            CreateMap<DotNetMetric, DotNetMetricsApiResponse>();

            CreateMap<HddMetricsApiResponse, HddMetric>();
            CreateMap<HddMetric, HddMetricsApiResponse>();

            CreateMap<NetworkMetricsApiResponse, NetworkMetric>();
            CreateMap<NetworkMetric, NetworkMetricsApiResponse>();

            CreateMap<RamMetricsApiResponse, RamMetric>();
            CreateMap<RamMetric, RamMetricsApiResponse>();
        }
    }
}
