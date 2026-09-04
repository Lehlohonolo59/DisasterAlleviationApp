using System.ComponentModel.DataAnnotations; // Imports validation attributes like [Required]
using Microsoft.EntityFrameworkCore; // Imports EF Core capabilities

namespace DisasterAlleviationApp.Models
{
    public class Donation
    {
        public int Id { get; set; } // Acts as the Primary Key in the database (automatically handles unique IDs)

        [Required(ErrorMessage = "Donor name is required.")] // Validation: Form submission will fail if this is empty
        [Display(Name = "Donor Name")] // Changes the label text when rendered automatically on HTML web forms
        public string DonorName { get; set; } = string.Empty; // Holds the name of the person/organization donating

        [Required(ErrorMessage = "Please specify the donation type.")]
        [Display(Name = "Donation Type")]
        public string DonationType { get; set; } = string.Empty; // Category of donation (e.g., Money, Food, Clothing)

        [Required(ErrorMessage = "Please enter an amount or quantity.")]
        [Display(Name = "Amount / Quantity")]
        public decimal Amount { get; set; } // Numeric value for monetary amount or physical quantity

        [Required(ErrorMessage = "Disaster location/name is required.")]
        [Display(Name = "Disaster / Incident")]
        public string DisasterLocation { get; set; } = string.Empty; // Specifies which disaster the donation is for

        [Display(Name = "Date Donated")]
        public DateTime DonationDate { get; set; } = DateTime.Now; // Automatically stamps the entry with the current date/time
    }
}