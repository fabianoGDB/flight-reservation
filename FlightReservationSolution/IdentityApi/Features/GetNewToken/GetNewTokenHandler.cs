using FluentValidation;
using IdentityApi.Infrastructure.Repository;
using MediatR;

namespace IdentityApi.Features.GetNewToken
{
    public record Request(string RefreshToken);

    public record Response(string NewJwtToken,string NewRefreshToken);

    public class Validation : AbstractValidator<Request>
    {
        public Validation() {
            RuleFor(m => m.RefreshToken).NotEmpty();
        }
    }

    public record Command(Request Request) : IRequest<Response>;

    public class Handler(IUnityOfWork unityOfWork, IValidator<Request> validator) : IRequestHandler<Response>
    {
        public Task Handle(Response request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
    public class GetNewTokenHandler
    {
    }
}
