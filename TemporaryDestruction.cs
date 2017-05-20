using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Core.Plugins;
using Rocket.API.Collections;
using SDG.Unturned;
using UnityEngine;
using System.Globalization;
using Rocket.Unturned.Chat;
using Rocket.API;
using Rocket.Core;

namespace arrowkuu.temporarydestruction
{
    public class TemporaryDestruction : RocketPlugin<TemporaryDestructionConfiguration>
    {
        public static TemporaryDestruction Instance;
        private DateTime one_date;
        private DateTime second_date;
        private bool secured = false;
        private float timer;
        private float structureArmor;
        private float barricadeArmor;

        protected override void Load()
        {
            Instance = this;

            structureArmor = Provider.modeConfigData.Structures.Armor_Multiplier;
            barricadeArmor = Provider.modeConfigData.Barricades.Armor_Multiplier;
            Rocket.Core.Logging.Logger.Log("AS: " + structureArmor.ToString() + " | AB: " + barricadeArmor.ToString(), ConsoleColor.Blue);
            one_date = DateTime.ParseExact(Configuration.Instance.TimeFrom, "HH:mm:ss", CultureInfo.CurrentCulture);
            second_date = DateTime.ParseExact(Configuration.Instance.TimeTo, "HH:mm:ss", CultureInfo.CurrentCulture);
            if (one_date >= second_date) Rocket.Core.Logging.Logger.LogError("First date must be less than second date. TimeFrom < TimeTo");
        }

        protected override void Unload()
        {
            base.Unload();
        }

        public void Update()
        {
            if (Instance.State == PluginState.Loaded)
            {
                if (one_date >= second_date)
                {
                    Instance.UnloadPlugin();
                    return;
                }
                timer += Time.deltaTime;
                if (timer >= 1)
                {
                    DateTime actually = DateTime.Now;
                    if (actually > one_date && actually < second_date)
                    {
                        if (secured == true)
                        {
                            secured = false;
                            UnturnedChat.Say(Translations.Instance.Translate("protection_disabled"), Color.green);
                            Provider.modeConfigData.Structures.Armor_Multiplier = structureArmor;
                            Provider.modeConfigData.Barricades.Armor_Multiplier = barricadeArmor;
                        }
                    }
                    else
                    {
                        if (secured == false)
                        {
                            secured = true;
                            UnturnedChat.Say(Translations.Instance.Translate("protection_enabled"), Color.red);
                            Provider.modeConfigData.Structures.Armor_Multiplier = 0;
                            Provider.modeConfigData.Barricades.Armor_Multiplier = 0;
                        }
                    }
                    timer = 0;
                }
            }
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    {"protection_disabled","Structure protection has been disabled."},
                    {"protection_enabled","Structure protection has been enabled and it runs from 23:00 to 10:00."},
                };
            }
        }
    }
}
