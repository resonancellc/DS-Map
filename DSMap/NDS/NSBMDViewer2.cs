using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

//using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace DSMap.NDS
{
    /*public class NSBMDViewer2 : OpenTK.GLControl
    {
        private bool _modelSet = false, _texSet = false;
        private Model model;
        private NSBTX textures;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Context.MakeCurrent(WindowInfo);
            GL.Viewport(0, 0, this.Width, this.Height);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, this.Width, this.Height);
        }

        public void SetModel(Model model)
        {
            this.model = model;
            _modelSet = true;
        }

        public void SetTextures(NSBTX textures)
        {
            this.textures = textures;
            _texSet = true;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            GL.ClearColor(0.2f, 0.2f, 0.2f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // TODO
            DrawEdges();

            GL.Flush();
            this.SwapBuffers();
        }

        private void DrawEdges()
        {
            GL.Begin(BeginMode.Lines);
            {
                GL.Color3(Color.DarkRed);
                GL.Vertex3(0f, 0f, 0f);
                GL.Vertex3(100f, 0f, 0f);

                GL.Color3(Color.Blue);
                GL.Vertex3(0f, 0f, 0f);
                GL.Vertex3(0f, 100f, 0f);

                GL.Color3(Color.Green);
                GL.Vertex3(0f, 0f, 0f);
                GL.Vertex3(0f, 0f, 100f);
            }
            GL.End();
            GL.Flush();
        }
    }*/
}
