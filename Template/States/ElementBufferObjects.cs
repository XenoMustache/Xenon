using OpenTK.Graphics.OpenGL4;
using Xenon.Client.Graphics;
using Xenon.Common.State;

namespace Template.States {
	class ElementBufferObjects : GameState {
		private Shader shader;
		private int elementBufferObject;

		int vertexBufferObject, vertexArrayObject;

		private readonly float[] vertices = {
			 0.5f,  0.5f, 0.0f,
			 0.5f, -0.5f, 0.0f,
			-0.5f, -0.5f, 0.0f,
			-0.5f,  0.5f, 0.0f,
		};

		private readonly uint[] indices = {
			0, 1, 3,
			1, 2, 3
		};

		public override void Init() {
			GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

			vertexBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
			GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

			elementBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);

			GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

			shader = new Shader("Shaders/triangle.vert", "Shaders/triangle.frag");
			shader.Use();

			vertexArrayObject = GL.GenVertexArray();
			GL.BindVertexArray(vertexArrayObject);

			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);

			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
			GL.EnableVertexAttribArray(0);
		}

		public override void Render() {
			GL.Clear(ClearBufferMask.ColorBufferBit);

			shader.Use();

			GL.BindVertexArray(vertexArrayObject);

			GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
		}

		public override void Dispose() {
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
			GL.BindVertexArray(0);
			GL.UseProgram(0);

			GL.DeleteBuffer(vertexBufferObject);
			GL.DeleteBuffer(elementBufferObject);
			GL.DeleteVertexArray(vertexArrayObject);
			GL.DeleteProgram(shader.handle);

			base.Dispose();
		}
	}
}
