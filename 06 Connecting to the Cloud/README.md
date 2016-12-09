# Connecting to the cloud
For this, we need to get back to Visual Studio and open the application we created in the previous module.

> **Hint:** If you got stuck during this module or lost the overview on where to place which code, you can always take the look at the [finished and working project](./Code) that is attached to this module.

## 1. Create a new Conference Service with Azure
### 1.1 Adding the SDK packages
Every Azure resource is available via REST API calls, but SDKs make a developer's life a lot easier, which is why we should add the [Azure Mobile Client SDK NuGet package](https://www.nuget.org/packages/Microsoft.Azure.Mobile.Client/) to the project.

As the SDK use platform specific components like the SQLite database that is used for offline synchronization, the NuGet package has to be added to all projects that uses it.

- Xamarin.Forms project
- iOS project
- Android project
- Windows project

In Visual Studio for Windows, I can manage packages solution wide by right-clicking the solution inside the Solution Explorer, selecting <kbd>Manage NuGet Packages for Solution...</kbd>.

![Import CSV files into Easy Tables Screenshot](../Misc/vsaddazuremobilenugetpack.png)

### 1.2 Initialize the Mobile Service
The Azure Mobile Service SDK has to initialized on each platform that uses it separately. We have already added the NuGet package, so we just have to kick it off on each platform.

```csharp
// Initialize Azure Mobile App Client for the current platform
CurrentPlatform.Init();
```

Just add this line above the Xamarin.Forms initialization in

- `AppDelegate.cs` file for iOS
- `MainActivity.cs` file for Android
- `MainPage.xaml.cs` file for Windows

### 1.3 Create a new Conference Service
As we already learned that the Azure Mobile Client SDK uses platform specifics, it would be hard to implement it inside the shared frontend project, so let's work inside the Xamarin.Forms project with it.

This does not matter at all, as we have a clean architecture and can simply create a `AzureConferenceService` in our Xamarin.Forms project. As long as it implements the `IConferenceService` interface, it can be used easily by the ViewModel.

> **Hint:** Remember, the ViewModel does not care, where the implementation of `IConferenceService` lives. It is tolerant enough and just wants to have *any* implementation of it, when it gets instantiated.

Add a new `AzureConferenceService` class to the Xamarin.Forms project, that implements the `IConferenceService` interface.

```csharp
public class AzureConferenceService : IConferenceService
{
    public async Task<List<Session>> GetSessionsAsync()
    {
    }

    public async Task<List<Speaker>> GetSpeakersAsync()
    {
    }
}
```

## 2. Connect with the App Service
It is time to add the logic for talking with the backend to our app.

### 2.1 Initialize the client
For this, we should define a `MobileServiceClient` inside our `AzureConferenceService` and initialize it in the constructor with the backend URL. This URL can be found inside the [Azure Portal](https://portal.azure.com).

```csharp
public class AzureConferenceService : IConferenceService
{
    private MobileServiceClient client;

    public AzureConferenceService()
    {
        client = new MobileServiceClient("<YOUR_URL>");
    }

    // ...
}
```

### 2.2 Connect to the database
Connecting to the Easy Tables, we have created with the CSV files in our App Service is as simple as defining a `IMobileServiceTable<Type>` for each table. Luckily, we have a `Type` that matches the structure of the database tables: Our `Speaker` and `Session` classes.

```csharp
private IMobileServiceTable<Session> sessionTable;
private IMobileServiceTable<Speaker> speakerTable;
```

> **Hint:** When initializing these, the `MobileServiceClient` will look up tables that matches the models' structures and names. You can also provide a custom name, if your tables are named differenetly.

Next, we can add the initialization of the tables and can access them inside the `GetSessionsAsync()` and `GetSpeakersAsync` methods.

```csharp
public class AzureConferenceService : IConferenceService
{
    private MobileServiceClient client;
    private IMobileServiceTable<Session> sessionTable;
    private IMobileServiceTable<Speaker> speakerTable;

    public AzureConferenceService()
    {
        client = new MobileServiceClient("<YOUR_URL>");
        sessionTable = client.GetTable<Session>();
        speakerTable = client.GetTable<Speaker>();
    }

    public async Task<List<Session>> GetSessionsAsync()
    {
        return await sessionTable.ToListAsync();
    }

    public async Task<List<Speaker>> GetSpeakersAsync()
    {
        return await speakerTable.ToListAsync();
    }
}
```

Just switch over to you `MainPage.xaml.cs` file inside the Xamarin.Forms project and replace the old `HttpConferenceService` with and instance of our new `AzureConferenceService`.

```csharp
public MainPage()
{
    InitializeComponent();

    var httpService = new FormsHttpService();
    //var conferenceService = new HttpConferenceService(httpService);
    var conferenceService = new AzureConferenceService();
    viewModel = new MainViewModel(conferenceService);

    BindingContext = viewModel;
}
```

That's it. Now your app connects to the Azure backend instead of simply downloading the JSON files from the GitHub repository.