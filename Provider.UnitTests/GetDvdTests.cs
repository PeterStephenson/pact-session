using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Provider.Controllers;
using Provider.Tests.Shared;
using TestStack.BDDfy;
using TestStack.BDDfy.Xunit;

namespace Provider.UnitTests
{
    public sealed class GetDvdTests : IDisposable
    {
        private readonly TestHarness _testHarness = new();

        private HttpResponseMessage _response;

        private DvdResponse _dvdResponse;

        [BddfyFact]
        public void ExistingDvd()
        {
            var dvdId = Guid.Parse("786AC676-AA3A-4EB0-BF0B-BC55AE40CB6E");
            this.Given(_=> _testHarness.Given.AnExistingDvd(dvdId,"Blade Runner", "Ridley Scott"))
                .When(_ => _.RetrievingADvdById(dvdId))
                .Then(_ => _.TheDvdIsReturned())
                .And(_ => TheDvdMatches("Blade Runner", "Ridley Scott"))
                .BDDfy();
        }
        
        [BddfyFact]
        public void DvdDoesNotExist()
        {
            this.When(_ => _.RetrievingADvdById(Guid.NewGuid()))
                .Then(_ => _.TheDvdIsNotFound())
                .BDDfy();
        }
        
        private void TheDvdIsReturned() => _response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        private void TheDvdIsNotFound() => _response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        private async Task RetrievingADvdById(Guid id)
        {
            using var client = _testHarness.CreateClient();
            _response = await client.GetAsync($"/dvds/{id}");
            if (_response.IsSuccessStatusCode)
            {
                _dvdResponse = await _response.Content.ReadFromJsonAsync<DvdResponse>();
            }
        }

        private void TheDvdMatches(string name, string director)
        {
            _dvdResponse.Should().NotBeNull();
            _dvdResponse.Name.Should().Be(name);
            _dvdResponse.Director.Should().Be(director);
        }
        
        public void Dispose()
        {
            _testHarness?.Dispose();
        }
    }

}