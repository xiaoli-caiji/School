using EntityConfigurationBase;
using Microsoft.EntityFrameworkCore;
using School.Data;
using SchoolCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace School.Core.Repository
{
    public class Repository<TEntity, key> : IRepository<TEntity, key>
        where TEntity : class, IEntity<key>
    {
        private readonly BaseDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
            //_dbContext.ChangeTracker.LazyLoadingEnabled = true;
            _dbSet = _dbContext.Set<TEntity>();
            
        }

        public IQueryable<TEntity> GetEntities<T>(Expression<Func<TEntity, bool>> expression)
        {
            IQueryable<TEntity> query = _dbSet;
            if (expression == null)
            {
                return query;
            }
            return query.Where(expression);
        }
        public async Task<AjaxResult> ChangeEntitiesAsync(TEntity entity)
        {
            //增加或修改某实体信息
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            string content = "数据更新失败！";
            AjaxResultType resultType = AjaxResultType.Error;
            switch (_dbContext.Update(entity).State)
            {
                case EntityState.Detached:
                    content += "该实例未被检索到！";
                    break;
                case EntityState.Unchanged:
                    content += "该实例未能修改！";
                    break;
                case EntityState.Modified:
                    content = "数据更新成功！";
                    resultType = AjaxResultType.Success;
                    break;
                default:
                    content += "发生未知错误！";
                    break;
            }
            return new AjaxResult(content, resultType);
        }
        public async Task<AjaxResult> DeleteEntities(TEntity entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return new AjaxResult("数据删除成功！", AjaxResultType.Success);
        }
    }
}
