namespace Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public DateTime DOB { get; set; }
    public string? Country { get; set; }

    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - DOB.Year;
            if (DOB.Date > today.AddYears(-age))
                age--;
            return age;

        }
    }

}
