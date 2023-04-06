using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Core;

public interface IFeature
{
    public IServiceCollection AddService(IServiceCollection services, IConfiguration configuration);
    public WebApplication UseService(WebApplication app);

}