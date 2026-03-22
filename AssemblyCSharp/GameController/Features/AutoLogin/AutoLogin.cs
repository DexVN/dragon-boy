using AssemblyCSharp.GameController.Command;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.AutoLogin
{
    internal class AutoLogin : ICommand
    {
        public static bool IsAutoLogin { get; private set; } = false;

        private const long AutoLoginDelayMs = 5000;

        private static long lastAttemptMs = 0;

        public AutoLogin() { }

        public static void Update()
        {

            if (!IsAutoLogin)
                return;

            if (GameCanvas.loginScr != null)
                return;

            long now = mSystem.currentTimeMillis();
            if (now - lastAttemptMs < AutoLoginDelayMs)
                return;

            lastAttemptMs = now;

            MainThreadDispatcher.Enqueue(() =>
            {
                if (GameCanvas.loginScr == null)
                {
                    GameCanvas.loginScr = new LoginScr();
                    GameCanvas.loginScr.doLogin();
                }
                GameCanvas.endDlg();
            });
        }

        public void Execute(GameControllerCommand cmdObj)
        {
            MainThreadDispatcher.Enqueue(() =>
            {
                try
                {
                    IsAutoLogin = cmdObj.value != 0f;
                    if (IsAutoLogin)
                        lastAttemptMs = 0;
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Failed to toggle auto login: {ex.Message}");
                }
            });
        }
    }
}