using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;

namespace JackUtil {

    public class TcpHelper {

        TcpClient tcp;
        string url;
        int port;
        byte[] recieveData;
        byte[] sendData;
        int maxDataLength;
        NetworkStream stream;
        CancellationToken token;
        CancellationTokenSource tokenSource;

        public event Action<string> RecieveMsgEvent;

        public TcpHelper(string url, int port, int maxDataLength = 8192) {

            this.url = url;
            this.port = port;
            this.maxDataLength = maxDataLength;
            try {
                
                tcp = new TcpClient(url, port);

            } catch(Exception e) {

                DebugHelper.Log(e);

            }

        }

        public async Task SendDataAsync(string message) {

            sendData = Encoding.UTF8.GetBytes(message);
            if (tcp == null || !tcp.Connected) {
                await StartTcp();
            }
            await stream?.WriteAsync(sendData, 0, sendData.Length);

        }

        public async Task StartTcp() {

            await Task.Run(async () => {

                while(tcp == null || !tcp.Connected) {

                    try {

                        tcp = new TcpClient(url, port);
                        await Task.Delay(2000);

                    } catch(Exception e) {

                        DebugHelper.Log(e);

                    }

                }

            });

            stream = tcp.GetStream();
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            
        }

        public async Task StartRecieving() {

            if (tcp == null || !tcp.Connected) {
                await StartTcp();
            }

            Task t = new Task(async () => {

                while(true) {

                    try {

                        if (token.IsCancellationRequested) {

                            return;

                        }

                        string resData = string.Empty;

                        recieveData = new byte[maxDataLength];
                        Int32 bytes = stream.Read(recieveData, 0, recieveData.Length);
                        resData = Encoding.UTF8.GetString(recieveData, 0, bytes);
                        stream.Flush();
                        RecieveMsgEvent?.Invoke(resData);

                        await Task.Delay(16);

                    } catch (Exception e) {

                        DebugHelper.Log(e);

                        Abort();

                        await Task.Delay(16);

                    }

                }

            }, token);

            t.Start();

        }

        public void Abort() {

            tokenSource?.Cancel();

            stream?.Close();
            tcp?.Close();

        }

    }
}