# Introduction

RecordAndRepeat is a plugin for [Unity3d](https://unity3d.com/), which supports recording of custom data and playback via the Unity3d Timeline.

The core of the plugin are recordings stored as [ScriptableObjects](https://docs.unity3d.com/ScriptReference/ScriptableObject.html). You can plug them into custom scripts via the Inspector (to access and visialize the data) or play them back in a flexible matter due to an integration into the powerful Unity Timeline.

This allows for a variety of applications!

### Prototyping with recorded data

Using recorded rather than live data can drastically smoothen the development process of your prototypes. It was originally developed for recording external tracking information (OpenPose), but can handle any kind of serializable data - produced inside Unity or coming from external.

<p align="center">
  <img src="Docs/OpenPoseExample.png" width=80%  />
</p>


### Spatial User Testing and Training

To track and visually layer user movements is another great usecase. In applications like spatial user tests or trainings conducted in AR/VR, it allows to compare, analyse and quantify user behaviours.

<p align="center">
  <img src="Docs/SpatialVRUserTesting.png" width=80%  />
</p>

## Getting started

* Download the latest unity-package under [releases](https://github.com/fx-lange/unity-record-and-repeat/releases).
* Drag and drop the package into your Asset folder inside the Project Window.
* Checkout the [example folders](#Examples) with scenes showcasing recording, playback and plotting.
* Get an overview of the API in the next [section](#Usage)

## Examples

### Mouse

Two scenes showcasing recording, plotting and playback of mouse data. The custom mouse class containing positions and button state is stored as a Json String.

Folder: [Example_MouseRecording](Example_MouseRecording)

![MouseRecordingPlot](Docs/MouseRecordingPlot.png)

### Transform

In this example scene we are recording a simplified transform of the character's  head. Besides plotting the character's behaviour (via gizmos) the example also showcases replaying the recording.

Folder: [Example_TransformRecording](Example_TransformRecording)

![HeadRecordingPlot](Docs/HeadRecordingPlot.png)


## Usage

In order to hook your own data objects into RecordAndRepeat it is neccesary to extend the Recorder and Listener classes. After that you can control recording and playback via RecordAndRepeat's Inspector interface and Unity Timeline features.

### Record

It is needed to extend the `Recorder` class to define what kind of data objects you want to record to control how you want to record them (for example every frame?).

Below you find a compressed script from the [MouseRecorder](Example_MouseRecording/Scripts/MouseRecorder.cs) example, which extends `Recorder` and passes serializable data objects to the `Recorder.RecordAsJson` function.

```csharp
using RecordAndRepeat;
public class MouseRecorder : Recorder
{
    [System.Serializable]
    public class MouseData //your custom data
    {
        public Vector3 worldPos;
        public bool pressed;
    }

    //Initialize members...

    protected new void Update()
    {
        base.Update();

        if (IsRecording)
        {
            // Update mouseData...

            RecordAsJson(mouseData);
        }
    }
}
```

Controlling the recorder as well as defining the name of the recording is done via the Inspector interface after adding the extended recorder as a component.

<p align="center">
  <img src="Docs/Recording.png" width=80%  />
</p>

### Plot

If you for example want to visualize a whole recording, the getter `List<IDataFrame> Recording.DataFrames` allows to work directly with Recordings in custom scripts. In this case it is not needed to extend `DataListener`.

```csharp
foreach (DataFrame frame in recording.DataFrames)
{
    MouseData mouseData = frame.ParseFromJson<MouseData>();
    ...
}
```

*Snippet of [MouseDrawer](Example_MouseRecording/Scripts/MouseDrawer.cs) example.*

### Repeat

Recordings can be drag&dropped into RecordAndRepeat Timeline tracks and arranged via the Timeline interface. 

<p align="center">
  <img src="Docs/TimelineDragAndDrop.gif" width=80%  />
</p>

In order to receive the data during playback you only have to implement the abstract `DataListener.ProcessData(IDataFrame)` method. By extending `DataListener` and adding it as a component you can use your GameObject as a _TrackBinding_ in corresponding tracks.


```csharp
using RecordAndRepeat;
public class MouseDrawer : DataListener

  //Initialize members...
  
  public override void ProcessData(IDataFrame frame)
  {
      DataFrame jsonFrame = frame as DataFrame;
      mouseData = jsonFrame.ParseFromJson<MouseData>();
  }
  
  ...
}
```

As you can see in this snippet from the [MouseDrawer](Example_MouseRecording/Scripts/MouseDrawer.cs) script, after converting `IDataFrame` you can access the data via `DataFrame.ParseFromJson<T>()`. Make sure to use the correct type _T_.

<!-- ### Settings (should both be part of the examples + comment )

* default name
* playmode only -->