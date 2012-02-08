using System;
using System.ComponentModel.DataAnnotations;
using Directus.SimpleDb.Attributes;

namespace Motore.Library.Models
{
    /// <summary>
    /// Represents a potential customer
    /// </summary>
    [DomainName("Motore_Customer")]
    public class Customer
    {
        /// <summary>
        /// Their email address
        /// </summary>
        [Key]
        public string EmailAddress { get; set; }

        /// <summary>
        /// When they were created
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// If they're interested in the Alpha program
        /// </summary>
        public bool IsAlphaCustomer { get; set; }
    }
}