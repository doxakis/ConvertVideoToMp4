using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConvertVideoToMp4
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.Write("Path: ");
			var folder = Console.ReadLine();
			if (!Directory.Exists(folder))
			{
				ExitWithErrorMessage("Directory not found");
				return;
			}

			Console.Write("Thread count: ");
			var threadCountStr = Console.ReadLine();
			if (!int.TryParse(threadCountStr, out int threadCount) || threadCount < 1 || threadCount > Environment.ProcessorCount)
			{
				ExitWithErrorMessage("Expecting a value between 1 and " + Environment.ProcessorCount);
				return;
			}

			var files = Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories);
			var supportedFilesExt = new string[] { ".avi", ".mpg", ".mkv", ".ts", ".divx" };
			Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, file =>
			{
				var ext = Path.GetExtension(file).ToLower();
				if (supportedFilesExt.Contains(ext))
				{
					var nearMp4File = file.Replace(ext, ".mp4");
					var alreadyConverted = File.Exists(nearMp4File);

					if (!alreadyConverted)
					{
						Console.WriteLine(nearMp4File);

						Process.Start(new ProcessStartInfo
						{
							WindowStyle = ProcessWindowStyle.Normal,
							UseShellExecute = false,
							FileName = "cmd.exe",
							Arguments = "/c ffmpeg.exe -i \"" + file + "\" -vcodec h264 -acodec aac -strict -2 \"" + nearMp4File + "\""
						}).WaitForExit();
					}
				}
			});

			Console.WriteLine("Conversion completed.");
			Console.ReadLine();
        }

		private static void ExitWithErrorMessage(string msg)
		{
			Console.WriteLine(msg);
			Console.ReadLine();
			Environment.Exit(1);
		}
	}
}
