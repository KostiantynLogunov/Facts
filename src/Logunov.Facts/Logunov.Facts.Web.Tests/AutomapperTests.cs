using Logunov.Facts.Web.Infrastructure.Mappers.Base;
using Xunit;

namespace Logunov.Facts.Web.Tests
{
    public class AutomapperTests
    {
        [Fact]
        [TraitAttribute("Automapper", "Mapper Configuration")]
        public void ItSholdCorrectlyConfigured()
        {
            //arrange
            var config = MapperRegistration.GetMapperConfiguration();

            //act

            //assert
            config.AssertConfigurationIsValid();
            //Assert.NotNull(config);
        }
    }
}
