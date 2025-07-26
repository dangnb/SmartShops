using Microsoft.EntityFrameworkCore;
using Shop.Apptication.Exceptions;
using Shop.Contract;
using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities;
using Shop.Domain.Entities.Metadata;
using Command = Shop.Contract.Services.V1.Customers.Command;

namespace Shop.Apptication.UserCases.V1.Commands.Customers;
public class UploadCustomerCommandHandler : ICommandHandler<Command.UploadCustomerCommand>
{
    private readonly IRepositoryBase<Customer, Guid> _repositoryBase;
    private readonly IRepositoryBase<Village, int> _villageRepository;
    private readonly IRepositoryBase<PaymentHistory, int> _paymentHistoryRepository;
    private readonly IUserProvider _userProvider;
    public UploadCustomerCommandHandler(IRepositoryBase<Customer, Guid> repositoryBase, IRepositoryBase<PaymentHistory, int> paymentHistoryRepository, IRepositoryBase<Village, int> villageRepository, IUserProvider userProvider)
    {
        _repositoryBase = repositoryBase;
        _userProvider = userProvider;
        _villageRepository = villageRepository;
        _paymentHistoryRepository = paymentHistoryRepository;
    }
    public async Task<Result> Handle(Command.UploadCustomerCommand request, CancellationToken cancellationToken)
    {

        using var stream = new MemoryStream();
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
            var mDVQHNS = row.TryGetValue("MDVQHNS", out value) ? value : "";
            customers.Add(Customer.CreateEntity(
                _userProvider.GetComID(),
                maKhachHang,
                tenKhachHang,
                diaChi,
                email,
                soDienThoai,
                request.VillageId // Assuming VillageId is not provided in the upload
            ));
        }

        int batchSize = 500;
        var existingCodes = new List<string>();

        foreach (var batch in customers.Select(x => x.Code).Chunk(batchSize))
        {
            var batchResult = await _repositoryBase.FindAll(x => x.ComId == _userProvider.GetComID())
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
