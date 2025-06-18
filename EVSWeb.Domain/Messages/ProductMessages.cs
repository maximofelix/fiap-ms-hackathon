using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVSWeb.Domain.Messages
{
    public static class ProductMessages
    {

#if LANG_US
        public const string PRODUCT_NOTFOUND = "Product not found";
        public const string PRODUCT_DUPLICATEDNAME = "Product Name alread exists";
        ...
#else
        public const string PRODUCT_NOTFOUND = "Produto não encontrado";
        public const string PRODUCT_BYCATEGORYNOTFOUND = "Nenhum produto encontrado para esta categoria.";
        public const string PRODUCT_DUPLICATEDNAME = "Já existe um produto com o nome informado";
        private const string PRODUCT_notfound_byid = "Não foi encontrado um Produto com ID {0}";
#endif
        public static string PRODUCT_NOTFOUND_BYID(Guid productId)
        {
            return string.Format(PRODUCT_notfound_byid, productId) ?? "";
        }
    }
}
