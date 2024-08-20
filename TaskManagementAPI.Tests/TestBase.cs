using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

public class TestBase : IClassFixture<WebApplicationFactory<Program>>
{
    protected readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public TestBase(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }
}


