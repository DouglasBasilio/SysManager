using System;
using System.Collections.Generic;
using System.Text;

namespace SysManager.Application.Contracts.ProductsType.Request
{
    public class ProductTypeGetFilterRequest
    {
        public string Name { get; set; }
        public string Active { get; set; }

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
