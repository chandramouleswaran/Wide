# Wide

### What is this?

Wide is a participatory IDE framework built using WPF for building IDE-like applications (eg: Visual Studio, Eclipse etc.) 

![Screenshot](https://raw.github.com/wiki/chandramouleswaran/Wide/Wide.png)

### What does it do?

Wide framework allows you to build your application by creating PRISM modules which participate in building the IDE. This way you can modularize the code for each part of your application.

Wide comes with 2 shells - MetroWindow and Window. The screenshot above is a MetroWindow. The one given below is a regular window.

![Screenshot](https://raw.github.com/wiki/chandramouleswaran/Wide/Wide-old.png)

Modules in this case are PRISM modules. (Note: You do NOT need to know PRISM to understand/use the framework). Check out the demo for more details.

For full details, look at the demo program.

### What modules are built-in?

Wide comes with two modules and various out of the box functions:

* Core module (Required)
 * Used for customizable splash screen
 * Used for Menus (supports regular menus with icon, checkable menus)
 * Used for Toolbar (menu view model can be reused for toolbars)
     * Multiple toolbars can be added to the IDE (check demo)
 * Themes (VS2010, VS2012 Light theme and no theme)
     * ThemeManager to add/remove themes
 * Used for Statusbar (in development)
 * Open file service with participatory handlers (could be based on extension or even file contents)
 * Save and restore layout along with opening documents
* Logger module (For the logging tool)


### Libraries used
It is built using variety of open source projects:

* [AvalonDock](http://avalondock.codeplex.com) - Used for docking
* [Prism](http://compositewpf.codeplex.com/) - Used for dependency injection and events
* [MahApps Metro](https://github.com/MahApps/MahApps.Metro) - Metro look and feel of the window
* [AvalonEdit](https://github.com/icsharpcode/SharpDevelop/wiki/AvalonEdit) - The generic text editor used in #D
* [Unity Container](http://msdn.microsoft.com/en-us/library/ff660899\(v=pandp.20\).aspx) - The container used for the application
* [NLog](http://nlog-project.org/) - For logging purposes
* [Extended WPF toolkit](http://wpftoolkit.codeplex.com/) - Not yet used. Will be used for settings manager.

In this process of building Wide, I was able to look at various open source projects, learn and most importantly contribute back to the project.


### What projects use Wide?

I am planning on using Wide for my own projects:

* [Hypertest](https://github.com/chandramouleswaran/Hypertest)
* coming soon...

### Plans ahead
* Settings manager for the application
* Status bar service
* Dark, Expression themes
* See milestones and open issues for more details.

### Credits
Some open source projects had certain parts built nicely of which a few were used in my project:

* [EDI](http://edi.codeplex.com/) - Visual studio 2010 theme was set perfect. Themes for menu and toolbar were flicked from this project.
* [MetroLikeWindow](https://github.com/Grabacr07/MetroLikeWindow) - This has some bugs but overall, adds a glow effect to the window. Contributed to MahApps.Metro.
* [CBR](http://wfpbookreader.codeplex.com/) - One really good and well maintained comic book reader.
* [StackOverflow](http://www.stackoverflow.com) - Most of my questions already had an answer here.

### Note
If you are venturing into serious IDE related development, I would suggest you look at VSX (Visual Studio extensibility). You can create your own isolated shell of Visual studio and create an IDE. This project was done to understand/explore WPF in general and how much I understand the tools given above.

If you have any questions or find bugs, feel free to contact me.
