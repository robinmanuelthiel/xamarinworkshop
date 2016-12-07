# Creating a Xamarin Platform App
Creating a Xamarin application is super simple, as Visual Studio does all the work for us. While installing, Xamarin creates Visual Studio templates, that contain everything we need to start coding for iOS, Android and Windows. Make sure, to setup your development environment correctly before starting to create Xamarin apps, as described in module [02 Setting up](/02%20Setting%20up).

In this module, we will create a Xamarin Plaform App, not a Xamarin.Forms one. This makes a huge difference and the naming is a little bit clumsy, so let's clear it first.

**What does *Xamarin Platform* mean?**

In general, Xamarin takes iOS, Android and Windows to the same developing technology (.NET) to makes it easier to develop for all three platforms at the same time and with the same team while enabling to share code snippets across all of these projets. This still means, that we will have a dedicated project for each platform and have to create layout and code for each platform. The main difference in the unifiend development language.

Xamarin.Forms often gets mistaken for Xamarin Platform and extends the concept by providing a unified UI technology with up- and downsides. In a real-world scenario, you have to choose carefully between these concepts. If you are not sure, what to use, I would always recommend Xamarin Platform, as it is a much cleaner and more powerful approach!

Enough theory for now. Let's create our first app!

## Creating the App
To create a new Xamarin app, simply click on <kbd>File</kbd> <kbd>New</kbd> <kbd>Project...</kbd> and navigate to the ***Cross-Platform*** section in the upcoming window. Here you will see a bunch of project templates. Select the ***Blank App (Native Portable)*** one, which creates a Xamarin Platform app for you, that shares its code in a [Portable Class Libary](https://msdn.microsoft.com/en-us/library/gg597391(v=vs.110).aspx).

![Creating a new Xamarin App in Visual Studio Screenshot](../Misc/vsnewxamarinproject.png)

As Visual Studio is creating multiple projects at once now, this process might take some seconds. When finished, you will the a new *Solution* with four different projects. One for each platform and one for the shared code. We will walk through all of these in this module. 

## The Android project

![Android project in a Xamarin Solution Screenshot](../Misc/vsexplorexamarinandroid.png)

## The iOS project

![Android project in a Xamarin Solution Screenshot](../Misc/vsexplorexamarinios.png)

## The Windows project

![Android project in a Xamarin Solution Screenshot](../Misc/vsexplorexamarinwindows.png)

## The Portable project
The most exciting project might be the Portable Class Library that Xamarin created. All platform projects are referencing this library. This implies that everything that gets created in here, is available for every platform.

This gets very important, when it comes to code-sharing across multiple platform. This project is there place where as much code as even possible should live.

![Android project in a Xamarin Solution Screenshot](../Misc/vsexplorexamarinshared.png)

The Portable project is the perfect place for your

- Models
- ViewModels
- Interfaces, that have to be implemented by the platform
- Services, that are platform independant

Every time, we add a new functionality to our solution, we should consider if it could be placed into this portable library as it makes **Maintaining**, **Testing**, **Bugfixes** and **Code Reuse** much easier!

> **Important:** Being able to placing code here wich becomes available for every other platform is the real benefit of Xamarin!

## Running the App
To run the app either in an Emulator, Simulator or physical device, you need to select at least one platform as StartUp project. For this, right-click on one of the platform projects and select <kbd>Set as StartUp Project</kbd>.

Now you can configure the Run Configurations for your app and select **Debug** or **Release** mode, **Architecture Model** and **Target Device** at the topbar of Visual Studio.

![Android project in a Xamarin Solution Screenshot](../Misc/vsrunxamarinandroidapp.png)

When testing you app on iOS, I have to make sure that my machine is connected to a **Mac Build Host**. Then I can select **iPhone** or **iPhone Simulator** as Architecture Model and run the app on a Simulator or physical device.

To start the application, simply hit the **Run button** with the green play symbol.