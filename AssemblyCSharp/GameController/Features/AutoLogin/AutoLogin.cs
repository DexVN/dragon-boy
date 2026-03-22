using AssemblyCSharp.GameController.Command;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.AutoLogin
{
    internal class AutoLogin : ICommand
    {
        public static bool IsAutoLogin { get; private set; } = false;

        // Delay between auto-login attempts (ms)
        private const long AutoLoginDelayMs = 5000;
        private static long lastAttemptMs = 0;

        public AutoLogin() { }

        public static void Update()
        {

            if (!IsAutoLogin)
                return;

            // if login screen already present, nothing to do
            if (GameCanvas.loginScr != null)
                return;

            long now = mSystem.currentTimeMillis();
            if (now - lastAttemptMs < AutoLoginDelayMs)
                return;

            // update timestamp immediately to avoid multiple enqueues in the same window
            lastAttemptMs = now;

            MainThreadDispatcher.Enqueue(() =>
            {
                Debug.Log("Auto Login");
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
                        lastAttemptMs = 0; // allow immediate attempt if desired when toggled on
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Failed to toggle auto login: {ex.Message}");
                }
            });

        }
    }
}