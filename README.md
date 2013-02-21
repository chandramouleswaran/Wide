# Wide

### What is this?

Wide is a participatory open IDE framework built using WPF for building IDE-like applications (eg: Visual Studio, Eclipse etc.) It is built using variety of open source projects:

* [AvalonDock](http://avalondock.codeplex.com)
* [Prism](http://compositewpf.codeplex.com/)
* [MahApps Metro](https://github.com/MahApps/MahApps.Metro)
* [AvalonEdit](https://github.com/icsharpcode/SharpDevelop/wiki/AvalonEdit)
* [Unity Container](http://msdn.microsoft.com/en-us/library/ff660899\(v=pandp.20\).aspx)

In this process of building Wide, I was able to look at various open source projects, learn and most importantly contribute back to the project.

![Screenshot](https://raw.github.com/wiki/chandramouleswaran/Wide/Wide.png)

### What does it do?

Wide framework allows you to build your IDE application by creating PRISM modules which participate in building the IDE. This way you can modularize the code for each part of your application. 

Modules in this case are PRISM modules. You do NOT need to know PRISM to understand/use the framework. Check out the sample for more details.

For full details, look at the [demo program](https://tobelinkedsoon).

### What modules are built-in?

Wide comes with two modules:

* Core module (Required)
* Logger module (For the logging tool)

### What projects use Wide?

I am planning on using Wide for my own projects:

* [Hypertest](https://github.com/chandramouleswaran/Hypertest)
* coming soon...