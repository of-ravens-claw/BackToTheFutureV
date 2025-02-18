﻿using FusionLibrary;
using KlangRageAudioLibrary;
using static BackToTheFutureV.InternalEnums;
using static FusionLibrary.FusionEnums;

namespace BackToTheFutureV
{
    internal class MrFusionRefillPlayer : Players.Player
    {
        private readonly AnimateProp _mrFusion;
        private readonly AnimateProp _mrFusionHandle;

        private readonly AudioPlayer _mrfusionOpen;
        private readonly AudioPlayer _mrfusionClosed;

        public MrFusionRefillPlayer(TimeMachine timeMachine) : base(timeMachine)
        {
            _mrFusion = new AnimateProp(ModelHandler.BTTFMrFusion, Vehicle, "mr_fusion");
            _mrFusion[AnimationType.Rotation][AnimationStep.First][Coordinate.X].Setup(true, false, -70, 0, 1, 140, 1, false);
            _mrFusion.OnAnimCompleted += MrFusion_OnAnimCompleted;
            _mrFusion.SpawnProp();

            _mrFusionHandle = new AnimateProp(ModelHandler.BTTFMrFusionHandle, Vehicle, "mr_fusion_handle");
            _mrFusionHandle[AnimationType.Rotation][AnimationStep.First][Coordinate.X].Setup(true, true, 0, 30, 1, 140, 1, false);
            _mrFusionHandle.OnAnimCompleted += MrFusionHandle_OnAnimCompleted;
            _mrFusionHandle.SpawnProp();

            _mrfusionOpen = Sounds.AudioEngine.Create("general/mrfusionOpen.wav", Presets.Exterior);
            _mrfusionClosed = Sounds.AudioEngine.Create("general/mrfusionClose.wav", Presets.Exterior);

            _mrfusionOpen.Volume = 0.4f;
            _mrfusionClosed.Volume = 0.4f;

            _mrfusionOpen.SourceBone = "mr_fusion";
            _mrfusionClosed.SourceBone = "mr_fusion";
        }

        private void MrFusionHandle_OnAnimCompleted(AnimationStep animationStep)
        {
            if (Properties.ReactorState == ReactorState.Opened)
            {
                _mrFusion.Play();
            }
            else
            {
                Particles.MrFusionSmoke?.Stop();

                IsPlaying = false;
                OnPlayerCompleted?.Invoke();
            }
        }

        private void MrFusion_OnAnimCompleted(AnimationStep animationStep)
        {
            if (Properties.ReactorState == ReactorState.Closed)
            {
                _mrFusionHandle.Play();
            }
            else
            {
                IsPlaying = false;
                OnPlayerCompleted?.Invoke();
            }
        }

        public override void Play()
        {
            IsPlaying = true;
            _mrfusionClosed?.Stop();
            _mrfusionOpen?.Stop();

            if (Properties.ReactorState == ReactorState.Opened)
            {
                _mrFusionHandle.Play();
                _mrfusionOpen.Play();
                Particles.MrFusionSmoke?.Play();
            }
            else
            {
                _mrFusion.Play();
                _mrfusionClosed.Play();
            }
        }

        public override void Tick()
        {

        }

        public override void Stop()
        {

        }

        public override void Dispose()
        {
            Particles.MrFusionSmoke?.Stop();
            _mrFusion.Dispose();
            _mrFusionHandle.Dispose();
            _mrfusionOpen?.Dispose();
            _mrfusionClosed?.Dispose();
        }
    }
}
