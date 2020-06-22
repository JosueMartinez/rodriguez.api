using System.Net.Http;
using System.Text;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace rodriguez.api.Clases
{
    public class NLogExceptionLogger : ExceptionLogger
    {
        private static readonly Logger NLog = LogManager.GetCurrentClassLogger();

        public override void Log(ExceptionLoggerContext context)
        {
            NLog.LogException(LogLevel.Error, RequestToString(context.Request), context.Exception);
        }

        private static string RequestToString(HttpRequestMessage request)
        {
            var message = new StringBuilder();

            if(request.Method != null)
            {
                message.Append(request.Method);
            }

            if(request.RequestUri != null)
            {
                message.Append(" ").Append(request.RequestUri);
            }

            return message.ToString();
        }
    }
}