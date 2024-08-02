using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Data;

namespace api.Repos
{
    public class StockRepo : IStockRepo
    {
        private readonly AppDbContext _context;

        public StockRepo(AppDbContext context) 
        {
            _context = context;
        }
        public Task<List<Stock>> GetAllStocksAsync()
        
        {
            return  _context.Stocks.ToListAsync();
        }
    }
}