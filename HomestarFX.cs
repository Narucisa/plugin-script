using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ScriptPortal.Vegas;

public class EntryPoint {
    public void FromVegas(Vegas vegas) {
        try {
            //--------------------------------
            // Get first video track & event
            //--------------------------------
            Track videoTrack = vegas.Project.Tracks[0];
            VideoEvent videoEvent = videoTrack.Events[0] as VideoEvent;

            //--------------------------------
            // VideoFX
            //--------------------------------

            // Invert
            Effect invert = videoEvent.Effects.AddEffect("Invert");
            invert.Parameters["Enable"].Value = true;

            // Hue
            Effect hueFx = videoEvent.Effects.AddEffect("Hue/Saturation");
            hueFx.Parameters["Hue"].Value = 120.0; // 0–360
            hueFx.Parameters["Saturation"].Value = -50.0; // -100 to 100
            hueFx.Parameters["Brightness"].Value = 20.0;  // -100 to 100
            hueFx.Parameters["Contrast"].Value = 0.5;     // -1 to 1

            // Flip
            Effect flip = videoEvent.Effects.AddEffect("Flip");
            flip.Parameters["Flip X"].Value = true;
            flip.Parameters["Flip Y"].Value = false;

            // Rotate
            Effect rotate = videoEvent.Effects.AddEffect("Rotate");
            rotate.Parameters["Angle"].Value = 45.0; // -360 to 360

            // Swirl
            Effect swirl = videoEvent.Effects.AddEffect("Swirl");
            swirl.Parameters["Amount"].Value = 5.0; // -10 to 10

            // Fisheye
            Effect fisheye = videoEvent.Effects.AddEffect("Fisheye");
            fisheye.Parameters["Strength"].Value = 0.5; // -1 to 1

            // LUT Filter
            Effect lut = videoEvent.Effects.AddEffect("LUT Filter");
            lut.Parameters["File"].Value = @"C:\LUTs\myfilter.cube";

            //--------------------------------
            // Get first audio track & event
            //--------------------------------
            Track audioTrack = vegas.Project.AudioTracks[0];
            AudioEvent audioEvent = audioTrack.Events[0] as AudioEvent;

            //--------------------------------
            // AudioFX
            //--------------------------------

            // Volume
            Effect volume = audioEvent.Effects.AddEffect("Volume");
            volume.Parameters["Gain"].Value = 5.0; // 0–10

            // Pitch Shift
            Effect pitch = audioEvent.Effects.AddEffect("Pitch Shift");
            pitch.Parameters["Semitones"].Value = -12.0; // -24 to 24

            // Reverse
            Effect reverse = audioEvent.Effects.AddEffect("Reverse");
            reverse.Parameters["Enable"].Value = true;

            // EQ (Bass, Mid, Treble)
            Effect eq = audioEvent.Effects.AddEffect("Track EQ");
            eq.Parameters["Bass"].Value = 80.0;   // 0–100
            eq.Parameters["Mid"].Value = 50.0;    // 0–100
            eq.Parameters["Treble"].Value = 70.0; // 0–100

            //--------------------------------
            // Overlay external audio (MP3)
            //--------------------------------
            string overlayPath = @"C:\Audio\overlay.mp3";
            Media overlayMedia = new Media(overlayPath);
            TrackEvent overlayEvent = audioTrack.AddAudioEvent(Timecode.FromSeconds(0), overlayMedia.Length);
            overlayEvent.AddTake(new Take(overlayMedia.Streams[0] as AudioStream));

        } catch (Exception ex) {
            MessageBox.Show("Script Error: " + ex.Message);
        }
    }
}
