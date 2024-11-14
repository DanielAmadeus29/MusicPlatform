using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

public class RegisterModel : PageModel
{
    private readonly IUserRegistrationService _userRegistrationService;

    [BindProperty]
    public string Username { get; set; }
    [BindProperty]
    public string Password { get; set; }

    public RegisterModel(IUserRegistrationService userRegistrationService)
    {
        _userRegistrationService = userRegistrationService;
    }

    public void OnPost()
    {
        if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
        {
            _userRegistrationService.RegisterUser(Username, Password);
            TempData["Message"] = "Registration successful!";
            RedirectToPage("/Login");
        }
        else
        {
            TempData["Message"] = "Please fill in both fields.";
        }
    }
}

