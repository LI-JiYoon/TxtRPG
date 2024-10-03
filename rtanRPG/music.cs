using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Threading;

//Install-Package NAudio

namespace rtanRPG
{
    public static class music
    {
        static IWavePlayer bgmPlayer;
        static IWavePlayer soundEffectPlayer;
        static AudioFileReader bgm;
        static AudioFileReader soundEffect;

        

        public static void bgmPlay(string filename) 
        { if (bgmPlayer == null)
            {
                bgmPlayer = new WaveOutEvent();
                bgm = new AudioFileReader(@"" + filename); // 배경음악 파일 경로
                bgmPlayer.Init(bgm);
            }
          bgmPlayer.Play();
        }
        public static void soundEffectPlay(string filename)
        {
            soundEffectPlayer = new WaveOutEvent(); // 효과음을 위한 별도 플레이어
            soundEffect = new AudioFileReader(filename); // 효과음 파일 경로
            soundEffectPlayer.Init(soundEffect);
            soundEffectPlayer.Play();
        }
        public static void StopBGM()
        {
            if (bgmPlayer != null)
            {
                bgmPlayer.Stop();
                bgm.Position = 0;  // 음악을 처음부터 다시 재생하려면 Position을 0으로 설정
            }
        }

    }
}
