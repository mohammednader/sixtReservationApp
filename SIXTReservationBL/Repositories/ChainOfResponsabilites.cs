using SIXTReservationBL.CoreBL.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Repositories
{
  public abstract class ChainOfResponsabilites <TEntity> :IChainOfResponsabilites<TEntity> where TEntity : class
    {
        private IChainOfResponsabilites<TEntity> _nextHandler;
        public virtual TEntity Handle(string request)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }

        public IChainOfResponsabilites<TEntity> SetNext(IChainOfResponsabilites<TEntity> handler)
        {
            this._nextHandler = handler;

            // Returning a handler from here will let us link handlers in a
            // convenient way like this:
            // --.SetNext(--).SetNext(--);
            return handler;
        }
    }
}
