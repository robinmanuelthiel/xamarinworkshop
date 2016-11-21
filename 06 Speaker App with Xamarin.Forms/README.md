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

As we are working with multiple views here, it is a good idea, to set up a `NavigationPage` container which is one of the [Xamarin.Forms pre-defined page containers](https://developer.xamarin.com/guides/xamarin-forms/controls/pages) that allows navigation between multiple pages and set its initial UI to the `MainPage` instance.

```charp
public App()
{
    InitializeComponent();

    var firstPage = new Conference.MainPage();
    MainPage = new NavigationPage(firstPage);
}
```

## 2. Creating the main UI
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