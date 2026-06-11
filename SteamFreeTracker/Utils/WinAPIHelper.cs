using System.Runtime.InteropServices;

namespace SteamFreeTracker.Utils
{
    public static class WinAPIHelper
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        /// <summary>
        /// 获取任务栏的真实高度
        /// </summary>
        public static int GetTaskbarHeight()
        {
            IntPtr taskbarHandle = FindWindow("Shell_TrayWnd", null);

            if (taskbarHandle != IntPtr.Zero && GetWindowRect(taskbarHandle, out RECT rect))
            {
                return rect.Bottom - rect.Top;
            }

            return 0;
        }
    }
}
