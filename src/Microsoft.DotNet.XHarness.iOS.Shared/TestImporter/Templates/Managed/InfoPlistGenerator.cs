using System;
using System.IO;
using System.Threading.Tasks;

namespace Xharness.TestImporter.Templates.Managed {
	/// <summary>
	/// Class that knows how to generate the plist of a test project.
	/// </summary>
	public class InfoPlistGenerator {
		public static string ApplicationNameReplacement = "%APPLICATION NAME%";
		public static string IndentifierReplacement = "%BUNDLE INDENTIFIER%";
		public static string WatchAppIndentifierReplacement = "%WATCHAPP INDENTIFIER%";
	
		public static async Task<string> GenerateCodeAsync (Stream template, string projectName)
		{
			if (template == null)
				throw new ArgumentNullException (nameof (template));
			if (projectName == null)
				throw new ArgumentNullException (nameof (projectName));
			// got the lines we want to add, read the template and substitute
			using (var reader = new StreamReader(template)) {
				var result = await reader.ReadToEndAsync ();
				result = result.Replace (ApplicationNameReplacement, projectName);
				result = result.Replace (IndentifierReplacement, $"com.xamarin.bcltests.{projectName}");
				result = result.Replace (WatchAppIndentifierReplacement, $"com.xamarin.bcltests.{projectName}.container");
				return result;
			}
		}

		// Generates the code for the type registration using the give path to the template to use
		public static string GenerateCode (Stream template, string projectName) => GenerateCodeAsync (template, projectName).Result;
	}
}
