﻿using Lingualizr.Localisation.DateToOrdinalWords;

namespace Lingualizr.Configuration;

internal class DateOnlyToOrdinalWordsConverterRegistry : LocaliserRegistry<IDateOnlyToOrdinalWordConverter>
{
    public DateOnlyToOrdinalWordsConverterRegistry()
        : base(new DefaultDateOnlyToOrdinalWordConverter())
    {
        Register("en-US", new UsDateOnlyToOrdinalWordsConverter());
        Register("fr", new FrDateOnlyToOrdinalWordsConverter());
        Register("es", new EsDateOnlyToOrdinalWordsConverter());
    }
}
