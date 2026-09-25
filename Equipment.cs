using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CP48
{
    public enum ItemCategory
    {
        Ammo,
        Weapon_Options,
        Armor,
        Fashion,
        Tools,
        Personal_Electronics,
        Data_Systems,
        Communications,
        Surveillance,
        Entertainment,
        Security,
        Medical,
        Furnishings,
        Vehicles,
        Lifestyle,
        Groceries,
        Housing        
    }

    public enum WeaponCategory
    {
        Light_Handgun,
        Medium_Handgun,
        Heavy_Handgun,
        Very_Heavy_Handgun,
        Light_SMG,
        Medium_SMG,
        Heavy_SMG,
        Assault_Rifle,
        Shotgun,
        Heavy_Weapon,
        Exotic,
        Melee
    }

    public interface IBuyable
    {
        int Price { get; set; }
    }
    [System.Serializable]
    public class Item:IBuyable
    {
        public string Name;
        public ItemCategory Type;
        public int Price { get; set; }
    }
    [System.Serializable]
    public class Armor:Item
    {
        public int Head;
        public int Torso;
        public int Arms;
        public int Legs;
        public int EV;
        public bool IsHard;
    }
    [System.Serializable]
    public class Weapon:IBuyable
    {
        public string Name;
        public WeaponCategory Type;
        public int Price { get; set; }
        public string WA;
        public string Concealability;
        public string Availability;
        public string Damage;
        public string AmmoType;
        public string Shots;
        public string RoF;
        public string Reliability;
        public string Range;

        public string DamageAndAmmo
        {
            get
            {
                string res = "";
                res += Damage;
                if(!string.IsNullOrEmpty(AmmoType))
                {
                    res += "(" + AmmoType + ")";
                }
                return res;
            }
        }
        public string CategoryString
        {
            get 
            {
                switch (Type)
                {
                    case WeaponCategory.Light_Handgun:
                    case WeaponCategory.Medium_Handgun:
                    case WeaponCategory.Heavy_Handgun:
                    case WeaponCategory.Very_Heavy_Handgun:
                        return "P";
                    case WeaponCategory.Light_SMG:
                    case WeaponCategory.Medium_SMG:
                    case WeaponCategory.Heavy_SMG:
                        return "SMG";
                    case WeaponCategory.Assault_Rifle:
                        return "RIF";
                    case WeaponCategory.Shotgun:
                        return "SHT";
                    case WeaponCategory.Heavy_Weapon:
                        return "HVY";
                    case WeaponCategory.Exotic:
                        return "EX";
                    case WeaponCategory.Melee:
                        return "MEL";
                }
                return "UNK";
            }
        }
    }
    [Serializable]
    public class Equipment
    {
        public List<Weapon> Weapons = new List<Weapon>();
        public List<Item> Items = new List<Item>();
    }

    public static class EquipmentManager
    {
        public static Equipment DB = new Equipment();
        private const string itemsXML = @"Data/Items.XML";
        private const string weaponsXML = @"Data/Weapons.XML";

        static EquipmentManager()
        {
          //  LoadItemsXML();
          //  LoadWeaponsXML();
           // CreateTestWeapons();
           // SaveWeaponsXMLTest();
            CreateTestItems();
            SaveItemsXMLTest();
        }

        private static void LoadItemsXML()
        {
            if (DB.Items == null)
                DB.Items = new List<Item>();
            DB.Items = JsonConvert.DeserializeObject<List<Item>>(System.IO.File.ReadAllText(itemsXML));
        }
        private static void LoadWeaponsXML()
        {
            if (DB.Weapons == null)
                DB.Weapons = new List<Weapon>();
            DB.Weapons = JsonConvert.DeserializeObject<List<Weapon>>(System.IO.File.ReadAllText(weaponsXML));
        }
        private static void SaveItemsXMLTest()
        {
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Include;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            settings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full;
            settings.TypeNameHandling = TypeNameHandling.All;
            string serData = JsonConvert.SerializeObject(DB.Items, Formatting.Indented, settings);
            System.IO.File.WriteAllText(itemsXML, serData);
        }
        private static void SaveWeaponsXMLTest()
        {
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Include;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            string serData = JsonConvert.SerializeObject(DB.Weapons, Formatting.Indented, settings);
            System.IO.File.WriteAllText(weaponsXML, serData);
        }
        private static void CreateTestItems()
        {
            Item testItem = new Item() { Name = "Pants (Generic)", Type = ItemCategory.Fashion, Price = 20 };
            Item testItem1 = new Item() { Name = "Top (Generic)", Type = ItemCategory.Fashion, Price = 15 };
            Item testItem2 = new Item() { Name = "Jacket (Generic)", Type = ItemCategory.Fashion, Price = 35 };
            Item testItem3 = new Item() { Name = "Footwear (Generic)", Type = ItemCategory.Fashion, Price = 25 };
            Item testItem4 = new Item() { Name = "Jewelry (Generic)", Type = ItemCategory.Fashion, Price = 50 };
            Item testItem5 = new Item() { Name = "Mirrorshades (Generic)", Type = ItemCategory.Fashion, Price = 25 };
            Item testItem6 = new Item() { Name = "Contact Lenses (Generic)", Type = ItemCategory.Fashion, Price = 100 };
            Item testItem7 = new Item() { Name = "Glasses (Generic)", Type = ItemCategory.Fashion, Price = 50 };
            DB.Items.Add(testItem);
            DB.Items.Add(testItem1);
            DB.Items.Add(testItem2);
            DB.Items.Add(testItem3);
            DB.Items.Add(testItem4);
            DB.Items.Add(testItem5);
            DB.Items.Add(testItem6);
            DB.Items.Add(testItem7);
            Item testItem8 = new Item() { Name = "Techscanner", Type = ItemCategory.Tools, Price = 600 };
            Item testItem9 = new Item() { Name = "Cutting Torch", Type = ItemCategory.Tools, Price = 40 };
            Item testItem10 = new Item() { Name = "Tech Toolkit", Type = ItemCategory.Tools, Price = 100 };
            Item testItem11 = new Item() { Name = "B&E Tools", Type = ItemCategory.Tools, Price = 120 };
            Item testItem12 = new Item() { Name = "Electronic Toolkit", Type = ItemCategory.Tools, Price = 100 };
            Item testItem13 = new Item() { Name = "Protective Googles", Type = ItemCategory.Tools, Price = 20 };
            Item testItem14 = new Item() { Name = "Flashtube", Type = ItemCategory.Tools, Price = 2 };
            Item testItem15 = new Item() { Name = "Glowstick", Type = ItemCategory.Tools, Price = 1 };
            Item testItem16 = new Item() { Name = "Paint, ltr", Type = ItemCategory.Tools, Price = 10 };
            Item testItem17 = new Item() { Name = "Flash Tape, mtr", Type = ItemCategory.Tools, Price = 10 };
            Item testItem18 = new Item() { Name = "Rope, mtr", Type = ItemCategory.Tools, Price = 2 };
            Item testItem19 = new Item() { Name = "Breathing Mask", Type = ItemCategory.Tools, Price = 30 };
            DB.Items.Add(testItem8);
            DB.Items.Add(testItem9);
            DB.Items.Add(testItem10);
            DB.Items.Add(testItem11);
            DB.Items.Add(testItem12);
            DB.Items.Add(testItem13);
            DB.Items.Add(testItem14);
            DB.Items.Add(testItem15);
            DB.Items.Add(testItem16);
            DB.Items.Add(testItem17);
            DB.Items.Add(testItem18);
            DB.Items.Add(testItem19);
            Item testItem20 = new Item() { Name = "Holo Generator", Type = ItemCategory.Personal_Electronics, Price = 500 };
            Item testItem21 = new Item() { Name = "Video Board, mtr2", Type = ItemCategory.Personal_Electronics, Price = 100 };
            Item testItem22 = new Item() { Name = "Data Chip", Type = ItemCategory.Personal_Electronics, Price = 10 };
            Item testItem23 = new Item() { Name = "Logcompass", Type = ItemCategory.Personal_Electronics, Price = 50 };
            Item testItem24 = new Item() { Name = "Digital Recorder", Type = ItemCategory.Personal_Electronics, Price = 300 };
            Item testItem25 = new Item() { Name = "Digital Camera", Type = ItemCategory.Personal_Electronics, Price = 150 };
            Item testItem26 = new Item() { Name = "VideoCam", Type = ItemCategory.Personal_Electronics, Price = 800 };
            Item testItem27 = new Item() { Name = "V/A Tape Player", Type = ItemCategory.Personal_Electronics, Price = 40 };
            Item testItem28 = new Item() { Name = "Videotape", Type = ItemCategory.Personal_Electronics, Price = 4 };
            Item testItem29 = new Item() { Name = "Pocket TV", Type = ItemCategory.Personal_Electronics, Price = 80 };
            Item testItem30 = new Item() { Name = "Digital Chip Player", Type = ItemCategory.Personal_Electronics, Price = 150 };
            Item testItem31 = new Item() { Name = "Digital Music Chip", Type = ItemCategory.Personal_Electronics, Price = 20 };
            Item testItem32 = new Item() { Name = "Electric Guitar", Type = ItemCategory.Personal_Electronics, Price = 250 };
            Item testItem33 = new Item() { Name = "Electric Keyboard", Type = ItemCategory.Personal_Electronics, Price = 700 };
            Item testItem34 = new Item() { Name = "Drum Synth", Type = ItemCategory.Personal_Electronics, Price = 600 };
            Item testItem35 = new Item() { Name = "Amplifier", Type = ItemCategory.Personal_Electronics, Price = 750 };
            DB.Items.Add(testItem21);
            DB.Items.Add(testItem22);
            DB.Items.Add(testItem23);
            DB.Items.Add(testItem24);
            DB.Items.Add(testItem25);
            DB.Items.Add(testItem26);
            DB.Items.Add(testItem27);
            DB.Items.Add(testItem28);
            DB.Items.Add(testItem29);
            DB.Items.Add(testItem30);
            DB.Items.Add(testItem31);
            DB.Items.Add(testItem32);
            DB.Items.Add(testItem33);
            DB.Items.Add(testItem34);
            DB.Items.Add(testItem35);
            Item testItem36 = new Item() { Name = "Ammo, Light Pistol/SMG (100)", Type = ItemCategory.Ammo, Price = 15 };
            Item testItem37 = new Item() { Name = "Ammo, Medium Pistol/SMG (50)", Type = ItemCategory.Ammo, Price = 15 };
            Item testItem38 = new Item() { Name = "Ammo, Heavy Pistol/SMG (50)", Type = ItemCategory.Ammo, Price = 18 };
            Item testItem39 = new Item() { Name = "Ammo, Very Heavy Pistol (50)", Type = ItemCategory.Ammo, Price = 20 };
            Item testItem40 = new Item() { Name = "Ammo, Assault Rifle (100)", Type = ItemCategory.Ammo, Price = 40 };
            Item testItem41 = new Item() { Name = "Ammo, Shotgun (12)", Type = ItemCategory.Ammo, Price = 15 };
            Item testItem42 = new Item() { Name = "Ammo, 20mm round (1)", Type = ItemCategory.Ammo, Price = 25 };
            Item testItem43 = new Item() { Name = "Ammo, Std. Arrows (12)", Type = ItemCategory.Ammo, Price = 24 };
            Item testItem44 = new Item() { Name = "Ammo, Std. Crossbow bolts (12)", Type = ItemCategory.Ammo, Price = 30 };
            Item testItem45 = new Item() { Name = "Ammo, Airgun pellets (100)", Type = ItemCategory.Ammo, Price = 6 };
            Item testItem46 = new Item() { Name = "Ammo, Needlegun rounds (50)", Type = ItemCategory.Ammo, Price = 25 };
            Item testItem47 = new Item() { Name = "Ammo, Flamethrower (1)", Type = ItemCategory.Ammo, Price = 50 };
            Item testItem48 = new Item() { Name = "Ammo, Std. Micro Missile (4)", Type = ItemCategory.Ammo, Price = 100 };
            Item testItem49 = new Item() { Name = "Ammo, AP Light Pistol/SMG (100)", Type = ItemCategory.Ammo, Price = 45 };
            Item testItem50 = new Item() { Name = "Ammo, Needlegun rounds, acid/drug (50)", Type = ItemCategory.Ammo, Price = 125 };
            DB.Items.Add(testItem36);
            DB.Items.Add(testItem37);
            DB.Items.Add(testItem38);
            DB.Items.Add(testItem39);
            DB.Items.Add(testItem40);
            DB.Items.Add(testItem41);
            DB.Items.Add(testItem42);
            DB.Items.Add(testItem43);
            DB.Items.Add(testItem44);
            DB.Items.Add(testItem45);
            DB.Items.Add(testItem46);
            DB.Items.Add(testItem47);
            DB.Items.Add(testItem48);
            DB.Items.Add(testItem49);
            DB.Items.Add(testItem50);
            Item testItem51 = new Item() { Name = "Silencer", Type = ItemCategory.Weapon_Options, Price = 100 };
            Item testItem52 = new Item() { Name = "Holster, any", Type = ItemCategory.Weapon_Options, Price = 20 };
            Item testItem53 = new Item() { Name = "Shoulder sling", Type = ItemCategory.Weapon_Options, Price = 5 };
            Item testItem54 = new Item() { Name = "Pistol Laser Pointer (+1WA)", Type = ItemCategory.Weapon_Options, Price = 100 };
            DB.Items.Add(testItem51);
            DB.Items.Add(testItem52);
            DB.Items.Add(testItem53);
            DB.Items.Add(testItem54);
            Armor testItem55 = new Armor() { Name = "Heavy Leather Jacket", Type = ItemCategory.Armor, Price = 50, Arms = 4, Head = 0, Torso = 4, Legs = 0, EV = 0, IsHard = false };
            Armor testItem56 = new Armor() { Name = "Heavy Leather Pants", Type = ItemCategory.Armor, Price = 50, Arms = 0, Head = 0, Torso = 0, Legs = 4, EV = 0, IsHard = false };
            Armor testItem57 = new Armor() { Name = "Kevlar Vest", Type = ItemCategory.Armor, Price = 90, Arms = 0, Head = 0, Torso = 10, Legs = 0, EV = 0, IsHard = false };
            Armor testItem58 = new Armor() { Name = "Corp Militar Body Armor", Type = ItemCategory.Armor, Price = 600, Arms = 25, Head = 25, Torso = 25, Legs = 25, EV = 2, IsHard = true };
            DB.Items.Add(testItem55);
            DB.Items.Add(testItem56);
            DB.Items.Add(testItem57);
            DB.Items.Add(testItem58);
        }
        private static void CreateTestWeapons()
        {
            Weapon testw1 = new Weapon() { Name = "BudgetArms C13", Type = WeaponCategory.Light_Handgun, WA = "-1", Concealability = "P", Availability = "E", Damage = "1D6", AmmoType = "5mm", Shots = "8", RoF = "2", Reliability = "ST", Range = "50m", Price = 75 };
            Weapon testw2 = new Weapon() { Name = "Dai Lung Cybermag 15", Type = WeaponCategory.Light_Handgun, WA = "-1", Concealability = "P", Availability = "C", Damage = "1D6+1", AmmoType = "6mm", Shots = "10", RoF = "2", Reliability = "UR", Range = "50m", Price = 50 };
            Weapon testw3 = new Weapon() { Name = "Federated Arms X22", Type = WeaponCategory.Light_Handgun, WA = "0", Concealability = "P", Availability = "E", Damage = "1D6+1", AmmoType = "6mm", Shots = "10", RoF = "2", Reliability = "ST", Range = "50m", Price = 150 };
            Weapon testw4 = new Weapon() { Name = "Militech Avenger", Type = WeaponCategory.Medium_Handgun, WA = "0", Concealability = "J", Availability = "E", Damage = "2D6+1", AmmoType = "9mm", Shots = "10", RoF = "2", Reliability = "VR", Range = "50m", Price = 250 };
            Weapon testw5 = new Weapon() { Name = "Dai Lung Streetmaster", Type = WeaponCategory.Medium_Handgun, WA = "0", Concealability = "J", Availability = "E", Damage = "2D6+3", AmmoType = "10mm", Shots = "12", RoF = "2", Reliability = "UR", Range = "50m", Price = 250 };
            Weapon testw6 = new Weapon() { Name = "Federated Arms X9", Type = WeaponCategory.Medium_Handgun, WA = "0", Concealability = "J", Availability = "E", Damage = "2D6+1", AmmoType = "9mm", Shots = "12", RoF = "2", Reliability = "ST", Range = "50m", Price = 300 };
            Weapon testw7 = new Weapon() { Name = "BudgetArms Auto3", Type = WeaponCategory.Heavy_Handgun, WA = "-1", Concealability = "J", Availability = "E", Damage = "3D6", AmmoType = "11mm", Shots = "8", RoF = "2", Reliability = "UR", Range = "50m", Price = 350 };
            Weapon testw8 = new Weapon() { Name = "Sternmeyer T35", Type = WeaponCategory.Heavy_Handgun, WA = "0", Concealability = "J", Availability = "C", Damage = "3D6", AmmoType = "11mm", Shots = "8", RoF = "2", Reliability = "VR", Range = "50m", Price = 400 };
            Weapon testw9 = new Weapon() { Name = "Armalite 44", Type = WeaponCategory.Very_Heavy_Handgun, WA = "0", Concealability = "J", Availability = "E", Damage = "4D6+1", AmmoType = "12mm", Shots = "8", RoF = "1", Reliability = "ST", Range = "50m", Price = 450 };
            Weapon testw10 = new Weapon() { Name = "Colt AMT 2000", Type = WeaponCategory.Very_Heavy_Handgun, WA = "0", Concealability = "J", Availability = "C", Damage = "4D6+1", AmmoType = "12mm", Shots = "8", RoF = "1", Reliability = "VR", Range = "50m", Price = 500 };
            DB.Weapons.Add(testw1);
            DB.Weapons.Add(testw2);
            DB.Weapons.Add(testw3);
            DB.Weapons.Add(testw4);
            DB.Weapons.Add(testw5);
            DB.Weapons.Add(testw6);
            DB.Weapons.Add(testw7);
            DB.Weapons.Add(testw8);
            DB.Weapons.Add(testw9);
            DB.Weapons.Add(testw10);
        }
    }
}
