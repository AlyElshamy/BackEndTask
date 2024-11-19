using AdminStaff.Data;
using AdminStaff.Interfaces;
using AdminStaff.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminStaff.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IBaseRepository<Student> Students { get; private set; }
        public IBaseRepository<FamilyMember> FamilyMembers { get; private set; }

        public UnitOfWork(AppDbContext Context)
        {
            _dbContext= Context ;
            Students = new BaseRepository<Student>(_dbContext);
            FamilyMembers = new BaseRepository<FamilyMember>(_dbContext);

        }
        public void Complete()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
