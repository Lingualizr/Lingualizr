﻿using Lingualizr.Localisation.DateToOrdinalWords;

namespace Lingualizr.Configuration;

internal class DateToOrdinalWordsConverterRegistry : LocaliserRegistry<IDateToOrdinalWordConverter>
{
    public DateToOrdinalWordsConverterRegistry()
        : base(new DefaultDateToOrdinalWordConverter())
    {
        Register("en-US", new UsDateToOrdinalWordsConverter());
        Register("fr", new FrDateToOrdinalWordsConverter());
        Register("es", new EsDateToOrdinalWordsConverter());
    }
}
