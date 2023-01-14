using System;
using System.Globalization;
using System.IO;

namespace BackToTheFutureV
{
	public class Logger
	{
		private static readonly string LogLocation = "scripts/BackToTheFutureV/bttfv.log";
		private static readonly string OldLogLocation = "scripts/BackToTheFutureV/bttfv_old.log";

		public static void LogCheck()
		{
			if (File.Exists(OldLogLocation))
				File.Delete(OldLogLocation);

			if (File.Exists(LogLocation))
				File.Move(LogLocation, OldLogLocation);
		}
		
		public static void Log(string str) => LogI(str);
		// Because someone will attempt to use Log instead of the proper functions.
		// Moron proof software is always annoying.
		
		// Line Feed is the only one that matters :)
		// To whoever is using an ancient system that cannot understand Line Feed, and instead relies on Carriage Return,
		// I am afraid I do not care. Update your system or obtain an application capable of understanding Line Feed.
		public static void LogI(string str) => File.AppendAllText(LogLocation, $"[{DateTime.Now}] INFO: {str}\n");
		public static void LogT(string str) => File.AppendAllText(LogLocation, $"[{DateTime.Now}] TRACE: {str}\n");
		public static void LogD(string str) => File.AppendAllText(LogLocation, $"[{DateTime.Now}] DEBUG: {str}\n");
		private static void LogR(string str) => File.AppendAllText(LogLocation, str + "\n");

		public static void InitSystem()
		{
			LogR($"Machine Name: {Environment.MachineName}\n" +
			        $"CPU Type: {Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER")}\n" +
			        $"CPU Count: {Environment.ProcessorCount}\n" +
			        $"TBD\n" +
			        $"TBD\n");
		}
	}
}