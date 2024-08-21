using System.Text;
using ncmdump_lib;

// ReSharper disable once IdentifierTypo
namespace ncmsharp_cli {
    internal static class Program {
        private static void Main(string[] args) {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Copyright \u00a9 2024 RainySummer, All Rights Reserved.");

            var list = new List<string>();

            if (args.Length == 0) {
                string path;
                do {
                    Console.Write(">> ");
                    path = Console.ReadLine() ?? string.Empty;
                } while (string.IsNullOrWhiteSpace(path));
                list.Add(path);
            } else {
                list.AddRange(args);
            }

            if (list.Count == 0) {
                return;
            }

            try {
                char[] trimChars = [
                    '"', '\'', ' '
                ];

                var fileList = new List<string>();

                for (var i = 0; i < list.Count; i++) {
                    list[i] = list[i].TrimStart(trimChars).TrimEnd(trimChars).Trim();
                    var filePath = list[i];
                    if ((File.GetAttributes(filePath) & FileAttributes.Directory) != FileAttributes.Directory) continue;
                    DirectoryInfo directoryInfo = new(filePath);
                    fileList.AddRange(GetFiles(directoryInfo));
                }

                if (fileList.Count > 0) {
                    var totalCount = fileList.Count;
                    Console.WriteLine("Totally " + totalCount + " Files.");
                    var (_, top) = Console.GetCursorPosition();
                    for (var index = 0; index < totalCount; index++) {
                        var filePath = fileList[index];
                        var status = ProcessFile(filePath);
                        Console.SetCursorPosition(0, top);
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, top);
                        var percentage = index == 0 ? 0 : (double)index / totalCount * 100;
                        if (index < totalCount - 1) {
                            Console.Write("[" + percentage.ToString("F2") + "%] " + filePath + " " + (status ? "√" : "×"));
                        } else {
                            Console.Write("[100%] " + (status ? "√" : "×"));
                        }
                    }
                }
            } catch (Exception) {
                // ignored
            }

            Console.WriteLine();
            Console.WriteLine("Press Any Key To Exit...");
            Console.ReadKey();
        }

        private static List<string> GetFiles(DirectoryInfo dirInfo) {
            if (dirInfo is not { Exists: true }) {
                throw new DirectoryNotFoundException($"The directory '{dirInfo.FullName}' does not exist.");
            }
            List<string> fileList = [];
            var fsInfos = dirInfo.GetFileSystemInfos();
            foreach (var fsInfo in fsInfos) {
                if (fsInfo is DirectoryInfo subDirInfo) {
                    fileList.AddRange(GetFiles(subDirInfo));
                } else {
                    if (fsInfo.Extension.Equals(".ncm", StringComparison.OrdinalIgnoreCase)) {
                        fileList.Add(fsInfo.FullName);
                    }
                }
            }
            return fileList;
        }

        private static bool ProcessFile(string filePath) {
            try {
                var neteaseCrypt = new NeteaseCrypt(filePath);
                var result = neteaseCrypt.Dump();
                if (result) {
                    neteaseCrypt.FixMetadata();
                }
                neteaseCrypt.Destroy();
                return true;
            } catch (Exception) {
                return false;
            }
        }
    }
}