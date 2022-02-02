using System;
using Provider.Controllers;

namespace Provider.Tests.Shared
{
    public class GivenSteps
    {
        private readonly TestDvdRepository _testDvdRepository;

        public GivenSteps(TestDvdRepository testDvdRepository)
        {
            _testDvdRepository = testDvdRepository;
        }

        public void AnExistingDvd(Guid id, string name, string director)
        {
            _testDvdRepository.Create(new DvdResponse(id, name, director));
        }
    }
}