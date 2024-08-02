using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Data;


namespace api.Interfaces
{
    public interface IStockRepo
    {
        Task<List<Stock>> GetAllStocksAsync();
    }
}