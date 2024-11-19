using AdminStaff.Entities;

namespace AdminStaff.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<Student> Students { get; }
        IBaseRepository<FamilyMember> FamilyMembers { get; }
        void Complete();

    }
}
