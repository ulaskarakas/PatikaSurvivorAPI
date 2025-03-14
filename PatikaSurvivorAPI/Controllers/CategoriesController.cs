using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatikaSurvivorAPI.Context;
using PatikaSurvivorAPI.Entities;
using PatikaSurvivorAPI.Models.Category;
using PatikaSurvivorAPI.Models.Competitor;

namespace PatikaSurvivorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly SurvivorDbContext _context;

        public CategoriesController(SurvivorDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateRequest request)
        {
            var newCategory = new CategoryEntity()
            {
                Name = request.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            _context.Categories.Add(newCategory);

            _context.SaveChanges();

            return Created();
        }

        [HttpGet]
        public IActionResult List()
        {
            var res = _context.Categories.Where(x => x.IsDeleted == false).OrderBy(x => x.Name).Select(x => new CategoryListResponse
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.Include(c => c.Competitors).FirstOrDefault(x => x.Id == id);

            if (category is null || category.IsDeleted == true)
                return NotFound();

            var res = new CategoryDetailResponse()
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
                ModifiedDate = category.ModifiedDate,
                Competitors = category.Competitors.Where(c => c.IsDeleted == false).Select(c => new CompetitorListResponse
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    CategoryId = category.Id
                }).ToList()
            };

            return Ok(res);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryUpdateRequest request)
        {
            var category = _context.Categories.Find(id);

            if (category is null || category.IsDeleted == true)
                return NotFound();

            category.Name = request.Name;
            category.ModifiedDate = DateTime.Now;

            _context.Categories.Update(category);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);

            if (category is null)
                return NotFound();

            category.IsDeleted = true;
            category.ModifiedDate = DateTime.Now;

            _context.Categories.Update(category);
            _context.SaveChanges();

            return Ok();
        }
    }
}
