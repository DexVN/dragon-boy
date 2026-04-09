using AssemblyCSharp.GameController.Command;
using AssemblyCSharp.GameController.Features.AutoFarm;
using AssemblyCSharp.GameController.Features.AutoPilgrimage;
using AssemblyCSharp.GameController.Features.Mission;
using AssemblyCSharp.GameController.Features.Navigation;
using AssemblyCSharp.GameController.Features.Speed;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace AssemblyCSharp.GameController
{
    public class CommandReceiver
    {
        private static readonly Queue<string> commandQueue = new Queue<string>();

        private static readonly object queueLock = new object();

        private static readonly string Host = "127.0.0.1";

        private static readonly int Port = 12345;

        private const int BufferSize = 1024;

        public static void StartListenting()
        {
            new Thread(() =>
            {
                try
                {
                    TcpListener server = new TcpListener(IPAddress.Parse(Host), Port);
                    server.Start();
                    Debug.Log($"[CommandReceiver] Đang lắng nghe lệnh tại {Host}:{Port}");

                    while (true)
                    {
                        using (var client = server.AcceptTcpClient())
                        using (var stream = client.GetStream())
                        {
                            byte[] buffer = new byte[BufferSize];
                            int len = stream.Read(buffer, 0, buffer.Length);

                            if (len > 0)
                            {
                                string cmd = Encoding.UTF8.GetString(buffer, 0, len);

                                lock (queueLock)
                                {
                                    commandQueue.Enqueue(cmd);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"[CommandReceiver] Lỗi mạng: {e.Message}");
                }
            })
            { IsBackground = true }.Start();
        }

        public static void Update()
        {
            string cmdToProcess = null;

            lock (queueLock)
            {
                if (commandQueue.Count > 0)
                {
                    cmdToProcess = commandQueue.Dequeue();
                }
            }

            if (cmdToProcess != null)
            {
                ProcessCommand(cmdToProcess);
            }
        }

        static void ProcessCommand(string cmd)
        {
            try
            {
                Debug.Log($"[CommandReceiver] Đã nhận lệnh: {cmd} ");
                cmd = cmd.Trim().Trim('\0');

                GameControllerCommand gcObj = JsonUtility.FromJson<GameControllerCommand>(cmd);

                if (gcObj == null || !gcObj.IsValid())
                {
                    Debug.Log("[CommandReceiver] Cú pháp lệnh (CMD) không hợp lệ");
                    return;
                }

                switch(gcObj.action)
                {
                    case "game_speed":
                        GameSpeed.gI().Execute(gcObj);
                        break;
                    case "player_speed":
                        PlayerSpeed.gI().Execute(gcObj);
                        break;
                    case "auto_farm":
                        AutoFarm.gI().Execute(gcObj);
                        break;
                    case "auto_login":                    
                        break;
                    case "auto_pilgrimage":
                        int targetMapID = Map.GetMapIdByName("yamete, hãy đưa ta đến Thung lũng Namếc");
                        Logger.Info("" + targetMapID);
                        if (gcObj.value != 0f) MissionManager.gI().CurrentMission = new HoTongDuongTangMission();
                        else MissionManager.gI().CurrentMission = null;

                        break;
                    default:
                        Debug.Log($"[CommandReceiver] Lệnh {gcObj.action} không hợp lệ");
                        break;
                }
            } 
            catch (Exception e)
            {
                Debug.LogError($"[CommandReceiver] Lỗi xử lý lệnh: {e.Message}");
            }
        }
    }
}
