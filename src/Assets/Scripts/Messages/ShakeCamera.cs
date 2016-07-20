namespace Assets.Scripts.Messages
{
    public class ShakeCamera
    {
        public float ShakeIntensity;
        public float ShakeDecay;

        public ShakeCamera(float shakeIntensity = .3f, float shakeDecay = .02f)
        {
            ShakeIntensity = shakeIntensity;
            ShakeDecay = shakeDecay;
        }
    }
}