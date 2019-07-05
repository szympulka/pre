using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Sendgrid
{
    public interface IEmailService
    {
        Task SendEmailasync(string toEmail,string body);

    }
}
