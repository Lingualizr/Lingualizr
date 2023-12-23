namespace Lingualizr.Localisation.DateToOrdinalWords;

internal class FrDateOnlyToOrdinalWordsConverter : DefaultDateOnlyToOrdinalWordConverter
{
    public override string Convert(DateOnly date)
    {
        string day = date.Day > 1 ? date.Day.ToString() : date.Day.Ordinalize();
        return day + date.ToString(" MMMM yyyy");
    }
}
