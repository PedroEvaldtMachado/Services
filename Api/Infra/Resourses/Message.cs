using System.Resources;

namespace Api.Infra.Resourses
{
    public static class Message
    {
        private static ResourceManager _resource = new ResourceManager(typeof(Messages));
        private const string MSG_FORMAT = $"MSG000000000";

        public static string Get(int messageNumber)
        {
            var formatNumber = FormatMessageNumber(messageNumber);
            var message = _resource.GetString(formatNumber) ?? string.Empty;

            return message;
        }

        public static string Get(int messageNumber, params string[] values)
        {
            var message = Get(messageNumber);
            message = string.Format(message, values);

            return message;
        }

        private static string FormatMessageNumber(int number)
        {
            var numberString = number.ToString();

            return $"{MSG_FORMAT.Remove(MSG_FORMAT.Length - numberString.Length)}{numberString}";
        }
    }
}
