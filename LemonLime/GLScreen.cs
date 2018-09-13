﻿using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;

namespace LemonLime
{
    class GLScreen : GameWindow
    {
        LLE.CTR CTR = new LLE.CTR();

        public GLScreen()
            : base(400, 240, GraphicsMode.Default, "LemonLime Screen")
        {
            VSync = VSyncMode.On;

            CTR.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, 240, 400, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);

            GL.Rotate(90, 0, 0, 1);

            // GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)TextureMagFilter.Nearest });
            GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)TextureMinFilter.Nearest });

            GL.Enable(EnableCap.Texture2D);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
                Exit();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, 240, 400, PixelFormat.Bgr, PixelType.UnsignedByte, CTR.Memory.TopScreenVRAM);

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0.0, 1.0); GL.Vertex2(0.0, 0.0);
            GL.TexCoord2(1.0, 1.0); GL.Vertex2(1.0, 0.0);
            GL.TexCoord2(1.0, 0.0); GL.Vertex2(1.0, 1.0);
            GL.TexCoord2(0.0, 0.0); GL.Vertex2(0.0, 1.0);

            GL.End();

            SwapBuffers();
        }
    }
}