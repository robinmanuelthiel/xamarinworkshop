# Optimizations and Tweaks
Now that out app is running, we can start optimizing and refactoring a few things to let our app look smoother and the architecture even cleaner.

## Layout and UI
### Tab icons on iOS (only)
As you might have noticed, the tabs at the bottom of iOS applications usually have icons that our app is missing. And even in Android, tabs can have icons. We can add a tab icon by settings the `Icon` property of a `ContentPage`. But as they can look a little bit weird on Android, let's only add them on iOS and write our first platform-specific UI decision.

In XAML, we can define platform-specific values with the `OnPlatform` tag.

```xml
 <OnPlatform
    x:TypeArguments="<TYPE>"
    Android="<ANDROID_SPECIFIC_VALUE>"
    WinPhone="<ANDROID_SPECIFIC_VALUE>"
    iOS="<IOS_SPECIFIC_VALUE>" />
```

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

Now, add the Icons, you can [find attached](/Icons) to the ***Resources*** folder of your iOS project. As the other platforms `Icon` property is set to an empty string, they will ignore the `Icon` property automatically.

> **Hint:** If we had defined the `Icon` the standard way like `<ContentPage Icon="Calendar.png">`, all platforms had to hold the image files inside their projects.
