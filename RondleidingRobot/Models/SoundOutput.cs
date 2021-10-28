using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace RondleidingRobot.Models
{
    public class SoundOutput : IOutput
    {
        private readonly LibVLC libVLC;
        private readonly MediaPlayer mediaPlayer;
        public SoundOutput() 
        {
            Core.Initialize();
            libVLC = new LibVLC();
            mediaPlayer = new MediaPlayer(libVLC);
        }

        public event InputEvent inputEvent;

        public void Output(string outputFile)
        {
            string filePath = FileHelper.getFileString(outputFile);

            var media = new Media(libVLC, filePath, FromType.FromPath);

            mediaPlayer.Play(media);
        }
    }
}
