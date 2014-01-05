using System.Windows.Forms;

namespace CherryTomato.UserActivity
{
    public interface IUserActivityHook
    {
        /// <summary>
        /// Occurs when the user moves the mouse, presses any mouse button or scrolls the wheel
        /// </summary>
        event MouseEventHandler OnMouseActivity;

        /// <summary>
        /// Occurs when the user presses a key
        /// </summary>
        event KeyEventHandler KeyDown;

        /// <summary>
        /// Occurs when the user presses and releases 
        /// </summary>
        event KeyPressEventHandler KeyPress;

        /// <summary>
        /// Occurs when the user releases a key
        /// </summary>
        event KeyEventHandler KeyUp;

        /// <summary>
        /// Installs both mouse and keyboard hooks and starts rasing events
        /// </summary>
        /// <exception cref="Win32Exception">Any windows problem.</exception>
        void Start();

        /// <summary>
        /// Stops monitoring both mouse and keyboard events and rasing events.
        /// </summary>
        /// <exception cref="Win32Exception">Any windows problem.</exception>
        void Stop();
    }
}
