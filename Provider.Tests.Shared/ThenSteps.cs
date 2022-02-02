using System.Linq;
using FluentAssertions;

namespace Provider.Tests.Shared
{
    public class ThenSteps
    {
        private readonly TestDvdRepository _testDvdRepository;

        public ThenSteps(TestDvdRepository testDvdRepository)
        {
            _testDvdRepository = testDvdRepository;
        }

        public void ADvdExistsMatching(string name, string director)
        {
            var dvds = _testDvdRepository.GetCreatedDvds();
            dvds.Should().HaveCount(1);
            var dvd = dvds.Single();
            dvd.Name.Should().Be(name);
            dvd.Director.Should().Be(director);
        }
    }
}