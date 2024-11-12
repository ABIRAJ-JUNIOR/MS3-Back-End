﻿using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IAssessmentRepository
    {
        Task<Assessment> AddAssessment(Assessment assessment);
        Task<ICollection<Assessment>> GetAllAssessment();
    }
}
