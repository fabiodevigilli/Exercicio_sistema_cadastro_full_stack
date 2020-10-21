﻿using FirstOne.Cadastros.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstOne.Cadastros.Infra.Data.Context
{
    public class SQLServerContext : DbContext
    {
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
