using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace UlearnGame
{
    public static class Sound
    { 
        public static SoundPlayer winSound = new SoundPlayer(Resources.Win);
        public static SoundPlayer loseSound = new SoundPlayer(Resources.gameOverSound);
        public static SoundPlayer attackSound = new SoundPlayer(Resources.attack);
        public static SoundPlayer deathSound = new SoundPlayer(Resources.death);
        public static SoundPlayer damageSound = new SoundPlayer(Resources.damage);
        public static SoundPlayer healthSound = new SoundPlayer(Resources.bonys);

    }
}
