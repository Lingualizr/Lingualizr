#if NET6_0_OR_GREATER
using System;

namespace Lingualizr.Localisation.DateToOrdinalWords
{
    internal class UsDateOnlyToOrdinalWordsConverter : DefaultDateOnlyToOrdinalWordConverter
    {
        public override string Convert(DateOnly date)
        {
            return date.ToString("MMMM ") + date.Day.Ordinalize() + date.ToString(", yyyy");
        }
    }
}
#endif
