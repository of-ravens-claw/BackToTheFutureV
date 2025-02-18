﻿using GTA;
using System.Windows.Forms;
using static BackToTheFutureV.InternalEnums;
using static FusionLibrary.FusionEnums;

namespace BackToTheFutureV
{

    internal class TFCHandler : HandlerPrimitive
    {
        private int playAt;
        private bool startBlink;
        private bool hasTimeTraveled;

        public TFCHandler(TimeMachine timeMachine) : base(timeMachine)
        {
            Props.Gauges.SpawnProp();

            Props.FuelGauge.OnAnimCompleted += FuelGauge_OnAnimCompleted;

            Props.TFCOff?.SpawnProp();

            Events.OnTimeCircuitsToggle += OnTimeCircuitsToggle;
            Events.StartFuelGaugeGoDown += StartFuelGaugeGoDown;
        }

        private void StartFuelGaugeGoDown(bool startBlink)
        {
            this.startBlink = startBlink;
            hasTimeTraveled = true;

            if (startBlink)
                ToggleFuelGauge(false);

            Props.FuelGauge?.Play();
        }

        private void FuelGauge_OnAnimCompleted(AnimationStep animationStep)
        {
            ToggleFuelGauge(true);

            if (hasTimeTraveled && !Properties.HasBeenStruckByLightning)
                Properties.ReactorCharge--;

            hasTimeTraveled = false;

            if (!Properties.AreTimeCircuitsOn)
                startBlink = false;

            if (!startBlink)
                return;

            Events.StartFuelBlink?.Invoke();
        }

        private void ToggleFuelGauge(bool state)
        {
            if (state)
            {
                Props.FuelGauge[AnimationType.Rotation][AnimationStep.First][Coordinate.Y].StepRatio = 1f;
                Props.FuelGauge[AnimationType.Rotation][AnimationStep.First][Coordinate.Y].SmoothEnd = true;
            }
            else
            {
                Props.FuelGauge[AnimationType.Rotation][AnimationStep.First][Coordinate.Y].StepRatio = 0.0454f;
                Props.FuelGauge[AnimationType.Rotation][AnimationStep.First][Coordinate.Y].SmoothEnd = false;
            }
        }

        private void OnTimeCircuitsToggle(bool instant = false)
        {
            if (instant)
            {
                if (Properties.AreTimeCircuitsOn)
                {
                    Props.TFCOn?.SpawnProp();
                    Props.TFCOff?.Delete();

                    Props.GaugeGlow?.SpawnProp();

                    Props.Gauges.Play(true);
                }
                else
                {
                    Props.TFCOn?.Delete();
                    Props.TFCOff?.SpawnProp();

                    Props.GaugeGlow?.Delete();

                    Props.Gauges.Play(true);
                }

                return;
            }

            for (int i = 0; i < 3; i++)
                Props.Gauges[i].Stop();

            if (Props.FuelGauge.IsPlaying && !hasTimeTraveled)
            {
                Props.FuelGauge.Stop(!hasTimeTraveled);

                ToggleFuelGauge(true);
            }

            if (Properties.AreTimeCircuitsOn)
            {
                Props.TFCOn?.SpawnProp();
                Props.TFCOff?.Delete();

                Props.TFCHandle?.Play();

                playAt = Game.GameTime + 2000;
                IsPlaying = true;
            }
            else
            {
                IsPlaying = false;
                Props.TFCOff?.SpawnProp();
                Props.TFCOn?.Delete();

                Props.TFCHandle?.Play();

                Props.Gauge1?.Play();
                Props.Gauge2?.Play();

                if (Props.FuelGauge.CurrentRotation.Y > 0 && !hasTimeTraveled)
                    Props.FuelGauge.Play();

                Props.GaugeGlow?.Delete();
            }
        }

        public override void KeyDown(KeyEventArgs e)
        {

        }

        public override void Tick()
        {
            if (IsPlaying && Game.GameTime >= playAt)
            {
                if (Mods.Reactor == ReactorType.Nuclear)
                {
                    Sounds.PlutoniumGauge?.Play();
                }

                Props.Gauge1?.Play();
                Props.Gauge2?.Play();

                if (Properties.IsFueled)
                {
                    Props.FuelGauge?.Play();
                }

                Props.GaugeGlow?.SpawnProp();

                IsPlaying = false;
            }
        }

        public override void Stop()
        {
        }

        public override void Dispose()
        {
        }
    }
}
