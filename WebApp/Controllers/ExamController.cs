using Application.Catalog.Interfaces;
using Application.Common.Interfaces;
using Application.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Dtos.Exam;
using Shared.Dtos.Exam.Filters;
using Shared.Dtos.Exam.Requests;

namespace WebApp.Controllers
{
    [Authorize]
    public class ExamController : BaseController
    {
        private readonly IExamService _service;
        private readonly IWebsiteContent _content;

        public ExamController(IExamService service, IWebsiteContent content)
        {
            _service = service;
            _content = content;
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetAllExams(ExamPaginationListFilter filter)
        {

            return View(await _service.GetPaginatedResult(filter));
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> ExamDetail(Guid id)
        {
            return View(await _service.GetExamById(id));
        }
        [HttpGet]
        public async Task<IActionResult> CreateExam()
        {
            var titlesAndContents = await _content.GetContentUrl();
            ViewBag.TitlesAndContents = titlesAndContents;
            
            var crq = new CreateExamRequest();
            var titleList = titlesAndContents.Select(x => new { Text = x.Title, Value = x.Title }).ToList();
            SelectList selectList = new SelectList(titleList, "Value","Text", titleList.FirstOrDefault());
            
            ViewBag.selectListForTitle = selectList;
            for (int i = 0; i < 4; i++)
            {
                crq.ExamQuestions.Add(new() { Question = "" });
            }
            foreach (var question in crq.ExamQuestions)
            {
                for (int i = 0; i < 4; i++)
                {
                    question.QuestionChoices.Add(new Shared.Dtos.ExamQuestionChoice.Requests.CreateExamQuestionChoiceRequest() { Choice = "", isCorrect = false });
                }

            }
            return View(crq);
        }
        [HttpPost]
        public async Task<IActionResult> CreateExam(CreateExamRequest request)
        {
            request.ExamQuestions.ForEach(x => x.QuestionChoices[x.CorrectIndex].isCorrect = true);
            // TODO move this logic process to ExamService
            await _service.ExamCreateAsync(request);
            return RedirectToAction("GetAllExams");
        }
    
        [HttpGet("remove/{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _service.RemoveExamRequest(id);
            return RedirectToAction("GetAllExams");
        }
    }
}
