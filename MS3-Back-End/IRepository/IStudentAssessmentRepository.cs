﻿using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IStudentAssessmentRepository
    {
        Task<ICollection<StudentAssessment>> GetAllAsync();
    }
}
