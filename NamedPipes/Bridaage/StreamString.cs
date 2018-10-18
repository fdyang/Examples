using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Defines the data protocol for reading and writing strings on our stream.
    /// </summary>
    public class StreamString
    {
        /// <summary>
        /// The io stream.
        /// </summary>
        private Stream streamIO;

        /// <summary>
        /// The stream encoding.
        /// </summary>
        private UnicodeEncoding streamEncoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamString"/> class.
        /// </summary>
        /// <param name="streamIO">The IO stream.</param>
        public StreamString(Stream streamIO)
        {
            this.streamIO = streamIO;
            this.streamEncoding = new UnicodeEncoding();
        }

        /// <summary>
        /// Reads the string.
        /// </summary>
        /// <returns>The string read.</returns>
        public string ReadString()
        {
            int len = 0;
            len = this.streamIO.ReadByte() * 256;

            if (len == -256)
            {
                return null;
            }

            len += this.streamIO.ReadByte();
            byte[] inBuffer = new byte[len];
            this.streamIO.Read(inBuffer, 0, len);

            return this.streamEncoding.GetString(inBuffer);
        }

        /// <summary>
        /// Writes the string.
        /// </summary>
        /// <param name="outString">The out string.</param>
        /// <returns>the result of writing string.</returns>
        public int WriteString(string outString)
        {
            byte[] outBuffer = this.streamEncoding.GetBytes(outString);
            int len = outBuffer.Length;
            if (len > ushort.MaxValue)
            {
                len = (int)ushort.MaxValue;
            }

            this.streamIO.WriteByte((byte)(len / 256));
            this.streamIO.WriteByte((byte)(len & 255));
            this.streamIO.Write(outBuffer, 0, len);
            this.streamIO.Flush();

            return outBuffer.Length + 2;
        }
    }
}
