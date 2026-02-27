using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.AuditLog;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MS3_Back_End.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDBContext _dbContext;

        public AdminRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Admin> AddAdmin(Admin admin)
        {
            var adminData = await _dbContext.Admins.AddAsync(admin);
            await _dbContext.SaveChangesAsync();
            return adminData.Entity;
        }

        public async Task<Admin> GetAdminByNic(string nic)
        {
            var adminData = await _dbContext.Admins.Include(al => al.AuditLogs).Where(a => a.IsActive != false).SingleOrDefaultAsync(a => a.Nic.ToLower() == nic.ToLower());
            return adminData!;
        }

        public async Task<Admin> GetAdminById(Guid id)
        {
            var adminData = await _dbContext.Admins.Include(al => al.AuditLogs).Where(a => a.IsActive != false).SingleOrDefaultAsync(a => a.Id == id);
            return adminData!;
        }

        public async Task<AdminAllDataResponseDTO> GetAdminFullDetailsById(Guid id)
        {
            var adminData = await (
                from admin in _dbContext.Admins
                where admin.IsActive != false && admin.Id == id

                join auditLog in _dbContext.AuditLogs
                    on admin.Id equals auditLog.AdminId into auditLogsGroup
                from auditLog in auditLogsGroup.DefaultIfEmpty()

                join user in _dbContext.Users
                    on admin.Id equals user.Id into usersGroup
                from user in usersGroup.DefaultIfEmpty()
                join userRole in _dbContext.UserRoles
                                    on user.Id equals userRole.UserId into userRoleGroup
                from userRole in userRoleGroup.DefaultIfEmpty()
                join role in _dbContext.Roles
                    on userRole.RoleId equals role.Id into roleGroup
                from role in roleGroup.DefaultIfEmpty()
                where admin.IsActive != false
                select new AdminAllDataResponseDTO
                {
                    Id = admin.Id,
                    RoleName = role.Name,
                    Nic = admin.Nic,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Phone = admin.Phone,
                    Email = user.Email,
                    ImageUrl = admin.ImageUrl,
                    CoverImageUrl = admin.CoverImageUrl,
                    CreatedDate = admin.CreatedDate,
                    UpdatedDate = admin.UpdatedDate,
                    IsActive = admin.IsActive,
                    AuditLogs = admin.AuditLogs!.Select(a => new AuditLogResponseDTO
                    {
                        Id = a.Id,
                        AdminId = a.AdminId,
                        ActionDate = a.ActionDate,
                        Details = a.Details,
                        Action = a.Action,
                        AdminResponse = null!,
                    }).ToList()
                }
            ).FirstOrDefaultAsync();

            return adminData!;
        }


        public async Task<ICollection<Admin>> GetAllAdmins()
        {
            var adminsList = await _dbContext.Admins.Include(al => al.AuditLogs).Where(a => a.IsActive != false).ToListAsync();
            return adminsList;
        }

        public async Task<Admin> UpdateAdmin(Admin admin)
        {
            var updatedData = _dbContext.Admins.Update(admin);
            await _dbContext.SaveChangesAsync();
            return updatedData.Entity;
        }


        public async Task<ICollection<AdminWithRoleDTO>> GetPaginatedAdmin(int pageNumber, int pageSize)
        {

            var result = await (from admin in _dbContext.Admins
                                join user in _dbContext.Users
                                    on admin.Id equals user.Id into userGroup
                                from user in userGroup.DefaultIfEmpty() 
                                join userRole in _dbContext.UserRoles
                                    on user.Id equals userRole.UserId into userRoleGroup
                                from userRole in userRoleGroup.DefaultIfEmpty() 
                                join role in _dbContext.Roles
                                    on userRole.RoleId equals role.Id into roleGroup
                                from role in roleGroup.DefaultIfEmpty() 
                                where admin.IsActive != false
                                orderby admin.CreatedDate descending
                                select new AdminWithRoleDTO
                                {
                                    Id = admin.Id,
                                    RoleName = role.Name,
                                    Nic = admin.Nic,
                                    FirstName = admin.FirstName,
                                    LastName = admin.LastName,
                                    Phone = admin.Phone,
                                    Email = user.Email,
                                    ImageUrl = admin.ImageUrl,
                                    CreatedDate = admin.CreatedDate,
                                    UpdatedDate = admin.UpdatedDate,
                                    IsActive = admin.IsActive,
                                    AuditLogs = admin.AuditLogs!.Select(a => new AuditLogResponseDTO
                                    {
                                        Id = a.Id,
                                        AdminId = a.AdminId,
                                        ActionDate = a.ActionDate,
                                        Details = a.Details,
                                        Action = a.Action,
                                    }).ToList()
                                })
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            return result;

        }

        public async Task<Admin> DeleteAdmin(Admin admin)
        {
            var admindata =  _dbContext.Admins.Update(admin);
            await _dbContext.SaveChangesAsync();
            return admindata.Entity;
        }
    }
}
