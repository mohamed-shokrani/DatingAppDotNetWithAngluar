namespace DatingApp.Extensions
{
    public static class DateOfBirth
    {
        public static int CalculateAge(this DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - today.Month;
            if (dob.Date > today.AddYears(age)) age--;
            return age;



        }
    }
}
