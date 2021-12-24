using Application.Wrapper;
using Shared.Dtos.Exam;
using Shared.Dtos.Exam.Filters;
using Shared.Dtos.Exam.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Catalog.Interfaces
{
    public interface IExamService
    {
        Task<Result<Guid>> ExamCreateAsync(CreateExamRequest request);
        Task<Result<Guid>> UpdateExamAsync(UpdateExamRequest request, Guid id);
        Task<Result<Guid>> RemoveExamRequest(Guid id);
        Task<Result<ExamDto>> GetExamById(Guid id);
        Task<PaginatedResult<ExamDetailDto>> GetPaginatedResult(ExamPaginationListFilter filter);
        Task<Result<ExamAnswersDto>> GetResults(GetExamQuestionAnswers answers);

    }
}
