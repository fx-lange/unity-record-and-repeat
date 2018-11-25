# Custom Recordings

For recording your own data there are two options:

* Extend the `Recorder` class (like the `MouseRecorder` example). All your data is stored as strings inside a `Recording`.
* If string recordings don't work for you, you can replace the `Recorder` with your own custom recorder and custom recording.

The plugin consists of two parts: 1) the recorder/recording component and 2) the playback support via Timeline.

### Record

For recording your own data you need to implement three abstract classes:

* `DataFrameBase` only contains timestamps

  Your implementation "MyCustomData" needs to add your own serializable data fields ([DataFrame](../../Source/Recording/DataFrame.cs) for reference).

* `RecordingBase` is an abstract `ScriptableObject`.

  In your "MyCustomRecording" add a private `List<MyCustomData>` and override `IEnumerable<IDataFrame> GetDataFrames()` and `void Add(IDataFrame data)` ([Recording](../../Source/Recording/Recording.cs) for reference).

* `RecorderBase` is an abstract `MonoBehaviour`.

  Your recorder implementation has to override the `RecorderBase CreateInstance()` which simply needs to return a `ScriptableObject.CreateInstance<MyCustomRecording>()`.
  Additionally, creating your "MyCustomData" objects and passing them to `RecordData(DataFrame dataFrame)` ([Recorder](../../Source/Recording/Recorder.cs) for reference).

### Repeat

Recordings stored as `ScriptableObjects` in the asset folder can be dragged&dropped into Timeline RecordingTracks/RecordingClips.

To receive your "MyCustomData" during playback a `DataListener` implementation is needed - overriding the `void ProcessData(DataFrame data)` method.

Finally, add your listener game object as a TrackBinding into RecordAndRepeat RecordingTracks. This game object is probably already part of your application - now using recorded data instead of live data.