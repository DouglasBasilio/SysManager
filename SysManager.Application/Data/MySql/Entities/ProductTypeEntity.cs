using SysManager.Application.Contracts.ProductsType.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SysManager.Application.Data.MySql.Entities
{
    [Table("producttype")]
    public class ProductTypeEntity
    {
        public ProductTypeEntity(){}

        public ProductTypeEntity(ProductTypePostRequest request)
        {
            this.Id = Guid.NewGuid();
            this.Name = request.Name;
            this.Active = request.Active;
        }

        public ProductTypeEntity(ProductTypePutRequest request)
        {
            this.Id = request.Id;
            this.Name = request.Name;
            this.Active = request.Active;
        }

        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("active")]
        public bool Active { get; set; }
    }
}
