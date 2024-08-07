using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Data;
using api.Dtos.Stock;
using Microsoft.EntityFrameworkCore;
using api.Helper;


namespace api.Interfaces
{
    public interface IStockRepo
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id );
        Task<Stock> CreateAsync(Stock stockmodel);
        Task<Stock?> UpdateAsync(int id ,UpdateStockDto updateDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExistAsync(int stockId);
        
    }
}