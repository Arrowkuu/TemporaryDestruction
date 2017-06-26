using Rocket.API;
using Rocket.Unturned.Chat;
using System.Collections.Generic;
using UnityEngine;

namespace arrowkuu.temporarydestruction
{
    public class CommandProtection : IRocketCommand
    {
        public List<string> Aliases
        {
            get
            {
                return new List<string>();
            }
        }

        public AllowedCaller AllowedCaller
        {
            get
            {
                return AllowedCaller.Both;
            }
        }

        public string Help
        {
            get
            {
                return "Check structure protection";
            }
        }

        public string Name
        {
            get
            {
                return "protection";
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>() { "td.protection" };
            }
        }

        public string Syntax
        {
            get
            {
                return "";
            }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if(TemporaryDestruction.Instance.secured)
            {
                UnturnedChat.Say(caller, TemporaryDestruction.Instance.Translate("protection_is_on"), Color.yellow);
                return;
            }
            else
            {
                UnturnedChat.Say(caller, TemporaryDestruction.Instance.Translate("protection_is_off"), Color.yellow);
                return;
            }
        }
    }
}
