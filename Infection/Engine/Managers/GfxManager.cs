using Aiv.Fast2D;
using System.Collections.Generic;
//using Aiv.Audio;

namespace AIV_Engine
{
    internal static class GfxManager
    {
        private static Dictionary<string, Texture> textures;
        //private static Dictionary<string, AudioClip> sounds;

        static GfxManager()
        {
            textures = new Dictionary<string, Texture>();
            //sounds = new Dictionary<string, AudioClip>();
        }

        public static Texture AddTexture(string name, string texturePath)
        {
            Texture t = new Texture(texturePath);

            if(t != null)
            {
                textures[name] = t;
            }

            return t;
        }

        public static Texture GetTexture(string name)
        {
            Texture t = null;

            if(textures.ContainsKey(name))
            {
                t = textures[name];
            }

            return t;
        }

        //public static AudioClip AddAudioClip(string name, string clipPath)
        //{
        //    AudioClip ac = new AudioClip(clipPath);

        //    if (ac != null)
        //    {
        //        sounds[name] = ac;
        //    }

        //    return ac;
        //}

        //public static AudioClip GetAudioClip(string name)
        //{
        //    AudioClip ac = null;

        //    if (sounds.ContainsKey(name))
        //    {
        //        ac = sounds[name];
        //    }

        //    return ac;
        //}

        public static void ClearAll()
        {
            textures.Clear();
            //sounds.Clear();
        }
    }
}
