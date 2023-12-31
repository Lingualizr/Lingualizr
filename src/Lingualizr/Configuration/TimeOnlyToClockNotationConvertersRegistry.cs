﻿using Lingualizr.Localisation.TimeToClockNotation;

namespace Lingualizr.Configuration;

internal class TimeOnlyToClockNotationConvertersRegistry : LocaliserRegistry<ITimeOnlyToClockNotationConverter>
{
    public TimeOnlyToClockNotationConvertersRegistry()
        : base(new DefaultTimeOnlyToClockNotationConverter())
    {
        Register("pt-BR", new BrazilianPortugueseTimeOnlyToClockNotationConverter());
        Register("fr", new FrTimeOnlyToClockNotationConverter());
        Register("es", new EsTimeOnlyToClockNotationConverter());
    }
}
