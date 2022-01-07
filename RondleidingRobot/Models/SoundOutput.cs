using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobot.Models
{
    //een output die een muziek bestand kan afspelen via vlc media player
    public class SoundOutput : IOutput
    {
        private readonly LibVLC libVLC;
        private readonly MediaPlayer mediaPlayer;
        //vlc mediaplayer aanmaken
        public SoundOutput() 
        {
            Core.Initialize();
            libVLC = new LibVLC();
            mediaPlayer = new MediaPlayer(libVLC);
        }

        public event InputEvent inputEvent;

        //speelt een geluidsbestand af met de naam outputFile
        public void Output(string outputFile)
        {
            string filePath = FileHelper.getFileString(outputFile);

            var media = new Media(libVLC, filePath, FromType.FromPath);

            mediaPlayer.Play(media);
        }
    }
}
