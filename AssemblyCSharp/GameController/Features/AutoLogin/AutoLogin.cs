using AssemblyCSharp.GameController.Command;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.AutoLogin
{
    internal class AutoLogin : ICommand
    {
        public static bool IsAutoLogin { get; private set; } = false;

        public static void Update()
        {
            Debug.Log("AutoLogin Update: " + IsAutoLogin);
            if (GameCanvas.loginScr == null && IsAutoLogin)
            {
                MainThreadDispatcher.Enqueue(() =>
                {
                    Debug.Log("Auto Login");
                    GameCanvas.loginScr = new LoginScr();
                    GameCanvas.loginScr.doLogin();
                    GameCanvas.endDlg();
                });
            }
        }

        public void Execute(GameControllerCommand cmdObj)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                try
                {
                    IsAutoLogin = cmdObj.value != 0f;
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Failed to toggle auto login: {ex.Message}");
                }
            });

        }
    }
}
