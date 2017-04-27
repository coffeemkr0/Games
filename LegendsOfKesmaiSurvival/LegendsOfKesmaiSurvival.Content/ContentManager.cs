using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LegendsOfKesmaiSurvival.Content
{
    public static class ContentManager
    {
        private static Random _randomNumberGenerator = new Random();

        public static System.Resources.ResourceManager ResourceManager
        {
            get { return Properties.Resources.ResourceManager; }
        }

        public static Bitmap GetImage(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                return ResourceManager.GetObject(name) as Bitmap;
            }
            return null;
        }

        public static List<string> GetCharacters()
        {
            List<string> characters = new List<string>();
            //Not sure why, but try parents had to be true for this one
            System.Resources.ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                if (entry.Key.ToString().StartsWith("Character_"))
                {
                    characters.Add(entry.Key.ToString());
                }
            }

            characters.Sort(delegate(string x, string y)
            {
                return x.CompareTo(y);
            });
            return characters;
        }

        public static Dictionary<string, Bitmap> GetTerrains()
        {
            Dictionary<string, Bitmap> terrains = new Dictionary<string, Bitmap>();
            System.Resources.ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, false);
            foreach (DictionaryEntry entry in resourceSet)
            {
                if (entry.Key.ToString().StartsWith("Terrain_"))
                {
                    terrains.Add(entry.Key.ToString(), (Bitmap)entry.Value);
                }
            }

            terrains.OrderBy(t => t.Key);
            return terrains;
        }

        public static List<string> GetWalls()
        {
            List<string> walls = new List<string>();
            System.Resources.ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, false);
            foreach (DictionaryEntry entry in resourceSet)
            {
                if (entry.Key.ToString().StartsWith("Wall_"))
                {
                    walls.Add(entry.Key.ToString());
                }
            }

            walls.Sort(delegate(string x, string y)
            {
                return x.CompareTo(y);
            });
            return walls;
        }

        public static List<string> GetDoors()
        {
            List<string> doors = new List<string>();
            System.Resources.ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, false);
            foreach (DictionaryEntry entry in resourceSet)
            {
                if (entry.Key.ToString().StartsWith("Door_"))
                {
                    doors.Add(entry.Key.ToString());
                }
            }

            doors.Sort(delegate(string x, string y)
            {
                return x.CompareTo(y);
            });
            return doors;
        }

        public static List<string> GetUnpassableObjects()
        {
            List<string> objects = new List<string>();
            System.Resources.ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, false);
            foreach (DictionaryEntry entry in resourceSet)
            {
                if (entry.Key.ToString().StartsWith("UnpassableObject_"))
                {
                    objects.Add(entry.Key.ToString());
                }
            }

            objects.Sort(delegate(string x, string y)
            {
                return x.CompareTo(y);
            });
            return objects;
        }

        public static List<string> GetObjects()
        {
            List<string> objects = new List<string>();
            System.Resources.ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentCulture, true, false);
            foreach (DictionaryEntry entry in resourceSet)
            {
                if (entry.Key.ToString().StartsWith("Object_"))
                {
                    objects.Add(entry.Key.ToString());
                }
            }

            objects.Sort(delegate(string x, string y)
            {
                return x.CompareTo(y);
            });
            return objects;
        }

        public static string GetRandomCharacterAvatar()
        {
            List<string> characters = GetCharacters();

            int index = _randomNumberGenerator.Next(0, characters.Count - 1);

            return characters[index];
        }
    }
}
