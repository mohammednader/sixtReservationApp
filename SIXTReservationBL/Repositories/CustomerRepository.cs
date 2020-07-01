using SIXTReservationBL.CoreBL.IRepositories;
using SIXTReservationBL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIXTReservationBL.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(SixtReservationContext context) : base(context)
        {

        }

        public int SavingNewCustomer(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    if (Context.Customer.Add(customer).Context.SaveChanges() > 0)
                        return customer.Id;
                    else return 0;
                }
                else return 0;


            }
            catch (Exception e)
            {
                return 0;
            }

        }
    }
}
