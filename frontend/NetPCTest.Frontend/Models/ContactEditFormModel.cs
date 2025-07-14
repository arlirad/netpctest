namespace NetPCTest.Frontend.Models;

public class ContactEditFormModel
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; } = DateTime.Today;

    public int CategoryId { get; set; } = 1;
    public int SubCategoryId { get; set; }
    public string? CustomSubCategory { get; set; }
    
    // PasswordChange is responsible for showing/hiding the password change part of the contact edit form.
    public bool PasswordChange { get; set; }
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}