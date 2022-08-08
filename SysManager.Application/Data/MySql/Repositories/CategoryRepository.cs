using Dapper;
using SysManager.Application.Contracts;
using SysManager.Application.Contracts.Category.Request;
using SysManager.Application.Data.MySql.Entities;
using SysManager.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysManager.Application.Data.MySql.Repositories
{
    public class CategoryRepository
    {
        private readonly MySqlContext _context;

        public CategoryRepository(MySqlContext context)
        {
            this._context = context;
        }

        public async Task<DefaultResponse> CreateAsync(CategoryEntity entity)
        {
            string strQuery = @"insert into category(id, name, active)
                                          Values(@id, @name, @active)";

            using (var cnx = _context.Connection())
            {
                var result = await cnx.ExecuteAsync(strQuery, new
                {
                    id = entity.Id,
                    name = entity.Name,
                    active = entity.Active
                });

                if (result > 0)
                    return new DefaultResponse(entity.Id.ToString(), "Categoria criada com sucesso", false);
            }
            return new DefaultResponse(entity.Id.ToString(), "Erro ao tentar criar Categoria", true);
        }

        public async Task<DefaultResponse> UpdateAsync(CategoryEntity entity)
        {
            string strQuery = $"update category set name = '{entity.Name}', active = {entity.Active} where id = '{entity.Id}'";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.ExecuteAsync(strQuery);

                if (result > 0)
                    return new DefaultResponse(entity.Id.ToString(), "Categoria alterada com sucesso", false);
            }
            return new DefaultResponse(entity.Id.ToString(), "Erro ao tentar alterar Categoria", true);
        }

        public async Task<DefaultResponse> DeleteByIdAsync(Guid id)
        {
            string strQuery = $"delete from category where id = '{id}'";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.ExecuteAsync(strQuery);
                if (result > 0)
                    return new DefaultResponse(id.ToString(), "Categoria excluída com sucesso", false);
            }
            return new DefaultResponse(id.ToString(), "Erro ao tentar excluir Categoria", true);
        }

        public async Task<CategoryEntity> GetCategoryByIdAsync(Guid id)
        {
            string strQuery = $"select id, name, active from category where id = '{id}' ";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.QueryFirstOrDefaultAsync<CategoryEntity>(strQuery);
                return result;
            }
        }

        public async Task<CategoryEntity> GetCategoryByNameAsync(string name)
        {
            string strQuery = $"select id, name, active from category where name = '{name}' limit 1";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.QueryFirstOrDefaultAsync<CategoryEntity>(strQuery);
                return result;
            }
        }

        public async Task<PaginationResponse<CategoryEntity>> GetCategoryByFilterAsync(CategoryGetFilterRequest filter)
        {
            using (var cnx = _context.Connection())
            {
                var _sql = new StringBuilder("select * from category where 1=1");
                var where = new StringBuilder();

                if (!string.IsNullOrEmpty(filter.Name))
                    where.Append(" AND name like '%" + filter.Name + "%'");

                if (filter.Active.ToLower() != "todos")
                {
                    string _booleanFilter = "";
                    if (filter.Active.ToLower() == "ativos")
                        _booleanFilter = " AND active = true";
                    else if (filter.Active.ToLower() == "inativos")
                        _booleanFilter = " AND active = false";

                    where.Append(_booleanFilter);
                }

                _sql.Append(where);

                if (filter.page > 0 && filter.pageSize > 0)
                    _sql.Append($" Limit {filter.pageSize * (filter.page - 1)}, {filter.pageSize}");

                var result = await cnx.QueryAsync<CategoryEntity>(_sql.ToString());
                var result2 = await cnx.QueryAsync<int>("select count(*) as count from category where 1=1 " + where.ToString());
                var totalRows = result2.FirstOrDefault();

                return new PaginationResponse<CategoryEntity>
                {
                    Items = result.ToArray(),
                    _pageSize = filter.pageSize,
                    _page = filter.page,
                    _total = totalRows
                };

            }
        }
    }
}
