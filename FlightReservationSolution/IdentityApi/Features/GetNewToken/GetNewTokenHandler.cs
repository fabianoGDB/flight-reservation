using FluentValidation;
using Grpc.Core;
using IdentityApi.Domain;
using IdentityApi.Infrastructure.Repository;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    //gerador de token

    public record Command(Request Request) : IRequest<Response>;

    public class Handler(IUnityOfWork unityOfWork, IValidator<Request> validator) : IRequestHandler<Command, Response>
    {
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage).FirstOrDefault();
                throw new RpcException(new Status(StatusCode.InvalidArgument, errors!));
            }

            var _refreshToken = await unityOfWork.RefreshToken.GetRefreshTokenAsync(request.Request.RefreshToken) ??
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid refresh token provided"));

            bool isRefreshTokenValid = await unityOfWork.RefreshToken.IsTokenValid(request.Request.RefreshToken);
            var _user = await unityOfWork.AppUser.GetById(_refreshToken.UserId!);
            var claims = await unityOfWork.Claim.GetClaimsAsync(_user!);
            string jwtToken = unityOfWork.JwtToken.GenerateToken(claims);
            if (!isRefreshTokenValid) 
            {
                string newRefreshToken = unityOfWork.RefreshToken.GenerateToken();
                var _newRefreshTokenModel = new RefreshToken
                {
                    Id = _refreshToken.Id,
                    UserId = _user!.Id,
                    Token = newRefreshToken,    
                    ExpiresAt = DateTime.UtcNow.AddHours(12),
                };
                unityOfWork.RefreshToken.UpdateToken(_newRefreshTokenModel);
                await unityOfWork.SaveChangesAsync();
                return new Response(jwtToken, newRefreshToken);
            }
            return new Response(jwtToken, _refreshToken.Token!);
        }
    }
    public class GetNewTokenHandler
    {
    }
}
