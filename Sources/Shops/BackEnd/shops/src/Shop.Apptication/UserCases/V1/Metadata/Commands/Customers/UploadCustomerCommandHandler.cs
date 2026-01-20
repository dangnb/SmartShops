using Microsoft.EntityFrameworkCore;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Command = Shop.Contract.Services.V1.Customers.Command;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Customers;

public class UploadCustomerCommandHandler : ICommandHandler<Command.UploadCustomerCommand>
{
    private readonly IRepositoryBase<Customer, Guid> _repositoryBase;
    private readonly ICurrentUser _currentUser;
    public UploadCustomerCommandHandler(IRepositoryBase<Customer, Guid> repositoryBase, ICurrentUser currentUser)
    {
        _repositoryBase = repositoryBase;
        _currentUser = currentUser;
    }
    public async Task<Result> Handle(Command.UploadCustomerCommand request, CancellationToken cancellationToken)
    {

        using var stream = new MemoryStream();
        Guid comid = _currentUser.GetRequiredCompanyId();
        await request.File.CopyToAsync(stream);
        stream.Position = 0;
        var rows = ExcelUltil.ReadExcelByKey(stream, startRow: 2);
        var customers = new List<Customer>();
        foreach (var row in rows)
        {
            var maTo = row.TryGetValue("MaTo", out string? value) ? value : "";
            var maKhachHang = row.TryGetValue("MaKhachHang", out value) ? value : "";
            var tenKhachHang = row.TryGetValue("TenKhachHang", out value) ? value : "";
            var diaChi = row.TryGetValue("DiaChi", out value) ? value : "";
            var mST = row.TryGetValue("MST", out value) ? value : "";
            var soDinhDanh = row.TryGetValue("SoDinhDanh", out value) ? value : "";
            var soDienThoai = row.TryGetValue("SoDienThoai", out value) ? value : "";
            var email = row.TryGetValue("Email", out value) ? value : "";
            var citizenIdNumber = row.TryGetValue("citizenIdNumber", out value) ? value : "";
            var passportNumber = row.TryGetValue("passportNumber", out value) ? value : "";

            customers.Add(Customer.CreateEntity(
                comid,
                maKhachHang,
                tenKhachHang,
                diaChi,
                email,
                soDienThoai,
                citizenIdNumber,
                passportNumber
            ));
        }

        int batchSize = 500;
        var existingCodes = new List<string>();

        foreach (var batch in customers.Select(x => x.Code).Chunk(batchSize))
        {
            var batchResult = await _repositoryBase.FindAll(x => x.ComId == comid)
                .Where(c => batch.Contains(c.Code))
                .Select(c => c.Code)
                .ToListAsync();

            existingCodes.AddRange(batchResult);
        }

        if (existingCodes.Count > 0)
        {
            var mess = $"Mã khách hàng bị trùng: {string.Join(", ", existingCodes.ToArray())}";
            return Result.Failure<string>(new Error("Dublicate_code", mess));
        }
        //luu lịch sử thanh toán cho khách hàng
        foreach (var customer in customers)
        {
            _repositoryBase.Add(customer);
        }
        return Result.Success();
    }
}
