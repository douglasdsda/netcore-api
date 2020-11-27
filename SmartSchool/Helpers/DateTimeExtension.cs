using System;

namespace SmartSchool.Helpers
{
    public static class DateTimeExtension
    {
        public static int GetCurrentAge(this DateTime datetime){
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - datetime.Year;
            if(currentDate < datetime.AddYears(age))
            age--;

            return age;
        }
    }
}