namespace ASPNET_WebAPI.Utils
{
    public class IdGenerator
    {
        /// <summary>
        ///     Generate Next ID
        /// </summary>
        /// <param name="PrefixLengthString">the string length of the head string</param>
        /// <param name="id">Current Id(eg: SP000234)</param>
        /// <returns>Return next id (eg: SP000235)</returns>
        public static string GenerateNextId(int PrefixLengthString, string id)
        {

            //cắt ra phần số

            string strnumber = id.Substring(PrefixLengthString);

            //chuyển chuỗi thành số

            int number = 0;

            if (!int.TryParse(strnumber, out number))

                return "Convert string to number fail, please check input parameters!";

            //tăng số lên 1 đơn vị

            number++;

            if (number.ToString().Length > strnumber.Length)

                return "Out of numbers to generate codes , please check input parameters!";

            //lấy độ dài của số sau khi tăng

            int length = number.ToString().Length;

            //lấy độ dài chuỗi số ban đầu

            int lengthOrigin = id.Length - PrefixLengthString;

            //xử lý thêm số 0 vào phía trước

            string result = "";

            for (int i = 0; i < lengthOrigin - length; i++)

            {

                result += "0";

            }

            //nối số không với số đã tăng lên

            result += number.ToString();

            //lấy phần ký hiệu ở đầu mã số

            string prefix = id.Substring(0, PrefixLengthString);

            //nối ký hiệu đó với chuỗi số đã tăng

            return prefix + result;

        }
    }
}
