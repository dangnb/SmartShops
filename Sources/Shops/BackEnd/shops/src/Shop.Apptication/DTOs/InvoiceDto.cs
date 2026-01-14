using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Apptication.DTOs;

public record AddInvoiceLineCommandBody(Guid ProductId, decimal Qty, decimal UnitPrice, decimal TaxRate);
