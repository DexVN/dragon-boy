using AssemblyCSharp.GameController.Command;
using UnityEngine;

namespace AssemblyCSharp.GameController.Features.AutoLogin
{
    internal class AutoLogin : ICommand
    {
        private static AutoLogin _instance;
        public static AutoLogin gI() => _instance ?? (_instance = new AutoLogin());
        public static bool IsAutoLogin { get; private set; } = false;
        private const long autoLoginDelayMs = 5000;
        private static long lastAttemptMs = 0;

        public static void Update()
        {
            if (!IsAutoLogin)return;
            if (GameCanvas.loginScr != null)return;
            long now = mSystem.currentTimeMillis();
            if (now - lastAttemptMs < autoLoginDelayMs) return;
            lastAttemptMs = now;
            Debug.Log("[AutoLogin] Attempting to reconnect and login...");
            GameCanvas.loginScr = new LoginScr();
            GameCanvas.loginScr.doLogin();
            GameCanvas.endDlg();
        }

        public void Execute(GameControllerCommand cmdObj)
        {
            try
            {
                IsAutoLogin = cmdObj.value != 0f;
                if (IsAutoLogin) lastAttemptMs = 0;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[AutoLogin] Failed to toggle auto login: {ex.Message}");
            }
        }
    }
}