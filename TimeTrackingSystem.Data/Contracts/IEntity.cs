using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TimeTrackingSystem.Data.Contracts
{
    public interface IEntity
    {
        Expression<Func<object, bool>> BuildSearchExpression(string searchTerm);
    }
}
