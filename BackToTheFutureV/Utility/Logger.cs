/* GTA V C# Logging Script, inspired by ranstar74's rageAm Logger.
 * 
 * MIT License
 * 
 * Copyright (c) 2023 of-ravens-claw
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/

using System;
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
		
		// Line Feed is the only one that matters :)
		// To whoever is using an ancient system that cannot understand Line Feed, and instead relies on Carriage Return,
		// I am afraid I do not care. Update your system or obtain an application capable of understanding Line Feed.
		public static void LogI(string str) => File.AppendAllText(LogLocation, $"[{DateTime.Now}] INFO: {str}\n");
		public static void LogT(string str) => File.AppendAllText(LogLocation, $"[{DateTime.Now}] TRACE: {str}\n");
		public static void LogD(string str) => File.AppendAllText(LogLocation, $"[{DateTime.Now}] DEBUG: {str}\n");
		private static void LogR(string str) => File.AppendAllText(LogLocation, str + "\n");

		public static void InitSystem()
		{
			/*LogR($"Machine Name: {Environment.MachineName}\n" +
			        $"CPU Type: {Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER")}\n" +
			        $"CPU Count: {Environment.ProcessorCount}\n" +
			        $"TBD\n" +
			        $"TBD\n");*/
		}
	}
}