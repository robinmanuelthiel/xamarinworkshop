# Optimizations and Tweaks
Now that out app is running, we can start optimizing and refactoring a few things to let our app look smoother and the architecture even cleaner.

## Code Sharing
### Resolve dependencies with Inversion of Control
Let's take a look at how we can improve the way we work with our dependencies and their instantiation. You remember, when it comes to creating the `MainViewModel` in the `MainPage`, we create instances of all the services it needs, pass them to the ViewModel's constructor and instantiate it then.

```csharp
public MainPage()
{
    InitializeComponent();

    var httpService = new FormsHttpService();
    var conferenceService = new HttpConferenceService(httpService);
    viewModel = new MainViewModel(conferenceService);

    BindingContext = viewModel;
}
```

#### The problem
This works for us as our app is fairly simple, but might cause problems, when getting more complex.

Mainly because we are not using **Singletons** here. So if multiple ViewModels would use the `ConferenceService` for example, they would all get their independent instance of it. This does not only demand more memory, it also causes consistency problems. Whenever the data in one `ConferenceService` changes, this would not be available in the other ViewModels.

We can solve this by managing all dependencies at one central spot and other components like ViewModels can ask for an implementation of a specific class.

#### The solution
Xamarin.Forms brings its own `DependencyService` class with handles registration and lazy resolving of dependencies, so we could use it to resolve the Service classes and provide them as singleton.

```csharp
// Register dependencies
DependencyService.Register<IHttpService, FormsHttpService>();

// Resolve dependencies
var httpService = DependencyService.Get<IHttpService>();
```

Unfortunately, the Xamarin.Forms `DependencyService` is very basic and it can only resolve Services with a Default Constructor. So it would not work with our `IConferenceService` or `MainViewModel`, because they need implementations of other services *injected*. This is, where **Inversion of Control (IoC)** comes into play.

When using the IoC Abstraction Pattern, we define a container, that handles a registry of all known dependencies. Additionally, it creates objects and injects required dependencies automatically into their constructors. After registering all dependencies at the container, we can ask it to create an instance of any `Type` for us. It will look at the constructors of this `Type` and try to create dependencies for it out of the ones that have been registered. Then it injects these dependencies to the constructor and gives us back the instantiated class. Super handy!

Fortunately, we do not have to implement this container by our own and can use one of the several free IoC Containers out there. As it is super compatible with all .NET platforms, I chose the one, that comes with the [MVVM Light Libs NuGet package](https://www.nuget.org/packages/MvvmLightLibs/). Once installed, navigate to the `App.xaml.cs` class and register the Services and ViewModels.

```csharp
public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Create Inversion of Control container
        ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

        // Register Services
        SimpleIoc.Default.Register<IHttpService, FormsHttpService>();
        SimpleIoc.Default.Register<IConferenceService, HttpConferenceService>();

        // Register ViewModels
        SimpleIoc.Default.Register<MainViewModel>();
        SimpleIoc.Default.Register<SessionDetailsViewModel>();
        SimpleIoc.Default.Register<SpeakerDetailsViewModel>();

        // ...
    }
}
```

On the `MainPage.xaml.cs` view for example, we can now get rid of the former dependency instantiation and just tell the `SimpleIoc` container, that we need the `MainViewModel` instance. It automatically creates the dependencies for it and injects it to the `MainViewModel`'s constructor. 

```csharp
public partial class MainPage : TabbedPage
{
    private MainViewModel viewModel;

    public MainPage()
    {
        InitializeComponent();

        // Get ViewModel from IoC Container
        viewModel = SimpleIoc.Default.GetInstance<MainViewModel>();
        BindingContext = viewModel;
    }
}
```

It might feel a little bit like black magic, but once this pattern is understood, it is one of the most powerful weapons in a developer's hands! 

## Layout and UI
### Tab icons on iOS (only)
As you might have noticed, the tabs at the bottom of iOS applications usually have icons that our app is missing. And even in Android, tabs can have icons. We can add a tab icon by settings the `Icon` property of a `ContentPage`. But as they can look a little bit weird on Android, let's only add them on iOS and write our first platform-specific UI decision.

Let's use this to set the `Icon` property for iOS only and extend the `MainPage.xaml` file from the Xamarin.Forms project.

```xml
<!-- Sessions Tab -->
<ContentPage Title="Sessions">
    <ContentPage.Icon>
        <OnPlatform
            x:TypeArguments="FileImageSource"
            Android=""
            WinPhone=""
            iOS="Calendar.png" />
    </ContentPage.Icon>
    <!-- ... -->
</ContentPage>

<!-- Speakers Tab -->
<ContentPage Title="Speakers">
    <ContentPage.Icon>
        <OnPlatform
            x:TypeArguments="FileImageSource"
            Android=""
            WinPhone=""
            iOS="Person.png" />
    </ContentPage.Icon>
    <!-- ... -->
</ContentPage>
```

Add the Icons, you can [find attached](/Icons) to the ***Resources*** folder of your iOS project. As the other platforms `Icon` property is set to an empty string, they will ignore the `Icon` property automatically.

![iOS Tab Icons Screenshot](../Misc/iostabimages.png)

> **Hint:** If we had defined the `Icon` the standard way like `<ContentPage Icon="Calendar.png">`, all platforms have to hold the image files in their projects.


## Performance
When it comes to performance, Xamarin.Forms needs a very clean architecture and some extra considerations regarding memory management, threading and garbage collection. So let's take a look at what we can do, to enhance your application's performance.

### XAML compilation
Layouts defined in XAML get rendered at runtime by default. That can cause massive performance drains as the XAML code gets parsed the moment the user should actually see the page. Fortunately, Xamarin offers XAML pre-compilation called **XAMLC**. This can be enabled application wide by registering the `XamlCompilation` assembly to the main namespace or can be opted in our out for each view using the `XamlCompilation` attribute for the class.

To enable XAMLC at the assembly level, register the assembly inside your `App.xaml.cs` file.

```csharp
using Xamarin.Forms.Xaml;

// ...

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Conference.Forms
{
    public partial class App : Application
    {
        // ...
    }
}
```

For a view specific XAML compilation, add the `XamlCompilation` attribute to the regarding class.

```csharp
using Xamarin.Forms.Xaml;

// ...

[XamlCompilation (XamlCompilationOptions.Compile)]
public class HomePage : ContentPage
{
    // ...
}
```

## Avoid Resource Intensive Tasks on the UI Thread

Change

```csharp
 _PollenList = JsonConvert.DeserializeObject<List<Pollen>>(result);
```

to 

```csharp
await Task.Factory.StartNew(() => _PollenList = JsonConvert.DeserializeObject<List<Pollen>>(result));
```

