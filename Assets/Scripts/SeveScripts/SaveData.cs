
using UnityEngine;

namespace SaveData
{
    [System.Serializable]

    public class Saves
    {
        public int[] dataIntegerScores;
        public Vector3 dataTransform;

        public Saves()
        {
            dataIntegerScores = new int[] {0, 0, 0};
        }
    }

}
