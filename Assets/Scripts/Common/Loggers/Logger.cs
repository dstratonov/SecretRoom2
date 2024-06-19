using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Common.Loggers
{
    public static class Logger
    {
        [Conditional("ENABLE_LOGS")] [Conditional("UNITY_EDITOR")]
        public static void Error<T>(object message, UnityEngine.Object context = null) =>
            Error(typeof(T), message, context);

        [Conditional("ENABLE_LOGS")] [Conditional("UNITY_EDITOR")]
        public static void Error(this object obj, object message, UnityEngine.Object context = null) =>
            Error(obj.GetType(), message, context);

        [Conditional("ENABLE_LOGS")] [Conditional("UNITY_EDITOR")]
        public static void Log<T>(object message, UnityEngine.Object context = null) =>
            Log(typeof(T), message, context);

        [Conditional("ENABLE_LOGS")] [Conditional("UNITY_EDITOR")]
        public static void Log(this object obj, object message, UnityEngine.Object context = null) =>
            Log(obj.GetType(), message, context);

        [Conditional("ENABLE_LOGS")] [Conditional("UNITY_EDITOR")]
        public static void Warning<T>(object message, UnityEngine.Object context = null) =>
            Warning(typeof(T), message, context);

        [Conditional("ENABLE_LOGS")] [Conditional("UNITY_EDITOR")]
        public static void Warning(this object obj, object message, UnityEngine.Object context = null) =>
            Warning(obj.GetType(), message, context);
        
        [Conditional("ENABLE_LOGS")]
        private static void Error(Type type, object message, UnityEngine.Object context) =>
            Debug.LogError(CreateMessage(type, message), context);

        [Conditional("ENABLE_LOGS")]
        private static void Log(Type type, object message, UnityEngine.Object context = null) =>
            Debug.Log(CreateMessage(type, message), context);

        [Conditional("ENABLE_LOGS")]
        private static void Warning(Type type, object message, UnityEngine.Object context = null) =>
            Debug.LogWarning(CreateMessage(type, message), context);
        
        private static string CreateMessage(Type type, object message)
        {
            var str = $"[{type.Name}] {message}";
            if (Application.isEditor)
            {
                return $"[{Time.frameCount.ToString()}] {str}";
            }

            return $"[{DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss")} | {Time.frameCount.ToString()}] {str}";
        }
    }
}