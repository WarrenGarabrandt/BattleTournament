using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

namespace BattleTournament
{
    public partial class Form1 : Form
    {
        GameLoop _gameLoop = null;
        KeyboardState KeyboardState = new KeyboardState();
        MouseState MouseState = new MouseState();

        #region
        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr handle;
            public uint msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public Point p;
        }

        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool PeekMessage(out NativeMessage message, IntPtr hWnd, uint filterMin, uint filterMax, uint flags);
        #endregion
        public void OnApplicationIdle(object sender, EventArgs e)
        {
            if (_gameLoop != null)
            {
                // we'll pay the 50ns lock time every time we resume the game loop
                // to avoid race conditions with the form's mouse move event
                lock (MouseState)
                {
                    while (AppStillIdle)
                    {
                        if (_gameLoop.Update(KeyboardState, MouseState))
                        {
                            using (var g = this.CreateGraphics())
                            {
                                this.Invalidate();
                                //_gameLoop.Draw(g);
                            }
                        }
                    }
                }
            }
        }

        private bool AppStillIdle
        {
            get
            {
                NativeMessage msg = new NativeMessage();
                return !PeekMessage(out msg, IntPtr.Zero, (uint)0, (uint)0, (uint)0);
            }
        }

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | 
                ControlStyles.UserPaint | 
                ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.ResizeRedraw, true);   
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            return;
            //gameLoop.Draw(e.Graphics);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _gameLoop.Draw(e.Graphics);
            return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Rectangle resolution = this.ClientRectangle;

            Game myGame = new Game();
            _gameLoop = new GameLoop();
            _gameLoop.SetResolution(resolution);
            _gameLoop.Load(myGame);
            _gameLoop.Start(KeyboardState, MouseState);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            lock (MouseState)
            {
                if (MouseState.ChangeMouseMode)
                {
                    MouseState.ChangeMouseMode = false;
                    if (MouseState.MouseMode == MouseState.MouseModes.Delta)
                    {
                        Point mouseCenter = new Point(this.ClientRectangle.Width / 2, this.ClientRectangle.Height / 2);
                        MouseState.Location = mouseCenter;
                        MouseState.dX = 0;
                        MouseState.dY = 0;
                        Cursor.Position = mouseCenter;
                    }
                }
                else
                {
                    if (MouseState.MouseMode == MouseState.MouseModes.RelativeCoords)
                    {
                        MouseState.Location = e.Location;
                    }
                    else if (MouseState.MouseMode == MouseState.MouseModes.Delta)
                    {
                        MouseState.dX += e.X - MouseState.Location.X;
                        MouseState.dY += e.Y - MouseState.Location.Y;
                        if (MouseState.Location.X != e.X || MouseState.Location.Y != e.Y)
                        {
                            Cursor.Position = MouseState.Location;
                        }
                    }
                }

                MouseState.Buttons = e.Button;
                MouseState.Clicks += e.Clicks;
                MouseState.WheelTicks += e.Delta;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (_gameLoop != null)
            {
                _gameLoop.SetResolution(this.ClientRectangle);
                MouseState.ChangeMouseMode = true;
            }
        }
    }
}
