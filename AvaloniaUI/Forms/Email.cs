using System;
using System.Net;
using System.Net.Mail;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

class Email{
    public static bool SendEmailCode(string email, ref int emailCode)
    {
        
        string stmpServer = "smtp.gmail.com";
        int smtpPort = 587;
        string smtpUserName = "maratsaitovv@gmail.com";
        string smtpPassword = "nmal bfyl dpgj wiph";

        using (SmtpClient smtpClient = new SmtpClient(stmpServer, smtpPort))
        {
            smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
            smtpClient.EnableSsl = true;
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(smtpUserName);
                    mailMessage.To.Add(email);
                    mailMessage.Subject = "Verification code";
                    emailCode = new Random().Next(1000, 9999);
                    mailMessage.Body = $"Your verification code: {emailCode}";
                    smtpClient.Send(mailMessage);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBoxManager.GetMessageBoxStandard("Error", $"Не удалось отправить сообщение на адрес {email}. Ошибка: {ex.Message}", ButtonEnum.Ok).ShowWindowAsync();
                return false;
            }
        }
    }
}