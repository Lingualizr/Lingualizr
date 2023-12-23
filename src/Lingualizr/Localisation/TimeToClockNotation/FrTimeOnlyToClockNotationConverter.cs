namespace Lingualizr.Localisation.TimeToClockNotation;

internal class FrTimeOnlyToClockNotationConverter : ITimeOnlyToClockNotationConverter
{
    public virtual string Convert(TimeOnly time, ClockNotationRounding roundToNearestFive)
    {
        var normalizedMinutes = (int)(roundToNearestFive == ClockNotationRounding.NearestFiveMinutes ? 5 * Math.Round(time.Minute / 5.0) : time.Minute);

        return normalizedMinutes switch
        {
            00 => getHourExpression(time.Hour),
            60 => getHourExpression(time.Hour + 1),
            _ => $"{getHourExpression(time.Hour)} {normalizedMinutes.ToWords(GrammaticalGender.Feminine)}",
        };

        static string getHourExpression(int hour)
        {
            return hour switch
            {
                0 => "minuit",
                12 => "midi",
                _ => hour.ToWords(GrammaticalGender.Feminine) + (hour > 1 ? " heures" : " heure"),
            };
        }
    }
}
