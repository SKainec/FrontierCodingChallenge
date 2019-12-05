using System;
using System.ComponentModel.DataAnnotations;

namespace FrontierAccounts.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        private string phoneNumber;
        [Display(Name = "Phone Number")]
        public string PhoneNumber
        {
            get { return "(" + phoneNumber.Substring(0, 3) + ")-" + phoneNumber.Substring(3, 3) + "-" + phoneNumber.Substring(6); }
            set { phoneNumber = value; }
        }

        [DataType(DataType.Currency)]
        [Display(Name = "Amount Due")]
        public decimal AmountDue { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Due Date")]
        public DateTimeOffset? PaymentDueDate { get; set; }
        public AccountStatuses AccountStatusId { get; set; }
        [Display(Name = "Name")]
        public string FullName { get { return LastName + ", " + FirstName;  } }
    }
}
