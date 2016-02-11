#region Imports

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

#endregion

public class SynchronousSocketListener
{
    // Incoming data from the client.
    public static string data = null;

    public static void StartListening()
    {
        // Data buffer for incoming data.
        byte[] bytes = new Byte[1024];

        // Establish the local endpoint for the socket.
        Console.WriteLine("Server started.");
        Console.Write("Enter local IP-address:");
        IPAddress IP;
        bool gotIP = IPAddress.TryParse(Console.ReadLine(), out IP);

        if (gotIP)
        {
            // Establish the remote endpoint for the socket.
            // This example uses port 11000 on the local computer.
            IPEndPoint remoteEP = new IPEndPoint(IP, 11000);


            IPEndPoint localEndPoint = new IPEndPoint(IP, 11000);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            // listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                
                // Start listening for connections.
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.
                    Socket handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.
                    while (true)
                    {
                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        
                        Console.WriteLine("Text received : {0}", data);
                                                
                        if (data == "EXIT")
                        {
                            Console.WriteLine("Client exited.");
                            break;
                        }
                    }

                    // Show the data on the console.
                    
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }
    }
}
