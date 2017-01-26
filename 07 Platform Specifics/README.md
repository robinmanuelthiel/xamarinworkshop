# Platform Specifics

## 1. Platform specific UI properties
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

As you might have noticed, the Android app does have a blue navigation bar by default, while the iOS one's is gray. Xamarin.Forms does not support navigation bar colors by now, but we can still define colors for the `UINavigationBar` the traditional way.

![Screenshots of the iOS app before and after the color change](../Misc/iosnavigationbarcolor.png)

To do so, let's modify the `AppDelegate.cs` file inside the `Conference.Forms.iOS` project.

```csharp
[Register("AppDelegate")]
public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
{
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        // Set Navigation Bar Color for iOS
        UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(33, 150, 243);
        UINavigationBar.Appearance.TintColor = UIColor.White;
        UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes() { ForegroundColor = UIColor.White };

        // ...        
    }
}
```
## 2. Custom Renderers
Actually, everything that Xamarin.Forms does when creating native controls out of the XAML definition is using one of its [Renderer Base Classes](https://developer.xamarin.com/guides/xamarin-forms/custom-renderer/renderers/). These renderers turn XAML into native controls on each platform and they can be extended.

Creating custom rendereders always follows the same steps

1. Create a renderer class inside the platform project(s)
1. Derive from [Renderer Base Class](https://developer.xamarin.com/guides/xamarin-forms/custom-renderer/renderers/)
1. Override the `OnElementChanged` method
1. Expose the renderer to the Xamarin.Forms framework using `ExportRenderer`

### 2.2 Extend existing renderers
With custom renderers we can change the look and behaviour of controls and views that Xamarin.Forms brings out of the box.

For example, we could change the way Xamarin.Forms renders images on iOS and extend the default `ImageRenderer` by a functionality that draws images in circles by default. For this, add a new class `CustomImageRenderer` to the `Conference.Forms.iOS` project and let it derive from `ImageRenderer`, which is the renderer that is used by Xamarin when using images according to [this list](https://developer.xamarin.com/guides/xamarin-forms/custom-renderer/renderers/).

Now we can override the `OnElementChanged` method, call `base.OnElementChanged(e);` method to call the default behaviour and extend it by the `CornerRadius`, which is used by iOS to create round images.

```csharp
public class CustomImageRenderer : ImageRenderer
{
    protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> e)
    {
        base.OnElementChanged(e);

        if (Control != null)
        {
            // Add corner radius to UIImage control
            Control.Layer.CornerRadius = Control.Image.Size.Width / 2;
            Control.ClipsToBounds = true;
        }
    }
}
```

Once we did this, the last thing we have to do is telling the Xamarin.Forms framework, that this is the new renderer to use, when it comes to an `Image`. This can be achieved by declaring the `ExportRenderer(Type FormsControl, Type Renderer)` assembly attribute to the namespace.

```csharp
[assembly: ExportRenderer(typeof(Xamarin.Forms.Image), typeof(CustomImageRenderer))]
namespace Conference.Forms.iOS
{
    public class CustomImageRenderer : ImageRenderer
    {
        // ...
```

After rebuilding the iOS application now and navigation to the `SpeakerDetailsPage`, we can see that the image on iOS is rendered differently from the images on Android and Windows, because we implemented a custom renderer for controls of type `Xamarin.Forms.Image` iOS.

![Screenshots of the iOS with round profile image](../Misc/iosroundimagerenderer.png)

### 2.3 Renderers for new controls
If you tanke a look at the official list of iOS, Android and Windows controls, you will find only a small subset of these covered by Xamarin.Forms. This is simply because Xamarin.Forms needs implement the controls on all mobile platforms while abstracting the same functionality for each platform in the shared code.

What is currently missing in Xamarin.Forms is a Hyperlink Button with basically is a clickable text. So let's go ahead and extend our project with such a button!

### 2.4 Renderers for platform specific controls
Sometimes of course, you want to use controls that are only available on one platform and show an alternative for the other platform. The [Floating Action Button](https://material.io/guidelines/components/buttons-floating-action-button.html) on Android is a good example for that. As it is part of Android's design pattern, you might want to show the prime functionality in a Floating Action Button and all others in the menu bar. On iOS and Windows, this pattern does not exist, so we just want to create an Andropid-only control on the Android platform only.


## 2. Platform specific functionality
