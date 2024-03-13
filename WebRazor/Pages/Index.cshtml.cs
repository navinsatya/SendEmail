using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazor.Contracts;

namespace WebRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IEmailSender _emailSender;

        public IndexModel(ILogger<IndexModel> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender; 
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(string toEmail, string subject, string body)
        {
            // Perform your operation here

            // Send email
            await _emailSender.SendEmailAsync(toEmail, subject, body);

            return RedirectToPage("/Index");
        }
    }
}