using UnityEngine;

public partial class JumpscareController
{
    [System.Serializable]
    public struct Jumpscare
    {
        public float jumpscareTime;
        public float jumpscareMax;
        public GameObject jumpscareImage;
    }
}
