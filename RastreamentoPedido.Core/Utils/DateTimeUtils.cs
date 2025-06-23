namespace RastreamentoPedido.Core.Utils
{
    public static class DateTimeUtils
    {
        public static string DateTimeToString(this DateTime value)
        {
            return ((long)(value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, value.Kind)).TotalMilliseconds)).ToString();
        }
        public static DateTime StringJSToDateTIme(this string value)
        {
            var isNumeric = long.TryParse(value, out long milliseconds);
            if (!isNumeric)
            {
                return DateTime.MinValue;
            }
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(milliseconds * 1000).ToLocalTime();
        }
    }
}
