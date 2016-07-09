﻿using System.Collections.Generic;
using Aurora.EffectsEngine;
using NAudio.CoreAudioApi;

namespace Aurora.Profiles.Overlays
{
    public class Event_VolumeOverlay : GameEvent
    {
        public bool IsEnabled()
        {
            return true;
        }

        public void UpdateLights(EffectFrame frame)
        {
            if (Global.Configuration.volume_overlay_settings.enabled)
            {
                Queue<EffectLayer> layers = new Queue<EffectLayer>();

                EffectLayer volume_bar = new EffectLayer("Overlay - Volume Bar");

                MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
                MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
                float currentVolume = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar;

                ColorSpectrum volume_spec = new ColorSpectrum(Global.Configuration.volume_overlay_settings.low_color, Global.Configuration.volume_overlay_settings.high_color);
                volume_spec.SetColorAt(0.75f, Global.Configuration.volume_overlay_settings.med_color);

                volume_bar.PercentEffect(volume_spec, Global.Configuration.volume_overlay_settings.sequence, currentVolume, 1.0f);

                layers.Enqueue(volume_bar);

                frame.AddOverlayLayers(layers.ToArray());
            }
        }

        public void UpdateLights(EffectFrame frame, GameState new_game_state)
        {
            //No need to do anything... This doesn't have any gamestates.
            UpdateLights(frame);
        }
    }
}