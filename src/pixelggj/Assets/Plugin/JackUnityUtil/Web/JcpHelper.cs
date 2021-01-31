using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace JackUtil {

    public class JcpHelper {

        TcpHelper tcpHelper;

        Dictionary<string, Action<Packet>> eventDic;

        public JcpHelper(string url, int port) {

            eventDic = new Dictionary<string, Action<Packet>>();

            tcpHelper = new TcpHelper(url, port);
            tcpHelper.RecieveMsgEvent += RecieveMsg;

        }

        public void AddEventListener(string eventName, Action<Packet> cb) {

            if (!eventDic.ContainsKey(eventName)) {

                eventDic.Add(eventName, cb);
                // DebugUtil.Log("注册: " + eventName);

            } else {

                DebugHelper.LogWarning("已存在: " + eventName);

            }

        }

        async void TriggerEvent(string eventName, Packet p) {

            await Task.Run(() => {

                try {

                    if (eventDic.ContainsKey(eventName)) {

                        // DebugUtil.Log("触发: " + eventName);

                        eventDic.GetValue(eventName)?.Invoke(p);

                    } else {

                        DebugHelper.LogError("事件未注册: " + eventName);

                    }

                } catch(Exception e) {

                    DebugHelper.Log(e);

                }

            });

            
        }

        public async void EmitEvent<T>(string eventName, T dataObj) {

            string o;

            if (dataObj is string) {

                o = dataObj as string;

            } else {

                o = JsonConvert.SerializeObject(dataObj);

            }

            Packet p = new Packet(eventName, o);

            await tcpHelper.SendDataAsync(p.ToString());

        }

        async void RecieveMsg(string packetStr) {

            // DebugUtil.Log("收到: " + packetStr);

            await Task.Run(() => {

                try {

                    while(packetStr.Length > 5) {

                        // DebugUtil.Log("循环中: " + packetStr.Length);

                        int l = Packet.ReadLength(packetStr);

                        if (l == 0) {

                            DebugHelper.Log(l);
                            break;

                        }

                        Packet p = Packet.CutString(l, packetStr);

                        if (p != null) {

                            TriggerEvent(p.e, p);

                        } else {

                            DebugHelper.Log("接收的数据非Packet String");
                            break;

                        }

                        packetStr = packetStr.Substring(5 + l);

                    }

                } catch(Exception e) {

                    DebugHelper.Log("线程错误: " + e);

                }

            });

        }

        public async void StartRecieving() {

            await tcpHelper?.StartTcp();
            await tcpHelper?.StartRecieving();

        }

        public void Abort() {

            tcpHelper?.Abort();

        }

    }
}