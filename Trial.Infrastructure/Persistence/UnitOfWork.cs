using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Trial.Application.Interfaces;

namespace Trial.Infrastructure.Persistence
{
    public class UnitOfWork : IUniteOfWork
    {
        private readonly DbContext _context;
        private IDbContextTransaction _transaction;

        // استلام الـ DbContext من خلال الكونستركتور (يمكن حقنه عبر DI)
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        // تغيير قاعدة البيانات (حسب دعم الـ provider)
        public void ChangeDatabase(string database)
        {
            _context.Database.GetDbConnection().ChangeDatabase(database);
        }

        // حفظ التغييرات بشكل غير متزامن
        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        // تنفيذ أمر SQL خام وإرجاع عدد الصفوف المتأثرة
        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlRaw(sql, parameters);
        }

        // تنفيذ استعلام SQL خام وإرجاع النتائج كـ IQueryable
        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return _context.Set<TEntity>().FromSqlRaw(sql, parameters);
        }

        // بدء معاملة غير متزامنة
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
            return _transaction;
        }

        // تنفيذ commit للمعاملة الحالية بطريقة غير متزامنة
        public async Task CommitTransaction()
        {
            if (_transaction != null)
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        // تنفيذ rollback للمعاملة الحالية بطريقة غير متزامنة
        public async Task RollbackTransaction()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        // التخلص من الموارد المُستخدمة (تصريف DbContext والمعاملة)
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
            _context.Dispose();
        }

        public bool HasActiveTransaction => _transaction != null;
    }
}
