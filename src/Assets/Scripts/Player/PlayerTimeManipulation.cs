using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerTimeManipulation : MonoBehaviour
    {
        void Update()
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, Input.GetKey(KeyCode.LeftShift) ? .25f : 1f, .1f);
        }
    }
}