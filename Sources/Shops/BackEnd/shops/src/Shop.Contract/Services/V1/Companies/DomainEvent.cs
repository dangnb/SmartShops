﻿using Shop.Contract.Abstractions.Message;

namespace Shop.Contract.Services.V1.Companies;
public static class DomainEvent
{
    public record CompanyCreated(int Id, string TaxCode, string Username) : IDomainEvent;
}
