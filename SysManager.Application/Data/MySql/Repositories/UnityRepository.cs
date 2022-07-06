using Dapper;
using SysManager.Application.Contracts;
using SysManager.Application.Data.MySql.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SysManager.Application.Data.MySql.Repositories
{
    public class UnityRepository
    {
        private readonly MySqlContext _context;

        public UnityRepository(MySqlContext context)
        {
            this._context = context;
        }

        //POST
        public async Task<DefaultResponse> CreateAsync(UnityEntity entity)
        {
            var _sql = $"INSERT INTO unity(id, name, active) VALUE('{entity.Id}', '{entity.Name}', {entity.Active})";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.ExecuteAsync(_sql);
                if (result > 0)
                    return new DefaultResponse(entity.Id.ToString(), "Unidade de medida criada com sucesso", false);
            }
            return new DefaultResponse(entity.Id.ToString(),"Falha ao tentar cadastrar uma unidade de medida", true);
        }
        //PUT
        public async Task<DefaultResponse> UpdateAsync(UnityEntity entity)
        {
            var _sql = $"update unity set name = '{entity.Name}', active = {entity.Active} where id = '{entity.Id}'";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.ExecuteAsync(_sql);
                if (result > 0)
                    return new DefaultResponse(entity.Id.ToString(), "Unidade de medida alterada com sucesso", false);
            }
            return new DefaultResponse(entity.Id.ToString(), "Falha ao tentar alterar uma unidade de medida", true);
        }

        //GET
        public async Task<UserEntity> GetByIdAsync(Guid id)
        {
            var _sql = $"select id, name, active from unity where id = '{id}' limit 1";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.QueryFirstOrDefaultAsync<UserEntity>(_sql);
                return result;
            }
        }

        public async Task<UnityEntity> GetByNameAsync(string name)
        {
            var _sql = $"SELECT id, name, active from unity WHERE name = '{name}' limit 1";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.QueryFirstOrDefaultAsync<UnityEntity>(_sql);
                return result;
            }
        }

        //DELETE
        public async Task<DefaultResponse> DeleteByIdAsync(Guid id)
        {
            var _sql = $"delete from unity where id = '{id}'";
            using (var cnx = _context.Connection())
            {
                var result = await cnx.ExecuteAsync(_sql);
                if (result > 0)
                    return new DefaultResponse(id.ToString(), "Unidade de medida excluída com sucesso", false);
            }
            return new DefaultResponse(id.ToString(), "Falha ao tentar excluir uma unidade de medida", true);
        }
    }
}
