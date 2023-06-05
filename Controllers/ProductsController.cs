using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAdvance.DAL.EfCore;
using WebApiAdvance.Entities;
using WebApiAdvance.Entities.Dtos.Products;

namespace WebApiAdvance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetProducts()
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            var result = await _context.Products.ToListAsync();
            List<GetProductDto> getProductDtos=_mapper.Map<List<GetProductDto>>(result);

            return getProductDtos;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDto>> GetProduct(int id)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            GetProductDto result = _mapper.Map<GetProductDto>(product);
            return result;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDto productdto)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            Product product = _mapper.Map<Product>(productdto);
            product.ProductCode = "TEst";
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostProduct([FromBody]CreateProductDto productdto)
        {
            Product product=_mapper.Map<Product>(productdto);
            product.Created= DateTime.UtcNow;
            product.ProductCode = product.Name.Substring(0,2);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
