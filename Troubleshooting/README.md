# Troubleshooting

## Error Message: The project needs to be deployed before it can be started

1. Right-click the Solution Name at the top of the folder structure and select <kbd>Properties</kbd>
1. Click the ***Configuration Properties*** tab
1. Select the checkboxes for ***Build*** and ***Deploy*** at the project that causes the error Message

## Xamarin.Android package errors
If you see something like
```
Please install package: 'Xamarin.Android.Support.v4' available in SDK installer. Java library file ...\AppData\Local\Xamarin\Xamarin.Android.Support.v4\23.3.0.0\content\libs/internal_impl-23.3.0.jar doesn't exist.	
Please install package: 'Xamarin.Android.Support.Vector.Drawable' available in SDK installer. Android resource directory ...\AppData\Local\Xamarin\Xamarin.Android.Support.Vector.Drawable\23.3.0.0\content\./ doesn't exist.
Please install package: 'Xamarin.Android.Support.v7.RecyclerView' available in SDK installer. Android resource directory ...\AppData\Local\Xamarin\Xamarin.Android.Support.v7.RecyclerView\23.3.0.0\content\./ doesn't 
```

or

```
...\Resources\values\styles.xml
error APT0000: Error retrieving parent for item: No resource found that matches the given name 'Theme.AppCompat.Light.DarkActionBar'.
error APT0000: No resource found that matches the given name: attr 'colorAccent'.
error APT0000: No resource found that matches the given name: attr 'colorPrimary'.
```
Right-click the Android project and select <kbd>Clean</kbd> then <kbd>Rebuild</kbd>.

If this does not fix the problem:

1. Close Visual Studio
1. Open the folder where your Android project lies and delete the `obj` and `bin` folder
1. Open the folder where your `.sln` file and `packages` lies and delte the `packages folder`
1. Go to the `%UserProfile%\.nuget\packages\` folder (Windows) or `~/.nuget/packages` folder (Mac) and delete all `Xamarin.Android.*` folders
1. Go to the `%UserProfile%\AppData\Local\Xamarin` folder (Windows) or `~/.local/share/NuGet/Cache` folder (Mac) and delete all `Xamarin.Android.*` folders and the `zips` folder
1. Reopen Visual Studio, right-click your Solution and select <kbd>Rebuild Solution</kbd>
1. Wait until it has finished and do not cancel this
