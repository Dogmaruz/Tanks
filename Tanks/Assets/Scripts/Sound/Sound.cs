    public enum Sound
    {
        Bullet = 0,
        Explosion = 1,
        HardBullet = 2,
        DestroyTank = 3
    }

    public static class SoundExtensions
    {//Метод расширения.
        public static void Play(this Sound sound) 
        {
            SoundPlayer.Instance.Play(sound);
        }
    }

