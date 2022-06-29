using SysManager.Application.Contracts.Users.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SysManager.Application.Data.MySql.Entities
{
    [Table("user")]
    public class UserEntity
    {
        public UserEntity(UserPostRequest request)
        {
            this.Id = Guid.NewGuid();
            this.UserName = request.UserName;
            this.Email = request.Email;
            this.Password = request.Password;
            this.Active = true;
        }

        public UserEntity()
        {

        }

        // quem vai gerar o ID é a classe e não o banco de dados
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("userName")]
        public string UserName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("active")]
        public bool Active { get; set; }
    }
}
