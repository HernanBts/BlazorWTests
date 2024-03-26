
using Orders.Shared.Entities;

namespace Orders.Backend.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;

        public SeedDB(DataContext context)
        {
            _context = context;
        }

        public async Task SeedDbAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if(!_context.Categories.Any()) 
            {
                _context.Categories.Add(new Category { Name = "Holgar" });
                _context.Categories.Add(new Category { Name = "Tecnologia" });
                _context.Categories.Add(new Category { Name = "Moda" });
                _context.Categories.Add(new Category { Name = "Ropa" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country 
                { 
                    Name = "Argentina",
                    States = [
                        new()
                        { 
                            Name = "Chaco",
                            Cities = [
                                new() { Name = "Resistencia" },
                                new() { Name = "Tirol" },
                                new() { Name = "Barranqueras" },
                                new() { Name = "Fontana" }
                            ],
                        },
                        new()
                        {
                            Name = "Corrientes",
                            Cities = [
                                new() { Name = "Corrientes" },
                                new() { Name = "Goya" },
                                new() { Name = "Santa Ana" },
                                new() { Name = "Paso de la patria" }
                            ],
                        }
                    ],
                });
                _context.Countries.Add(new Country { Name = "Chile" });
                _context.Countries.Add(new Country { Name = "Colombia" });
                _context.Countries.Add(new Country { 
                    Name = "EEUU",
                    States = [
                        new()
                        {
                            Name = "Florida",
                            Cities = [
                                new() { Name = "Miami" },
                                new() { Name = "Orlando" },
                                new() { Name = "Tampa" },
                                new() { Name = "Key West" }
                            ],
                        },
                        new()
                        {
                            Name = "Texas",
                            Cities = [
                                new() { Name = "Houston" },
                                new() { Name = "San antonio" },
                                new() { Name = "Dallas" },
                                new() { Name = "El Paso" }
                            ],
                        }
                    ],
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}
