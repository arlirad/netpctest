using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Frontend.Models;

public class ContactEditFormModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }

    public int CategoryId { get; set; } = 1;
    public int SubCategoryId { get; set; }
    public string? CustomSubCategory { get; set; }
    
    // PasswordChange is responsible for showing/hiding the password change part of the contact edit form.
    public bool PasswordChange { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}