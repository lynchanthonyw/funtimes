using System.Collections.Generic;
using System.Linq;

namespace WalkingAround
{
    public static class StaticResources
    {
        public static Dictionary<string, string> _nodetypes = new Dictionary<string, string>();
        public static Dictionary<string, string> _models = new Dictionary<string, string>();

        static StaticResources()
        {
            _nodetypes.Add("Water", @"pack://application:,,,/resources/Water.png");
            _nodetypes.Add("Wall", @"pack://application:,,,/resources/Wall.png");
            _nodetypes.Add("Mud", @"pack://application:,,,/resources/Mud.png");
            _nodetypes.Add("Grass", @"pack://application:,,,/resources/Grass.png");
            _nodetypes.Add("Ice", @"pack://application:,,,/resources/Ice.png");
            _nodetypes.Add("Sand", @"pack://application:,,,/resources/Sand.png");
            _nodetypes.Add("Hill", @"pack://application:,,,/resources/Hill.png");
            _nodetypes.Add("Mountains", @"pack://application:,,,/resources/unknown.png");
            _nodetypes.Add("Unkown", @"pack://application:,,,/resources/unknown.png");

            _models.Add("Stand", @"pack://application:,,,/resources/models/Char_Stand.png");
            _models.Add("WalkL", @"pack://application:,,,/resources/models/Char_Walk_L.png");
            _models.Add("WalkR", @"pack://application:,,,/resources/models/Char_Walk_R.png");
        }

        public static string Get(string type, string key)
        {
            return GetList(type)[key];
        }

        public static string Get(string type, int key)
        {
            Dictionary<string, string> list = GetList(type);

            if (key < list.Count)
            {
                return list.Values.ElementAt(key);
            }
            else
            {
                return list.Values.ElementAt(list.Count - 1);
            }
        }

        private static Dictionary<string, string> GetList(string type)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();

            switch (type.ToLower())
            {
                case "node":
                    list = _nodetypes;
                    break;

                case "model":
                    list = _models;
                    break;

                default:
                    break;
            }
            return list;
        }

        public static int Count(string type)
        {
            return GetList(type).Count;
        }
    }
}