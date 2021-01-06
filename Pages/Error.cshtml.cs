using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeRacer.Pages
{
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
