using System;
using System.Threading.Tasks;

namespace ncmsharp_cli {
    internal class Program {
        static void Main(string[] args) {
            // 文件名
            string filePath = "test.ncm";

            // 创建 NeteaseCrypt 类的实例
            Cryption cryption = new Cryption(filePath);

            // 启动转换过程
            int result = cryption.Dump();

            // 修复元数据
            cryption.FixMetadata();

            // [务必]销毁 NeteaseCrypt 类的实例
            cryption.Destroy();
        }
    }
}