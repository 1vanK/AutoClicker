using System;
using System.Drawing;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;

class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(MouseEventFlags dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

    [Flags]
    public enum MouseEventFlags : uint
    {
        None        = 0,
        LeftDown    = 0x00000002,
        LeftUp      = 0x00000004,
        MiddleDown  = 0x00000020,
        MiddleUp    = 0x00000040,
        Move        = 0x00000001,
        Absolute    = 0x00008000,
        RightDown   = 0x00000008,
        RightUp     = 0x00000010
    }

    // Функция жмет нужные кнопки мыши
    public static void Click(bool left, bool middle, bool right)
    {
        Point pos = Cursor.Position;

        MouseEventFlags flags = MouseEventFlags.None;
        if (left)
            flags |= MouseEventFlags.LeftDown | MouseEventFlags.LeftUp;
        if (middle)
            flags |= MouseEventFlags.MiddleDown | MouseEventFlags.MiddleUp;
        if (right)
            flags |= MouseEventFlags.RightDown | MouseEventFlags.RightUp;

        mouse_event(flags, (uint)pos.X, (uint)pos.Y, 0, 0);
    }

    static void Main(string[] args)
    {
        while (true)
        {
            if (Control.IsKeyLocked(Keys.NumLock))
                Click(true, true, true);

            Thread.Sleep(10); // Увеличьте задержку, если комп не справляется
        }
    }
}
