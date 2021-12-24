using Application.Catalog.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Exam.Requests;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamCheckController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamCheckController(IExamService examService)
        {
            _examService = examService;
        }
        [HttpPost]
        public async Task<IActionResult> CheckAnswers([FromBody]GetExamQuestionAnswers req)
        {
              return Ok(await _examService.GetResults(req));
        }
    }
}
