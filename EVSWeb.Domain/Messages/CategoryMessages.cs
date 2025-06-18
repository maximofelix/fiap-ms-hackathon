using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSWeb.Domain.Messages
{
    public static class CategoryMessages
    {

#if LANG_US
        public const string CATEGORY_NOTFOUND = "Category not found";
        public const string CATEGORY_DUPLICATEDNAME = "Category Name alread exists";
        ...
#else
        public const string CATEGORY_NOTFOUND = "Categoria não encontrada";
        public const string CATEGORY_DUPLICATEDNAME = "Já existe uma categoria com o nome informado";
        public const string CATEGORY_VIOLATIONFK_PRODUTO = "Não é possível remover uma categoria com produtos associados";
        private const string CATEGORY_notfound_byid = "Não foi encontrado uma Categoria com ID {0}";
#endif
        public static string CATEGORY_NOTFOUND_BYID(Guid CATEGORYId)
        {
            return string.Format(CATEGORY_notfound_byid, CATEGORYId) ?? "";
        }
    }
}
