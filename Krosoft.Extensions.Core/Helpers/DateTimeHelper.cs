namespace Krosoft.Extensions.Core.Helpers;

public static class DateTimeHelper
{
    public static DateTime ConvertToDateTime(string currentDate)
    {
        var defautDate = DateTime.Now;
        try
        {
            var newDate = Convert.ToDateTime(currentDate);
            return newDate;
        }
        catch (Exception)
        {
            return defautDate;
        }
    }

    public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
    {
        // Java timestamp is millisecods past epoch
        var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        dtDateTime = dtDateTime.AddSeconds(Math.Round(javaTimeStamp / 1000)).ToLocalTime();
        return dtDateTime;
    }

    public static DateTime TimestampToDateTime(long timestamp)
    {
        var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Math.Round(timestamp / 1000d)).ToLocalTime();
        return dt;
    }

    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }
}