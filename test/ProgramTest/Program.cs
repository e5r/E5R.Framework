using System;
using System.Text;
using E5R.Framework.Security.Auth;
using E5R.Framework.Security.Auth.Models;

namespace E5R.Framework.ProgramTest
{
    public class Program
    {
        public void Main()
        {
            Console.WriteLine("E5R.Framework Manual Tests Program");

            Console.WriteLine("\nnew App()");
            Console.WriteLine("------------------------------------------------");
            var app = new App(){ Name= "My App", Description = "Application for tests!" };
            Console.WriteLine(string.Format("App.Id:          {0}", app.Id));
            Console.WriteLine(string.Format("App.Name:        {0}", app.Name));
            Console.WriteLine(string.Format("App.Description: {0}", app.Description));
            Console.WriteLine(string.Format("App.PrivateKey:  {0}", app.PrivateKey));

            Console.WriteLine("\n\nnew AppInstance()");
            Console.WriteLine("------------------------------------------------");
            var appInstance = new AppInstance(app){ Host= "myhost.com" };
            Console.WriteLine(string.Format("AppInstance.Id:   {0}", appInstance.Id));
            Console.WriteLine(string.Format("AppInstance.App:  {0} ({1})", appInstance.App?.Name, appInstance.App?.Id));
            Console.WriteLine(string.Format("AppInstance.Host: {0}", appInstance.Host));

            Console.WriteLine("\n\nnew AccessToken()");
            Console.WriteLine("------------------------------------------------");
            var accessToken = new AccessToken(){ Nonce = "CustomGeneratedNonceCode" };
            Console.WriteLine(string.Format("AccessToken.Token:          {0}", accessToken.Token));
            Console.WriteLine(string.Format("AccessToken.AppInstance:    {0} ({1})", accessToken.AppInstance?.Id, accessToken.AppInstance?.Host));
            Console.WriteLine(string.Format("AccessToken.Nonce:          {0}", accessToken.Nonce));
            Console.WriteLine(string.Format("AccessToken.NonceConfirmed: {0}", accessToken.NonceConfirmed));
        }
    }
}
