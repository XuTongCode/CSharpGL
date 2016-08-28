﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CSharpGL
{
    /// <summary>
    /// Create, update, use and delete a framebuffer object.
    /// </summary>
    public partial class NewFramebuffer : IDisposable
    {
        private static OpenGL.glGenFramebuffersEXT glGenFramebuffers;
        private static OpenGL.glBindFramebufferEXT glBindFramebuffer;
        private static OpenGL.glFramebufferTexture2DEXT glFramebufferTexture2D;
        private static OpenGL.glCheckFramebufferStatusEXT glCheckFramebufferStatus;
        private static OpenGL.glDeleteFramebuffersEXT glDeleteFramebuffers;
        private static OpenGL.glDrawBuffers glDrawBuffers;

        uint[] frameBuffer = new uint[1];
        /// <summary>
        /// Framebuffer Id.
        /// </summary>
        public uint Id { get { return frameBuffer[0]; } }

        /// <summary>
        /// Create, update, use and delete a framebuffer object.
        /// </summary>
        public NewFramebuffer()
        {
            if (glGenFramebuffers == null)
            {
                glGenFramebuffers = OpenGL.GetDelegateFor<OpenGL.glGenFramebuffersEXT>();
                glBindFramebuffer = OpenGL.GetDelegateFor<OpenGL.glBindFramebufferEXT>();
                glFramebufferTexture2D = OpenGL.GetDelegateFor<OpenGL.glFramebufferTexture2DEXT>();
                glCheckFramebufferStatus = OpenGL.GetDelegateFor<OpenGL.glCheckFramebufferStatusEXT>();
                glDeleteFramebuffers = OpenGL.GetDelegateFor<OpenGL.glDeleteFramebuffersEXT>();
                glDrawBuffers = OpenGL.GetDelegateFor<OpenGL.glDrawBuffers>();
            }

            glGenFramebuffers(1, frameBuffer);
        }

        /// <summary>
        /// check completeness.
        /// </summary>
        /// <returns></returns>
        public bool CheckCompleteness()
        {
            uint result = glCheckFramebufferStatus(OpenGL.GL_FRAMEBUFFER);

            if (result != OpenGL.GL_FRAMEBUFFER_COMPLETE)
            {
                throw new Exception("Failed to create frame buffer object!");
            }

            return true;
        }

        /// <summary>
        /// start to use this framebuffer.
        /// </summary>
        public void Bind()
        {
            glBindFramebuffer(OpenGL.GL_FRAMEBUFFER, this.Id);
        }

        /// <summary>
        /// stop to use this framebuffer.
        /// </summary>
        public void Unbind()
        {
            glBindFramebuffer(OpenGL.GL_FRAMEBUFFER, 0);
        }
    }
}