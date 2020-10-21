using AutoMapper;
using FirstOne.Cadastros.Application.AutoMapper;
using FirstOne.Cadastros.Application.Interfaces;
using FirstOne.Cadastros.Application.Services;
using FirstOne.Cadastros.Domain.Commands;
using FirstOne.Cadastros.Domain.CommandHandler;
using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Domain.Mediator;
using FirstOne.Cadastros.Domain.Messaging;
using FirstOne.Cadastros.Infra.Data.Context;
using FirstOne.Cadastros.Infra.Data.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using FirstOne.Cadastros.Domain.Commands.UsuarioCommands;
using Microsoft.EntityFrameworkCore;

namespace FirstOne.Cadastros.Api.Config
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));

            // Mediator
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // MongoDb
            //MongoDbContext.ConnectionString = configuration.GetSection("MongoConnection:ConnectionString").Value;
            //MongoDbContext.DatabaseName = configuration.GetSection("MongoConnection:Database").Value;
            //MongoDbContext.IsSSL = Convert.ToBoolean(configuration.GetSection("MongoConnection:IsSSL").Value);

            // Application
            services.AddScoped<IPessoaAppService, PessoaAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<AdicionarPessoaCommand, Unit>, PessoaCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarPessoaCommand, Unit>, PessoaCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverPessoaCommand, Unit>, PessoaCommandHandler>();

            services.AddScoped<IRequestHandler<AdicionarUsuarioCommand, Unit>, UsuarioCommandHandler>();

            // Infra - Data
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<SQLServerContext>();

            services.AddDbContext<SQLServerContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
