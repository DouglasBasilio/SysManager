using System;
using System.Collections.Generic;
using System.Text;

namespace SysManager.Application.Helpers
{
    public class PaginationResponse<T> where T : class
    {
        /// <summary>
        /// tamanho da página
        /// </summary>
        public int _pageSize { get; set; }

        /// <summary>
        /// página atual
        /// </summary>
        public int _page { get; set; }

        /// <summary>
        /// Total de registros da pesquisa
        /// </summary>
        public int _total { get; set; }

        /// <summary>
        /// Lista de itens resultado da pesquisa paginada
        /// </summary>
        public T[] Items { get; set; }
    }
}
