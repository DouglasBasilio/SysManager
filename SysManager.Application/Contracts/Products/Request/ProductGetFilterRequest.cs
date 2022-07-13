using System;
using System.Collections.Generic;
using System.Text;

namespace SysManager.Application.Contracts.Products.Request
{
    public class ProductGetFilterRequest
    {
        public string Name { get; set; }
        public string Active { get; set; }
        public string CategoryId { get; set; }
        public string ProductTypeId { get; set; }
        public string UnityId { get; set; }

        /// <summary>
        /// Página atual da consulta
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// Tamanho total da página
        /// </summary>
        public int pageSize { get; set; }
    }
}
