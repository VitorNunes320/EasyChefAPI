using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail(string receiver, string subject, string content);
    }
}
