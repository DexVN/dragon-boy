using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public class CallerMemberNameAttribute : Attribute { }
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public class CallerFilePathAttribute : Attribute { }
}

public static class Logger
{
    public static void Info(string message,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string methodName = "")
    {
        string header = GetHeader(filePath, methodName);
        Debug.Log(string.Format("{0} {1}", header, message));
    }

    public static void Warning(string message,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string methodName = "")
    {
        string header = GetHeader(filePath, methodName);
        Debug.LogWarning(string.Format("{0} {1}", header, message));
    }

    public static void Error(string message,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string methodName = "")
    {
        string header = GetHeader(filePath, methodName);
        Debug.LogError(string.Format("{0} {1}", header, message));
    }

    // Hàm bổ trợ để tạo phần đầu Log [Class::Method]
    private static string GetHeader(string filePath, string methodName)
    {
        try
        {
            string className = Path.GetFileNameWithoutExtension(filePath);
            return string.Format("[{0}::{1}]", className, methodName);
        }
        catch
        {
            return "[Unknown]";
        }
    }
}

