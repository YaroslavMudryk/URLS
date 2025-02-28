using Riok.Mapperly.Abstractions;
using URLS.Data.Entities;
using URLS.Groups.Features.Groups.Dtos;

namespace URLS.Groups;

[Mapper]
public static partial class GroupsMapper
{
    public static partial FullGroupDto MapToDto(this Group group);
}

public static class GroupsMapperExtensions
{
    public static FullGroupDto MapToFullDto(this Group group)
    {
        var fullGroupDto = group.MapToDto();


        return fullGroupDto;
    }
}
