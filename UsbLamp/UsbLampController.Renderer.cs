using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using HIDLibrary;
using CherryTomato.Core;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.UsbLamp
{
    public partial class UsbLampController
    {
        private Renderer renderer;

        public class Renderer
        {
            private UsbLampController controller;

            private HidDevice hidDevice;
            private Boolean dreamCheeky = true;

            private Color idleColor = Color.Black;
            private Color pomodoroColor = Color.Red;

            public Renderer(UsbLampController c)
            {
                this.controller = c;
            }

            public void Initialize()
            {
                HIDLibrary.HidDevice[] hidDeviceList = HidDevices.Enumerate(0x1D34, 0x0004);
                if (hidDeviceList.Length == 0)
                {
                    hidDeviceList = HidDevices.Enumerate(0x1294, 0x1320);
                    if (hidDeviceList.Length > 0)
                    {
                        this.dreamCheeky = false;
                    }
                }

                if (hidDeviceList.Length > 0)
                {
                    this.hidDevice = hidDeviceList[0];
                    this.hidDevice.Open();
                    while (!this.hidDevice.IsConnected || !this.hidDevice.IsOpen) { }
                    if (this.dreamCheeky)
                    {
                        this.hidDevice.Write(new byte[9] { 0x00, 0x1F, 0x01, 0x29, 0x00, 0xB8, 0x54, 0x2C, 0x03 });
                        this.hidDevice.Write(new byte[9] { 0x00, 0x00, 0x01, 0x29, 0x00, 0xB8, 0x54, 0x2C, 0x04 });
                    }
                }
            }

            public void SetColorType(ColorType colorType, Color specialColor = default(Color))
            {
                if (this.hidDevice == null)
                {
                    return;
                }

                if (colorType == ColorType.Special)
                {
                    if (specialColor == default(Color))
                    {
                        specialColor = this.idleColor;
                    }

                    this.SetColor(specialColor);
                }
                else if (colorType == ColorType.Idle)
                {
                    this.SetColor(this.idleColor);
                }
                else if (colorType == ColorType.Pomodoro)
                {
                    this.SetColor(this.pomodoroColor);
                }
                else
                {
                    throw new PluginException("Usb Lamp Controller internal error.");
                }
            }

            public void SetColor(Color color)
            {
                var r = color.R;
                var g = color.G;
                var b = color.B;

                if (this.hidDevice != null)
                {
                    if (this.dreamCheeky)
                    {
                        this.hidDevice.Write(new byte[9] { 0x00, r, g, b, 0x00, 0x00, 0x54, 0x2C, 0x05 });
                    }
                    else
                    {
                        if (r == 0x00 && g == 0x00 && b == 0x00)
                        {
                            this.hidDevice.Write(new byte[6] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                        }
                        if (r > 0x00 && g == 0x00 && b == 0x00)
                        {
                            this.hidDevice.Write(new byte[6] { 0x00, 0x02, 0x00, 0x00, 0x00, 0x00 });
                        }
                        if (r == 0x00 && g > 0x00 && b == 0x00)
                        {
                            this.hidDevice.Write(new byte[6] { 0x00, 0x01, 0x00, 0x00, 0x00, 0x00 });
                        }
                        if (r == 0x00 && g == 0x00 && b > 0x00)
                        {
                            this.hidDevice.Write(new byte[6] { 0x00, 0x03, 0x00, 0x00, 0x00, 0x00 });
                        }
                        if (r > 0x00 && g > 0x00 && b == 0x00)
                        {
                            this.hidDevice.Write(new byte[6] { 0x00, 0x05, 0x00, 0x00, 0x00, 0x00 });
                        }
                        if (r > 0x00 && g == 0x00 && b > 0x00)
                        {
                            this.hidDevice.Write(new byte[6] { 0x00, 0x06, 0x00, 0x00, 0x00, 0x00 });
                        }
                        if (r == 0x00 && g > 0x00 && b > 0x00)
                        {
                            this.hidDevice.Write(new byte[6] { 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 });
                        }
                        if (r > 0x00 && g > 0x00 && b > 0x00)
                        {
                            this.hidDevice.Write(new byte[6] { 0x00, 0x07, 0x00, 0x00, 0x00, 0x00 });
                        }
                    }
                }
            }
        }
    }
}
