# Wide

### What is this?

Wide is a participatory IDE framework which uses WPF and Prism to build IDE-like applications (eg: Visual Studio 2012/2013, Eclipse etc.) 


![Screenshot](https://raw.github.com/wiki/chandramouleswaran/Wide/Wide.png)

### What does it do?

Wide allows you to build your application by creating PRISM modules which participate in building the IDE. This way you can modularize the code for each part of your application.

Wide comes with 2 shells - MetroWindow and Window. The screenshot above is a MetroWindow. The one given below is a regular window.

![Screenshot](https://raw.github.com/wiki/chandramouleswaran/Wide/Wide-old.png)

Modules in this case are PRISM modules. (Note: You do NOT need to know PRISM to understand/use the framework). Check out the demo for more details.

For full details, look at the demo program.

### How is Wide organized and how to use it?

- **Wide.Core** - The core module is required and loaded first when the framework is used 
 - **Overridable**
      * Provides a default splash screen for your application - (check `Wide.Splash.SplashView.xaml`).
      * Provides a default logger for your application - (check `Wide.Core.Services.NLogService.cs`).
      * Provides a default workspace for your application - (check `Wide.Core.Workspace.cs`).
 - **Interactable**
      * Provides StatusBar, ToolBar and Menu service for you application.
      * Provides settings manager for your application which can be interacted with.
      * Provides command manager for your application which can be used for reusable commands.
      * Provides a theme manager for your application which can be used to add/remove new themes in your application. (Wide comes with the following themes: VS2010, VS2012 Light/Dark theme and no theme)
      * Provides a default service to open documents - content handlers can register themselves to the content registry which will be used in this service to open different contents.

- **Wide.Interfaces** - The interfaces namespace provides a list of interfaces that you can use in your application to interact with the services provided by Wide.<br/> <br/>The interfaces namespace is split into Controls, Converters, Events, Services, Settings, Styles and Themes. 
 - **Interfaces and Services**
      * IShell - the interface that is implemented on the shell.
      * IWorkspace - the workspace which has the documents/tools in it.
      * IMenuService - the menu service which you can interact with.
      * ILoggerService - the logger service to log messages.
      * IToolbarService - the toolbar service which you can interact with.
      * IThemeManager - the theme manager service.
      * IContentHandlerRegistry - the registry service which is used to create/open/save/close contents.
      * ICommandManager - the command manager service.
      * IOpenDocumentService - the service which which makes use of the content handler registry to figure out which ContentViewModel needs to be presented for which type of document.
 - **Controls**
      * AbstractMenuItem - Extend this class to create your own menu type.
      * MenuItemViewModel - A sample AbstractMenuItem - mostly useful for general text based menus.
      * IToolbar - the interface for creating a toolbar.
      * AbstractToolbar - Extend this class to create toolbars.
      * ToolbarViewModel - A sample AbstractToolbar - mostly useful for creating simple toolbars.
 - **Events**
      * ActiveContentChangedEvent - Event triggered when the active document changed in the workspace.
      * LogEvent - Event triggered when you message is logged by the application.
      * OpenContentEvent - Event triggered when a new document is openend.
      * SplashCloseEvent - Event triggered when the splash window closes.
      * SplashMessageUpdateEvent - Event triggered when a new message is sent to show in the splash window.
      * ThemeChangeEvent - Event triggered when a theme is changed.
      * WindowClosingEvent - Event triggered when a window is closing.
 - **Settings**
      * ISettingsManager - The settings manager service - you can create your own settings by extending `AbstractSettings` class. Your settings, if end user needs to modify should be registered with the ISettingsManger.
      * AbstractSettings - If you need settings to persist per user, extend this class. If you want settings to be editable by end user, register it with ISettingsManager.
      * IRecentViewSettings - The default recent view settings - this is not exposed to the end users but one can get values from this settings. This is used in the "Recently Opened" menu.
      * IThemeSettings - Used to remember the theme used in your application.
      * IToolbarPositionSettings - Used to remember the toolbar position and visibility.
 - **Themes**
      * Dark - The Visual Studio 2012 dark theme.
      * LightTheme - The Visual Studio 2012 light theme.
      * VS2010 - The Visual Studio 2010 theme.

###Other things
- Wide by default Save's and Restore's layout along with opening documents.
- Wide also comes with a logger module as a separate assembly which you can use in you application. (Wide.Tools.Logger)


### Libraries used
It is built using variety of open source projects:

* [AvalonDock](http://avalondock.codeplex.com) - Used for docking
* [Prism](http://compositewpf.codeplex.com/) - Used for dependency injection and events
* [MahApps Metro](https://github.com/MahApps/MahApps.Metro) - Metro look and feel of the window
* [AvalonEdit](https://github.com/icsharpcode/SharpDevelop/wiki/AvalonEdit) - The generic text editor used in #D
* [Unity Container](http://msdn.microsoft.com/en-us/library/ff660899\(v=pandp.20\).aspx) - The container used for the application
* [NLog](http://nlog-project.org/) - For logging purposes
* [Extended WPF toolkit](http://wpftoolkit.codeplex.com/) - Used for settings manager.

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
