using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Text;
using System.IO;
using System.Net.Configuration;

public class Emailtest
{
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
    // read the buyer info
    private static string FILE_BUYERCOMPANYINFO = "BuyerCompanyInfo.txt";
    private static string FILE_BUYERCONTACTINFO = "BuyerContactInfo.txt";
    private static string FILE_BUYERNOTIFYINFO = "BuyerNotifyInfo.txt";
    private static string FILE_BUYERBANKINFO = "BuyerBankInfo.txt";
    private static string FILE_BUYERPORTINFO = "BuyerPortInfo.txt";
    private static string FILE_BUYERPRODUCTLIST = "BuyerProductList.txt";
    // update the buyerinfo
    private static string FILE_UPD_Company = "UpdBuyerCompanyInfo.txt";
    private static string FILE_UPD_CONTACT = "UpdBuyerContact.txt";
    private static string FILE_UPD_Notify = "UpdBuyerNotifyInfo.txt";
    private static string FILE_UPD_BankInfo = "UpdBuyerBankInfo.txt";
    private static string FILE_UPD_PortInfo = "UpdBuyerPortInfo.txt";
    private static string FILE_SendOrderInformation = "SendOrderInformation.txt";
    private static string FILE_UpdBuyerProduct = "UpdBuyerProduct.txt";

    private static string ExtractMailBody(string bodyFileName)
    {
        try
        {
            string file = HttpContext.Current.Server.MapPath(emailBodyFolder + bodyFileName);
            if (!File.Exists(file)) return "";
            return File.ReadAllText(file);
        }
        catch (Exception ex)
        {
            return "";
        }
    }
    public void testmethod()
    {
        MailMessage mailMessage = new MailMessage();
        mailMessage.To.Add("your.own@mail-address.com");
        mailMessage.From = new MailAddress("another@mail-address.com");
        mailMessage.Subject = "ASP.NET e-mail test";
        mailMessage.Body = "Hello world,\n\nThis is an ASP.NET test e-mail!";
        SmtpClient smtpClient = new SmtpClient("smtp.your-isp.com");
        smtpClient.Send(mailMessage);
    }
    public void EmailMethod(string body, string ToEmail, string Subj)
    {
        try
        {
            //MailMessage mailmessage = new MailMessage();
            //string fromemail = ConfigurationManager.AppSettings["SupportFromEmail"];
            //mailmessage.Subject = "<h1> " + Subj + "</h1> ";
            //mailmessage.From = new MailAddress(fromemail);
            //mailmessage.To.Add(new MailAddress(ToEmail));
            //mailmessage.To.Add(new MailAddress("rbhanu@vgsoft.in"));
            //mailmessage.Body = body;
            //mailmessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html));
            //string SmtpServer = ConfigurationManager.AppSettings["SMTPServer"];
            //string SmtpUsername = ConfigurationManager.AppSettings["SupportFromEmail"];
            //string SmtpPassword = ConfigurationManager.AppSettings["SupportFromPwd"];
            //string port = ConfigurationManager.AppSettings["SMTPServer_port"];
            //SmtpClient smtpClient = new SmtpClient(SmtpServer, Convert.ToInt32(port));
            //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(SmtpUsername, SmtpPassword);
            //smtpClient.Credentials = credentials;
            //smtpClient.Send(mailmessage);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void SendEmail(string CName, string UID, string PWD, string toemail,string Sub)
    {
        string body = ExtractMailBody(FILE_LOGINCREDINTILAS);
        body = body.Replace("@CompanyName@", CName);
        body = body.Replace("@login@", UID);
        body = body.Replace("@password@", PWD);
        MailMessage mailmessage = new MailMessage();
        string fromemail = ConfigurationManager.AppSettings["SupportFromEmail"];
        mailmessage.Body = body;
        mailmessage.To.Add(new MailAddress(toemail));
        mailmessage.From = new MailAddress(fromemail);
        mailmessage.Subject = Sub;
        mailmessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html));
        string SmtpServer = ConfigurationManager.AppSettings["SMTPServer"];
        string SmtpUsername = ConfigurationManager.AppSettings["SupportFromEmail"];
        string SmtpPassword = ConfigurationManager.AppSettings["SupportFromPwd"];
        string port = ConfigurationManager.AppSettings["SMTPServer_port"];
        SmtpClient smtpClient = new SmtpClient(SmtpServer, Convert.ToInt32(port));
        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(SmtpUsername, SmtpPassword);
        smtpClient.Credentials = credentials;
        smtpClient.Send(mailmessage);
    }
    public bool SendBuyerCompanyInfo(string CName, string Address1, string Address2, string Address3, string city, string state, string pin, string country, string ToEmail, string CcEmail, string Subj)
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
        EmailMethod(body, ToEmail, Subj);
        return true;
    }
    public bool UpdBuyerCompanyInfo(string CName, string Address1, string Address2, string Address3, string city, string state, string pin, string country, string ToEmail, string CcEmail, string Subj)
    {
        string body = ExtractMailBody(FILE_UPD_Company);
        body = body.Replace("@Company@", CName);
        body = body.Replace("@Address1@", Address1);
        body = body.Replace("@Address2@", Address2);
        body = body.Replace("@Address3@", Address3);
        body = body.Replace("@City@", city);
        body = body.Replace("@State@", state);
        body = body.Replace("@PinCode@", pin);
        body = body.Replace("@Country@", country);
        EmailMethod(body, ToEmail, Subj);
        return true;
    }
    public bool SendBuyerContactInfo(string CName, string Contact, string Phone, string Mobile, string eMail, string Website,string ToEmail, string CcEmail, string Subj)
    {
        string body = ExtractMailBody(FILE_BUYERCONTACTINFO);

        body = body.Replace("@ContactPerson@", Contact);
        body = body.Replace("@Phone@", Phone);
        body = body.Replace("@Mobile@", Mobile);
        body = body.Replace("@email@", eMail);
        body = body.Replace("@website@", Website);
        body = body.Replace("@Company@", CName);
        EmailMethod(body, ToEmail, Subj);
        return true;
    }
    public bool UpdBuyerContactInfo(string CName, string Contact, string Phone, string Mobile, string Website, string ToEmail, string CcEmail, string Subj)
    {
        string body = ExtractMailBody(FILE_UPD_CONTACT);

        body = body.Replace("@ContactPerson@", Contact);
        body = body.Replace("@Phone@", Phone);
        body = body.Replace("@Mobile@", Mobile);
        body = body.Replace("@email@", ToEmail);
        body = body.Replace("@website@", Website);
        body = body.Replace("@Company@", CName);
        EmailMethod(body, ToEmail, Subj);
        return true;
    }
    public bool SendBuyerNotifyInfo(string CName, string Notify, string Address1, string Address2, string Address3, string city, string state, string pin, string country, string ToEmail, string CcEmail, string Subj)
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
        EmailMethod(body, ToEmail, Subj);
        return true;

        //MailMessage mailmessage = new MailMessage();
        //string fromemail = ConfigurationManager.AppSettings["SupportFromEmail"];
        //mailmessage.Body = body;
        //mailmessage.To.Add(new MailAddress(ToEmail));
        //mailmessage.From = new MailAddress(fromemail);
        //mailmessage.Subject = Subj;
        //mailmessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html));
        //string SmtpServer = ConfigurationManager.AppSettings["SMTPServer"];
        //string SmtpUsername = ConfigurationManager.AppSettings["SupportFromEmail"];
        //string SmtpPassword = ConfigurationManager.AppSettings["SupportFromPwd"];
        //string port = ConfigurationManager.AppSettings["SMTPServer_port"];
        //SmtpClient smtpClient = new SmtpClient(SmtpServer, Convert.ToInt32(port));
        //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(SmtpUsername, SmtpPassword);
        //smtpClient.Credentials = credentials;
        //smtpClient.Send(mailmessage);
    }
    public bool UpdBuyerNotifyInfo(string CName, string Notify, string Address1, string Address2, string Address3, string city, string state, string pin, string country, string ToEmail, string CcEmail, string Subj)
    {
        string body = ExtractMailBody(FILE_UPD_Notify);
        body = body.Replace("@Company@", CName);
        body = body.Replace("@Notify@", Notify);
        body = body.Replace("@Address1@", Address1);
        body = body.Replace("@Address2@", Address2);
        body = body.Replace("@Address3@", Address3);
        body = body.Replace("@City@", city);
        body = body.Replace("@State@", state);
        body = body.Replace("@PinCode@", pin);
        body = body.Replace("@Country@", country);
        EmailMethod(body, ToEmail, Subj);
        return true;
    }
    public bool SendBuyerBankInfo(string CName, string Bank, string Address1, string Address2, string Address3, string city, string state, string pin, string country, string ToEmail, string CcEmail, string Subj)
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
        EmailMethod(body, ToEmail, Subj);
        return true;
    }
    public bool UpdBuyerBankInfo(string CName, string Bank, string Address1, string Address2, string Address3, string city, string state, string pin, string country, string ToEmail, string CcEmail, string Subj)
    {
        string body = ExtractMailBody(FILE_UPD_BankInfo);
        body = body.Replace("@Company@", CName);
        body = body.Replace("@Bank@", Bank);
        body = body.Replace("@Address1@", Address1);
        body = body.Replace("@Address2@", Address2);
        body = body.Replace("@Address3@", Address3);
        body = body.Replace("@City@", city);
        body = body.Replace("@State@", state);
        body = body.Replace("@PinCode@", pin);
        body = body.Replace("@Country@", country);
        EmailMethod(body, ToEmail, Subj);
        return true;
    }
    public bool SendBuyerPortInfo(string CName, string TransportMode, string Air, string Sea, string Road, string Rail, string ToEmail, string CcEmail, string Subj)
    {
        string body = ExtractMailBody(FILE_BUYERPORTINFO);
        body = body.Replace("@TransportMode@", TransportMode);
        body = body.Replace("@Air@", Air);
        body = body.Replace("@Sea@", Sea);
        body = body.Replace("@Road@", Road);
        body = body.Replace("@Rail@", Rail);
        body = body.Replace("@Company@", CName);
        EmailMethod(body, ToEmail, Subj);
        return true;
    }
    public bool UpdBuyerPortInfo(string CName, string TransportMode, string Air, string Sea, string Road, string Rail, string ToEmail, string CcEmail, string Subj)
    {
        string body = ExtractMailBody(FILE_UPD_PortInfo);
        body = body.Replace("@TransportMode@", TransportMode);
        body = body.Replace("@Air@", Air);
        body = body.Replace("@Sea@", Sea);
        body = body.Replace("@Road@", Road);
        body = body.Replace("@Rail@", Rail);
        body = body.Replace("@Company@", CName);
        EmailMethod(body, ToEmail, Subj);
        return true;
    }
    public bool SendBuyerProductList(string CName, string ProductList, string ToEmail, string CcEmail, string Subj)
    {
        string body = ExtractMailBody(FILE_BUYERPRODUCTLIST);
        body = body.Replace("@Company@", CName);
        body = body.Replace("@ProductList@", ProductList);
        EmailMethod(body, ToEmail, Subj);
        return true;
    }
    public bool UpdBuyerProductList(string CName, string ProductList, string ToEmail, string CcEmail, string Subj)
    {
        string body = ExtractMailBody(FILE_UpdBuyerProduct);
        body = body.Replace("@Company@", CName);
        body = body.Replace("@ProductList@", ProductList);
        EmailMethod(body, ToEmail, Subj);
        return true;
    }
    public bool SendOrderDetails(string OrderList, string ToEmail, string Subj)
    {
        string body = ExtractMailBody(FILE_SendOrderInformation);
        body = body.Replace("@ProductList@", OrderList);
        EmailMethod(body, ToEmail, Subj);
        return true;
    }
}