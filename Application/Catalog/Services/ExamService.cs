using Application.Catalog.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using Application.Identity.Interfaces;
using Application.Wrapper;
using Domain.Catalog.Quiz;
using Shared.Dtos.Exam;
using Shared.Dtos.Exam.Filters;
using Shared.Dtos.Exam.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Catalog.Services;
public class ExamService : IExamService
{
    private readonly IUow _uow;
    private readonly IUserService _userService;
    public ExamService(IUow uow, IUserService userService)
    {
        _uow = uow;
        _userService = userService;
    }

    public async Task<Result<Guid>> ExamCreateAsync(CreateExamRequest request)
    {
        var repository = _uow.GetRepository();
        var exam = new Exam(request.ExamTitle, request.ExamText, await _userService.GetCurrentUserId());
        var examId = await repository.CreateAsync(exam);
        if (request.ExamQuestions.Count < 4)
        {
            return await Result<Guid>.FailAsync("Question count must be equal or greater than 4");
        }
        var questions = new List<ExamQuestion>();
        foreach (var question in request.ExamQuestions)
        {
            if (question.QuestionChoices.Count < 4 && question.QuestionChoices.Where(x => x.isCorrect == true).ToList().Count == 1)
            {
                return await Result<Guid>.FailAsync($"Each questions choice count must be equal or greater than 4|| Maybe you have more than 1 right choice");
            }
            else
            {
                var examQuestionEntity = new ExamQuestion(question.Question, examId);
                var examQuestionEntityId = await repository.CreateAsync(examQuestionEntity);
                foreach (var questionChoice in question.QuestionChoices)
                {
                    var examQuestionChoiceEntity = new ExamQuestionChoice(examQuestionEntityId, questionChoice.Choice, questionChoice.isCorrect);
                    var examQuestionChoiceEntityId = await repository.CreateAsync(examQuestionChoiceEntity);
                }
            }
        }

        if (await _uow.SaveChangesAsync() > 0)
        {
            return await Result<Guid>.SuccessAsync(examId);
        }
        else
        {
            return await Result<Guid>.FailAsync($"Something went wrong while adding exam!");
        }
    }

    public async Task<Result<ExamDto>> GetExamById(Guid id)
    {
        BaseSpecification<Exam> examSpec = new BaseSpecification<Exam>();
        examSpec.Includes.Add(x => x.Questions);
        examSpec.IncludeStrings.Add("Questions.ExamQuestionChoices");
        var result = await _uow.GetRepository().GetByIdAsync<Exam, ExamDto>(id, examSpec);
        return await Result<ExamDto>.SuccessAsync(result);
    }

    public async Task<PaginatedResult<ExamDetailDto>> GetPaginatedResult(ExamPaginationListFilter filter)
    {
        var currentUserId = await _userService.GetCurrentUserId();
        return await _uow.GetRepository().GetSearchResult<Exam, ExamDetailDto>(filter.PageNumber, filter.PageSize, null, null, x => x.UserId == currentUserId);
    }

    public async Task<Result<ExamAnswersDto>> GetResults(GetExamQuestionAnswers answers)
    {
        BaseSpecification<ExamQuestionChoice> spec = new BaseSpecification<ExamQuestionChoice>();

        var repository = _uow.GetRepository();
        var examAnswersDto = new ExamAnswersDto();
        foreach (var questionAnswerId in answers.AnswersId)
        {
            var questionAndAnswers = await repository.GetByIdAsync<ExamQuestionChoice>(questionAnswerId);
            if (questionAndAnswers.IsCorrect)
            {
                examAnswersDto.CorrectCount++;
            }
            examAnswersDto.QuestionAndAnswers.Add(new()
            {
                AnswerId = questionAndAnswers.Id,
                IsCorrect = questionAndAnswers.IsCorrect
            });

        }
        return await Result<ExamAnswersDto>.SuccessAsync(examAnswersDto);
    }

    public async Task<Result<Guid>> RemoveExamRequest(Guid id)
    {
        var repository = _uow.GetRepository();
        var entity = await repository.GetByIdAsync<Exam>(id);
        if (entity?.UserId == await _userService.GetCurrentUserId())
        {
            await repository.RemoveAsync<Exam>(entity);
            await _uow.SaveChangesAsync();
            return await Result<Guid>.SuccessAsync(id);
        }
        return await Result<Guid>.FailAsync("Failed while removing exam");
    }
}
