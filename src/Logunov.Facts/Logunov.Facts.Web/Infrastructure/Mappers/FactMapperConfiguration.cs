using Logunov.Facts.Web.Data;
using Logunov.Facts.Web.Infrastructure.Mappers.Base;
using Logunov.Facts.Web.ViewModels;
using Calabonga.UnitOfWork;

namespace Logunov.Facts.Web.Infrastructure.Mappers
{
    public class FactMapperConfiguration : MapperConfigurationBase
    {
        public FactMapperConfiguration()
        {
            CreateMap<Fact, FactViewModel>();

            CreateMap<FactCreateViewModel, Fact>()
                .ForMember(x=>x.Id, o=>o.Ignore())
                .ForMember(x=>x.Tags, o=>o.Ignore())
                .ForMember(x=>x.CreatedAt, o=>o.Ignore())
                .ForMember(x=>x.CreatedBy, o=>o.Ignore())
                .ForMember(x=>x.UpdatedAt, o=>o.Ignore())
                .ForMember(x=>x.UpdatedBy, o=>o.Ignore());

            CreateMap<Fact, FactUpdateViewModel>();

            CreateMap<FactUpdateViewModel, Fact>()
                .ForMember(x => x.Tags, o => o.Ignore())
                .ForMember(x => x.CreatedAt, o => o.Ignore())
                .ForMember(x => x.CreatedBy, o => o.Ignore())
                .ForMember(x => x.UpdatedAt, o => o.Ignore())
                .ForMember(x => x.UpdatedBy, o => o.Ignore());

            CreateMap<IPagedList<Fact>, IPagedList<FactViewModel>>()
                .ConvertUsing<PagedListConverter<Fact, FactViewModel>>();
        }
    }
}
