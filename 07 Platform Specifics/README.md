# Platform Specifics

## 1. Platform specific UI
### 1.1 Platform specific properties in XAML and code-behind
When creating a shared UI with Xamarin.Forms, we might come to a point, where we want to adjust specific properties of these UIs regarding to the platform it runs on. Typical platform adjustments can be

- Margin and Padding
- Colors
- Layouting and Positioning
- Font size

Xamarin.Forms brings an easy way to give an UI element's properties different values regarding to the platform it is running on: The `OnPlatform` tag.

```xml
<OnPlatform
    x:TypeArguments="<TYPE>"
    Android="<ANDROID_SPECIFIC_VALUE>"
    WinPhone="<ANDROID_SPECIFIC_VALUE>"
    iOS="<IOS_SPECIFIC_VALUE>" />
```

In XAML, we can use this tag to define platform specific values for each property. For example, instead of defining the `Margin` of a button like this

```xml
<Button Name="TestButton" Text="Click Me!" Margin="20" />
```

you can use the `OnPlatform` tag to do it for each platform.

```xml
<Button Name="TestButton" Text="Click Me!">
    <Button.Margin>
        <OnPlatform
            x:TypeArguments="Thickness"
            Android="14"
            WinPhone="20"
            iOS="20" />
    </Button.Margin
</Button>
```

This pattern can be used for every property of every UI element in Xamarin.Forms. You can also call this in code-behind by using the `Device.OnPlatform` method, to declare platform specifics there.

```csharp
Device.OnPlatform(iOS: () => TestButton.Margin = new Thickness(14));
```

### 1.2 Platform specific styles
Beside the XAML UI in Xamarin.Forms, you can also always access the platform projects directly and modify the UI there. Remember, that everything we define in XAML will be rendered to **native** UI elements for the platforms.

As you might have noticed, the Android app does have a blue navigation bar by default, while the iOS one's is gray. As Xamarin.Forms does not support navigation bar colors by now, we could modify the `AppDelegate.cs` file inside the `Conference.Forms.iOS` project.

![Screenshots of the iOS app before and after the color change](../Misc/iosnavigationbarcolor.png)

Here we can define colors for the `UINavigationBar` the traditional way.

```csharp
[Register("AppDelegate")]
public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
{
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        // Set Navigation Bar Color on iOS
        UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(33, 150, 243);
        UINavigationBar.Appearance.TintColor = UIColor.White;
        UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.White };

        // ...        
    }
}
```

### 1.3 Custom Renderers

```csharp
[assembly: ExportRenderer(typeof(Xamarin.Forms.Image), typeof(CustomImageRenderer))]
namespace Conference.Forms.iOS
{
    public class CustomImageRenderer : ImageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Layer.CornerRadius = Control.Image.Size.Width / 2;
                Control.ClipsToBounds = true;
            }
        }
    }
}
```