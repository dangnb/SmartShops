using Shop.Contract.Abstractions.Message;
using Shop.Contract.Abstractions.Shared;
using Shop.Contract.Services.Payables.V1.Payments;
using Shop.Domain.Abstractions.Repositories;
using Shop.Domain.Entities.Purchases;

namespace Shop.Apptication.UserCases.Payments.V1.Commands;

public class RecordPaymentHandler : ICommandHandler<Command.RecordPaymentCommand>
{
    private readonly IRepositoryBase<Payment, Guid> _repositoryBase;
    public RecordPaymentHandler(IRepositoryBase<Payment, Guid> repositoryBase) => _repositoryBase = repositoryBase;
    public async Task<Result> Handle(Command.RecordPaymentCommand req, CancellationToken cancellationToken)
    {
        var payment = new Payment(req.PaymentNo, req.SupplierId, req.PaymentDate, req.Amount, req.Method, req.Reference);
        _repositoryBase.Add(payment);
        return Result.Success(payment);
    }
}
