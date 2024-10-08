using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Data;
using api.Dtos.Stock;
using Newtonsoft.Json;
using api.Helper;

namespace api.Repos
{
    public class StockRepo : IStockRepo
    {
        private readonly AppDbContext _context;

        public StockRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetAllAsync(QueryObject query)

        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
        

            if (!string.IsNullOrWhiteSpace(query.OrderBy)){

                if (query.OrderBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol): stocks.OrderBy(s => s.Symbol); 
                }
            }
                //pagination
            var skipNum =( query.PageNum-1)*query.PageSize;


                return await stocks.Skip(skipNum).Take(query.PageSize).ToListAsync();



        }
        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return null;
            }
            return stock;
        }
        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }
        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto updateDto)
        {

            var existStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (existStock == null)
            {
                return null;
            }
            existStock.Symbol = updateDto.Symbol;
            existStock.CompanyName = updateDto.CompanyName;
            existStock.Purchase = updateDto.Purchase;
            existStock.LastDiv = updateDto.LastDiv;
            existStock.Industry = updateDto.Industry;
            existStock.MarketCap = updateDto.MarketCap;

            await _context.SaveChangesAsync();
            return existStock;

        }
        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }
        public async Task<bool> StockExistAsync(int stockId)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == stockId);
        }

    }
}