using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Mappers;
using api.Dtos.Stock;
using System.Text.Json;


namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StockController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetALL()
        {
            var stocks = _context.Stocks.ToList()
            .Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id).ToStockDto();
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);

        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockDto stockDto)

        {
            var stockModel = stockDto.ToCreateStockDto();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockDto updateDto)
        {
            var stockModel = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }
            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            _context.SaveChanges();

            return Ok(stockModel.ToStockDto());
        }
        [HttpDelete("{id}")]

        public IActionResult Delete([FromRoute] int id)
        {
            var stockModel = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stockModel);
            _context.SaveChanges();

            return NoContent();
        }
    }
}