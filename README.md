# Record and Play

This work in progress plugin for Unity supports recording and playback of custom data via Unity Timeline.

Using recorded rather than live data can drastically smoothen the development process of your prototypes. It was originally developed for external, camera based tracking data (OpenPose) but can handle any kind of serializable data - in game or external.

The core of this plugin are recordings stored as [ScriptableObjects](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) which can be dragged&dropped into Timeline Clips. 

To account for the technical limitations of serialization the architecture is based on several abstract classes which need to be implemented fitting your data. However, the `StringRecorder` implementation should help to get you started: Either use it as a reference or work with it directly. 

## Getting started

* Download the latest unity-package under [releases](https://github.com/fx-lange/unity-record-and-play/releases).
* Checkout the Example folder. Under Scenes you find a scene for a recording and one for playback of mouse interactions. The `MouseRecorder` script utilizes the `StringRecorder` and [JsonUtility](https://docs.unity3d.com/ScriptReference/JsonUtility.html) to serialize mouse data and record them as strings.
* For recording your own data there are two options: 
  * Build a recorder on top of the `StringRecorder` (like the `MouseRecorder`)
  * Replace the `StringRecorder` with your own custom recorder and custom recording. Check the next [section](#custom-recordings) for more details. 

## Custom Recordings

The plugin consists of two parts: 1) the recorder/recording component and 2) the playback support via Timeline.

### Record 

For recording your own data you need to implement three abstract classes - please use the `StringRecorder` as a reference: 

* `DataFrame` only contains timestamps 

  Your implementation "MyCustomData" needs to add your own serializable data fields ([StringData](https://github.com/fx-lange/unity-record-and-play/tree/master/src/Recorders/StringRecorder/StringData.cs) for reference). 
  
* `Recording` is an abstract `ScriptableObject`. 
  
  In your "MyCustomRecording" add a private `List<MyCustomData>` and override `IEnumerable<DataFrame> GetDataFrames()` and `void Add(DataFrame data)` ([StringRecording](https://github.com/fx-lange/unity-record-and-play/tree/master/src/Recorders/StringRecorder/StringRecording.cs) for reference). 
  
* `Recorder` is an abstract `MonoBehaviour`.

  Your recorder implementation has to override the `Recording CreateInstance()` which simply needs to return a `ScriptableObject.CreateInstance<MyCustomRecording>()`. 
  Additionally, creating your "MyCustomData" objects and passing them to `RecordData(DataFrame dataFrame)` ([StringRecorder](https://github.com/fx-lange/unity-record-and-play/tree/master/src/Recorders/StringRecorder/StringRecorder.cs) for reference). 
  
### Play

Recordings stored as `ScriptableObjects` in the asset folder can be dragged&dropped into Timeline RecordingTracks/RecordingClips. 

To receive your "MyCustomData" during playback a `DataListener` implementation is needed - overriding the `void ProcessData(DataFrame data)` method. 

Finally, add your listener game object as a TrackBinding into RecordAndPlay RecordingTracks. This game object is probably already part of your application - now using recorded data instead of live data.
