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

        static string relativePath = @"..\..\..\music"; // 상대경로

        // 절대 경로로 변환
        static string fullPath = Path.GetFullPath(relativePath);
      // static string folderName = "music"; // 폴더이름



        public static void bgmPlay(string filename) 
        { if (bgmPlayer == null)
            {
                string bgmFilePath = Path.Combine(fullPath, filename);
                bgmPlayer = new WaveOutEvent();
                bgm = new AudioFileReader(bgmFilePath); // 배경음악 파일 경로
                bgmPlayer.Init(bgm);
            }
          bgmPlayer.Play();
        }
        public static void soundEffectPlay(string filename)
        {
            string soundEffectFilePath = Path.Combine(fullPath, filename);
            soundEffectPlayer = new WaveOutEvent(); // 효과음을 위한 별도 플레이어
            soundEffect = new AudioFileReader(soundEffectFilePath); // 효과음 파일 경로
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
