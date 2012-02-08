using Directus.SimpleDb.Providers;
using Motore.Library.Models;
using Motore.Library.Utils.Entities;
using Rolstad.System.Dates;

namespace Motore.Library.Programs
{
    /// <summary>
    /// Program for the alpha stage
    /// </summary>
    public class AlphaProgram
    {
        private readonly SimpleDBProvider<Customer, string> _customerProvider;

        /// <summary>
        /// Default constructor for testing
        /// </summary>
        protected AlphaProgram()
        {
            
        }

        /// <summary>
        /// Construtor with dependencies
        /// </summary>
        /// <param name="customerProvider">Provider for persisting customer data</param>
        public AlphaProgram(SimpleDBProvider<Customer,string> customerProvider)
        {
            _customerProvider = customerProvider;
        }

        /// <summary>
        /// Saves an interested customer
        /// </summary>
        /// <param name="emailAddress">Email to save</param>
        /// <returns>Reponse of what happened</returns>
        public virtual GenericResponse SaveInterestedCustomer(string emailAddress)
        {
            // Save the customer
            var customer = CreateCustomer(emailAddress);
            _customerProvider.Save(new[]{customer});

            // Return the response
            return new GenericResponse(true,"Thank You for you interest.  When we have something to show, you'll be the first to know!");
        }

        /// <summary>
        /// Creates a new customer instance
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        private Customer CreateCustomer(string emailAddress)
        {
           return new Customer { EmailAddress = emailAddress, CreateDate = Clock.Now, IsAlphaCustomer = true };
        }
    }
}
