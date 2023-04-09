using MediatR;

namespace Inventofree.Module.User.Core.Command.User.CreateUser
{
    public class CreateUserCommand : IRequest<long>
    {
        public string Email { get; init; }
        public string Firstname { get; set; }
        public string Password { get; init; }
        public string ConfirmPassword { get; init; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
    }
}