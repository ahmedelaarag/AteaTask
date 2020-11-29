using System;
using Microsoft.Owin.Hosting;

namespace PresentationAPI
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var domainAddress = "http://localhost:25250";
            using (WebApp.Start(domainAddress))
            {
                Console.WriteLine("Service Hosted " + domainAddress);
                System.Threading.Thread.Sleep(-1);
            }
        }
    }
}