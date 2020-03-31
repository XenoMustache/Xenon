using OpenTK.Graphics.OpenGL4;
using Xenon.Client.Graphics;
using Xenon.Common.State;

namespace Template.States {
	public class Textures : GameState {
		private readonly float[] vertices = {
			 0.5f,  0.5f, 0.0f, 1.0f, 1.0f,
			 0.5f, -0.5f, 0.0f, 1.0f, 0.0f,
			-0.5f, -0.5f, 0.0f, 0.0f, 0.0f,
			-0.5f,  0.5f, 0.0f, 0.0f, 1.0f
		};

		private readonly uint[] indices = {
			0, 1, 3,
			1, 2, 3
		};

		private int elementBufferObject, vertexBufferObject, vertexArrayObject;
		private Shader shader;
		private Texture texture;

		public override void Load() {
			GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

			vertexBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
			GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

			elementBufferObject = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
			GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

			shader = new Shader("Shaders/texture.vert", "Shaders/texture.frag");
			shader.Use();

			texture = new Texture("Resources/container.png");
			texture.Use();

			vertexArrayObject = GL.GenVertexArray();
			GL.BindVertexArray(vertexArrayObject);

			GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);

			var vertexLocation = shader.GetAttribLocation("aPosition");
			GL.EnableVertexAttribArray(vertexLocation);
			GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

			var texCoordLocation = shader.GetAttribLocation("aTexCoord");
			GL.EnableVertexAttribArray(texCoordLocation);
			GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
		}

		public override void Render() {
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.BindVertexArray(vertexArrayObject);

			texture.Use();
			shader.Use();

			GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
		}

		public override void Dispose() {
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindVertexArray(0);
			GL.UseProgram(0);

			GL.DeleteBuffer(vertexBufferObject);
			GL.DeleteVertexArray(vertexArrayObject);

			if (shader != null) GL.DeleteProgram(shader.handle);
			if (texture != null) GL.DeleteTexture(texture.handle);

			base.Dispose();
		}
	}
}
