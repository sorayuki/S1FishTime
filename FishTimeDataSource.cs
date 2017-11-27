using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace FishTime
{
    class FishData
    {
        public string WindowTitle;
        public string ProcessName;
        public DateTime TimeStamp;

        public override string ToString() 
        {
            return string.Format("{1} ({0}) / {2}", WindowTitle, ProcessName, TimeStamp);
        }
    }

    class FishDataSource
    {
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        extern static IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        extern static UInt32 GetWindowThreadProcessId(IntPtr hwnd, out UInt32 processId);

        [DllImport("user32.dll", EntryPoint = "GetWindowTextW", CallingConvention = CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Unicode)]
        extern static int GetWindowText(IntPtr hwnd, [MarshalAs(UnmanagedType.LPArray)] char[] lpString, int nMaxCount);

        public FishDataSource()
        {
        }

        public FishData RetrieveFishData()
        {
            FishData result = new FishData();
            IntPtr foreWin = GetForegroundWindow();
            if (foreWin == IntPtr.Zero)
                return null;

            UInt32 procId = 0;
            GetWindowThreadProcessId(foreWin, out procId);
            if (procId == 0)
                return null;

            using (Process proc = Process.GetProcessById((int)procId))
            {
                result.ProcessName = Path.GetFileNameWithoutExtension(proc.ProcessName);
            }

            char[] buf = new char[512];
            int len = GetWindowText(foreWin, buf, 512);

            result.TimeStamp = DateTime.Now;
                
            if (len > 0)
                result.WindowTitle = new string(buf, 0, len);
            else
                result.WindowTitle = "";
            return result;
        }
    }
}
