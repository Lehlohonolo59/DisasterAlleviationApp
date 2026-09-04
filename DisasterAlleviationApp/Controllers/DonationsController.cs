using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisasterAlleviationApp.Models;
using DisasterAlleviationApp.Data;

public class DonationsController : Controller
{
    // Read-only field for the database context so this controller can talk to SQL Server
    private readonly ApplicationDbContext _context;

    // Constructor: ASP.NET Core automatically injects the database context here (Dependency Injection)
    public DonationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: DONATIONS (Loads the main page listing all donation records)
    public async Task<IActionResult> Index()
    {
        // Asynchronously fetches all rows from the Donations table and passes them to the Index view
        return View(await _context.Donations.ToListAsync());
    }

    // GET: DONATIONS/Details/5 (Loads a single record's details view based on its ID)
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound(); // Returns a 404 error page if no ID was provided in the URL
        }

        // Searches the database for the first donation matching the ID
        var donation = await _context.Donations
            .FirstOrDefaultAsync(m => m.Id == id);

        if (donation == null)
        {
            return NotFound(); // Returns 404 if the record doesn't exist in SQL Server
        }

        return View(donation); // Passes the single donation object to the Details view
    }

    // GET: DONATIONS/Create (Displays the blank HTML form to add a new donation)
    public IActionResult Create()
    {
        return View();
    }

    // POST: DONATIONS/Create (Receives the form submission data from the user and saves it)
    [HttpPost]
    [ValidateAntiForgeryToken] // Security measure preventing Cross-Site Request Forgery (CSRF) attacks
    public async Task<IActionResult> Create([Bind("Id,DonorName,DonationType,Amount,DisasterLocation,DonationDate")] Donation donation)
    {
        // Checks if all validation rules (like [Required]) on the model were successfully met
        if (ModelState.IsValid)
        {
            _context.Add(donation); // Stages the new donation in memory for saving
            await _context.SaveChangesAsync(); // Executes the SQL INSERT command to save it permanently in SSMS
            return RedirectToAction(nameof(Index)); // Sends the user back to the list page
        }
        return View(donation); // If validation fails, reloads the form with the user's current inputs and error messages
    }

    // GET: DONATIONS/Edit/5 (Loads the edit form pre-populated with an existing donation's data)
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var donation = await _context.Donations.FindAsync(id); // Quickly finds the record by its primary key ID
        if (donation == null)
        {
            return NotFound();
        }
        return View(donation); // Passes the found record to the Edit view
    }

    // POST: DONATIONS/Edit/5 (Handles saving the modifications made on the edit form)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,DonorName,DonationType,Amount,DisasterLocation,DonationDate")] Donation donation)
    {
        if (id != donation.Id)
        {
            return NotFound(); // Safety check ensuring the URL ID matches the form data ID
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(donation); // Marks the entity as modified
                await _context.SaveChangesAsync(); // Executes the SQL UPDATE command in SSMS
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handles edge cases where another user might have deleted the record while it was being edited
                if (!DonationExists(donation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index)); // Returns to the list page on success
        }
        return View(donation);
    }

    // GET: DONATIONS/Delete/5 (Displays the confirmation screen before deleting a record)
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var donation = await _context.Donations
            .FirstOrDefaultAsync(m => m.Id == id);
        if (donation == null)
        {
            return NotFound();
        }

        return View(donation);
    }

    // POST: DONATIONS/Delete/5 (Executes the actual deletion after user confirms)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var donation = await _context.Donations.FindAsync(id);
        if (donation != null)
        {
            _context.Donations.Remove(donation); // Marks record for deletion
        }

        await _context.SaveChangesAsync(); // Executes the SQL DELETE command in SSMS
        return RedirectToAction(nameof(Index)); // Returns to the list page
    }

    // Helper method to check if a specific donation ID exists in the database
    private bool DonationExists(int? id)
    {
        return _context.Donations.Any(e => e.Id == id);
    }
}