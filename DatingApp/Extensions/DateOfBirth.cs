namespace DatingApp.Extensions
{
    public static class DateOfBirth
    {
        public static int CalculateAge(this DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - today.Month;
            while(dob.Date < today.AddYears(age))
            {
                age--;
            }
            //if () age--
            var real_age = Math.Abs(age);
            return real_age;



        }
    }
}
