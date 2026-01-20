using Microsoft.EntityFrameworkCore;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Command = Shop.Contract.Services.V1.Common.Categories.Command;

namespace Shop.Apptication.UserCases.V1.Metadata.Commands.Categories;

public class UploadCustomerCommandHandler : ICommandHandler<Command.UploadCategoryCommand>
{
    private readonly IRepositoryBase<Category, Guid> _repositoryBase;
    private readonly ICurrentUser _currentUser;
    public UploadCustomerCommandHandler(IRepositoryBase<Category, Guid> repositoryBase, ICurrentUser currentUser)
    {
        _repositoryBase = repositoryBase;
        _currentUser = currentUser;
    }
    public async Task<Result> Handle(Command.UploadCategoryCommand request, CancellationToken cancellationToken)
    {

        using var stream = new MemoryStream();
        Guid comid = _currentUser.GetRequiredCompanyId();
        await request.File.CopyToAsync(stream);
        stream.Position = 0;
        var rows = ExcelUltil.ReadExcelByKey(stream, startRow: 2);
        var categories = new List<Category>();
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

            categories.Add(Category.CreateEntity(
                maKhachHang,
                tenKhachHang,
                Guid.Empty,
                1,
                1,
                true
            ));
        }

        int batchSize = 500;
        var existingCodes = new List<string>();

        foreach (var batch in categories.Select(x => x.Code).Chunk(batchSize))
        {
            var batchResult = await _repositoryBase.FindAll(x => x.ComId == comid)
                .Where(c => batch.Contains(c.Code))
                .Select(c => c.Code)
                .ToListAsync();

            existingCodes.AddRange(batchResult);
        }

        if (existingCodes.Count > 0)
        {
            var mess = $"Mã loại bị trùng: {string.Join(", ", existingCodes.ToArray())}";
            return Result.Failure<string>(new Error("Dublicate_code", mess));
        }
        //luu lịch sử thanh toán cho khách hàng
        foreach (var customer in categories)
        {
            _repositoryBase.Add(customer);
        }
        return Result.Success();
    }
}
