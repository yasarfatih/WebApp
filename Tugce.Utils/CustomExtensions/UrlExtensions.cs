using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tugce.Utils
{
    public static class UrlExtensions
    {
        public static string ParseStringToFormalUrl(this string str)
        {
            return str.ToLower().Replace(' ', '-')
                .Replace('ş', 's')
                .Replace('ç', 'c')
                .Replace('ö', 'o')
                .Replace('ı', 'i')
                .Replace('ü', 'u')
                .Replace('ğ', 'g')
                .Replace('?', '_')
                .Replace('&', '_');
        }
    }
}
