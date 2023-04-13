using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Inventofree.Module.Item.Core.Abstractions;
using Inventofree.Module.Item.Core.Resources;
using Inventofree.Module.User.Core.Abstractions;
using Inventofree.Module.User.Core.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Inventofree.Module.Item.Core.Command.Category.AddCategory
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, long>
    {
        private readonly IItemDbContext _itemDbContext;
        private readonly IUserDbContext _userDbContext;
        private readonly IMapper _mapper;

        public AddCategoryCommandHandler(IItemDbContext itemDbContext, IUserDbContext userDbContext, IMapper mapper)
        {
            _itemDbContext = itemDbContext;
            _userDbContext = userDbContext;
            _mapper = mapper;
        }

        public async Task<long> Handle(AddCategoryCommand command, CancellationToken cancellationToken)
        {
            if (await _itemDbContext.Categories.AnyAsync(c => c.Name == command.Name, cancellationToken))
                throw new NullReferenceException(string.Format(ItemErrorMessages.DuplicateName, nameof(Entities.Category)));
            
            var user = await _userDbContext.Users.FirstOrDefaultAsync(a => a.Id == command.CreatedBy,
                cancellationToken);
            if (user == null)
                throw new NullReferenceException(UserErrorMessages.UserNotFound);
            
            var category = _mapper.Map<Entities.Category>(command);
            await _itemDbContext.Categories.AddAsync(category, cancellationToken);
            await _itemDbContext.SaveChangesAsync(cancellationToken);
            return category.Id;
        }
    }
}