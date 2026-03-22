using AssemblyCSharp.GameController.Command;
using AssemblyCSharp.GameController.Features.AutoFarm;
using AssemblyCSharp.GameController.Features.AutoLogin;
using AssemblyCSharp.GameController.Features.AutoPilgrimage;
using AssemblyCSharp.GameController.Features.Speed;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;


public class SocketServer
{


    public static void Start()
    { 
        new Thread(() =>
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 12345);
            server.Start();

            while (true)
            {
                var client = server.AcceptTcpClient();
                var stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int len = stream.Read(buffer, 0, buffer.Length);

                string cmd = Encoding.UTF8.GetString(buffer, 0, len);

                HandleCommand(cmd);
            }
        }).Start();
    }

    static void HandleCommand(string cmd)
    {
        try
        {
            Debug.Log("Received command: " + cmd);
            cmd = cmd.Trim().Trim('\0');

            GameControllerCommand gcObj = JsonUtility.FromJson<GameControllerCommand>(cmd);

            if (gcObj == null || !gcObj.IsValid())
            {
                Debug.Log("CMD INVALID");
                return;
            }

            switch (gcObj.action)
            {
                case "game_speed":
                    new GameSpeed().Execute(gcObj);
                    break;
                case "player_speed":
                    new PlayerSpeed().Execute(gcObj);
                    break;
                case "auto_farm":
                    new AutoFarm().Execute(gcObj);
                    break;
                case "auto_login":
                    new AutoLogin().Execute(gcObj);
                    break;
                case "auto_pilgrimage":
                    new AutoPilgrimage().Execute(gcObj);
                    break;
                default:
                    Debug.Log("Unknown command: " + gcObj.action);
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Command error: " + e.Message);
        }
    }
}