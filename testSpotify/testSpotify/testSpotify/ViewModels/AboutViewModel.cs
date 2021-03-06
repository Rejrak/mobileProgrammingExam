﻿using System.Windows.Input;
using Xamarin.Forms;
using SpotifyAPI.Web.Models;
using System.Collections.Generic;

namespace testSpotify.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {

        private PlaybackContext playback;
        public ICommand TestAuthCommand { get; }

        public AboutViewModel()
        {
            Title = "About";
            TestAuthCommand = new Command(() => TestAuth());
        }

        private async void TestAuth()
        {

            //Problemi 
            // - funziona solo quando spotify è già in riproduzione
            // - funziona solo quando spotify è aperto in uno dei qualsiasi dispositivi a cui è collegato l'account

            // Istanzio qui il playback dato che ad ogni click deve scaricare le informazioni sul brano
            playback = await SpotifyApi.GetPlaybackAsync();

            if (playback.Context != null || playback.Item != null)
            {
                if (playback.IsPlaying)
                {
                    ErrorResponse x = await SpotifyApi.PausePlaybackAsync();
                }
                else
                {
                    ErrorResponse x = await SpotifyApi.ResumePlaybackAsync(playback.Device.Id, string.Empty,
                        new List<string>() { playback.Item.Uri }, "", playback.ProgressMs);
                }
            }
        }
    }
}
