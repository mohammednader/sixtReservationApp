using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
   public interface IChainOfResponsabilites <TEntity> where TEntity : class
    {
        IChainOfResponsabilites<TEntity> SetNext(IChainOfResponsabilites<TEntity> handler);
        TEntity Handle(string request);
    }
}
