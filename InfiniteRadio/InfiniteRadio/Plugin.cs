using Exiled.API.Features;
using Exiled.API.Enums;
using Exiled.API.Features.Items;
using MEC;
using System;
using System.Collections.Generic;

namespace InfiniteRadio
{
    public class Plugin : Plugin<Config>
    {
        private CoroutineHandle _coroutineHandle;

        public override string Name => "InfiniteRadio";
        public override string Author => "cyberco";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 0, 0);

        public override void OnEnabled()
        {
            if (!Config.IsEnabled)
                return;

            Exiled.Events.Handlers.Server.WaitingForPlayers += OnWaitingForPlayers;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Timing.KillCoroutines(_coroutineHandle);

            Exiled.Events.Handlers.Server.WaitingForPlayers -= OnWaitingForPlayers;

            base.OnDisabled();
        }

        private void OnWaitingForPlayers()
        {
            _coroutineHandle = Timing.RunCoroutine(InfiniteBatteryRoutine());
        }
        private IEnumerator<float> InfiniteBatteryRoutine()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(1f);

                foreach (Player player in Player.List)
                {
                    foreach (var item in player.Items)
                    {
                        if (item is Radio radio)
                        {
                            radio.Base.BatteryPercent = (byte)100;
                        }
                    }
                }
            }
        }
    }
}
