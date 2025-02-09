using Microsoft.EntityFrameworkCore;
using URLS.Data;
using URLS.University.Features.Universities.Dtos;

namespace URLS.University.Features.Universities.DataAccess;

public class UniversityQuery(UrlsContext urlsContext)
{
    public async Task<UniversityDto> GetUniversityAsync(int id) =>
        await urlsContext.Universities.AsNoTracking().ProjectToDto().FirstOrDefaultAsync(x => x.Id == id);
}
