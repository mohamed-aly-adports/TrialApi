using Microsoft.EntityFrameworkCore.Storage;
using Trial.Application.Repository;

namespace Trial.Application.Interfaces
{
    public interface IUniteOfWork :IDisposable
    {
        IUserRepository Users { get; }  
        void ChangeDatabase(string database); 
        Task<int> SaveChanges();
        int ExecuteSqlCommand(string sql, params object[] parameters); // تنفيذ أمر SQL خام وإرجاع عدد الكيانات المتأثرة
        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class; // تنفيذ استعلام SQL خام وإرجاع النتائج كـ IQueryable
       Task <IDbContextTransaction> BeginTransaction();
        Task RollbackTransaction();
        Task CommitTransaction();
        bool HasActiveTransaction { get; }
        #region advanced
        //IAsyncRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class; // الحصول على المستودع المناسب للكائن، اختيار مخصص أو عام
        //void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback); // استخدام TrackGraph لربط الكيانات غير المرتبطة بتتبع التغييرات
        #endregion
    }
}
