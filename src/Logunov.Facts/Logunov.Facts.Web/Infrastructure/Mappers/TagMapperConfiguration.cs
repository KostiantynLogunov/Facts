using Logunov.Facts.Web.Data;
using Logunov.Facts.Web.Infrastructure.Mappers.Base;
using Logunov.Facts.Web.ViewModels;

namespace Logunov.Facts.Web.Infrastructure.Mappers
{
    public class TagMapperConfiguration : MapperConfigurationBase
    {
        public TagMapperConfiguration()
        {
            CreateMap<Tag, TagViewModel>();

            CreateMap<Tag, TagUpdateViewModel>();

            CreateMap<TagUpdateViewModel, Tag>()
                .ForMember(x=>x.Facts, o=>o.Ignore());
        }
    }
}
