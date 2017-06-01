using MT.Domain.Shared.Interfaces.Repositories;
using MT.Infra.DataEF.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MT.Infra.DataEF.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected MTContext Context { get; set; }
        protected DbSet<TEntity> DbSet { get; set; }
        public Repository(MTContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }
        public void Adicionar(TEntity model)
        {
            //Context.Entry(model).State = EntityState.Added;
            DbSet.Add(model);
        }

        public void Atualizar(TEntity model)
        {
            DbSet.Attach(model);
            Context.Entry(model).State = EntityState.Modified;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task ExcluirAsync(Expression<Func<TEntity, bool>> predicado)
        {
            var lista = await Selecionar().Where(predicado).ToListAsync();
            foreach (var item in lista)
            {
                DbSet.Attach(item);
                Context.Entry(item).State = EntityState.Deleted;
            }
        }

        public IQueryable<TEntity> Selecionar()
        {
            return DbSet.AsNoTracking();
        }


        //protected Task<List<TEntity>> Selecionar<TProperty>(Expression<Func<TEntity, bool>> predicado, params Expression<Func<TEntity, TProperty>>[] includes)
        //{
        //    var query = DbSet.Where(predicado);

        //    foreach (var item in includes)
        //    {
        //        string include;
        //        if (!TryParsePath(item.Body, out include)
        //            || include == null)
        //        {
        //            throw new ArgumentException("include errado", "includes");
        //        }
        //        else
        //        {
        //            query = query.Include(item);
        //        }
        //    }

        //    return query.ToListAsync();
        //}       

        #region EF Helps

        private Expression RemoveConvert(Expression expression)
        {
            while (expression.NodeType == ExpressionType.Convert
                   || expression.NodeType == ExpressionType.ConvertChecked)
            {
                expression = ((UnaryExpression)expression).Operand;
            }

            return expression;
        }

        private bool TryParsePath(Expression expression, out string path)
        {

            path = null;
            var withoutConvert = RemoveConvert(expression); // Removes boxing
            var memberExpression = withoutConvert as MemberExpression;
            var callExpression = withoutConvert as MethodCallExpression;

            if (memberExpression != null)
            {
                var thisPart = memberExpression.Member.Name;
                if (!TryParsePath(memberExpression.Expression, out string parentPart))
                {
                    return false;
                }
                path = parentPart == null ? thisPart : (parentPart + "." + thisPart);
            }
            else if (callExpression != null)
            {
                if (callExpression.Method.Name == "Select"
                    && callExpression.Arguments.Count == 2)
                {
                    if (!TryParsePath(callExpression.Arguments[0], out string parentPart))
                    {
                        return false;
                    }
                    if (parentPart != null)
                    {
                        if (callExpression.Arguments[1] is LambdaExpression subExpression)
                        {
                            if (!TryParsePath(subExpression.Body, out string thisPart))
                            {
                                return false;
                            }
                            if (thisPart != null)
                            {
                                path = parentPart + "." + thisPart;
                                return true;
                            }
                        }
                    }
                }
                return false;
            }

            return true;
        }
        #endregion
    }
}
