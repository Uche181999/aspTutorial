using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Mappers;
using api.Dtos.Stock;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Helper;


namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IStockRepo _stockRepo;
        public StockController(IStockRepo stockRepo)
        {

            _stockRepo = stockRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var stocks = await _stockRepo.GetAllAsync(query);
            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)

        {
            var stockModel = stockDto.ToCreateStockDto();
            await _stockRepo.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateDto)
        {
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);
            if (stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }
        [HttpDelete("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}