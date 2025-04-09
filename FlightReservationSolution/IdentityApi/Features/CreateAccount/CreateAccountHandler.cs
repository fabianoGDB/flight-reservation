using FluentValidation;

namespace IdentityApi.Features.CreateAccount
{
    public class Request
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }

    public class Validation : AbstractValidator<Request>
    {
        public Validation() {
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Password).NotEmpty()
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("Password must contain at least one uppercase letter, and one special character");
        }
    public class CreateAccountHandler
    {
            public CreateAccountHandler()
            {
            }
    }
}
