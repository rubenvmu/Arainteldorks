using Araintelsoftware.Areas.Identity.Data;
using Araintelsoftware.Services.EmailSender;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

public class RegisterModel : PageModel

{

    private readonly SignInManager<SampleUser> _signInManager;

    private readonly UserManager<SampleUser> _userManager;

    private readonly IUserStore<SampleUser> _userStore;

    private readonly IUserEmailStore<SampleUser> _emailStore;

    private readonly ILogger<RegisterModel> _logger;


    private readonly InterfazEmailSender _emailSender;

    public RegisterModel(

        UserManager<SampleUser> userManager,

        IUserStore<SampleUser> userStore,

        SignInManager<SampleUser> signInManager,

        ILogger<RegisterModel> logger,

        InterfazEmailSender emailSender)

    {

        _userManager = userManager;

        _userStore = userStore;


        _signInManager = signInManager;

        _logger = logger;

        _emailSender = emailSender;

    }


    [BindProperty]

    public InputModel Input { get; set; }


    public string ReturnUrl { get; set; }


    public IList<AuthenticationScheme> ExternalLogins { get; set; }


    public class InputModel

    {

        [Required]

        [EmailAddress]

        [Display(Name = "Email")]

        public string Email { get; set; }


        [Required]

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]

        [DataType(DataType.Password)]

        [Display(Name = "Password")]

        public string Password { get; set; }


        [DataType(DataType.Password)]

        [Display(Name = "Confirm password")]

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]

        public string ConfirmPassword { get; set; }

        [Required]

        [Display(Name = "Phone Number")]

        [StringLength(12, MinimumLength = 0)]

        [RegularExpression(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", ErrorMessage = "Invalid phone number format.")]

        public string PhoneNumber { get; set; }


        [Required]

        [Display(Name = "First Name")]

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "First name can only contain letters and spaces.")]

        public string FirstName { get; set; }


        [Required]

        [Display(Name = "Last Name")]

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Last name can only contain letters and spaces.")]

        public string LastName { get; set; }


        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

    }


    public async Task OnGetAsync(string returnUrl = null)

    {

        ReturnUrl = returnUrl;

        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

    }


    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        if (ModelState.IsValid)
        {
            var user = new SampleUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                Birthdate = Input.Birthdate,
                PhoneNumber = Input.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);

                try
                {
                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _emailSender.SendEmailAsync("business@araintel.com", "New user registration",
                        $"A new user has registered with the following details: {Input.Email}, {Input.FirstName} {Input.LastName}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while sending the confirmation email.");
                    ModelState.AddModelError(string.Empty, "Error sending confirmation email. Please try again later.");
                    return Page();
                }

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        return Page();
    }

    private SampleUser CreateUser()
    {
        return new SampleUser();
    }

    private IUserEmailStore<SampleUser> GetEmailStore()

    {

        if (!_userManager.SupportsUserEmail)

        {

            throw new NotSupportedException("The default UI requires a user store with email support.");

        }

        return (IUserEmailStore<SampleUser>)_userStore;

    }

}
