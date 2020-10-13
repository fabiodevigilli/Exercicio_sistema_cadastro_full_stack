using AutoMapper;
using FirstOne.Cadastros.Application.AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.Services;
using FirstOne.Cadastros.Domain.Command;
using FirstOne.Cadastros.Domain.CommandHandler;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using FirstOne.Cadastros.Infra.Data.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FirstOne.Cadastros.Api.Config
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));

            // Mediator
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Application
            services.AddScoped<IPessoaAppService, PessoaAppService>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<AdicionarPessoaCommand, bool>, PessoaCommandHandler>();

            // Infra - Data
            services.AddScoped<IPessoaRepository, PessoaRepository>();
        }
    }
}
