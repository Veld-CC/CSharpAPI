namespace CShapr.API.Helpers
{
    public static class WriteLog
    {
        public static void Error(ILogger logger, Exception ex, string method)
        {
            var separador = "\n=======================================================================================";
            var type = $"\n\tType = {ex?.GetType()},";
            var message = $"\n\tMessage = {ex?.Message},";
            var stackTrace = $"\n\tStackTrace = {ex?.StackTrace},";
            var source = $"\n\tSource = {ex?.Source},";
            var innerException = $"\n\tInnerException = {ex?.InnerException}";
            logger.LogError($"{separador}Ocurrió un error en el método {method}:{type}{message}{stackTrace}{source}{innerException}{separador}");
        }

        public static void Info(ILogger logger, string origin, string message)
        {
            var separador = "\n=======================================================================================";
            origin += $"\n\tOrigen = {origin},";
            message += $"\n\tMensaje = {message}";
            logger.LogInformation($"{separador}{origin}{message}{separador}");
        }
    }
}
