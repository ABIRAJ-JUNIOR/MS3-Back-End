using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.StudentAssessment
{
    public class EvaluationRequestDTO
    {
        public int MarksObtaines { get; set; }
        public string FeedBack { get; set; } = string.Empty;

    }
}
