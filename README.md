# Record and Play

This work in progress plugin for Unity supports recording and playback of custom data via Unity Timeline.

Using recorded rather than live data smoothens the development process of your application. 
It was originally developed for external, camera based tracking data (OpenPose) but can generally handle any kind of serializable data - in game or external.

The core of this plugin are recordings stored as [ScriptableObjects](https://docs.unity3d.com/ScriptReference/ScriptableObject.html) which can be dragged&dropped into Timeline Clips. 

To account for the technical limitations of serialization the architecture is based on several abstract classes which need to be implemented fitting your data. 
However, you can checkout the `StringRecorder` implementation: Either just as a reference or to work with it directly by serializing your data - for example via [JsonUtility](https://docs.unity3d.com/ScriptReference/JsonUtility.html).

## Architecture and Usage

The architecture consists of two parts: 1) the recorder/recording component and 2) the playback support via Timeline.

### Record 

For recording your own data you need to implement three abstract classes - please use the `StringRecorder` as a reference: 

* `DataFrame` only contains timestamps. 

  Your implementation "MyCustomData" only needs to add your own serilizable data fields.
  
* `Recording` is an abstract `ScriptableObject`. 
  
  In your "MyCustomRecording" add a private `List<MyCustomData>` and override `IEnumerable<DataFrame> GetDataFrames()` and `void Add(DataFrame data)`. 
  
* `Recorder` is an abstract `MonoBehaviour`.

  Your recorder implementation has to override the `Recording CreateInstance()` which simply needs to return a `ScriptableObject.CreateInstance<MyCustomRecording>()`. 
  Additionally, creating your "MyCustomData" objects and passing them to `RecordData(DataFrame dataFrame)`.
  
### Play

Recordings stored as `ScriptableObjects` in the asset folder can be dragged&dropped into Timeline RecordingTracks/RecordingClips. 

To receive your "MyCustomData" during playback a `DataListener` implementation is needed - overriding the `void ProcessData(DataFrame data)` method. 

Finally, add your listener game object as a TrackBinding into RecordAndPlay RecordingTracks. This game object is probably already part of your application - now using recorded instead of live data.
