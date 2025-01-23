using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories
{
    // Abstract bir sınıf tanımlıyoruz. Bu sınıf, türetilen sınıflar için bir temel (base) sağlar.
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
    // Generic tip T için bazı kısıtlamalar getiriyoruz:
    // 1. T bir referans tipi (class) olmalıdır.
    // 2. T'nin parametresiz bir public kurucusu (constructor) olmalıdır.
    where T : class, new() 
    {
        // new lenemeyecek
        // temel sınıflar abstract olacak. 
        // ilgili sınıflar eksikleri tamamlayacak

        // devraldığımız sınıflarda da kullanabilmek için protected yaptık
        protected readonly RepositoryContext _context; // veritabanına erişim sağlayacak

        protected RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        // Bu metot, T türündeki tüm nesneleri sorgulamak için bir şablondur.
        // IQueryable<T>: Veritabanından sorgulama yapılabilen, LINQ sorgularını destekleyen bir arayüzdür.
        // trackChanges: Değişiklik izleme (tracking) etkin olup olmadığını belirler.
        public IQueryable<T> FindAll(bool trackChanges)
        {
            // Henüz bu metot implement edilmediği için bir NotImplementedException fırlatıyoruz.
            // Türetilen sınıflar bu metodu kendilerine özgü bir şekilde dolduracaktır.
            return trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking();
        }

        public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges 
                ? _context.Set<T>().Where(expression).SingleOrDefault() 
                : _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault(); 
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        } 

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}