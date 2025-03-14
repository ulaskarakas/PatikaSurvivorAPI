using Microsoft.AspNetCore.Mvc;
using PatikaSurvivorAPI.Context;
using PatikaSurvivorAPI.Entities;
using PatikaSurvivorAPI.Models.Competitor;

namespace PatikaSurvivorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitorsController : ControllerBase
    {
        private readonly SurvivorDbContext _context;

        public CompetitorsController(SurvivorDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(CompetitorCreateRequest request)
        {
            var newCompetitor = new CompetitorEntity()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                CategoryId = request.CategoryId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            _context.Competitors.Add(newCompetitor);

            _context.SaveChanges();

            return Created();
        }

        [HttpGet]
        public IActionResult List()
        {
            var res = _context.Competitors.Where(x => x.IsDeleted == false).OrderBy(x => x.FirstName).Select(x => new CompetitorListResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                CategoryId = x.CategoryId
            }).ToList();

            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var competitor = _context.Competitors.Find(id);

            if(competitor is null || competitor.IsDeleted == true)
                return NotFound();

            var res = new CompetitorDetailResponse
            {
                Id = competitor.Id,
                FirstName = competitor.FirstName,
                LastName = competitor.LastName,
                CategoryId = competitor.CategoryId, 
                CreatedDate = competitor.CreatedDate,
                ModifiedDate = competitor.ModifiedDate
            };

            return Ok(res);
        }

        [HttpGet("categories/{categoryId}")]
        public IActionResult GetByCategory(int categoryId)
        {
            var res = _context.Competitors.Where(x => x.IsDeleted == false && x.CategoryId == categoryId).OrderBy(x => x.FirstName).Select(x => new CompetitorListResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                CategoryId = x.CategoryId
            }).ToList();

            return Ok(res);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CompetitorUpdateRequest request)
        {
            var competitor = _context.Competitors.Find(id);

            if (competitor is null)
                return NotFound();

            competitor.FirstName = request.FirstName;
            competitor.LastName = request.LastName;
            competitor.CategoryId = request.CategoryId;
            competitor.ModifiedDate = DateTime.Now;

            _context.Competitors.Update(competitor);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var competitor = _context.Competitors.Find(id);

            if (competitor is null)
                return NotFound();

            competitor.IsDeleted = true;
            competitor.ModifiedDate = DateTime.Now;

            _context.Competitors.Update(competitor);
            _context.SaveChanges();

            return Ok();
        }
    }
}