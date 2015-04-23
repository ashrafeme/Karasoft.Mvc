using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace Karasoft.Mvc.SMS
{
    public class MobilySms
    {

        public string Username { get; set; }
        public string Password { get; set; }

        public string Message { get; set; }

        public string Sender { get; set; }

        public string Numbers { get; set; }

        public string PostData(string url, NameValueCollection values)
        {
            string toreturn = string.Empty;

            using (var client = new WebClient())
            {
                var response = client.UploadValues(url, values);

                toreturn = Encoding.Default.GetString(response);
            }

            return toreturn;
        }

        public string SendMessage()
        {
            return SendMessage(Message);
             //var client = new HttpClient();
        }


        public string SendMessage(string msg)
        {
            return SendMessage(msg, Numbers);
        }

        public string SendMessage(string msg, string numbers)
        {
            return SendMessage(msg, numbers, Sender);
        }

        public string SendMessage(string msg, string numbers, string sender)
        {
            return SendMessage(Username, Password, msg, numbers, sender);
        }


        public string SendMessage(string username, string password, string msg, string numbers, string sender)
        {
            //int temp = '0';

            NameValueCollection values = new NameValueCollection();

            values.Add("mobile", username);
            values.Add("password", password);
            values.Add("numbers", numbers);
            values.Add("sender", sender);
            values.Add("msg", ConvertToUnicode(msg));
            //values.Add("deleteKey", );
            //values.Add("msgId", );
            values.Add("applicationType", "24");

            return PostData("http://www.mobily.ws/api/msgSend.php", values);
            /*
            HttpWebRequest req = (HttpWebRequest)
            WebRequest.Create("http://www.mobily.ws/api/msgSend.php");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string postData = "mobile=" + username + "&password=" + password + "&numbers=" + numbers + "&sender=" + sender + "&msg=" + msg + "&applicationType=24";
            req.ContentLength = postData.Length;

            StreamWriter stOut = new
            StreamWriter(req.GetRequestStream(),
            System.Text.Encoding.ASCII);
            stOut.Write(postData);
            stOut.Close();
            // Do the request to get the response
            string strResponse;
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();
            return strResponse;
            */
        }


        public string ShowSendResult(string res)
        {

            string toreturn = string.Empty;
            switch (res)
            {
                case "1": toreturn = "لقد تمت العملية بنجاح"; break;
                case "2": toreturn = "إن رصيدك لدى موبايلي قد إنتهى ولم يعد به أي رسائل. (لحل المشكلة قم بشحن رصيدك من الرسائل لدى موبايلي. لشحن رصيدك إتبع تعليمات شحن الرصيد)"; break;
                case "3": toreturn = "إن رصيدك الحالي لا يكفي لإتمام عملية الإرسال. (لحل المشكلة قم بشحن رصيدك من الرسائل لدى موبايلي. لشحن رصيدك إتبع تعليمات شحن الرصيد"; break;
                case "4": toreturn = "إن إسم المستخدم الذي إستخدمته للدخول إلى حساب الرسائل غير صحيح (تأكد من أن إسم المستخدم الذي إستخدمته هو نفسه الذي تستخدمه عند دخولك إلى موقع موبايلي)."; break;
                case "5": toreturn = "هناك خطأ في كلمة المرور (تأكد من أن كلمة المرور التي تم إستخدامها هي نفسها التي تستخدمها عند دخولك موقع موبايلي,إذا نسيت كلمة المرور إضغط على رابط نسيت كلمة المرور لتصلك رسالة على جوالك برقم المرور الخاص بك)"; break;
                case "6": toreturn = "إن صفحة الإرسال لاتجيب في الوقت الحالي (قد يكون هناك طلب كبير على الصفحة أو توقف مؤقت للصفحة فقط حاول مرة أخرى أو تواصل مع الدعم الفني إذا إستمر الخطأ)"; break;
                case "12": toreturn = "إن حسابك بحاجة إلى تحديث يرجى مراجعة الدعم الفني"; break;
                case "13": toreturn = "إن إسم المرسل الذي إستخدمته في هذه الرسالة لم يتم قبوله. (يرجى إرسال الرسالة بإسم مرسل آخر أو تعريف إسم المرسل لدى موبايلي)"; break;
                case "14": toreturn = "إن إسم المرسل الذي إستخدمته غير معرف لدى موبايلي. (يمكنك تعريف إسم المرسل من خلال صفحة إضافة إسم مرسل)"; break;
                case "15": toreturn = "يوجد رقم جوال خاطئ في الأرقام التي قمت بالإرسال لها. (تأكد من صحة الأرقام التي تريد الإرسال لها وأنها بالصيغة الدولية)"; break;
                case "16": toreturn = "الرسالة التي قمت بإرسالها لا تحتوي على إسم مرسل. (أدخل إسم مرسل عند إرسالك الرسالة)"; break;
                case "17": toreturn = "لم يتم ارسال نص الرسالة. الرجاء التأكد من ارسال نص الرسالة والتأكد من تحويل الرسالة الى يوني كود (الرجاء التأكد من استخدام الدالة()"; break;
                case "-1": toreturn = "لم يتم التواصل مع خادم (Server) الإرسال موبايلي بنجاح. (قد يكون هناك محاولات إرسال كثيرة تمت معا , أو قد يكون هناك عطل مؤقت طرأ على الخادم إذا إستمرت المشكلة يرجى التواصل مع الدعم الفني)"; break;
                case "-2": toreturn = "لم يتم الربط مع قاعدة البيانات (Database) التي تحتوي على حسابك وبياناتك لدى موبايلي. (قد يكون هناك محاولات إرسال كثيرة تمت معا , أو قد يكون هناك عطل مؤقت طرأ على الخادم إذا إستمرت المشكلة يرجى التواصل مع الدعم الفني)"; break;
                default: toreturn = res.ToString(); break;
            }

            return toreturn;
        }


        public string GetBalance()
        {
            return GetBalance(Username, Password);
        }

        public string GetBalance(string username, string password)
        {
            // int temp = '0';

            NameValueCollection values = new NameValueCollection();

            values.Add("mobile", username);
            values.Add("password", password);
            return PostData("http://www.mobily.ws/api/balance.php", values);

            /*
            WebResponse myResponse = null;


            HttpWebRequest req = (HttpWebRequest)
            WebRequest.Create("http://www.mobily.ws/api/balance.php");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string postData = "mobile=" + username + "&password=" + password;
            req.ContentLength = postData.Length;

            StreamWriter stOut = new
            StreamWriter(req.GetRequestStream(),
            System.Text.Encoding.ASCII);
            stOut.Write(postData);
            stOut.Close();
            // Do the request to get the response
            string strResponse;
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();
            */


            /*
            switch (strResponse)
            {
                case "1": MessageBox.Show("إن إسم المستخدم الذي إستخدمته للدخول غير صحيح (تأكد من أن إسم المستخدم الذي إستخدمته هو نفسه الذي تدخله عند دخولك إلى موقع موبايلي)."); break;
                case "2": MessageBox.Show("هناك خطأ في كلمة المرور (تأكد من أن كلمة المرور التي تستخدمها هي نفسها التي تستخدمها عند دخولك موقع موبايلي)."); break;
                case "-1": MessageBox.Show("لم يتم التواصل مع خادم (Server) الإرسال موبايلي بنجاح. (قد يكون هناك محاولات إرسال كثيرة تمت معا , أو قد يكون هناك عطل مؤقت طرأ على الخادم إذا إستمرت المشكلة يرجى التواصل مع الدعم الفني)"); break;
                case "-2": MessageBox.Show("لم يتم الربط مع قاعدة البيانات (Database) التي تحتوي على حسابك وبياناتك لدى موبايلي. (قد يكون هناك محاولات إرسال كثيرة تمت معا , أو قد يكون هناك عطل مؤقت طرأ على الخادم إذا إستمرت المشكلة يرجى التواصل مع الدعم الفني)"); break;
            }
            */
            // label7.Text = strResponse;

        }
        private string ConvertToUnicode(string val)
        {
            string msg2 = string.Empty;

            for (int i = 0; i < val.Length; i++)
            {
                msg2 += convertToUnicode(System.Convert.ToChar(val.Substring(i, 1)));
            }

            return msg2;
        }

        private string convertToUnicode(char ch)
        {
            System.Text.UnicodeEncoding class1 = new System.Text.UnicodeEncoding();
            byte[] msg = class1.GetBytes(System.Convert.ToString(ch));

            return fourDigits(msg[1] + msg[0].ToString("X"));
        }

        private string fourDigits(string val)
        {
            string result = string.Empty;

            switch (val.Length)
            {
                case 1: result = "000" + val; break;
                case 2: result = "00" + val; break;
                case 3: result = "0" + val; break;
                case 4: result = val; break;
            }

            return result;
        }

        public string CheckSender()
        {
            return CheckSender(Username, Password);
        }
        public string CheckSender(string username, string password)
        {

            NameValueCollection values = new NameValueCollection();

            values.Add("mobile", username);
            values.Add("password", password);
            return PostData("http://www.mobily.ws/api/checkSender.php", values);

            /*
            int temp = '0';

            HttpWebRequest req = (HttpWebRequest)
            WebRequest.Create("http://www.mobily.ws/api/checkSender.php");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string postData = "mobile=" + UserName + "&password=" + Password + "&senderId=" + textBox1.Text;
            req.ContentLength = postData.Length;

            StreamWriter stOut = new
            StreamWriter(req.GetRequestStream(),
            System.Text.Encoding.ASCII);
            stOut.Write(postData);
            stOut.Close();
            // Do the request to get the response
            string strResponse;
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();
            switch (strResponse)
            {

                case "0": MessageBox.Show("اسم المرسل غير مفعل"); break;
                case "1": MessageBox.Show("اسم المرسل مستخدم"); break;
                case "2": MessageBox.Show("اسم المرسل مرفوض"); break;
                case "3": MessageBox.Show("اسم المستخدم غير معرف"); break;
                case "4": MessageBox.Show("كلمة السر خطأ"); break;

            }
            */
        }

        public string AddSender(string sender)
        {
            return AddSender(Username, Password, sender);
        }
        public string AddSender(string username, string password, string sender)
        {
            NameValueCollection values = new NameValueCollection();

            values.Add("mobile", username);
            values.Add("password", password);
            values.Add("sender", sender);
            return PostData("http://www.mobily.ws/api/addSender.php", values);

            /*
            int temp = '0';

            HttpWebRequest req = (HttpWebRequest)
            WebRequest.Create("http://www.mobily.ws/api/addSender.php");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string postData = "mobile=" + UserName + "&password=" + Password + "&sender=" + textBox1.Text;
            req.ContentLength = postData.Length;

            StreamWriter stOut = new
            StreamWriter(req.GetRequestStream(),
            System.Text.Encoding.ASCII);
            stOut.Write(postData);
            stOut.Close();
            // Do the request to get the response
            string strResponse;
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();

            switch (strResponse)
            {
                case "1": MessageBox.Show("إن إسم المستخدم الذي إستخدمته للدخول إلى حساب الرسائل غير صحيح (تأكد من أن إسم المستخدم الذي إستخدمته هو نفسه الذي تستخدمه عند دخولك إلى موقع موبايلي)"); break;
                case "2": MessageBox.Show("هناك خطأ في كلمة المرور (تأكد من أن كلمة المرور التي تم إستخدامها هي نفسها التي تستخدمها عند دخولك موقع موبايلي,إذا نسيت كلمة المرور إضغط على رابط نسيت كلمة المرور لتصلك رسالة على جوالك برقم المرور الخاص بك)"); break;
                case "3": MessageBox.Show("إن رقم الجوال الذي تم إدخاله ليكون إسم مرسل ليس صحيحا(يرجى التأكد من صحة الرقم وأنه بالصيغة الدولية مثال (966500000000)"); break;
                case "4": MessageBox.Show("إسم المرسل الذي أدخلته ليس بحاجة لتفعيل"); break;
                case "5": MessageBox.Show(" رصيدك غير كافي لأتمام العملية (لحل المشكلة قم بشحن رصيدك من الرسائل لدى موبايلي. لشحن رصيدك إتبع تعليمات شحن الرصيد)"); break;
                case "-1": MessageBox.Show("لم يتم التواصل مع خادم (Server) الإرسال موبايلي بنجاح. (قد يكون هناك محاولات إرسال كثيرة تمت معا , أو قد يكون هناك عطل مؤقت طرأ على الخادم إذا إستمرت المشكلة يرجى التواصل مع الدعم الفني)"); break;
                case "-2": MessageBox.Show("لم يتم الربط مع قاعدة البيانات (Database) التي تحتوي على حسابك وبياناتك لدى موبايلي. (قد يكون هناك محاولات إرسال كثيرة تمت معا , أو قد يكون هناك عطل مؤقت طرأ على الخادم إذا إستمرت المشكلة يرجى التواصل مع الدعم الفني)"); break;
                //default: MessageBox.Show(temp.ToString ()); break;
            }
            //  textBox2.Text = strResponse;
            */
        }

        public string ActiveSender(string sender, string activeKey)
        {
            return ActiveSender(Username, Password, sender, activeKey);
        }

        public string ActiveSender(string username, string password, string sender, string activeKey)
        {

            NameValueCollection values = new NameValueCollection();

            values.Add("mobile", username);
            values.Add("password", password);
            values.Add("senderId", sender);
            values.Add("activeKey", activeKey);
            return PostData("http://www.mobily.ws/api/activeSender.php", values);

            /*
            int temp = '0';

            HttpWebRequest req = (HttpWebRequest)
            WebRequest.Create("http://www.mobily.ws/api/activeSender.php");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            string postData = "mobile=" + UserName + "&password=" + Password + "&senderId=" + textBox2.Text + "&activeKey" + textBox3.Text;
            req.ContentLength = postData.Length;

            StreamWriter stOut = new
            StreamWriter(req.GetRequestStream(),
            System.Text.Encoding.ASCII);
            stOut.Write(postData);
            stOut.Close();
            // Do the request to get the response
            string strResponse;
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();

            switch (strResponse)
            {
                case "1": MessageBox.Show("إن إسم المستخدم الذي إستخدمته للدخول غير صحيح (تأكد من أن إسم المستخدم الذي إستخدمته هو نفسه الذي تدخله عند دخولك إلى موقع موبايلي)."); break;
                case "2": MessageBox.Show("هناك خطأ في كلمة المرور (تأكد من أن كلمة المرور التي تستخدمها هي نفسها التي تستخدمها عند دخولك موقع موبايلي)"); break;
                case "3": MessageBox.Show("تم تفعيل إسم المرسل بنجاح"); break;
                case "4": MessageBox.Show("هناك خطأ في كود التفعيل الذي تم إرساله. (عليك التأكد من أن كود التفعيل صحيح أو مراجعة الدعم الفني لإعادة إرسال كود التفعيل مرة أخرى)"); break;
                case "5": MessageBox.Show("هناك خطأ في رقم إسم المرسل الذي تم إرساله"); break;
                case "-1": MessageBox.Show("لم يتم التواصل مع خادم (Server) الإرسال موبايلي بنجاح. (قد يكون هناك محاولات إرسال كثيرة تمت معا , أو قد يكون هناك عطل مؤقت طرأ على الخادم إذا إستمرت المشكلة يرجى التواصل مع الدعم الفني)"); break;
                case "-2": MessageBox.Show("لم يتم الربط مع قاعدة البيانات (Database) التي تحتوي على حسابك وبياناتك لدى موبايلي. (قد يكون هناك محاولات إرسال كثيرة تمت معا , أو قد يكون هناك عطل مؤقت طرأ على الخادم إذا إستمرت المشكلة يرجى التواصل مع الدعم الفني)"); break;

                default: MessageBox.Show(temp.ToString()); break;
            }

            */
        }

    }
}
