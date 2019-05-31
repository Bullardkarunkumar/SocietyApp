using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Net.Configuration;

namespace MudarOrganic.EMails
{
    #region Enumerations
    /// <summary>
    /// This enum is used to indicate the mail format.
    /// </summary>
    public enum EmailFormat
    {
        HTML,
        TEXT
    }
    /// <summary>
    /// Entity hold the data for sending email
    /// </summary>
    public class MailEntity
    {
        public string TicketNumber;
        public string ToEmails;
        public string CCEmails;
        public string UserName;
        public string Area;
        public string ProductGroup;
        public string Product;
        public string Issue;
        public string Description;
        public string Comment;
        public string Resolution;
        public string Status;
        public string Urgency;
        public string Language = "en";
        public string CAEName;
        public string Date;
        public string PhoneAccessNumber;
        public string Attachment;
        public string AssignedTo;
        public string AssignedFrom;
        public string ModifiedBy;
        //Aslam add Bcc field;
        public string BCCEmails;
        public string TicketID;
    }
    #endregion Enumerations

    /// <summary>
    /// Class to send Email to the given email addresses with/without attachment
    /// </summary>
    public class Email
    {
        #region Private variables
        //Vikram : 30 Mar 2011 - As per the todays call with Purush, 
        //remove "NEW SYSTEM TESTING" string with empty string.
        private static string SUBJECT_PREFIX = string.Empty;
        private static string emailBodyFolder = ConfigurationManager.AppSettings["emailBodyFolder"];
        private static string FILE_ACKNOWLEDGE = "Acknowledge.txt";
        private static string FILE_UPDATE = "Update Comment.txt";
        private static string FILE_DISCLAIMER = "DISCLAIMER.txt";
        private static string FILE_NOTIFICATION = "Ticket Notification.txt";
        private static string FILE_RESOLUTION = "Proposed Resolution.txt";
        private static string FILE_REQUEST_TO_KB = "Request For KB.txt";
        private static string FILE_CUSTOMER_UPDATE = "CustomerUpdate.txt";
        private static string FILE_INTERNAL_COM = "InternalComment.txt";
        private static string FILE_TICKET_REALLOCATION = "TicketReallocation.txt";
        private static string FILE_TICKET_ALLOCATION = "TicketAllocation.txt";
        private static string INTERNAL_WEEKLY_UPDATE = "WeeklyUpdate.txt";

        private static string FILE_LOGINCREDINTILAS = "LoginCredintals.txt";
        private static string FILE_BUYERCOMPANYINFO = "BuyerCompanyInfo.txt";
        private static string FILE_BUYERCONTACTINFO = "BuyerContactInfo.txt";
        private static string FILE_BUYERNOTIFYINFO = "BuyerNotifyInfo.txt";
        private static string FILE_BUYERBANKINFO = "BuyerBankInfo.txt";
        private static string FILE_BUYERPORTINFO = "BuyerPortInfo.txt";
        private static string FILE_BUYERPRODUCTLIST = "BuyerProductList.txt";
        #endregion

        #region [ Send Mail Overloaded Methods ]
        /// <summary>
        /// Send mail using Net.MailMessage and SMTP
        /// </summary>
        /// <param name="toMailIds">To mail ids.</param>
        /// <param name="ccMailIds">The cc mail ids.</param>
        /// <param name="bccMailIds">The BCC mail ids.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="attachmentList">The attachment list.</param>
        /// <param name="mailFormat">The mail format.</param>
       
 
  public static void _SendEMail(string toMailIds, string ccMailIds, string bccMailIds, string subject, string body, string[] attachmentList, EmailFormat mailFormat)
  {
                //System.Net.Mail.MailMessage MyMailMessage = new System.Net.Mail.MailMessage();
                //try
                //{
                //    string FromAddr = "info@mudarorganic.com";//System.Configuration.ConfigurationManager.AppSettings["FrmMailAdr"];
                //    string strstsError = string.Empty;
                //    string Smtpadr = string.Empty;
                //    string strTemp = string.Empty;
                //    string strCCAddress = string.Empty;
                //    Smtpadr = "mail.mudarorganic.com";//System.Configuration.ConfigurationManager.AppSettings["Smtpserver"];
                //    int Port = 587;//Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Port"]);
                //    string MailClientid = "info@mudarorganic.com";//System.Configuration.ConfigurationManager.AppSettings["FrmMailAdr"];
                //    string strMailUserID = "info@mudarorganic.com"; ;//System.Configuration.ConfigurationManager.AppSettings["MailUserID"];
                //    string MailClntPwd = "Mudar$1223";//System.Configuration.ConfigurationManager.AppSettings["Password"];
                //    string CCList = "rbhanu@vgsoft.in";// System.Configuration.ConfigurationManager.AppSettings["CCAdrs"];
                //    string sslCert = string.Empty; //System.Configuration.ConfigurationManager.AppSettings["SSL"];
                //    CCList = CCList + ";" + strCCAddress;
                //    System.Net.NetworkCredential MailAuthentication = null;
                //    System.Net.Mail.SmtpClient MailClient = null;
                //    MyMailMessage.From = new System.Net.Mail.MailAddress(FromAddr);
                //    string[] SToMail = ToMailAdr.Split(';');
                //    for (int i = 0; i < SToMail.Length; i++)
                //    {
                //        if (SToMail[i] != "")
                //            MyMailMessage.To.Add(new MailAddress(SToMail[i]));
                //    }

                //    MyMailMessage.Bcc.Add(new MailAddress("karunmca2008@gmail.com"));
                //    MyMailMessage.Bcc.Add(new MailAddress("bhanu1236@gmail.com"));

                //    MyMailMessage.Subject = "Welcome";
                //    MyMailMessage.Body = "Greetings !  from Mudar India Exports. Thanks for the registration in with Mudar Organic. Perform all the Operations with Mudar India Exports with ease and comfort using login credentials:";
                //    MyMailMessage.IsBodyHtml = true;
                //    MyMailMessage.Priority = System.Net.Mail.MailPriority.Normal;
                //    MailAuthentication = new System.Net.NetworkCredential("info@mudarorganic.com", "Mudar$123");
                //    MailClient = new System.Net.Mail.SmtpClient("mail.mudarorganic.com", 587);
                //    if (sslCert.ToUpper() == "YES")
                //    {
                //        MailClient.EnableSsl = true;
                //    }
                //    else
                //    {
                //        MailClient.EnableSsl = false;
                //    }
                //    MailClient.UseDefaultCredentials = true;
                //    MailClient.Credentials = MailAuthentication;
                //    MailClient.Send(MyMailMessage);
                //    MailClient = null;
                   
                //}
                //catch (Exception ex)
                //{
                //    // WriteErrorLog(ex, "Email Send Exception StackTrace");
                //    //return "Not sent";
                //}
                //finally
                //{
                //    MyMailMessage.Dispose();
                //    MyMailMessage = null;
                //}
                //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EmailEnabled"])
                //    && ConfigurationManager.AppSettings["EmailEnabled"].Trim().ToUpper() != "YES")
                //    return;

                //string strMailBody = body;// +ExtractMailBody(FILE_DISCLAIMER);
                //MailMessage msg = new MailMessage();
                //if (strMailBody.Contains("<img "))
                //{
                //    //AlternateView AV = 
                //    MailMessage NewMessage = new MailMessage();
                //    ConvertImageBody(strMailBody, out NewMessage);
                //    msg = NewMessage;
                //    //msg.Body = body + ExtractMailBody(FILE_DISCLAIMER);
                //    msg.IsBodyHtml = true;
                //}
                //else
                //{
                //    msg.Body = body; //+ ExtractMailBody(FILE_DISCLAIMER);
                //    msg.IsBodyHtml = (mailFormat.ToString().ToUpper() == "HTML");
                //}


                //if (!string.IsNullOrEmpty(toMailIds))
                //{
                //    // multiple email addresses should be seperated with "," operator ie;abc@xyz.com,123@abc.com
                //    // if (toMailIds.Contains("@microchip.com"))//TODO comment this line during release
                //    msg.To.Add(toMailIds.Replace(";", ","));
                //}

                //// BCC and CC email addresses also should be in the same format. ie;abc@xyz.com,123@abc.com
                //if (!string.IsNullOrEmpty(ccMailIds))
                //{
                //    msg.CC.Add(ccMailIds.Replace(";", ","));
                //}
                //if (!string.IsNullOrEmpty(bccMailIds))
                //{
                //    msg.Bcc.Add(bccMailIds.Replace(";", ","));
                //}

                ////TODO comment this condition during release
                //if (msg.To.Count == 0 && msg.Bcc.Count == 0 && msg.CC.Count == 0)
                //    return;

                ////string fromEmail = ConfigurationManager.AppSettings["SupportFromEmail"];
                ////string fromName = ConfigurationManager.AppSettings["SupportFromName"];
                //string fromEmail = "info@mudarorganic.com";
                //string fromName = "Sudheer MudarOrganic";
                //msg.From = new MailAddress(fromEmail, fromName);
                //msg.Subject = subject;

                //if (attachmentList != null && attachmentList.Length > 0)
                //{
                //    ///Retrieve path of the attachment file from the attchmentList
                //    foreach (string attachment in attachmentList)
                //    {
                //        ///Check whether attachment is there or not
                //        if (!string.IsNullOrEmpty(attachment))
                //            msg.Attachments.Add(new System.Net.Mail.Attachment(attachment));
                //    }
                //}



                /////Assign Smtp mail server
                ////string mailServer = ConfigurationManager.AppSettings["SMTPServer"];
                //string mailServer = "mail.mudarorganic.com";
                //SmtpClient client = new SmtpClient();
                //client.Host = mailServer;
                //if (ConfigurationManager.AppSettings["ServerType"].ToUpper() == "LOCAL")
                //{
                //    client.UseDefaultCredentials = true;
                //    client.EnableSsl = true;
                //    client.Port = 587;//Convert.ToInt32(ConfigurationManager.AppSettings["SMTPServer_port"]);
                //    client.Credentials = new System.Net.NetworkCredential(fromEmail,"Mudar$123");
                //}
                //client.Send(msg);
            
    }
        #region Appned Images in the Mail body

        private static LinkedResource GetResource(string Path, int iCount, AlternateView av)
        {
            Path = Path.Replace("%20", " ");
            string strContentId = "";
            if (iCount == 0)
            {
                LinkedResource logo = new LinkedResource(Path, MediaTypeNames.Image.Jpeg);
                strContentId = "Image" + iCount.ToString();
                logo.ContentId = strContentId;
                return logo;
            }
            else
            {
                LinkedResource logo = av.LinkedResources[0];
                logo = new LinkedResource(Path, MediaTypeNames.Image.Jpeg);
                strContentId = "Image" + iCount.ToString();
                logo.ContentId = strContentId;
                return logo;
            }
            //LinkedResource logo = new LinkedResource(Path, MediaTypeNames.Image.Jpeg);
            //string strContentId = "Image" + iCount.ToString();
            //logo.ContentId = strContentId;
            //return logo;

        }

        public static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }

        private static void ConvertImageBody(string strMailBody, out MailMessage msg)
        {
            #region the working code
            msg = new MailMessage();
            string strFinalBody = string.Empty;
            strFinalBody = strMailBody;
            int iCount = 0;
            AlternateView av1 = null;
            int iImageCount = CountStringOccurrences(strMailBody, "<img alt=");
            string[] strLinkedResource = null;
            strLinkedResource = new string[iImageCount];
            while (strFinalBody.Contains("<img alt="))
            {
                //strMailBody = ConvertImageBody(strMailBody, iCount);

                int iIndexofImg = strMailBody.IndexOf("<img ");
                string strImage = strMailBody.Substring(iIndexofImg, strMailBody.Length - iIndexofImg);
                int iLastIndexOfImg = strImage.IndexOf(" />");
                string Image = strMailBody.Substring(iIndexofImg, (iLastIndexOfImg + 3));
                int iIndexOfSrc = Image.IndexOf("src=");
                //string[] strImage = msg.Body.ToString().Split("<img");
                string strSourcePath = string.Empty;
                string[] SourcePath = Image.Split(' ');

                //strSourcePath = SourcePath[SourcePath.Length - 2].Remove(0, 4).Replace('/', '\\');
                //strSourcePath = strSourcePath.Replace('\"', ' ');

                //string path = System.Web.HttpContext.Current.Server.MapPath(@strSourcePath);

                int iSrcIndex = Image.IndexOf("src");
                strSourcePath = Image.Substring(iSrcIndex + 5, Image.Length - (iSrcIndex + 5)).Replace('/', '\\');

                int iLastSrcIndex = strSourcePath.IndexOf('"');
                strSourcePath = strSourcePath.Substring(0, iLastSrcIndex);

                string RootFolder = System.Web.HttpContext.Current.Request.PhysicalApplicationPath;
                string path = RootFolder + strSourcePath;
                string strContentId = "Image" + iCount.ToString();
                string strToReplace = "<html><body><img src=cid:" + strContentId + "><br></body></html>";

                strFinalBody = strFinalBody.Replace(Image, strToReplace);
                strMailBody = strMailBody.Replace(Image, "");

                strLinkedResource[iCount] = path;


                iCount++;
            }

            av1 = AlternateView.CreateAlternateViewFromString(strFinalBody, null, MediaTypeNames.Text.Html);

            for (int iIndex = 0; iIndex < iCount; iIndex++)
            {

                string Path = strLinkedResource[iIndex].ToString();
                //Remove any blank space
                Path = Path.Replace("%20", " ");

                //Remove any 
                Path = Path.Replace("%3C", "<");
                //Remove any 
                Path = Path.Replace("%3E", ">");
                //Remove any 
                Path = Path.Replace("%23", "#");
                //Remove any 
                Path = Path.Replace("%25", "%");
                //Remove any 
                Path = Path.Replace("%7B", "{");
                //Remove any 
                Path = Path.Replace("%7D", "}");
                //Remove any 
                Path = Path.Replace("%7C", "|");
                //Remove any 
                Path = Path.Replace("%5C", "\\");
                //Remove any 
                Path = Path.Replace("%5E", "^");
                //Remove any 
                Path = Path.Replace("%7E", "~");
                //Remove any 
                Path = Path.Replace("%5B", "[");
                //Remove any 
                Path = Path.Replace("%5D", "]");
                //Remove any 
                Path = Path.Replace("%60", "`");

                string strContentId = "";
                if (iIndex == 0)
                {
                    LinkedResource logo = new LinkedResource(Path, MediaTypeNames.Image.Jpeg);
                    strContentId = "Image" + iIndex.ToString();
                    logo.ContentId = strContentId;

                }
                else
                {
                    LinkedResource logo = av1.LinkedResources[0];
                    logo = new LinkedResource(Path, MediaTypeNames.Image.Jpeg);
                    strContentId = "Image" + iIndex.ToString();
                    logo.ContentId = strContentId;

                }
                av1.LinkedResources.Add(GetResource(Path, iIndex, av1));
            }

            msg.AlternateViews.Add(av1);
            #endregion

        }
        #endregion

        /// <summary>
        /// Overloaded method,used to call SendMail method with the arguments toMailIds, fromMailId and subject
        /// </summary>
        /// <param name="toMailIds">Recipient MailId / MailIds - Can give more than one mail ids. Use semicolon ";" to separate mail ids</param>
        /// <param name="subject">Subject</param>
        public static void SendEMail(string toMailIds, string subject)
        {
            //Call SendMail method with the arguments toMailIds, fromMailId and subject
            _SendEMail(toMailIds, null, null, subject, string.Empty, null, EmailFormat.TEXT);
        }
        /// <summary>
        /// Overloaded method,used to call SendMail method with the arguments toMailIds, fromMailId,subject and body
        /// </summary>
        /// <param name="toMailIds">Recipient MailId / MailIds - Can give more than one mail ids. Use semicolon ";" to separate mail ids</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body of the Mail</param>
        public static void SendEMail(string toMailIds, string subject, string body)
        {
            //Call SendMail method with the arguments toMailIds, fromMailId,subject and body
            _SendEMail(toMailIds, null, null, subject, body, null, EmailFormat.TEXT);
        }
        /// <summary>
        /// Overloaded method,used to call SendMail method with the arguments toMailIds, fromMailId,subject,body and attachments
        /// </summary>
        /// <param name="toMailIds">Recipient MailId / MailIds - Can give more than one mail ids. Use semicolon ";" to separate mail ids</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body of the Mail</param>
        /// <param name="attachments">path of the attaching file</param>
        public static void SendEMail(string toMailIds, string subject, string body, string[] attachments)
        {
            //Call SendMail method with the arguments toMailIds, fromMailId,subject,body and attachments
            _SendEMail(toMailIds, null, null, subject, body, attachments, EmailFormat.TEXT);
        }
        /// <summary>
        /// Overloaded method,used to call SendMail method with the arguments toMailIds, fromMailId,subject,body and mailFormat
        /// </summary>
        /// <param name="toMailIds">Recipient MailId / MailIds - Can give more than one mail ids. Use semicolon ";" to separate mail ids</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body of the Mail</param>
        /// <param name="mailFormat">mail format</param>
        public static void SendEMail(string toMailIds, string subject, string body, EmailFormat mailFormat)
        {
            //Call SendMail method with the arguments toMailIds, fromMailId,subject,body,attachments and mailFormat
            _SendEMail(toMailIds, null, null, subject, body, null, mailFormat);
        }
       
        /// <summary>
        /// Overloaded method,used to call SendMail method with the arguments toMailIds, fromMailId,subject,body,attachments and mailFormat
        /// </summary>
        /// <param name="toMailIds">Recipient MailId / MailIds - Can give more than one mail ids. Use semicolon ";" to separate mail ids</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body of the Mail</param>
        /// <param name="attachments">path of the attaching file</param>
        /// <param name="mailFormat">mail format</param>
        public static void SendEMail(string toMailIds, string subject, string body, string[] attachments, EmailFormat mailFormat)
        {
            //Call SendMail method with the arguments toMailIds, fromMailId,subject,body,attachments and mailFormat
            _SendEMail(toMailIds, null, null, subject, body, attachments, mailFormat);

        }
        /// <summary>
        /// Overloaded method,used to call SendMail method with the arguments toMailIds, fromMailId,subject,body, mailFormat and bccmailIds
        /// </summary>
        /// <param name="toMailIds">Recipient MailId / MailIds - Can give more than one mail ids. Use semicolon ";" to separate mail ids</param>
        /// <param name="bccMailIds">The BCC mail ids.</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body of the Mail</param>
        /// <param name="mailFormat">mail format</param>
        public static void SendEMail(string toMailIds, string bccMailIds, string subject, string body, EmailFormat mailFormat)
        {
            //Call SendMail method with the arguments toMailIds, fromMailId,subject,body,attachments, mailFormat and bcc ids
            _SendEMail(toMailIds, null, bccMailIds, subject, body, null, mailFormat);
        }
        /// <summary>
        /// Overloaded method,used to call SendMail method with all the arguments.
        /// </summary>
        /// <param name="toMailIds">To mail ids.</param>
        /// <param name="ccMailIds">The cc mail ids.</param>
        /// <param name="bccMailIds">The BCC mail ids.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="attachmentList">The attachment list.</param>
        /// <param name="mailFormat">The mail format.</param>
        public static void SendEMail(string toMailIds, string ccMailIds, string bccMailIds, string subject, string body, string[] attachmentList, EmailFormat mailFormat)
        {
            _SendEMail(toMailIds, ccMailIds, bccMailIds, subject, body, attachmentList, mailFormat);
        }
        public static void SendEMail(string toMailIds, string ccMailIds, string subject, string body, string[] attachmentList, EmailFormat mailFormat)
        {
            _SendEMail(toMailIds, ccMailIds, null, subject, body, attachmentList, mailFormat);
        }
        #endregion

        #region [ Public Methods - Various Email Options ]

        public static void SendLoginCredintials(string CName, string UID, string PWD, string ToEmail, string Subj)
        {
            string body = ExtractMailBody(FILE_LOGINCREDINTILAS);

            body = body.Replace("@CompanyName@", CName);
            body = body.Replace("@login@", UID);
            body = body.Replace("@password@", PWD);

            SendEMail(ToEmail, Subj, body, EmailFormat.HTML);
        }
        public static void SendBuyerCompanyInfo(string CName, string Address1, string Address2, string Address3, string city, string state, string pin, string country, string ToEmail, string CcEmail, string Subj)
        {
            string body = ExtractMailBody(FILE_BUYERCOMPANYINFO);

            body = body.Replace("@Company@", CName);
            body = body.Replace("@Address1@", Address1);
            body = body.Replace("@Address2@", Address2);
            body = body.Replace("@Address3@", Address3);
            body = body.Replace("@City@", city);
            body = body.Replace("@State@", state);
            body = body.Replace("@PinCode@", pin);
            body = body.Replace("@Country@", country);

            SendEMail(ToEmail, CcEmail, Subj, body, null, EmailFormat.HTML);
        }
        public static void SendBuyerContactInfo(string CName, string Contact, string Phone, string Mobile, string eMail, string Website, string ToEmail, string CcEmail, string Subj)
        {
            string body = ExtractMailBody(FILE_BUYERCONTACTINFO);

            body = body.Replace("@ContactPerson@", Contact);
            body = body.Replace("@Phone@", Phone);
            body = body.Replace("@Mobile@", Mobile);
            body = body.Replace("@email@", eMail);
            body = body.Replace("@website@", Website);
            body = body.Replace("@Company@", CName);

            SendEMail(ToEmail, CcEmail, Subj, body, null, EmailFormat.HTML);
        }
        public static void SendBuyerNotifyInfo(string CName, string Notify, string Address1, string Address2, string Address3, string city, string state, string pin, string country, string ToEmail, string CcEmail, string Subj)
        {
            string body = ExtractMailBody(FILE_BUYERNOTIFYINFO);

            body = body.Replace("@Company@", CName);
            body = body.Replace("@Notify@", Notify);
            body = body.Replace("@Address1@", Address1);
            body = body.Replace("@Address2@", Address2);
            body = body.Replace("@Address3@", Address3);
            body = body.Replace("@City@", city);
            body = body.Replace("@State@", state);
            body = body.Replace("@PinCode@", pin);
            body = body.Replace("@Country@", country);

            SendEMail(ToEmail, CcEmail, Subj, body, null, EmailFormat.HTML);
        }
        public static void SendBuyerBankInfo(string CName, string Bank, string Address1, string Address2, string Address3, string city, string state, string pin, string country, string ToEmail, string CcEmail, string Subj)
        {
            string body = ExtractMailBody(FILE_BUYERBANKINFO);

            body = body.Replace("@Company@", CName);
            body = body.Replace("@Bank@", Bank);
            body = body.Replace("@Address1@", Address1);
            body = body.Replace("@Address2@", Address2);
            body = body.Replace("@Address3@", Address3);
            body = body.Replace("@City@", city);
            body = body.Replace("@State@", state);
            body = body.Replace("@PinCode@", pin);
            body = body.Replace("@Country@", country);

            SendEMail(ToEmail, CcEmail, Subj, body, null, EmailFormat.HTML);
        }
        public static void SendBuyerPortInfo(string CName, string TransportMode, string Air, string Sea, string Road, string Rail, string ToEmail, string CcEmail, string Subj)
        {
            string body = ExtractMailBody(FILE_BUYERPORTINFO);

            body = body.Replace("@TransportMode@", TransportMode);
            body = body.Replace("@Air@", Air);
            body = body.Replace("@Sea@", Sea);
            body = body.Replace("@Road@", Road);
            body = body.Replace("@Rail@", Rail);
            body = body.Replace("@Company@", CName);

            SendEMail(ToEmail, CcEmail, Subj, body, null, EmailFormat.HTML);
        }

        public static void SendBuyerProductList(string CName, string ProductList, string ToEmail, string CcEmail, string Subj)
        {
            string body = ExtractMailBody(FILE_BUYERPRODUCTLIST);

            body = body.Replace("@Company@", CName);
            body = body.Replace("@ProductList@", ProductList);

            SendEMail(ToEmail, CcEmail, Subj, body, null, EmailFormat.HTML);
        }
        
        #endregion

        #region [ Private Methods ]
        /// <summary>
        /// Extracts the mail body from the text file.
        /// </summary>
        /// <param name="bodyFileName">Name of the body file.</param>
        /// <returns></returns>
        private static string ExtractMailBody(string bodyFileName)
        {
            try
            {
                string file = HttpContext.Current.Server.MapPath( emailBodyFolder + bodyFileName);
                if (!File.Exists(file)) return "";
                return File.ReadAllText(file);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion
    }
}
