using EntityConfigurationBase;
using School.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Repository
{
    public interface IRepository<TEntity, key>
        where TEntity : class, IEntity<key>
    {
        //IQueryable接口的泛型给的就是可协变的
        Task<AjaxResult> ChangeEntitiesAsync(TEntity entity);
        Task<AjaxResult> DeleteEntities(TEntity entity);
        IQueryable<TEntity> GetEntities<T>(Expression<Func<TEntity, bool>> expression);

    }
}
