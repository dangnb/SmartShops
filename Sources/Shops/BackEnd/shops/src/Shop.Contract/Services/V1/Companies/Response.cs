﻿namespace Shop.Contract.Services.V1.Companies;
public class Response
{
    public record PermissionsResponse(Guid Id, string Description, string Code, string GroupCode, string GroupName);
}
