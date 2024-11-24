using Microsoft.CodeAnalysis;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.AuditLog;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MS3_Back_End.Service
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentRepository _repository;

        public AssessmentService(IAssessmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<AssessmentResponseDTO> AddAssessment(AssessmentRequestDTO request)
        {
            if(request.StartDate >=  DateTime.Now)
            {
                if (request.EndDate > request.StartDate)
                {
                    var assessment = new Assessment()
                    {
                        CourseId = request.CourseId,
                        AssessmentType = request.AssessmentType,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        TotalMarks = request.TotalMarks,
                        PassMarks = request.PassMarks,
                        CreatedDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Status = AssessmentStatus.NotStarted,
                    };

                    var assessmentData = await _repository.AddAssessment(assessment);

                    var response = new AssessmentResponseDTO()
                    {
                        Id = assessmentData.Id,
                        CourseId = assessmentData.CourseId,
                        AssessmentType = ((AssessmentType)assessmentData.AssessmentType).ToString(),
                        StartDate = assessmentData.StartDate,
                        EndDate = assessmentData.EndDate,
                        TotalMarks = assessmentData.TotalMarks,
                        PassMarks = assessmentData.PassMarks,
                        CreatedDate = assessmentData.CreatedDate,
                        UpdateDate = assessmentData.UpdateDate,
                        Status = ((AssessmentStatus)assessmentData.Status).ToString(),
                    };

                    return response;
                }
                else
                {
                    throw new Exception("Invalid End Date");
                }
            }
            else
            {
                throw new Exception("Invalid Start Date");
            }
        }

        public async Task<ICollection<AssessmentResponseDTO>> GetAllAssessment()
        {
            var assessmentList = await _repository.GetAllAssessment();

            var responseList = assessmentList.Select(item => new AssessmentResponseDTO()
            {
                Id = item.Id,
                CourseId = item.CourseId,
                AssessmentType = ((AssessmentType)item.AssessmentType).ToString(),
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                TotalMarks = item.TotalMarks,
                PassMarks = item.PassMarks,
                CreatedDate = item.CreatedDate,
                UpdateDate = item.UpdateDate,
                Status = ((AssessmentStatus)item.Status).ToString(),
            }).ToList();

            return responseList;
        }

        public async Task<AssessmentResponseDTO> UpdateAssessment(Guid id , UpdateAssessmentRequestDTO request)
        {
            var assessment = await _repository.GetAssessmentById(id);
            if(assessment == null)
            {
                throw new Exception("Assessment not found");
            }

            assessment.AssessmentType = request.AssessmentType;
            assessment.StartDate = request.StartDate;
            assessment.EndDate = request.EndDate;
            assessment.TotalMarks = request.TotalMarks;
            assessment.PassMarks = request.PassMarks;
            assessment.UpdateDate = DateTime.Now;
            assessment.Status = request.Status;

            var updatedData = await _repository.UpdateAssessment(assessment);

            var response = new AssessmentResponseDTO()
            {
                Id = updatedData.Id,
                CourseId = updatedData.CourseId,
                AssessmentType = ((AssessmentType)updatedData.AssessmentType).ToString(),
                StartDate = updatedData.StartDate,
                EndDate = updatedData.EndDate,
                TotalMarks = updatedData.TotalMarks,
                PassMarks = updatedData.PassMarks,
                CreatedDate = updatedData.CreatedDate,
                UpdateDate = updatedData.UpdateDate,
                Status = ((AssessmentStatus)updatedData.Status).ToString(),
            };

            return response;
        }
        public async Task<PaginationResponseDTO<AssessmentResponseDTO>> GetPaginatedAssessment(int pageNumber, int pageSize)
        {
            var allAssessment = await _repository.GetAllAssessment();
            if (allAssessment == null)
            {
                throw new Exception("Admins Not Found");
            }

            var assessments = await _repository.GetPaginatedAssessment(pageNumber, pageSize);

            var response = assessments.Select(item => new AssessmentResponseDTO()
            {
                Id = item.Id,
                CourseId = item.CourseId,
                AssessmentType = ((AssessmentType)item.AssessmentType).ToString(),
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                TotalMarks = item.TotalMarks,
                PassMarks = item.PassMarks,
                CreatedDate = item.CreatedDate,
                UpdateDate = item.UpdateDate,
                Status = ((AssessmentStatus)item.Status).ToString(),
                courseResponse = new CourseResponseDTO()
                {
                    Id = item.Course.Id,
                    CourseCategoryId = item.Course.CourseCategoryId,
                    CourseName = item.Course.CourseName,
                    Level = ((CourseLevel)item.Course.Level).ToString(),
                    CourseFee = item.Course.CourseFee,
                    Description = item.Course.Description,
                    Prerequisites = item.Course.Prerequisites,
                    ImageUrl = item.Course.ImageUrl,
                    CreatedDate = item.Course.CreatedDate,
                    UpdatedDate = item.Course.UpdatedDate,
                },
                studentAssessmentResponses = item.StudentAssessments != null ? item.StudentAssessments.Select(sa => new StudentAssessmentResponseDTO()
                {
                    Id = sa.Id,
                    MarksObtaines = sa.MarksObtaines,
                    Grade = sa.Grade != null ? ((Grade)sa.Grade).ToString() : null,
                    FeedBack = sa.FeedBack,
                    DateEvaluated = sa.DateEvaluated,
                    DateSubmitted = sa.DateSubmitted,
                    StudentAssessmentStatus = ((StudentAssessmentStatus)sa.StudentAssessmentStatus).ToString(),
                    StudentId = sa.StudentId,
                    AssessmentId = sa.AssessmentId,
                    AssessmentResponse = null!,
                }).ToList() : new List<StudentAssessmentResponseDTO>()

            }).ToList(); ;

            var paginationResponseDto = new PaginationResponseDTO<AssessmentResponseDTO>
            {
                Items = response,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(allAssessment.Count / (double)pageSize),
                TotalItem = allAssessment.Count,
            };

            return paginationResponseDto;
        }
    }
}
