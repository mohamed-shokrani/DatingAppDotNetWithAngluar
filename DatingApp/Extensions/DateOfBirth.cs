namespace DatingApp.Extensions;
public static class DateOfBirth
{
    public static int CalculateAge(this DateTime dob)
        =>   (int) ((DateTime.Now - dob).TotalDays / 365.242199);
}
