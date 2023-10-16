using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        protected readonly RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
            _context.Set<T>().AsNoTracking() :
            _context.Set<T>();
        //Kod Aciklamasi

        /*IQueryable<T>, bir LINQ sorgusu olusturmak icin kullanilan bir turdur.
        Bu yontem, bir veri tabanindan ogeleri sorgulamak veya secmek icin kullanilir.*/

        /*Eger trackChanges false ise, AsNoTracking() yontemi cagrilir.
        Bu, veritabanindaki ogeleri "değişiklikleri takip etmeksizin" getirir ve performans acisindan iyidir.
        Eger true ise, sadece _context.Set<T>() cagrilir. Bu, "degisiklikleri takip ederek" veritabanindan ogeleri getirir.*/

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges) =>
            !trackChanges ?
            _context.Set<T>().Where(expression).AsNoTracking() :
            _context.Set<T>().Where(expression);
        //Kod Aciklamasi

        /*Expression<Func<T, bool>> parametresi, veri sorgusu icin filtreleme ifadesini temsil eder.
        Bu, bir lambda ifadesi kullanarak filtreleme kosullarini belirtmenize olanak tanir.*/

        /*Bu metot, veri tabanindan belirli bir filtreleme kosulu kullanarak ogeleri getirmenizi sağlar ve
        trackChanges parametresine bagli olarak degisiklik takibi yapabilir veya yapmayabilir.*/
    }
}
