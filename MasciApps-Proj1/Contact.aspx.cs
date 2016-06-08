using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MasciApps_Proj1
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ContactSubmitButton_Click(object sender, EventArgs e)
        {
            if (FullNameValidator.IsValid &&
                EmailAddressValidator.IsValid &&
                EmailRegexValidator.IsValid &&
                MessageValidator.IsValid)
            {
                /*
                 * Tom ... must change the Credentials to an active Yahoo Account in order for Email service to work.
                 */
                string emailAddressCred = "<Enter Yahoo Email Address>", //Yahoo Email you want to use to connect to the host - in order to send email
                   emailPasswordCred = "<Enter Password>", //Password of the email you want to use to connect to the host - in order to send email
                   sendToAddress = "minimasci@yahoo.ca", //Email that sends
                   sendFromAddress = "minimasci@yahoo.ca", //Who to send the email to
                   messageBody,
                   messageSubject;

                try
                {
                    //Prepare data
                    messageBody = SubjectTextBox.Text + ": " + MessageTextBox.Text;
                    messageSubject = "Message from: " + FullNameTextBox.Text + "<" + EmailAddressTextBox.Text + ">";
                    sendToAddress += ", " + EmailAddressTextBox.Text;

                    //Send email using Yahoo
                    SmtpClient smtpClient = new SmtpClient("smtp.mail.yahoo.com", 587);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential(emailAddressCred, emailPasswordCred);
                    smtpClient.Send(sendFromAddress, sendToAddress, messageSubject, messageBody);

                    //Indicate email success
                    ContactSubmitButton.Text = "Success";
                }
                catch (Exception err)
                {
                    //Indicate email failure - timeout
                    ContactSubmitButton.Text = "Failed - Please try again";
                }
            }
        }
    }
}