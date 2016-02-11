#region Imports

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

#endregion

public class SynchronousSocketClient
{
    public static void StartClient()
    {
        // Data buffer for incoming data.
        byte[] bytes = new byte[1024];

        // Connect to a remote device.
        try
        {
            Console.WriteLine("Client started.");
            Console.Write("Enter IP-address to connect to: ");
            IPAddress IP;
            bool gotIP = IPAddress.TryParse(Console.ReadLine(), out IP);

            if (gotIP)
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.
                IPEndPoint remoteEP = new IPEndPoint(IP, 11000);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender.Connect(remoteEP);
                    
                    Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

                    while (true)
                    {
                        Console.Write("Enter a string (type EXIT to close): ");
                        string input =  Console.ReadLine();   
                        // Encode the data string into a byte array.
                        byte[] msg = Encoding.ASCII.GetBytes(input);

                        // Send the data through the socket.
                        int bytesSent = sender.Send(msg);
                                             
                        if (input == "EXIT")
                            break;
                    }
                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
