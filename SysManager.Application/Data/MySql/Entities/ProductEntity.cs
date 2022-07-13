using SysManager.Application.Contracts.Products.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SysManager.Application.Data.MySql.Entities
{
    [Table("product")]
    public class ProductEntity
    {
        public ProductEntity(){}

        public ProductEntity(ProductPostRequest request)
        {
            this.Id = Guid.NewGuid();
            this.ProductCode = request.ProductCode;
            this.Name = request.Name;
            this.ProductTypeId = request.ProductTypeId;
            this.CategoryId = request.CategoryId;
            this.UnityId = request.UnityId;
            this.CostPrice = request.CostPrice;
            this.Price = request.Price;
            this.Percentage = request.Percentage;
            this.Active = request.Active;
        }

        public ProductEntity(ProductPutRequest request)
        {
            this.Id = request.Id;
            this.ProductCode = request.ProductCode;
            this.Name = request.Name;
            this.ProductTypeId = request.ProductTypeId;
            this.CategoryId = request.CategoryId;
            this.UnityId = request.UnityId;
            this.CostPrice = request.CostPrice;
            this.Price = request.Price;
            this.Percentage = request.Percentage;
            this.Active = request.Active;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("productCode")]
        public string ProductCode { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("productTypeId")]
        public string ProductTypeId { get; set; }

        [Column("categoryId")]
        public string CategoryId { get; set; }

        [Column("unityId")]
        public string UnityId { get; set; }

        [Column("costPrice")]
        public decimal CostPrice { get; set; }

        [Column("percentage")]
        public decimal Percentage { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("active")]
        public bool Active { get; set; }
    }
}
