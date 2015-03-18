using System;
using System.Security.Cryptography;
using System.Text;

namespace Karasoft.Mvc.SMS
{
    public class OTP
    {
        public static string GeneratePasswordByUserAndIteration(string userId, long iterationNumber, int digits = 6)
        {
            //Here the system converts the iteration number to a byte[]
            byte[] iterationNumberByte = BitConverter.GetBytes(iterationNumber);
            //To BigEndian (MSB LSB)
            if (BitConverter.IsLittleEndian) Array.Reverse(iterationNumberByte);

            //Hash the userId by HMAC-SHA-1 (Hashed Message Authentication Code)
            byte[] userIdByte = Encoding.ASCII.GetBytes(userId);
            HMACSHA1 userIdHMAC = new HMACSHA1(userIdByte, true);
            byte[] hash = userIdHMAC.ComputeHash(iterationNumberByte); //Hashing a message with a secret key

            //RFC4226 http://tools.ietf.org/html/rfc4226#section-5.4
            int offset = hash[hash.Length - 1] & 0xf; //0xf = 15d
            int binary =
                ((hash[offset] & 0x7f) << 24)      //0x7f = 127d
                | ((hash[offset + 1] & 0xff) << 16) //0xff = 255d
                | ((hash[offset + 2] & 0xff) << 8)
                | (hash[offset + 3] & 0xff);

            int password = binary % (int)Math.Pow(10, digits); // Shrink: 6 digits
            return password.ToString(new string('0', digits));
        }

        public static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static string GetPassword(string userId)
        {
            // We get the "Unix Time" by sub from 1/1/1970 00:00:00 and now
            long iteration = (long)(DateTime.UtcNow - UNIX_EPOCH).TotalSeconds / 30;
            return GeneratePasswordByUserAndIteration(userId, iteration);
        }


    }
}
