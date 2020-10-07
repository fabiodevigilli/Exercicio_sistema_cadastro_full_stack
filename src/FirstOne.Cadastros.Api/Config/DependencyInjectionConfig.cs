using AutoMapper;
using FirstOne.Cadastros.Application.AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.Services;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace FirstOne.Cadastros.Api.Config
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));

            // Application
            services.AddScoped<IPessoaAppService, PessoaAppService>();

            // Infra - Data
            services.AddScoped<IPessoaRepository, PessoaRepository>();
        }
    }
}
