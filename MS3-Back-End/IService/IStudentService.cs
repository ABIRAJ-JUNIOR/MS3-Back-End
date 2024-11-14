﻿using MS3_Back_End.DTOs.RequestDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.IService
{
    public interface IStudentService
    {
        Task<StudentResponseDTO> AddStudent(StudentRequestDTO StudentReq);
        Task<List<StudentResponseDTO>> SearchStudent(string SearchText);
        Task<StudentResponseDTO> GetStudentById(Guid StudentId);
        Task<List<StudentResponseDTO>> GetAllStudent();
        Task<StudentResponseDTO> UpdateStudent(StudentUpdateDTO studentUpdate);
        Task<string> DeleteStudent(Guid Id);
    }
}