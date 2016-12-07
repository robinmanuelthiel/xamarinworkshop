# Optimizations and Tweaks
Now that out app is running, we can start optimizing and refactoring a few things to let our app look smoother and the architecture even cleaner.

## Layout and UI
### Tab icons on iOS
As you might have noticed, the tabs at the bottom of iOS applications ususally have icons that our app is missing. So let's fix this. Copy the icons from the attached folder to the ***Resources*** folder of your iOS project and extend the `MainPage.xaml` file from the Xamarin.Forms project with two `Icon` properties.

```xml
<!-- Sessions Tab -->
<ContentPage Title="Sessions" Icon="Calendar.png">
    <!-- ... -->
</ContentPage>

<!-- Speakers Tab -->
<ContentPage Title="Speakers" Icon="Person.png">
    <!-- ... -->
</ContentPage>
```

As the other platforms do not have any `Calendar.png` or `Person.png` files, they will ignore the `Icon` property automatically.