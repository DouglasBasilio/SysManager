﻿using Dapper;
using SysManager.Application.Contracts;
using SysManager.Application.Data.MySql.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SysManager.Application.Data.MySql.Repositories
{
    /// <summary>
    /// Classe responsável por se comunicar com o Banco de dados
    /// e executar os comandos de CRUD
    /// </summary>
    public class UserRepository
    {
        private readonly MySqlContext _context;
        public UserRepository(MySqlContext context)
        {
            this._context = context;
        }

        public async Task<DefaultResponse> CreateUser(UserEntity entity)
        {
            var _sql = @$"INSERT INTO user(id, userName, email, password, active)
                                     VALUE(@id, @userName, @email, @password, @active)";

            // o metodo 'using' abre uma conexão, passa para uma variável, executa e finaliza
            using (var cnx = _context.Connection())
            {
                var mapper = new
                {
                    id = entity.Id,
                    userName = entity.UserName,
                    email = entity.Email,
                    password = entity.Password,
                    active = entity.Active
                };
                // executa o script no banco de dados
                var result = await cnx.ExecuteAsync(_sql, mapper);
                if (result > 0)
                    return new DefaultResponse(entity.Id.ToString(), "Conta criada com sucesso", false);
            }

            return new DefaultResponse("", "Erro ao tentar criar uma conta", true); ;
        }

        public async Task<UserEntity> UpdateUser(string password, Guid id)
        {
            var _sql = @$"UPDATE user set password = '{password}' where id = '{id}'";

            return new UserEntity();
        }
        public async Task<UserEntity> GetUserByEmail(string email)
        {
            var _sql = @$"SELECT id, userName, email, password, active from user
                          WHERE email = '{email}' limit 1";

            using (var cnx = _context.Connection())
            {
               // executa o script no banco de dados
               var result = await cnx.QueryFirstOrDefaultAsync<UserEntity>(_sql);
               return result;
            }

        }
        public async Task<UserEntity> InactiveUser(Guid id)
        {
            var _sql = @$"UPDATE user set active = false where id = '{id}'";
            return new UserEntity();
        }
    }
}
