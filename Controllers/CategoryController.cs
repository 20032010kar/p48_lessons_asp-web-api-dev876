using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using p48_lessons_asp.Data;
using p48_lessons_asp.Services.Slug;

namespace p48_lessons_asp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ISlugService _slugService;
        private readonly DataContext _dataContext;
        public CategoryController(
            ISlugService slugService, 
            DataContext dataContext)
        {
            _slugService = slugService;
            _dataContext = dataContext;
        }
    }
}
