using Lingualizr.Configuration;

namespace Lingualizr.Localisation.DateToOrdinalWords;

internal class EsDateOnlyToOrdinalWordsConverter : DefaultDateOnlyToOrdinalWordConverter
{
    public override string Convert(DateOnly date)
    {
        DateTime equivalentDateTime = date.ToDateTime(TimeOnly.MinValue);
        return Configurator.DateToOrdinalWordsConverter.Convert(equivalentDateTime);
    }
}
