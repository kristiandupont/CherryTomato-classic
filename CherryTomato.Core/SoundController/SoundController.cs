using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;
using System.Xml;
using CherryTomato.Core;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Core.SoundController
{
    public class SoundController : IPlugin, IDisposable, ICherryCommandsProvider
    {
        public bool Enabled { get; set; }

        const string pomoTickFilename = @"pomo_tick.wav";
        const string pomoStartFilename = @"pomo_start.wav";
        const string pomoRingFilename = @"pomo_ring.wav";

        private SoundSettings soundSettings = new SoundSettings();

        public SoundController()
        {
            this.Enabled = true;

            this.soundSettings.PlayRewindSound = true;
            this.soundSettings.PlayTickingSound = true;
            this.soundSettings.PlayRingSound = true;

            this.getSettingsCommand = new CherryCommand(
                "Get Sound Settings",
                ca => this.soundSettings,
                "Returns current SoundSettings object. The settings were read from configuration.");
            this.setSettingsCommand = new CherryCommand(
                "Set Sound Settings",
                ssca => this.soundSettings = (ssca as SoundSettingsCommandArgs).Settings,
                "Set the new sound settings.");
            this.playSoundCommand = new CherryCommand(
                "Play Sound",
                psca => this.Play((psca as PlaySoundCommandArgs).FileName),
                "Plays the given file. The sound is played once.");
        }

        private SoundPlayer tickingSoundPlayer;
        private SoundPlayer TickingSoundPlayer
        {
            get
            {
                return this.tickingSoundPlayer ?? (this.tickingSoundPlayer = new SoundPlayer(pomoTickFilename));
            }
        }

        private SoundPlayer startSoundPlayer;
        private SoundPlayer StartSoundPlayer
        {
            get
            {
                return this.startSoundPlayer ?? (this.startSoundPlayer = new SoundPlayer(pomoStartFilename));
            }
        }

        private SoundPlayer ringSoundPlayer;
        private SoundPlayer RingSoundPlayer
        {
            get
            {
                return this.ringSoundPlayer ?? (this.ringSoundPlayer = new SoundPlayer(pomoRingFilename));
            }
        }

        public bool Play(string filename)
        {
            if (this.Enabled)
            {
                new SoundPlayer(filename).Play();
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            this.StopPomodoroInternal(false);
        }
        
        public string PluginName { get { return "SoundController"; } }

        public void LoadConfiguration(ConfigurePluginEventArgs cpea)
        {
            XmlElement fromElement = cpea.GetMyNode(this.PluginName);
            if (fromElement != null)
            {
                this.Enabled = bool.Parse(fromElement.GetAttribute("enabled"));
                this.soundSettings = new SoundSettings()
                {
                    PlayRewindSound = bool.Parse(fromElement.GetAttribute("playRewindSound")),
                    PlayTickingSound = bool.Parse(fromElement.GetAttribute("playTickingSound")),
                    PlayRingSound = bool.Parse(fromElement.GetAttribute("playRingSound")),
                };
            }
        }

        public void SaveConfiguration(ConfigurePluginEventArgs cpea)
        {
            var pluginElement = cpea.CreateNewPluginNode(this.PluginName);
            pluginElement.SetAttribute("enabled", this.Enabled.ToString());

            pluginElement.SetAttribute("playRewindSound", this.soundSettings.PlayRewindSound.ToString());
            pluginElement.SetAttribute("playTickingSound", this.soundSettings.PlayTickingSound.ToString());
            pluginElement.SetAttribute("playRingSound", this.soundSettings.PlayRingSound.ToString());
        }

        public void StartPomodoroInternal()
        {
            if (!this.Enabled) return;

            if (this.soundSettings.PlayRewindSound || this.soundSettings.PlayTickingSound)
            {
                // Play in separate thread because ticking sound should start _after_ rewind sound.
                // This thread will end just after the PlaySync() ended.
                new Thread(() =>
                {
                    if (this.soundSettings.PlayRewindSound)
                    {
                        // This line will play the sound in that thread.
                        this.StartSoundPlayer.PlaySync();
                    }

                    if (this.soundSettings.PlayTickingSound)
                    {
                        // This line will play the sound in a new thread.
                        this.TickingSoundPlayer.PlayLooping();
                    }
                }).Start();
            }
        }

        public void StopPomodoroInternal(bool completed)
        {
            if (!this.Enabled) return;
            
            this.TickingSoundPlayer.Stop();

            if (completed)
            {
                if (this.soundSettings.PlayRingSound)
                {
                    this.RingSoundPlayer.Play();
                }
            }
        }

        public void TieEvents(PluginRepository plugins)
        {
            plugins.CherryEvents.Subscribe("Pomodoro Started", this.StartPomodoroInternal);

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Pomodoro Finishing",
                ea => this.StopPomodoroInternal((ea as PomodoroEventArgs).PomodoroData.Successful)));

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Load Plugin Configuration Event",
                cpea => this.LoadConfiguration(cpea as ConfigurePluginEventArgs)));
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Save Plugin Configuration Event",
                cpea => this.SaveConfiguration(cpea as ConfigurePluginEventArgs)));
        }

        private CherryCommand getSettingsCommand;
        private CherryCommand setSettingsCommand;
        private CherryCommand playSoundCommand;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.getSettingsCommand;
            yield return this.setSettingsCommand;
            yield return this.playSoundCommand;
        }
    }
}

