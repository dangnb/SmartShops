namespace Shop.Apptication.DTOs;

public record AddGoodsReceiptLineCommandBody(Guid ProductId, decimal Qty, decimal? UnitCost);
