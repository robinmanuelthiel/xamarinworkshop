# Speaker App with Xamarin.Forms
To dig deeper into the workflow of Xamarin development, we will create a real application in the next modules: A conference app. To keep UI work simple, we will use Xamarin.Forms for this to create the UI only once and share it across the other platforms. Please keep in mind, that Xamarin.Forms layouts are defined in XAML and will be rendered to **native** controls on each platform.

We will create a conference app that will list all sessions with their speakers in seperate tabs. Both, sessions and speakers will also have details pages that will be shown when the user tabs on an item. This results in having three views for the app:

- `MainPage` with two taps for sessions and speakers
- `SessionDetailsPage` with session details
- `SpeakerDetailsPage` with speaker details

## 1. Create the base structure
Creating a new Xamarin.Forms project is similar to creating a Xamarin Platform app as we already did before. In Visual Studio on Windows it's clickinng <kbd>File</kbd> <kbd>New</kbd> <kbd>Project...</kbd>, navigating to the ***Cross-Platform*** section and selecting ***Blank Xaml App (Xamarin.Forms Portable)***.

Let's inspect the Portable Class Library, as this will be our main workplace. As we can see, there have also been iOS, Android and Windows projects for us but they are all linked to the shared project and only have to be touched when we want to implement platform specific features.

The `App.xaml` and its according `App.xaml.cs` file are the main entry point for our Xamarin.Forms app. While the XAML file only defines application wide resources, the `App.xaml.cs` file holds the application lifecycle methods and  kicks off the Xamarin.Forms framework and your UI. The first thing, Xamarin.Forms does here is setting the entry point to a new instance of the `MainPage` class whose logic and layout we can also find in the shared project.

```charp
MainPage = new Conference.MainPage();
```

As we are working with multiple views later, it is a good idea, to set up a `NavigationPage` container which is one of the [Xamarin.Forms pre-defined page containers](https://developer.xamarin.com/guides/xamarin-forms/controls/pages) that allows navigation between multiple pages and set its initial UI to the `MainPage` instance.

```charp
public App()
{
    InitializeComponent();

    var firstPage = new Conference.MainPage();
    MainPage = new NavigationPage(firstPage);
}
```

## 2. Create the models
Now it's time to give thought to the models that we want to use. Actually, our conference should deal with two types of objects: **Speakers** and **Sessions**. So let's create a model class for each. Before, let's take a second and think about where to place them. In a cross-platform scenario and in general in every cleanly seperated code project we should also be aware of what's the best project to put a new function, class or service in!

As the models we want to create do not have any platform specifics and are not used in a specific scenario only, we should definitely put them into a new project that holds everything we want to span across multiple usage scenarios. For this, let's create a new ***Portable Class Library*** by right-clicking on the Solution in Visual Studio an selecting <kbd>Add</kbd> <kbd>New Project...</kbd>. In the ***Visual C#*** section, we will find the ***Class Library (Portable)*** project template. Let's call it ***Conference.Core*** and add it to our solution.

![Add Portable Class Library in Visual Studio Screenshot](../Misc/vsaddpcl.png)

> **Hint:**  At this point, we should take a short digression on Portable Class Libaries. In .NET, these are projects that combine a subset of APIs and Types and offer the developer to use these across multiple projects. Inside the settings of Portable Class Libaries, we can choose which platforms, we want to target and the Library will offer all of the overlapping APIs. Unfortunately, this drives fragmentation and whenever you want to increase the target platform range of the library, some APIs might get lost and you have to rethink your code. That is why the .NET Foundation introduced [.NET Standard](https://blogs.msdn.microsoft.com/dotnet/2016/09/26/introducing-net-standard/), which is a pre-defined set of APIs that every .NET platform has to implement. So whenever we choose .NET Standard as target, we can combine the library with any other .NET platform that supports this standard. And (surprise): Xamarin does!

To change the target platforms of our Portable Class Library, just right-click on it, choose <kbd>Properties</kbd> and click the ***Target .NET Platform Standard*** link, that you can find in the first ***Library*** tab, to make out Library super compatible and future proof.

Now we can finally create the classes for the **Speakers** and **Sessions** models.

```csharp
public class Speaker
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Bio { get; set; }
    public string ImagePath { get; set; }
}
```

```csharp
public class Session
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Room { get; set; }
    public DateTime StartTime { get; set; }
    public string SpeakerId { get; set; }
}
```

The last thing, we need to do is connecting the Portable Class Library with our Xamarin.Forms project. For this, just right-click the ***References*** folder and select <kbd>Add Reference...</kbd>. A new Windows opens that allows to manage project references. In the ***projects*** tab, we find the recently created ***Conference.Core*** project and can add it to the Xamarin.Forms project by simply checking the box next to it.

![Add References in Visual Studio Screenshot](../Misc/vsaddreference.png)

## 3. Creating the main UI
### 2.1 Tabs
When taking a look at the `MainPage.xaml` file, we can see, that Xamarin.Forms created a new page of type `ContentPage` for us. As we want to create a page with multiple tabs, we need to change this to a `TabbedPage`, which is a [pre-defined Xamarin.Forms Layout](https://developer.xamarin.com/guides/xamarin-forms/controls/layouts/) and create two `ContentPage`s inside which will define the content of our session and speaker tabs.

```xaml
<TabbedPage
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:Conference"
    x:Class="Conference.MainPage"
    Title="Conference">

    <ContentPage Title="Sessions">
        <!-- Sessions Tab -->
    </ContentPage>

    <ContentPage Title="Speakers">
        <!-- Speakers Tab -->
    </ContentPage>
</TabbedPage>
```

Please notice, that we also added a `Title="Conference"` property to the `TabbedPage` root element to give the whole container a header. As we changed the page from a `ContentPage` to a `TabbedPage`, we also have to change this in the `MainPage.xaml.cs` code behind file.

```charp
public partial class MainPage : TabbedPage
{
    // ...
}
```