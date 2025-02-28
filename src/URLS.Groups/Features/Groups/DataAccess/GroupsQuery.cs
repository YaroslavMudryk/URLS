using Microsoft.EntityFrameworkCore;
using URLS.Data;
using URLS.Data.Entities;
using URLS.Shared.Exceptions;

namespace URLS.Groups.Features.Groups.DataAccess;

public class GroupsQuery(UrlsContext urlsContext)
{
    public async Task<Group> GetGroupByIdAsync(int id)
    {
        var group = await urlsContext.Groups.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
        if (group == null)
            ThrowNotFound(id);

        return group;
    }

    private void ThrowNotFound(int groupId)
    {
        throw new NotFoundException($"Group with id:{groupId} not found");
    }
}
