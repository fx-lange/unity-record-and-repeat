// MIT License

// Copyright (c) 2018 Felix Lange 

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RecordAndRepeat.Examples
{
    [RequireComponent(typeof(Recorder))]
    [ExecuteInEditMode]
    public class MouseRecorder : MonoBehaviour
    {
        
        private Recorder recorder;
        private MouseData mouseData = new MouseData();

        void Awake()
        {
            recorder = GetComponent<Recorder>();
            recorder.DefaultRecordingName = "New Mouse Recording";
        }

        void Update()
        {
            Vector3 mouse = Input.mousePosition;
            mouse.z = 10;

            mouseData.worldPos = Camera.main.ScreenToWorldPoint(mouse);
            mouseData.pressed = Input.GetMouseButton(0);

            if (recorder.IsRecording)
            {
                recorder.RecordAsJson(mouseData);
            }
        }

        void OnDrawGizmos()
        {
            if (recorder.IsRecording)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.grey;
            }
            Gizmos.DrawWireSphere(mouseData.worldPos, mouseData.pressed ? 1.2f : 1f);
        }
    }
}
