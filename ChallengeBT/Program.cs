using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeBT
{
    class Program
    {
        static void Main(string[] args)
        {
            string oneTimePassword = "";
            DateTime now = DateTime.Now;
            Console.Write("Current Date and Time is : " + now);
            Console.WriteLine("\nPlease insert User ID :");
            string id = Console.ReadLine();
            string theinput = now.ToString() + " " + id;
            Console.WriteLine("\n OTP input: " + theinput);

            {
                using (MD5 md5 = MD5.Create())
                {
                    //Get hash code of entered request id in byte format.
                    byte[] _reqByte = md5.ComputeHash(Encoding.UTF8.GetBytes(theinput));
                    //convert byte array to integer.
                    int _parsedReqNo = BitConverter.ToInt32(_reqByte, 0);
                    string _strParsedReqId = Math.Abs(_parsedReqNo).ToString();
                    //Check if length of hash code is less than 9.
                    //If so, then prepend multiple zeros upto the length becomes atleast 9 characters.
                    if (_strParsedReqId.Length < 9)
                    {
                        StringBuilder sb = new StringBuilder(_strParsedReqId);
                        for (int k = 0; k < (9 - _strParsedReqId.Length); k++)
                        {
                            sb.Insert(0, '0');
                        }
                        _strParsedReqId = sb.ToString();
                    }
                    oneTimePassword = _strParsedReqId;
                }
                //Adding random letters to the OTP.
                StringBuilder builder = new StringBuilder();
                Random random = new Random();
                string randomString = "";
                for (int i = 0; i < 4; i++)
                {
                    randomString += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                }
                //Deciding number of characters in OTP.
                Random ran = new Random();
                int randomNumber = ran.Next(2, 5);
                Random num = new Random();
                //Form alphanumeric OTP and rearrange it.
                string otpString = randomString.Substring(0, randomNumber);
                otpString += oneTimePassword.Substring(0, 7 - randomNumber);
                oneTimePassword = new string(otpString.ToCharArray().OrderBy(s => (num.Next(2) % 2) == 0).ToArray());

                Console.WriteLine("Hi " + id + "!" + " Your otp is: " + oneTimePassword);
            }
            //DateTime expired = now.AddSeconds(30);
            Console.Read();
        }
    }
}