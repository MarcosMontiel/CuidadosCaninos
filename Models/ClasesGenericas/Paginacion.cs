using System.Collections.Generic;

namespace Cuidados.Caninos.Marcos.Montiel.Models.ClasesGenericas
{
    public class Paginacion<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
    }
}