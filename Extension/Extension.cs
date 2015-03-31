using Karasoft.Mvc.Utilities;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Karasoft.Mvc.Extension
{
    public static class Extensions
    {
        public static decimal TryDivide(this Decimal root, decimal d1, decimal d2)
        {
            decimal toreutrn = decimal.Zero;

            if (d2 != 0)
            {
                toreutrn = decimal.Divide(d1, d2);
            }

            return toreutrn;
        }


        public static bool HasArabicCharacters(this string text)
        {
            char[] glyphs = text.ToCharArray();
            foreach (char glyph in glyphs)
            {
                if (glyph >= 0x600 && glyph <= 0x6ff) return true;
                if (glyph >= 0x750 && glyph <= 0x77f) return true;
                if (glyph >= 0xfb50 && glyph <= 0xfc3f) return true;
                if (glyph >= 0xfe70 && glyph <= 0xfefc) return true;
            }
            return false;
        }

        public static bool IsNumeric(this string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }

            return true;
        }



        public static bool IsNullOrEmpty(this String text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static int? ToIntN(this String text)
        {
            int toreturn;
            if (string.IsNullOrEmpty(text))
                return null;
            else
            {
                if (int.TryParse(text, out toreturn))
                    return toreturn;
                else return null;
            }
        }

        public static int ToInt(this String text)
        {

            int toreturn;

            int.TryParse(text.ToEnInt(), out toreturn);

            return toreturn;
        }

        public static int ToInt(this String text, int defualtvalue)
        {

            int toreturn;

            if (!int.TryParse(text.ToEnInt(), out toreturn))
            {
                toreturn = defualtvalue;
            }

            return toreturn;
        }

        /// <summary>
        /// Convert arabic numbers to english number
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToEnInt(this string text)
        {
            text = text.IsNullOrEmpty() == true ? "0" : text;
            string toreurn = string.Empty;
            if (HasArabicCharacters(text))
            {
                foreach (char item in text)
                {
                    if (char.IsNumber(item))
                    {
                        switch (item)
                        {
                            case '١':
                                toreurn += "1";
                                break;
                            case '٢':
                                toreurn += "2";
                                break;
                            case '٣':
                                toreurn += "3";
                                break;
                            case '٤':
                                toreurn += "4";
                                break;
                            case '٥':
                                toreurn += "5";
                                break;
                            case '٦':
                                toreurn += "6";
                                break;
                            case '٧':
                                toreurn += "7";
                                break;
                            case '٨':
                                toreurn += "8";
                                break;
                            case '٩':
                                toreurn += "9";
                                break;
                            default:
                                toreurn += "0";
                                break;
                        }
                    }
                }
            }
            else
            {
                toreurn = text;
            }

            return toreurn;
        }

        public static long ToLong(this String text)
        {

            long toreturn;

            long.TryParse(text.ToEnInt(), out toreturn);

            return toreturn;
        }

        public static string ToArabicString(this int input)
        {
            return ToArabicString(input.ToString());
        }
        public static string ToArabicString(this string input)
        {
            string toreturn = string.Empty;

            System.Text.UTF8Encoding utf8Encoder = new UTF8Encoding();

            System.Text.Decoder utf8Decoder = utf8Encoder.GetDecoder();

            System.Text.StringBuilder convertedChars = new System.Text.StringBuilder();

            char[] convertedChar = new char[1];

            byte[] bytes = new byte[] { 217, 160 };

            char[] inputCharArray = input.ToCharArray();

            foreach (char c in inputCharArray)
            {

                if (char.IsDigit(c))
                {

                    bytes[1] = Convert.ToByte(160 + char.GetNumericValue(c));

                    utf8Decoder.GetChars(bytes, 0, 2, convertedChar, 0);

                    convertedChars.Append(convertedChar[0]);

                }

                else
                {

                    convertedChars.Append(c);

                }

            }

            toreturn = convertedChars.ToString();

            return toreturn;
        }

        public static bool? ToBooleanN(this String text)
        {
            bool toreturn;
            if (string.IsNullOrEmpty(text))
                return null;
            else
            {
                if (bool.TryParse(text, out toreturn))
                    return toreturn;
                else return null;
            }
        }

        public static bool ToBoolean(this String text)
        {
            bool toreturn;
            bool.TryParse(text, out toreturn);
            return toreturn;
        }

        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }


        public static DateTime KSANow(this DateTime odate)
        {
            return Karasoft.Mvc.Utilities.DateUtility.SADateNow;
        }

        public static string ToHijriDate(this DateTime odate, string format)
        {
            return Karasoft.Mvc.Utilities.DateUtility.ConvertToHijriDate(odate, format);
        }

        public static Age ToArabicAge(this DateTime odate, DateTime todate)
        {
            return Karasoft.Mvc.Utilities.DateUtility.FormatAgeAr(odate, todate);
        }

        public static Age ToArabicAge(this DateTime odate)
        {
            return Karasoft.Mvc.Utilities.DateUtility.FormatAgeAr(odate, DateTime.Now);
        }

        public static Age ToAge(this DateTime odate)
        {
            return Karasoft.Mvc.Utilities.DateUtility.FormatAge(odate, DateTime.Now);
        }
        public static Age ToAge(this DateTime odate, DateTime todate)
        {
            return Karasoft.Mvc.Utilities.DateUtility.FormatAge(odate, todate);
        }
        public static string ToHijriDate(this DateTime odate)
        {
            return Karasoft.Mvc.Utilities.DateUtility.ConvertToHijriDate(odate);
        }

        public static string ToHijriDate(this DateTime? odate, string format)
        {
            return Karasoft.Mvc.Utilities.DateUtility.ConvertToHijriDate(odate, format);
        }

        public static int ToHijriYear(this DateTime odate)
        {
           
                return Karasoft.Mvc.Utilities.DateUtility.ConvertToYearName(odate);
           
        }
        public static string ToHijriMonth(this DateTime odate)
        {
            
                return Karasoft.Mvc.Utilities.DateUtility.ConvertToMonthName(odate);
            

        }

        public static int? ToHijriYear(this DateTime? odate)
        {
            if (odate.HasValue)
                return Karasoft.Mvc.Utilities.DateUtility.ConvertToYearName(odate.GetValueOrDefault(DateTime.Now));
            else return null;
        }
        public static string ToHijriMonth(this DateTime? odate)
        {
            if (odate.HasValue)
                return Karasoft.Mvc.Utilities.DateUtility.ConvertToMonthName(odate.GetValueOrDefault(DateTime.Now));
            else return string.Empty;

        }
        public static string ToHijriDate(this DateTime? odate)
        {
            return Karasoft.Mvc.Utilities.DateUtility.ConvertToHijriDate(odate);
        }

        public static bool IsKasMobileNumberValid(this String mobileNumber)
        {


            // check if it's the right length
            if (mobileNumber.Length != 12 || mobileNumber.Substring(3, 1) != "5")
            {
                return false;
            }

            return true;
        }

        private static string CleanNumber(string phone)
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            return digitsOnly.Replace(phone, "");
        }

        public static string ToKsaMobileNumber(this String mobileNumber)
        {

            string toreturn = string.Empty;

            // remove all non-numeric characters
            var _mobileNumber = CleanNumber(mobileNumber.ToEnInt());

            // trim any leading zeros
            _mobileNumber = _mobileNumber.TrimStart(new char[] { '0' });

            // check for this in case they've entered 966 (0)xxxxxxxxx or similar
            if (_mobileNumber.StartsWith("9660"))
            {
                _mobileNumber = _mobileNumber.Remove(3, 1);
            }

            // add country code if they haven't entered it
            if (!_mobileNumber.StartsWith("966"))
            {
                _mobileNumber = "966" + _mobileNumber;
            }

            return toreturn = _mobileNumber;
        }

        

        public static bool IslengthEquel(this string value, int length)
        {
            return (value.Length == length);
        }

        public static bool IsValidSaudiID(this string value)
        {
            bool toreturn = true;
            if (!value.IsNumeric())
                return false;
            value = value.ToEnInt();
            if (!value.IslengthEquel(10))
                return false;
            if (value.StartsWith("1") == false && value.StartsWith("2") == false)
                return false;
            var sum = 0;
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    var padStr = (value.Substring(i, 1).ToInt() * 2).ToString().PadLeft(2, '0');
                    sum += padStr.Substring(0, 1).ToInt() + padStr.Substring(1, 1).ToInt();
                }
                else
                {
                    sum += value.Substring(i, 1).ToInt();
                }
            }
            toreturn = sum % 10 == 0 ? true : false;
            return toreturn;
        }
    }
}

