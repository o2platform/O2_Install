// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using NGit.Api;
//O2Ref:C:\_WorkDir\Git_O2OPlatform\_O2_Platform_Source_Code\O2 Installer\Install Files\Ngit.dll
//O2Ref:C:\_WorkDir\Git_O2OPlatform\_O2_Platform_Source_Code\O2 Installer\Install Files\Sharpen.dll

namespace O2.Platform
{  
	public class GitHub_Actions
	{
		public static string 			TargetDir 		{ get; set; }
		public static string 			GitUrl_Template { get; set; }
		public static Action<string>   	LogMessage   	{ get;set;}
		
		
		static GitHub_Actions()
		{
			LogMessage = (message) => Console.WriteLine("* " + message);
			TargetDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\GitHub.Repositories"));
			GitUrl_Template = "git://github.com/o2platform/{0}.git";
		}
		
		public static void start()
		{
		
			//var tempDir = @"C:\E_Drive\O2_Tests\O2_Launcher\_O2_TempDir\3-11-2012\_O2_Installer\Git_Clone_";
			
			
			LogMessage(" in GitHub_Actions");
						
			//LogMessage("git : " + git.GetRepository());
		//	CloneRepository(tempDir, gitUri);
			CloneRepository("Scripts-by-O2-Users");
			CloneRepository("O2-Platform-Scripts");
			//CloneRepository("O2_Platform_ReferencedAssemblies");
			
			//LogMessage("DIR: " + TargetDir);
		
		}
		
		public static void CloneRepository(string repositoryName )
		{
			Git git = null;
			var repositoryUrl	= String.Format(GitUrl_Template	, repositoryName);
			var repositoryPath 	= Path.Combine (TargetDir		, repositoryName);
			
			if (Directory.Exists(repositoryPath))
			{
				git = Git.Open(repositoryPath);
			}
			else
			{
				LogMessage(" Cloning Repository: " + repositoryUrl);
				LogMessage("  into Folder: " + repositoryPath);
				
				var cloneCommand = Git.CloneRepository();
				
				cloneCommand.SetDirectory(repositoryPath);			
				cloneCommand.SetURI(repositoryUrl);
				
				LogMessage(" .. starting clone");
				git = cloneCommand.Call(); 
				
				LogMessage(" .. clone completed");
			}
			var repository = git.GetRepository();
			
			LogMessage("  local repository parh: " + repository);
		}
		
	}
}