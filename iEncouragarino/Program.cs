using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace iEncouragarino
{
    class Program
    {
        private static Obj_AI_Hero player;
        private static int deathCount;
        private static float tickLastDeath;
        private static bool Encourage;
        private static Menu menu;
        private static bool incomplete;
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += GameLoad;
        }

        static void GameLoad(EventArgs args)
        {
            player = ObjectManager.Player;

            Encourage = true;
            deathCount = player.Deaths;
            incomplete = false;

            //Initialize Menu

            menu = new Menu("iEncouragarino", "iEncouragarino", true);
            menu.AddItem(new MenuItem("always Encouragarino", "always Encouragarino").SetValue(true));
            menu.AddItem(new MenuItem("allChat", "In All chat").SetValue(false));
            menu.AddToMainMenu();

            Game.OnUpdate += GameUpdateCheck;


            Game.PrintChat("<font color = \"#D6B600\">iEncouragarino by CptSparkles</font>");
        }

        static void CustomSayAll(string whatToSay)
        {
            if (menu.Item("allChat").GetValue<bool>()) // all chat or no all chat
            {
                Game.Say("/all " + whatToSay);
            }
            else
            {
                Game.Say(whatToSay);
            }
        }

        static string AddGs(string whatToSay)
        {
            var random = new Random();
            var randomNumber = random.Next(4);
            for (var i = 0; i != randomNumber; i++)
            {
                var randomNumber3 = random.Next(2);
                if (randomNumber3 == 0)
                {
                    whatToSay = whatToSay + "g";
                }
                else
                {
                    whatToSay = whatToSay + "G";
                }
            }
            return whatToSay;

        }

        static void GameUpdateCheck(EventArgs args)
        {

            string whatToSay;

            if (player.IsDead)
            {

                if (player.Deaths > deathCount)
                {
                    deathCount = player.Deaths;
                    tickLastDeath = Game.Time;
                    Encourage = false;
                }
            }

            if (incomplete && (tickLastDeath + 3.5f) <= Game.Time)
            {
                whatToSay = "lag ";
                incomplete = false;
                if (menu.Item("randomG").GetValue<bool>())
                {
                    whatToSay = AddGs(whatToSay);
                }
                CustomSayAll(whatToSay);
            }

            if ((tickLastDeath + 1.5f) <= Game.Time && !Encourage && !incomplete)
            {
                Encourage = true;
                var random = new Random();
                var randomNumber = random.Next(menu.Item("alwaysEncourage").GetValue<bool>() ? 7 : 14);


                switch (randomNumber)
                {
                    case 0:
                        whatToSay = "We got dis guys!";
                        break;
                    case 1:
                        whatToSay = "Let's do this, Never give up!";
                        break;
                    case 2:
                        whatToSay = "No Flame, Great game";
                        break;
                    case 3:
                        whatToSay = "Let's not give up!";
                        break;
                    case 4:
                        whatToSay = ":P One death isn't the end";
                        break;
                    case 5:
                        whatToSay = "I love people";
                        break;
                    case 6:
                        whatToSay = "";
                        incomplete = true;
                        break;
                    default:
                        whatToSay = "";
                        break;
                }

                {
                    whatToSay = whatToSay + " ";
                }



                if (randomNumber < 7)
                {
                    CustomSayAll(whatToSay);
                }

            }

        }
    }
}
