namespace Lingualizr.Localisation.DateToOrdinalWords;

internal class FrDateToOrdinalWordsConverter : DefaultDateToOrdinalWordConverter
{
    public override string Convert(DateTime date)
    {
        string day = date.Day > 1 ? date.Day.ToString() : date.Day.Ordinalize();
        return day + date.ToString(" MMMM yyyy");
    }
}
