using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics; // Process. ~ . ~ 생성에 필요함


namespace Client_Thread_
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            if (args.Length < 4)
            {
                Console.WriteLine("사용법 : {0} <Bind IP> <Bind Port> <Bind IP> <Message>",
                    Process.GetCurrentProcess().ProcessName);
                return;
            }
            */
            string bindIp = "127.0.0.1";//args[0];
            int bindPort = Convert.ToInt32(1001);
            string serverIp = "127.0.0.1";
            const int serverPort = 5426;
            string message = "";

            try
            {
                IPEndPoint clientAddress = new IPEndPoint(IPAddress.Parse(bindIp), bindPort);
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

                Console.WriteLine("클라이언트 : {0}, 서버: {1}", clientAddress.ToString(), serverAddress.ToString());

                TcpClient client = new TcpClient(clientAddress);

                client.Connect(serverAddress);

                Console.WriteLine("보낼메세지를 입력하세요 .....");
                message = Console.ReadLine();

                byte[] data = System.Text.Encoding.Default.GetBytes(message);
                // ??

                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                Console.WriteLine("송신: {0}", message);

                data = new byte[256];

                string responseData = "";

                int bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.Default.GetString(data, 0, bytes);
                Console.WriteLine("수신 : {0}", responseData);

                stream.Close();
                client.Close();

            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("클라이언트를 종료합니다");
            }
        }
    }
}
