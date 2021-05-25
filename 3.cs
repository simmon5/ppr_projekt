using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using CookComputing.XmlRpc;
using System.IO;


namespace socket
{

    [XmlRpcUrl("http://localhost:10003/RPC2")]
    public interface HEXX : IXmlRpcProxy
    {
        [XmlRpcMethod]
        String add();
        
        [XmlRpcMethod]
        String abc(byte[] text);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Proces 3");
			byte[] bytes = new Byte[1024];
			string data = null;
			Encoding enc8 = Encoding.UTF8;
			IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 10002);
			Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			try {
				listener.Bind(localEndPoint);
				listener.Listen(10); 
				while (true) {
					try{
						//Console.WriteLine("Waiting for a connection...");
						Socket handler = listener.Accept();
						//Console.WriteLine($"Connection from {handler.RemoteEndPoint.ToString()}");
						handler.Receive(bytes);
						//data = Encoding.Unicode.GetString(bytes);
						data = enc8.GetString(bytes);
						Console.WriteLine("Otrzymano wiadomosc od procesu 2.");
						byte[] msg = Encoding.Unicode.GetBytes(data);
						handler.Send(msg);
						handler.Shutdown(SocketShutdown.Both);
						handler.Close();
						
						HEXX proxy = XmlRpcProxyGen.Create<HEXX>();
                        proxy.NonStandard = XmlRpcNonStandard.All;
                        Console.WriteLine("Wyslano wiadomosc do procesu 3.");
                        proxy.abc(msg);
                        						
						
					} catch (Exception e) {
						//Console.WriteLine(e.ToString());
					}
					
					
                    
					
				}
			} catch (Exception e) {
				Console.WriteLine(e.ToString());
			}

			
			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();
        }
    }
}
