﻿using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Repositories.Interfaces;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Implementations
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly DataContext _context;

        public CountriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<Country>> GetAsync(int id) 
        {
            var country = await _context.Countries
                .Include(s => s.States!)
                .ThenInclude(c => c.Cities)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (country == null) 
            {
                return new ActionResponse<Country>
                {
                    WasSuccess = false,
                    Message = "Pais no econtrado."
                };
            }

            return new ActionResponse<Country>
            {
                WasSuccess = true,
                Result = country
            };
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
        {
            var countries = await _context.Countries.Include(x => x.States).ToListAsync();

            return new ActionResponse<IEnumerable<Country>>
            {
                WasSuccess = true,
                Result = countries
            };
        }
    }
}
