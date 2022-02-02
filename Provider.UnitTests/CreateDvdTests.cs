using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Provider.Tests.Shared;
using TestStack.BDDfy;
using TestStack.BDDfy.Xunit;

namespace Provider.UnitTests
{
    public sealed class CreateDvdTests : IDisposable
    {
        private readonly TestHarness _testHarness = new();

        private HttpResponseMessage _response;

        [BddfyFact]
        public void CreateNewDvd()
        {
            this.When(_ => _.CreatingANewDvd("Blade Runner", "Ridley Scott"))
                .Then(_ => _.TheDvdIsCreated())
                .And(_ => _testHarness.Then.ADvdExistsMatching("Blade Runner", "Ridley Scott"))
                .BDDfy();
        }
        
        [BddfyFact]
        public void NameIsUnique()
        {
            this.Given(_=> _testHarness.Given.AnExistingDvd(Guid.Parse("33B2AA18-AFF2-402A-B24E-E54E835351C9"),"Blade Runner", "Ridley Scott"))
                .When(_ => _.CreatingANewDvd("Blade Runner", "Ridley Scott"))
                .Then(_ => _.TheDvdIsNotCreated())
                .BDDfy();
        }

        private void TheDvdIsCreated() => _response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        private void TheDvdIsNotCreated() => _response.StatusCode.Should().Be(HttpStatusCode.Conflict);

        private async Task CreatingANewDvd(string name, string director)
        {
            using var client = _testHarness.CreateClient();
            _response = await client.PostAsJsonAsync("/dvds", new
            {
                name = name,
                director = director
            });
        }

        public void Dispose()
        {
            _testHarness?.Dispose();
        }
    }
}