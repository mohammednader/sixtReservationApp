using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.CoreBL.IRepositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        int SavingNewCustomer(Customer customer);
    }

}
