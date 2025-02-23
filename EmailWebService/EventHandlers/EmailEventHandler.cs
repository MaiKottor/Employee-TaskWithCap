using DotNetCore.CAP;
using EmailWindowsService.Models;
using EmailWindowsService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailWindowsService.EventHandlers
{
    public class EmailEventHandler : ICapSubscribe

    {
        private readonly IEmailSender _emailSender;
    private readonly ILogger<EmailEventHandler> _logger;

    public EmailEventHandler(IEmailSender emailSender, ILogger<EmailEventHandler> logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }

    [CapSubscribe("employee.added")]
    public async Task HandleEmployeeAdded(EmailEventDetails emailEvent)
    {
        _logger.LogInformation("Received email event for {Email}", emailEvent.To);

        await _emailSender.SendEmailAsync(emailEvent.To, emailEvent.Subject, emailEvent.Body);

        _logger.LogInformation("Email sent successfully to {Email}", emailEvent.To);
    }
}
}
