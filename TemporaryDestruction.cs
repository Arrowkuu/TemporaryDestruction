﻿using System;
using Rocket.Core.Plugins;
using Rocket.API.Collections;
using SDG.Unturned;
using UnityEngine;
using System.Globalization;
using Rocket.Unturned.Chat;
using Rocket.API;

namespace arrowkuu.temporarydestruction
{
    public class TemporaryDestruction : RocketPlugin<TemporaryDestructionConfiguration>
    {
        public static TemporaryDestruction Instance;
        private DateTime one_date;
        private DateTime second_date;
        public bool secured = false;
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
        }

        protected override void Unload()
        {
            base.Unload();
        }

        public void Update()
        {
            if (Instance.State == PluginState.Loaded)
            {
                timer += Time.deltaTime;
                if (timer >= 1)
                {
                    DateTime actually = DateTime.Now;
                    if (one_date <= second_date)
                    {
                        if (!(actually >= one_date && actually <= second_date))
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
                                UnturnedChat.Say(Translations.Instance.Translate("protection_enabled", Configuration.Instance.TimeFrom.Remove(5), Configuration.Instance.TimeTo.Remove(5)), Color.red);
                                Provider.modeConfigData.Structures.Armor_Multiplier = 0;
                                Provider.modeConfigData.Barricades.Armor_Multiplier = 0;
                            }
                        }
                    }
                    else
                    {
                        if (!(actually >= one_date || actually <= second_date))
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
                                UnturnedChat.Say(Translations.Instance.Translate("protection_enabled", Configuration.Instance.TimeFrom.Remove(5), Configuration.Instance.TimeTo.Remove(5)), Color.red);
                                Provider.modeConfigData.Structures.Armor_Multiplier = 0;
                                Provider.modeConfigData.Barricades.Armor_Multiplier = 0;
                            }
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
                    {"protection_enabled","Structure protection has been enabled and it runs from {0} to {1}."},
                    {"protection_is_on", "Structure protection is now enabled."},
                    {"protection_is_off", "Structure protection is now disabled."}
                };
            }
        }
    }
}
