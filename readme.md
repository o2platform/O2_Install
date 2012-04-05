# O2Platform 

#Install instuctions:

To install do git clone of this repository or [download the zip file](https://github.com/o2platform/O2_Install/zipball/master)

You will see an Install_O2.bat script which when executed will:

* Do a git clone of these 3 repositories: [O2.FluentSharp](https://github.com/o2platform/O2.FluentSharp) , [O2.Platform.Projects](https://github.com/o2platform/O2.Platform.Projects) , [O2.Platform.Scripts](https://github.com/o2platform/O2.Platform.Scripts)
* Compile the C# projects using MSBuild (you don't need VisualStudio installed)
* Start the "O2 Platform.exe" file which if the compilation was sucessfull, will exist in the O2.Platform.Projects\binaries folder

Note that you DON'T need to have Windows GIT installed on the Box/VM you try this. I added support for the amazing NGIT (https://github.com/mono/ngit) project to O2, so now O2 has native (C#) support for GIT repositories :)

#Scripts examples
There are lots of script samples at the [O2 Platform blog](http://o2platform.wordpress.com)

#O2 Website

see also the [o2platform.com](http://o2platform.com) website