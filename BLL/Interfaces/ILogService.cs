using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ILogService
    {
        public void SuccessSignInLogs(Guid UserId);

        public void UnsuccessSignInLogs();       
    }
}
