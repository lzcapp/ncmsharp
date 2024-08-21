using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;

namespace ncmdump_lib {
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public partial class NeteaseCrypt {
        private const string DllPath = "libncmdump.dll";

        [LibraryImport(DllPath, EntryPoint = "CreateNeteaseCrypt", StringMarshalling = StringMarshalling.Utf8)]
        private static partial IntPtr CreateNeteaseCrypt(IntPtr path);

        [LibraryImport(DllPath, EntryPoint = "Dump")]
        private static partial int Dump(IntPtr NeteaseCrypt);

        [LibraryImport(DllPath, EntryPoint = "FixMetadata")]
        private static partial void FixMetadata(IntPtr NeteaseCrypt);

        [LibraryImport(DllPath, EntryPoint = "DestroyNeteaseCrypt")]
        private static partial void DestroyNeteaseCrypt(IntPtr NeteaseCrypt);

        private readonly IntPtr NeteaseCryptClass;

        /// <summary>
        /// 创建 NeteaseCrypt 类的实例。
        /// </summary>
        /// <param name="fileName">网易云音乐 ncm 加密文件路径</param>
        public NeteaseCrypt(string fileName) {
            var bytes = Encoding.UTF8.GetBytes(fileName);

            var inputPtr = Marshal.AllocHGlobal(bytes.Length + 1);
            Marshal.Copy(bytes, 0, inputPtr, bytes.Length);
            Marshal.WriteByte(inputPtr, bytes.Length, 0);

            NeteaseCryptClass = CreateNeteaseCrypt(inputPtr);
        }

        /// <summary>
        /// 启动转换过程。
        /// </summary>
        /// <returns>返回一个整数，指示转储过程的结果。如果成功，返回true；如果失败，返回false。</returns>
        public bool Dump() {
            return Dump(NeteaseCryptClass) == 0;
        }

        /// <summary>
        /// 修复音乐文件元数据。
        /// </summary>
        public void FixMetadata() {
            FixMetadata(NeteaseCryptClass);
        }

        /// <summary>
        /// 销毁 NeteaseCrypt 类的实例。
        /// </summary>
        public void Destroy() {
            DestroyNeteaseCrypt(NeteaseCryptClass);
        }
    }
}
