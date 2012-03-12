// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace O2.Platform
{
	public class Program
	{
		public static void Main()//string[] args)
		{			
			new Install_O2().start_Install_Workflow();
			
			System.Windows.Forms.MessageBox.Show("Thanks for Installing O2 .... now run 'Start O2.Bat'");
		}
	}
	
    public class Install_O2
    {       
    	public string 			InstallFiles { get;set;}    	
    	public Action<string>   LogMessage   { get;set;}
    	
		public Install_O2()
		{
			//setting default values
			InstallFiles = @"C:\_WorkDir\Git_O2OPlatform\_O2_Platform_Source_Code\O2 Installer\Install Files";			
			
			LogMessage = (message) => Console.WriteLine("> " + message);
			Misc_Helper_ExtensionMethods.LogMessage = LogMessage;
		//	Console.ReadLine();		
		}
		
		public Install_O2 start_Install_Workflow()
		{
			LogMessage("");
			LogMessage("***********************");
			LogMessage("Welcome to O2 Installer");
			LogMessage("");
			
			loadNGitDlls();
			compileAndExecute_GitHub_Actions();
			
			LogMessage("");
			LogMessage("O2 Install Ended");
//			LogMessage("");
			return this;
		}
		
		public void loadNGitDlls()
		{
			LogMessage(" .. loading NGit Dlls");	
			"Sharpen.dll".loadDll(InstallFiles);
			"NSch.dll".loadDll(InstallFiles);
			"Mono.Security.dll".loadDll(InstallFiles);
			"NGit.dll".loadDll(InstallFiles);
		}
		
		public void compileAndExecute_GitHub_Actions()
		{
			LogMessage(" .. compiling GitHub_Actions.cs");	
			var script = @"C:\_WorkDir\Git_O2OPlatform\_O2_Platform_Source_Code\O2 Installer\Install Files\GitHub_Actions.cs";
			//var nGit_Assembly = "NGit.dll".loadDll(InstallFiles);


			//var file2 = @"C:\_WorkDir\Git_O2OPlatform\_O2_Platform_Source_Code\O2 Installer\Install Files\GitHub_Actions.cs";
			
			var codeProvider = CodeDomProvider.CreateProvider("CSharp");
			
			var compilerParameters = new CompilerParameters()
					{
						//OutputAssembly = targetFile
					}; 
			compilerParameters.ReferencedAssemblies.Add("NGit.dll".loadDll(InstallFiles).Location);
			compilerParameters.ReferencedAssemblies.Add("Sharpen.dll".loadDll(InstallFiles).Location);
			
			var compilerResults = codeProvider.CompileAssemblyFromFile(compilerParameters, script);
			
			compilerResults.CompiledAssembly.GetType("O2.Platform.GitHub_Actions").GetMethod("start").Invoke(null,null);
			
		}
			
		
		
						
		
						
    }		
    
    public static class Misc_Helper_ExtensionMethods
    {
    	public static Action<string>   LogMessage   { get;set;}
    	
    	public static Assembly loadDll(this string dll, string path)
    	{
    		return Path.Combine(path, dll).loadDll();
    	}
    	
    	public static Assembly loadDll(this string pathToDll)
		{
			try
			{
				var assembly = Assembly.LoadFrom(pathToDll);
				LogMessage("       Assembly loaded ok: " + Path.GetFileName(pathToDll));
				return assembly;
			}
			catch
			{
				LogMessage("ERROR: failed to load assembly: " + pathToDll);
				return null;
			}
		}
    }
}
