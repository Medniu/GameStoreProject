using BLL.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LogService : ILogService
    {
        public void SuccessSignInLogs(Guid UserId)
        {
            Log.Information($"User with ID: {UserId} complete authorization successfully!");
        }
        public void UnsuccessSignInLogs()
        {
            Log.Information("Something go wrong!");
        }
    }
}
