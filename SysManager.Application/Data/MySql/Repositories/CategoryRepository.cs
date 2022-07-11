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

        //POST
        public async Task<DefaultResponse> CreateAsync(CategoryEntity entity)
        {
            var _sql = $"INSERT INTO category (id, name, active) VALUE('{entity.Id}', '{entity.Name}', {entity.Active})";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.ExecuteAsync(_sql);
                if (result > 0)
                    return new DefaultResponse(entity.Id.ToString(), "Categoria criada com sucesso", false);
            }
            return new DefaultResponse(entity.Id.ToString(), "Falha ao tentar cadastrar uma categoria", true);
        }
        //PUT
        public async Task<DefaultResponse> UpdateAsync(CategoryEntity entity)
        {
            var _sql = $"update category set name = '{entity.Name}', active = {entity.Active} where id = '{entity.Id}'";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.ExecuteAsync(_sql);
                if (result > 0)
                    return new DefaultResponse(entity.Id.ToString(), "Categoria alterada com sucesso", false);
            }
            return new DefaultResponse(entity.Id.ToString(), "Falha ao tentar alterar uma categoria", true);
        }

        //GET
        public async Task<CategoryEntity> GetByIdAsync(Guid id)
        {
            var _sql = $"select id, name, active from category where id = '{id}' limit 1";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.QueryFirstOrDefaultAsync<CategoryEntity>(_sql);
                return result;
            }
        }

        public async Task<CategoryEntity> GetByNameAsync(string name)
        {
            var _sql = $"SELECT id, name, active from category WHERE name = '{name}' limit 1";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.QueryFirstOrDefaultAsync<CategoryEntity>(_sql);
                return result;
            }
        }

        public async Task<PaginationResponse<CategoryEntity>> GetByFilterAsync(CategoryGetFilterRequest filter)
        {
            var _sql = new StringBuilder("select * from category where 1=1");
            var _where = new StringBuilder();

            if (!string.IsNullOrEmpty(filter.Name))
                _where.Append($" AND name like '%{filter.Name}%'");
            if (filter.Active.ToLower() != "todos")
            {
                string _activeFilter = "";

                if (filter.Active.ToLower() == "ativos")
                    _activeFilter = " AND active = true";
                else if (filter.Active.ToLower() == "inativos")
                    _activeFilter = " AND active = false";

                _where.Append(_activeFilter);
            }
            _sql.Append(_where);

            if (filter.page > 0 && filter.pageSize > 0)
                _sql.Append($" limit {filter.pageSize * (filter.page - 1)}, {filter.pageSize}");
            using (var cnx = _context.Connection())
            {
                var result = await cnx.QueryAsync<CategoryEntity>(_sql.ToString());
                var resultCount = await cnx.QueryAsync<int>("select count(*) as count from category where 1=1" + _where.ToString());
                return new PaginationResponse<CategoryEntity>
                {
                    _page = filter.page,
                    _pageSize = filter.pageSize,
                    _total = resultCount.FirstOrDefault()
,
                    Items = result.ToArray()
                };
            }
        }

        //DELETE
        public async Task<DefaultResponse> DeleteByIdAsync(Guid id)
        {
            var _sql = $"delete from category where id = '{id}'";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.ExecuteAsync(_sql);
                if (result > 0)
                    return new DefaultResponse(id.ToString(), "Categoria excluída com sucesso", false);
            }
            return new DefaultResponse(id.ToString(), "Falha ao tentar excluir uma categoria", true);
        }
    }
}
