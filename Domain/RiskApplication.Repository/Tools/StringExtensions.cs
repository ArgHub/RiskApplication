using System;


namespace RiskApplication.Repository.Tools
{
    public static class StringExtensions
    {
        public static Tuple<int, int, int, int, int> ParseData(this string betData)
        {
            var splittedData = betData.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var betArray = new int[5];

            if (splittedData.Length != 5)
                throw new Exception(
                    "An unexpected number of fields in data record");

            for (var i = 0; i < 5; i++)
            {
                int number;

                if (!int.TryParse(splittedData[i], out number))
                    throw new Exception("A non-numeric field value");

                betArray[i] = number;
            }

            return new Tuple<int, int, int, int, int>(
                betArray[0],
                betArray[1],
                betArray[2],
                betArray[3],
                betArray[4]);
        }
    }
}
