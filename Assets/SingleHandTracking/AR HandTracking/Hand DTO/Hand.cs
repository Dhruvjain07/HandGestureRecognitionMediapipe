using UnityEngine;
namespace MediapipeHandTracking {
    public class Hand {
        private Vector3[] landmarks, landmarksCP = default;
        public float currentDepth = 0.1f;
        private Camera cam;

        public Hand() {
            landmarks = new Vector3[21];
            cam = Camera.main;
        }

        private Hand(Vector3[] landmarks) {
            this.landmarks = landmarks;
        }

        public void ParseFrom(float[] arr, bool isHandRectChange, float c) {
            if (null == arr || arr.Length < 63) return;
            // depths of points in the wrists
           
            for (int i = 0; i < 21; i++) {
                float xScreen = Screen.width * ((arr[i * 3 + 1] - 0.5f * (1 - c)) / c);
                float yScreen = Screen.height * (arr[i * 3]);
                this.landmarks[i] = cam.ScreenToWorldPoint(new Vector3(xScreen, yScreen, arr[i * 3 + 2] / 80 + currentDepth));
            }

            if (landmarksCP == default) {
                landmarksCP = new Vector3[21];
                landmarksCP = (Vector3[])landmarks.Clone();
            } else {
                // vibration keeps the old landmark
                if (isVibrate(0.01f)) {
                    landmarks = (Vector3[])landmarksCP.Clone();
                } else { // save landmark when not shaken
                    landmarksCP = (Vector3[])landmarks.Clone();
                }
            }
        }

        public bool isVibrate(float deltaVibrate) {
            for (int i = 0; i < 21; i++) {
                if (Vector3.Distance(landmarksCP[i], landmarks[i]) < deltaVibrate) return true;
            }
            return false;
        }

        public Vector3 GetLandmark(int index) => this.landmarks[index];
        public Vector3[] GetLandmarks() => this.landmarks;
    }
}