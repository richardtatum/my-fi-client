using MyFi.TheBadlands.Repositories;

namespace MyFi.TheBadlands.Services;

public class UserService
{
    private readonly QueryRepository _query;

    public UserService(QueryRepository query)
    {
        _query = query;
    }

    public async Task<bool> UserExistsAsync(Guid userId)
    {
        var user = await _query.GetUserAsync(userId);
        return user is not null;
    }
}