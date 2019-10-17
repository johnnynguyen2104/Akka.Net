using System;
using System.Collections.Generic;
using System.Text;

namespace Brokerage.Service.Domain.Enumerations
{
    public enum CancellationReasonType
    {
        ProviderError,
        Expired,
        AccountIssue,
        Declined,
        NotEnoughBalance
    }
}
