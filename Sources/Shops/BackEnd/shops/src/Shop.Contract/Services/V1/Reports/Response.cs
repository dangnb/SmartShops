﻿namespace Shop.Contract.Services.V1.Reports;
public class Response
{
    public record DataReportResponse(int Id, string Code, string Name, decimal Price, bool IsActive, int ProductType);
}

