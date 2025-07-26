using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Enumerations;
using static Shop.Contract.Services.V1.Users.Response;

namespace Shop.Contract.Services.V1.Users;
public static class Query
{
    public record GetUsersQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, IDictionary<string, SortOrder>? SortColumnAndOrder, int PageIndex, int PageSize) : IQuery<PagedResult<UserResponse>>;
    public record GetUserByIdQuery(Guid Id) : IQuery<UserDetailResponse>;
    public record GetAllUserQuery() : IQuery<IList<UserResponse>>;
    public record GetUserByTokenQuery() : IQuery<UserInforByToken>;
    public record GetDistrictCodeQuery(Guid Id) : IQuery<string[]>;
}
