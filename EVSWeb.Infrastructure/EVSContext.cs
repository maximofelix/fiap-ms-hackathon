using EVSWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EVSWeb.Infrastructure
{
    public class EVSContext : DbContext
    {
        public EVSContext()
        {
            
        }
        public EVSContext(DbContextOptions<EVSContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            base.OnModelCreating(modelBuilder);
        }

        public void Seed()
        {
            var exist = this.Products.Any();
            if (!exist)
            {
                var unidade = new Unit
                {
                    Id = Guid.NewGuid(),
                    Name = "Unidade"
                };
                this.Unities.Add(unidade);

                var quilo = new Unit
                {
                    Id = Guid.NewGuid(),
                    Name = "Quilo"
                };
                this.Unities.Add(quilo);

                var nutricao = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Nutrição"
                };
                this.Categories.Add(nutricao);

                var bebida = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Bebida"
                };
                this.Categories.Add(bebida);

                var lanche = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Lanche"
                };
                this.SaveChanges();
                this.Categories.Add(lanche);
                this.SaveChanges();

                var shake = new Product
                {
                    Id = Guid.NewGuid(),
                    Code = "A0001",
                    Name = "Shake de Banana",
                    Description = "Shake de Banana com Aveia",
                    Weight = 0.5M,
                    Qtde = 1,
                    Coast = 5.00M,
                    Price = 10.00M,
                    IsAtive = true,
                    Unit = unidade,
                    Category = nutricao,
                    SellPoints = 2M
                };
                this.Products.Add(shake);
                shake = new Product
                {
                    Id = Guid.NewGuid(),
                    Code = "A0002",
                    Name = "Shake de Chocolate",
                    Description = "Shake de Chocolate Sensation",
                    Weight = 0.5M,
                    Qtde = 1,
                    Coast = 5.00M,
                    Price = 10.00M,
                    IsAtive = true,
                    Unit = unidade,
                    Category = nutricao,
                    SellPoints = 2M
                };
                this.Products.Add(shake);

                var suco = new Product
                {
                    Id = Guid.NewGuid(),
                    Code = "A0003",
                    Name = "Suco de Laranja",
                    Description = "Suco de Laranja Natural",
                    Weight = 0.3M,
                    Qtde = 1,
                    Coast = 3.00M,
                    Price = 6.00M,
                    IsAtive = true,
                    Unit = quilo,
                    Category = bebida,
                    SellPoints = 1.2M
                };
                this.Products.Add(suco);

                var sanduiche = new Product
                {
                    Id = Guid.NewGuid(),
                    Code = "A0004",
                    Name = "Sanduíche Natural",
                    Description = "Sanduíche Natural de Frango",
                    Weight = 0.2M,
                    Qtde = 1,
                    Coast = 4.00M,
                    Price = 8.00M,
                    IsAtive = true,
                    Unit = unidade,
                    Category = lanche,
                    SellPoints = 1.5M
                };
                this.Products.Add(sanduiche);
                this.SaveChanges();
            }
        }

        public virtual DbSet<Unit> Unities { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
