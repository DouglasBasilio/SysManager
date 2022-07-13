using Dapper;
using SysManager.Application.Contracts;
using SysManager.Application.Contracts.ProductsType.Request;
using SysManager.Application.Data.MySql.Entities;
using SysManager.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysManager.Application.Data.MySql.Repositories
{
    public class ProductTypeRepository
    {
        private readonly MySqlContext _context;

        public ProductTypeRepository(MySqlContext context)
        {
            this._context = context;
        }

        //POST
        public async Task<DefaultResponse> CreateAsync(ProductTypeEntity entity)
        {
            var _sql = $"INSERT INTO producttype (id, name, active) VALUE('{entity.Id}', '{entity.Name}', {entity.Active})";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.ExecuteAsync(_sql);
                if (result > 0)
                    return new DefaultResponse(entity.Id.ToString(), "Tipo de produto criado com sucesso", false);
            }
            return new DefaultResponse(entity.Id.ToString(), "Falha ao tentar cadastrar um tipo de produto", true);
        }

        //PUT
        public async Task<DefaultResponse> UpdateAsync(ProductTypeEntity entity)
        {
            var _sql = $"update producttype set name = '{entity.Name}', active = {entity.Active} where id = '{entity.Id}'";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.ExecuteAsync(_sql);
                if (result > 0)
                    return new DefaultResponse(entity.Id.ToString(), "Tipo de produto alterado com sucesso", false);
            }
            return new DefaultResponse(entity.Id.ToString(), "Falha ao tentar alterar um tipo de produto", true);
        }

        //GET
        public async Task<ProductTypeEntity> GetProductTypeByIdAsync(Guid id)
        {
            var _sql = $"select id, name, active from producttype where id = '{id}' limit 1";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.QueryFirstOrDefaultAsync<ProductTypeEntity>(_sql);
                return result;
            }
        }

        public async Task<ProductTypeEntity> GetByNameAsync(string name)
        {
            var _sql = $"SELECT id, name, active from producttype WHERE name = '{name}' limit 1";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.QueryFirstOrDefaultAsync<ProductTypeEntity>(_sql);
                return result;
            }
        }

        //Pagination
        public async Task<PaginationResponse<ProductTypeEntity>> GetByFilterAsync(ProductTypeGetFilterRequest filter)
        {
            var _sql = new StringBuilder("select * from producttype where 1=1");
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
                var result = await cnx.QueryAsync<ProductTypeEntity>(_sql.ToString());
                var resultCount = await cnx.QueryAsync<int>("select count(*) as count from producttype where 1=1" + _where.ToString());
                return new PaginationResponse<ProductTypeEntity>
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
            var _sql = $"delete from producttype where id = '{id}'";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.ExecuteAsync(_sql);
                if (result > 0)
                    return new DefaultResponse(id.ToString(), "Tipo de produto excluído com sucesso", false);
            }
            return new DefaultResponse(id.ToString(), "Falha ao tentar excluir um tipo de produto", true);
        }
    }
}
