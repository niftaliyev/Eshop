using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Service
{
    public interface IEmailSenderTwo
    {
        Task SendEmailAsync(string email, string subject, string content);
    }
}
