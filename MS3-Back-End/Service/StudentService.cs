﻿using MS3_Back_End.DBContext;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _StudentRepo;
        public StudentService(IStudentRepository StudentRepo)
        {
            _StudentRepo = StudentRepo;
        }

    }
}