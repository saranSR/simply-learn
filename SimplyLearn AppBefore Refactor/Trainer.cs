using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplyLearn
{

    public class Trainer
    {
        public Trainer(string firstName, string lastName, string email, bool hasBlog, string blogURL, WebBrowser browser, string employer, int registrationFee)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.hasBlog = hasBlog;
            this.blogURL = blogURL;
            this.browser = browser;
            this.employer = employer;
            this.registrationFee = registrationFee;

        }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int? exp { get; set; }
        public bool hasBlog { get; set; }
        public string blogURL { get; set; }
        public WebBrowser browser { get; set; }
        public List<string> certifications { get; set; }
        public string employer { get; set; }
        public int registrationFee { get; set; }
        public List<Session> sessions { get; set; }

        public RegisterResponse Register(IRepository repository)
        {

            int? trainerId = null;
            bool good = false;
            bool appr = false;

            var ot = new List<string>() { "vb6", "assembly", "forrtan", "VBScript" };

            var domains = new List<string>() { "gmail.com", "yahoo.com", "hotmail.com" };

            if (string.IsNullOrWhiteSpace(firstName))
            {
                return new RegisterResponse(RegisterError.firstNameRequired);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(lastName))
                {
                    return new RegisterResponse(RegisterError.lastNameRequired);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(email))
                    {
                        return new RegisterResponse(RegisterError.emailRequired);
                    }
                    else
                    {
                        //put list of employers in array
                        var emps = new List<string>() { "Salesforce", "Microsoft", "Google", "Amazon" };

                        good = Exp > 10 || HasBlog || Certifications.Count() > 3 || emps.Contains(Employer);

                        if (!good)
                        {
                            //need to get just the domain from the email
                            string emailDomain = email.Split('@').Last();

                            if (!domains.Contains(emailDomain) && (!(Browser.Name == WebBrowser.BrowserName.InternetExplorer && Browser.MajorVersion < 9)))
                            {
                                good = true;
                            }
                        }

                        if (good)
                        {
                            if (Sessions.Count() == 0)
                            {
                                return new RegisterResponse(RegisterError.NoSessionsProvided);
                            }
                            else
                            {
                                appr = NewMethod(appr, ot);
                            }

                            if (!appr)
                            {
                                return new RegisterResponse(RegisterError.NoSessionsApproved);
                            }
                            else
                            {
                                NewMethod2();
                                //Now, save the speaker and sessions to the db.
                                trainerId = NewMethod1(repository, trainerId);
                            }
                        }
                        else
                        {
                            return new RegisterResponse(RegisterError.TrainerDoesNotMeetStandards);
                        }
                    }
                }
            }

            //if we got this far, the speaker is registered.
            return new RegisterResponse((int)trainerId);
        }

        private static void NewMethod2()
        {
            NewMethod3();
        }

        private static void NewMethod3()
        {
            if (Exp > 1)
            {
                if (Exp < 2 || Exp > 3)
                {
                    if (Exp < 4 || Exp > 5)
                    {
                        if (Exp < 6 || Exp > 9)
                        {
                            RegistrationFee = 0;
                        }
                        else
                        {
                            RegistrationFee = 50;
                        }
                    }
                    else
                    {
                        RegistrationFee = 100;
                    }
                }
                else
                {
                    RegistrationFee = 250;
                }
            }
            else
            {
                RegistrationFee = 500;
            }
        }

        private int? NewMethod1(IRepository repository, int? trainerId)
        {
            try
            {
                trainerId = repository.SaveTrainer(this);
            }
            catch (Exception e)
            {
                //in case the db call fails 
                Console.WriteLine("Data doesn't exist");
            }

            return trainerId;
        }

        private static bool NewMethod(bool appr, List<string> ot)
        {
            appr = NewMethod4(appr, ot);

            return appr;
        }

        private static bool NewMethod4(bool appr, List<string> ot)
        {
            foreach (var session in Sessions)
            {
                foreach (var tech in ot)
                {
                    if (session.Title.Contains(tech) || session.Description.Contains(tech))
                    {
                        session.Approved = false;
                        break;
                    }
                    else
                    {
                        session.Approved = true;
                        appr = true;
                    }
                }
            }

            return appr;
        }
    }
}