using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorSearch.Domain.Enums;

namespace VectorSearch.EF.Tools
{
    public static class Extensions
    {
        private static readonly Dictionary<SourceType, string> _companyNameMap = new()
        {
            { SourceType.digikala_goods, "digiKala" },
            { SourceType.faranShimi, "faranShimi" },
            { SourceType.padidehShimiGharb, "psg" },
        };

        public static string GetPklFileName(this SourceType sourceType)
        {

            if (_companyNameMap.TryGetValue(sourceType, out var name))
                return name;

            throw new ArgumentException($"No company name mapping for SourceType '{sourceType}'");
        }
    }
}
