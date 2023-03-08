namespace DatingApp.Extensions
{
    public static class DateOfBirth
    {
        public static int CalculateAge(this DateTime dob)
        {
            var today = DateTime.Today;
          //  var age = today.Year - today.Month;
            var age = (int)((DateTime.Now - dob).TotalDays / 365.242199);
            //while (dob.Date < today.AddYears(age))
            //{
            //    age--;
            //}
            //if () age--
            //var real_age = Math.Abs(age);
            return age;



        }
    }
}
