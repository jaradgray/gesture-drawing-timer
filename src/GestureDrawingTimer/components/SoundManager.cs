using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace GestureDrawingTimer.components
{
    /// <summary>
    /// The SoundManager class provides methods for playing audio
    /// </summary>
    public class SoundManager
    {
        /// <summary>
        /// Enumeration for each of the different sounds the SoundManager class can play
        /// </summary>
        public enum Sound
        {
            Beep1,
            Beep2
        }

        // Instance variables
        private SoundPlayer mPlayer;

        // Constructor
        public SoundManager()
        {
            mPlayer = new SoundPlayer();
        }

        // Public Methods
        public void Play(Sound snd)
        {
            System.IO.Stream stream = null;
            switch (snd)
            {
                case Sound.Beep1:
                    stream = Properties.Resources.beep_24;
                    break;
                case Sound.Beep2:
                    stream = Properties.Resources.beep_22;
                    break;
            }

            if (stream == null) return;
            mPlayer.Stream = stream;
            mPlayer.Play();
        }
    }
}
