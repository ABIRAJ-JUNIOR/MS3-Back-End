using Microsoft.CodeAnalysis;
using MS3_Back_End.DTOs.RequestDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

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
                        AssessmentType = assessmentData.AssessmentType,
                        StartDate = assessmentData.StartDate,
                        EndDate = assessmentData.EndDate,
                        TotalMarks = assessmentData.TotalMarks,
                        PassMarks = assessmentData.PassMarks,
                        CreatedDate = assessmentData.CreatedDate,
                        UpdateDate = assessmentData.UpdateDate,
                        Status = assessmentData.Status,
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

            var responseList = new List<AssessmentResponseDTO>();

            foreach (var item in assessmentList)
            {
                var responseObj = new AssessmentResponseDTO()
                {
                    Id = item.Id,
                    CourseId = item.CourseId,
                    AssessmentType = item.AssessmentType,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    TotalMarks = item.TotalMarks,
                    PassMarks = item.PassMarks,
                    CreatedDate = item.CreatedDate,
                    UpdateDate = item.UpdateDate,
                    Status = item.Status,
                };
                responseList.Add(responseObj);
            }

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
                AssessmentType = updatedData.AssessmentType,
                StartDate = updatedData.StartDate,
                EndDate = updatedData.EndDate,
                TotalMarks = updatedData.TotalMarks,
                PassMarks = updatedData.PassMarks,
                CreatedDate = updatedData.CreatedDate,
                UpdateDate = updatedData.UpdateDate,
                Status = updatedData.Status,
            };

            return response;
        }
    }
}
