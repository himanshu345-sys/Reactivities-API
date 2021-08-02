using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Photos
{
    public class SetMain
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {

            private readonly IUsernameAccessor _usernameAccessor;

            private readonly DataContext _dataContext;
            public Handler(DataContext dataContext,IUsernameAccessor usernameAccessor)
            {
                _dataContext = dataContext;
                _usernameAccessor = usernameAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _dataContext.Users.Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == _usernameAccessor.GetUsername());

                if(user == null) return null;

                var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);

                if(photo ==null) return null;

                var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

                if(currentMain != null) currentMain.IsMain = false;

                photo.IsMain = true;

                var success = await _dataContext.SaveChangesAsync()>0;
                if(success) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Problem setting main photo");
            }
        }


    }
}