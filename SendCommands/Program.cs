using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
//using Claims;

namespace CTRL
{
    class Program
    {
        //public static DumptoText DTT;
        public static HttpListener listener;
        public static string url = "http://localhost:8000/";
        public static int pageViews = 0;
        public static int requestCount = 0;
        public static string pageData =
            "<!DOCTYPE>" +
            "<html>" +
            "  <head>" +
            "    <title>HttpListener Example</title>" +
            "  </head>" +
            "  <body>" +
            "    <p>Page Views: {0}</p>" +
            "    <form method=\"post\" action=\"shutdown\">" +
            "      <input type=\"submit\" value=\"Shutdown\" {1}>" +
            "    </form>" +
            "  </body>" +
            "</html>";

        public static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
            string projectDirectory = "";

            try
            {
                string workingDirectory = Environment.CurrentDirectory;
                projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                //Program.DTT = new DumptoText(projectDirectory + "\\LOG.txt");
                Console.WriteLine("DumpToText: " + projectDirectory + "\\LOG.txt");

            }
            catch (Exception e)
            {
                Console.WriteLine("A fatal exception occurred when creating Log file.");
            }



            try
            {

                do
                {


                    //var RegisteredUsers = new List<Person>();
                    //RegisteredUsers.Add(new Person() { PersonID = 1, Name = "Bryon Hetrick", Registered = true });
                    //RegisteredUsers.Add(new Person() { PersonID = 2, Name = "Nicole Wilcox", Registered = true });
                    //RegisteredUsers.Add(new Person() { PersonID = 3, Name = "Adrian Martinson", Registered = false });
                    //RegisteredUsers.Add(new Person() { PersonID = 4, Name = "Nora Osborn", Registered = false });

                    //Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                    //var serializedResult = serializer.Serialize(RegisteredUsers);


                    // Create a Http server and start listening for incoming connections
                    //listener = new HttpListener();
                    //listener.Prefixes.Add(url);
                    //listener.Start();
                    //Console.WriteLine("Listening for connections on {0}", url);

                    //// Handle requests
                    //Task listenTask = HandleIncomingConnections(sz);
                    //listenTask.GetAwaiter().GetResult();

                    //// Close the listener
                    //listener.Close();
                    //MyWebServer ws = new MyWebServer(sz);
                    var server = new SocketServer(projectDirectory);
                    server.Start("http://+:24277/ss/");
                    Console.WriteLine("Press any key to restart...or q to quit");
                    cki = Console.ReadKey(true);
                    if (server.webSocket != null)
                        server.webSocket.Dispose();
                    try
                    {
                        System.Diagnostics.Process.Start("netsh.exe", "http delete urlacl url=http://+:24277/ss/");
                    }
                    catch (Exception e2)
                    {
                        //DTT.AddWrite(e2.ToString());
                        //DTT.AddWrite("ERROR WHEN TRYING TO UNREGISTER URL WHEN RESTARTING SERVER");
                        //DTT.Write();
                    }
                } while (cki.Key != ConsoleKey.Q);

            }
            catch (Exception e)
            {
                //DTT.AddWrite(e.ToString());
                //DTT.Write();
                Console.WriteLine("A fatal exception occurred.");
                cki = Console.ReadKey(true);
            }





        }
    }
}
