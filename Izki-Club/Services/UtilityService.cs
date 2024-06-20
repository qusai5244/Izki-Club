using Izki_Club.Data;
using Izki_Club.Services.Interfaces;

namespace Izki_Club.Services
{
    public class UtilityService : IUtilityService
    {
        public UtilityService()
        {
        }

        public int CalculateAge(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateOfBirth.Year;

            // Adjust age if the birthday hasn't occurred yet this year
            if (currentDate.Month < dateOfBirth.Month || (currentDate.Month == dateOfBirth.Month && currentDate.Day < dateOfBirth.Day))
            {
                age--;
            }

            return age;
        }
    }
}
