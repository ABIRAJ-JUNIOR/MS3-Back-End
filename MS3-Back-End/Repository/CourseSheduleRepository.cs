﻿using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class CourseSheduleRepository : ICourseSheduleRepository
    {
        private readonly AppDBContext _Db;
        public CourseSheduleRepository(AppDBContext db)
        {
            _Db = db;

        }

        public async Task<CourseSchedule> AddCourseShedule(CourseSchedule courseReq)
        {
                var data = await _Db.CourseSchedules.AddAsync(courseReq);
                await _Db.SaveChangesAsync();
                return data.Entity;
        }

        public  Task<List<CourseSchedule>> SearchSheduleLocation(string SearchText)
        {
            var data =  _Db.CourseSchedules.Where(n => n.Location.Contains(SearchText)).ToListAsync();
            return data;
        }

        public async Task<List<CourseSchedule>> GetAllCourseShedule()
        {
            var data = await _Db.CourseSchedules.ToListAsync();
            return data;
        }

        public async Task<CourseSchedule> GetCourseSheduleById(Guid id)
        {
            var data = await _Db.CourseSchedules.SingleOrDefaultAsync(c => c.Id == id);
            return data!;
        }

        public async Task<CourseSchedule> UpdateCourse(CourseSchedule course)
        {
            var data = _Db.CourseSchedules.Update(course);
            await _Db.SaveChangesAsync();
            return data.Entity;
        }
       
    }
}
