using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml;

namespace CP2020
{
    public enum ItemCategory:int
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

    public enum WeaponCategory:int
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
    public enum CyberwareCategory:int
    {
        Fashion,
        Neural,
        Wear,
        Bioware,
        Wapons,
        Optics,
        Audio,
        Arm,
        Leg,
        Hands,
        Feet,
        Limb_Integrated,
        Limb_Weapons,
        Frame,
        Body_Plating
    }
    public interface ICyberware
    {
        float Humanity { get; set; }
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
                        return "P";
                    case WeaponCategory.Medium_Handgun:
                        return "MP";
                    case WeaponCategory.Heavy_Handgun:
                        return "HP";
                    case WeaponCategory.Very_Heavy_Handgun:
                        return "VHP";
                    case WeaponCategory.Light_SMG:
                        return "SMG";
                    case WeaponCategory.Medium_SMG:
                        return "MSMG";
                    case WeaponCategory.Heavy_SMG:
                        return "HSMG";
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
    public class Cyberware: IBuyable, ICyberware
    {
        public int Price { get; set; }
        public CyberwareCategory Category;
        public virtual float Humanity { get; set; }
        public string Name;
        public string Surgery;
        public string Description;
        public eSkill ModSkill = eSkill.Empty;
        public eStats ModStat;
        public int ModValue;
        public string Requires = string.Empty;
    }
    [Serializable]
    public class Cyberware_D6:Cyberware
    {
        [JsonProperty("Humanity")]
        protected float humanity;
        [JsonIgnore]
        public override float Humanity
        {
            get
            {
                float res = (float)Math.Ceiling(humanity * new Random(System.DateTime.Now.Millisecond).Next(1, 7));
                if(humanity <= 1.0f)
                    return Math.Max(Math.Min(res, 6), 1);
                else
                    return Math.Max(Math.Min(res, 6*humanity), 1);
            }
            set => humanity = value;
        }
        public string DiceString
        {
            get
            {
                if (humanity <= 0.4)
                    return "1D6/3";
                else if (humanity <= 0.51f)
                    return "1D6/2";
                else return (humanity.ToString("0") + "D6");
            } 
        }
    }
    [Serializable]
    public class Equipment
    {
        public List<Weapon> Weapons = new List<Weapon>();
        public List<Item> Items = new List<Item>();
        public List<Cyberware> Cyberwares = new List<Cyberware>();
    }

    public static class EquipmentManager
    {
#if DEBUG
        public static bool CreateTestEquipmentXML = false;
        public static bool OverwriteSolutionEquipmentXML = false;
#endif
        public static Equipment DB = new Equipment();
        private const string itemsXML = @"Data/Items.json";
        private const string weaponsXML = @"Data/Weapons.json";
        private const string cyberwareXML = @"Data/Cyberware.json";

        private static JsonSerializerSettings settings;
        static EquipmentManager()
        {
            settings = new JsonSerializerSettings();
            settings.MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead;
            settings.NullValueHandling = NullValueHandling.Include;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            settings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;
            settings.TypeNameHandling = TypeNameHandling.All;

#if DEBUG
            if (!CreateTestEquipmentXML)
            {
                LoadItemsXML();
                LoadWeaponsXML();
                LoadCyberwareXML();
            }
            else
            {
                CreateTestWeapons();
                SaveWeaponsXMLTest();
                CreateTestItems();
                SaveItemsXMLTest();
                CreateCyberware();
                SaveCyberwareXMLTest();
            }
#else
                LoadItemsXML();
                LoadWeaponsXML();
                LoadCyberwareXML();
#endif

        }

        private static void LoadItemsXML()
        {
            if (DB.Items == null)
                DB.Items = new List<Item>();
            DB.Items = JsonConvert.DeserializeObject<List<Item>>(System.IO.File.ReadAllText(itemsXML),settings);
        }
        private static void LoadWeaponsXML()
        {
            if (DB.Weapons == null)
                DB.Weapons = new List<Weapon>();
            DB.Weapons = JsonConvert.DeserializeObject<List<Weapon>>(System.IO.File.ReadAllText(weaponsXML), settings);
        }
        private static void LoadCyberwareXML()
        {
            if (DB.Cyberwares == null)
                DB.Cyberwares = new List<Cyberware>();
            DB.Cyberwares = JsonConvert.DeserializeObject<List<Cyberware>>(System.IO.File.ReadAllText(cyberwareXML),settings);
            foreach(Cyberware t in DB.Cyberwares)
            {
                Console.WriteLine(t.GetType().Name);
            }
        }
#if DEBUG

        private static void SaveItemsXMLTest()
        {
            string serData = JsonConvert.SerializeObject(DB.Items, Newtonsoft.Json.Formatting.Indented, settings);
            System.IO.File.WriteAllText(itemsXML, serData);
            if (OverwriteSolutionEquipmentXML)
                System.IO.File.Copy(itemsXML, @"../../Data/Items.json", true);
        }
        private static void SaveWeaponsXMLTest()
        {
            string serData = JsonConvert.SerializeObject(DB.Weapons, Newtonsoft.Json.Formatting.Indented, settings);
            System.IO.File.WriteAllText(weaponsXML, serData);
            if(OverwriteSolutionEquipmentXML)
                System.IO.File.Copy(weaponsXML, @"../../Data/Weapons.json", true);
        }
        private static void SaveCyberwareXMLTest()
        {
            string serData = JsonConvert.SerializeObject(DB.Cyberwares, Newtonsoft.Json.Formatting.Indented, settings);
            System.IO.File.WriteAllText(cyberwareXML, serData);
            if (OverwriteSolutionEquipmentXML)
                System.IO.File.Copy(cyberwareXML, @"../../Data/Cyberware.json", true);

        }
#endif

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
            Item testItem60 = new Item() { Name = "Laptop", Type = ItemCategory.Data_Systems, Price = 900 };
            Item testItem61 = new Item() { Name = "Pocket Computer", Type = ItemCategory.Data_Systems, Price = 100 };
            Item testItem62 = new Item() { Name = "Interface Cables", Type = ItemCategory.Data_Systems, Price = 25 };
            Item testItem63 = new Item() { Name = "Low Impedance Cables", Type = ItemCategory.Data_Systems, Price = 60 };
            Item testItem64 = new Item() { Name = "Trode Set", Type = ItemCategory.Data_Systems, Price = 20 };
            Item testItem65 = new Item() { Name = "Keyboard (Computer)", Type = ItemCategory.Data_Systems, Price = 100 };
            Item testItem66 = new Item() { Name = "Terminal (Full PC)", Type = ItemCategory.Data_Systems, Price = 400 };
            DB.Items.Add(testItem60);
            DB.Items.Add(testItem61);
            DB.Items.Add(testItem62);
            DB.Items.Add(testItem63);
            DB.Items.Add(testItem64);
            DB.Items.Add(testItem65);
            DB.Items.Add(testItem66);
            Item testItem67 = new Item() { Name = "Mastoid Communicator", Type = ItemCategory.Communications, Price = 100 };
            Item testItem68 = new Item() { Name = "Pocket Communicator", Type = ItemCategory.Communications, Price = 50 };
            Item testItem69 = new Item() { Name = "Cellphone", Type = ItemCategory.Communications, Price = 400 };
            Item testItem70 = new Item() { Name = "Mini Cellphone", Type = ItemCategory.Communications, Price = 800 };
            DB.Items.Add(testItem67);
            DB.Items.Add(testItem68);
            DB.Items.Add(testItem69);
            DB.Items.Add(testItem70);
            Item testItem71 = new Item() { Name = "Binoglasses", Type = ItemCategory.Surveillance, Price = 200 };
            Item testItem72 = new Item() { Name = "Binoculars", Type = ItemCategory.Surveillance, Price = 20 };
            Item testItem73 = new Item() { Name = "Light Booster Googles", Type = ItemCategory.Surveillance, Price = 200 };
            Item testItem74 = new Item() { Name = "IR Googles", Type = ItemCategory.Surveillance, Price = 250 };
            Item testItem75 = new Item() { Name = "IR Flash", Type = ItemCategory.Surveillance, Price = 50 };
            DB.Items.Add(testItem71);
            DB.Items.Add(testItem72);
            DB.Items.Add(testItem73);
            DB.Items.Add(testItem74);
            DB.Items.Add(testItem75);
            Item testItem76 = new Item() { Name = "Movie", Type = ItemCategory.Entertainment, Price = 10 };
            Item testItem77 = new Item() { Name = "Chip Rental", Type = ItemCategory.Entertainment, Price = 4 };
            Item testItem78 = new Item() { Name = "Braindance", Type = ItemCategory.Entertainment, Price = 20 };
            Item testItem79 = new Item() { Name = "Live/Sports Event", Type = ItemCategory.Entertainment, Price = 50 };
            Item testItem80 = new Item() { Name = "Fast Food", Type = ItemCategory.Entertainment, Price = 5 };
            DB.Items.Add(testItem76);
            DB.Items.Add(testItem77);
            DB.Items.Add(testItem78);
            DB.Items.Add(testItem79);
            DB.Items.Add(testItem80);
            Item testItem81 = new Item() { Name = "Keylock, per Level", Type = ItemCategory.Security, Price = 20 };
            Item testItem82 = new Item() { Name = "Cardlock, per level", Type = ItemCategory.Security, Price = 100 };
            Item testItem83 = new Item() { Name = "Vocolock, per level", Type = ItemCategory.Security, Price = 200 };
            Item testItem84 = new Item() { Name = "Line Tap Bug", Type = ItemCategory.Security, Price = 200 };
            Item testItem85 = new Item() { Name = "Code Decryptor", Type = ItemCategory.Security, Price = 500 };
            Item testItem86 = new Item() { Name = "Voc Decryptor", Type = ItemCategory.Security, Price = 1000 };
            Item testItem87 = new Item() { Name = "Security Scanner", Type = ItemCategory.Security, Price = 1500 };
            Item testItem88 = new Item() { Name = "Poison Sniffer", Type = ItemCategory.Security, Price = 1500 };
            Item testItem89 = new Item() { Name = "Jamming Transmitter", Type = ItemCategory.Security, Price = 500 };
            Item testItem90 = new Item() { Name = "Scanner Plate", Type = ItemCategory.Security, Price = 500 };
            Item testItem91 = new Item() { Name = "Movement Sensor", Type = ItemCategory.Security, Price = 40 };
            Item testItem92 = new Item() { Name = "Passcard", Type = ItemCategory.Security, Price = 10 };
            Item testItem93 = new Item() { Name = "Tracking Device", Type = ItemCategory.Security, Price = 1000 };
            Item testItem94 = new Item() { Name = "Tracer Button", Type = ItemCategory.Security, Price = 50 };
            Item testItem95 = new Item() { Name = "Remote Sensors", Type = ItemCategory.Security, Price = 700 };
            Item testItem96 = new Item() { Name = "PlasKuffs", Type = ItemCategory.Security, Price = 100 };
            Item testItem97 = new Item() { Name = "Stripwire Binders", Type = ItemCategory.Security, Price = 5 };
            DB.Items.Add(testItem81);
            DB.Items.Add(testItem82);
            DB.Items.Add(testItem83);
            DB.Items.Add(testItem84);
            DB.Items.Add(testItem85);
            DB.Items.Add(testItem86);
            DB.Items.Add(testItem87);
            DB.Items.Add(testItem88);
            DB.Items.Add(testItem89);
            DB.Items.Add(testItem90);
            DB.Items.Add(testItem91);
            DB.Items.Add(testItem92);
            DB.Items.Add(testItem93);
            DB.Items.Add(testItem94);
            DB.Items.Add(testItem95);
            DB.Items.Add(testItem96);
            DB.Items.Add(testItem97);
            Item testItem98 = new Item() { Name = "Dermal Stapler", Type = ItemCategory.Medical, Price = 1000 };
            Item testItem99 = new Item() { Name = "Spray Skin, per can", Type = ItemCategory.Medical, Price = 50 };            
            Item testItem101 = new Item() { Name = "Cryotank", Type = ItemCategory.Medical, Price = 100000 };
            Item testItem102 = new Item() { Name = "Medikit", Type = ItemCategory.Medical, Price = 50 };
            Item testItem103 = new Item() { Name = "Surgical Kit", Type = ItemCategory.Medical, Price = 400 };
            Item testItem104 = new Item() { Name = "First Aid Kit", Type = ItemCategory.Medical, Price = 10 };
            Item testItem105 = new Item() { Name = "Medscanner", Type = ItemCategory.Medical, Price = 300 };
            Item testItem106 = new Item() { Name = "Drug Analyser", Type = ItemCategory.Medical, Price = 75 };
            Item testItem107 = new Item() { Name = "Airhypo", Type = ItemCategory.Medical, Price = 100 };
            DB.Items.Add(testItem98);
            DB.Items.Add(testItem99);
            DB.Items.Add(testItem101);
            DB.Items.Add(testItem102);
            DB.Items.Add(testItem103);
            DB.Items.Add(testItem104);
            DB.Items.Add(testItem105);
            DB.Items.Add(testItem106);
            DB.Items.Add(testItem107);
            Item testItem108 = new Item() { Name = "Nylon Carrybag", Type = ItemCategory.Furnishings, Price = 5 };
            Item testItem109 = new Item() { Name = "Sleeping Bag", Type = ItemCategory.Furnishings, Price = 25 };
            Item testItem110 = new Item() { Name = "Inflatable Bed", Type = ItemCategory.Furnishings, Price = 25 };
            Item testItem111 = new Item() { Name = "Futon", Type = ItemCategory.Furnishings, Price = 90 };
            Item testItem112 = new Item() { Name = "Real Wood Furniture, per piece", Type = ItemCategory.Furnishings, Price = 200 };
            Item testItem113 = new Item() { Name = "Synthetic Furniture, per piece", Type = ItemCategory.Furnishings, Price = 100 };
            Item testItem114 = new Item() { Name = "Apartment Cube", Type = ItemCategory.Furnishings, Price = 5000 };
            Item testItem115 = new Item() { Name = "Lamp", Type = ItemCategory.Furnishings, Price = 20 };
            Item testItem116 = new Item() { Name = "Cleaning Bot", Type = ItemCategory.Furnishings, Price = 1000 };
            Item testItem117 = new Item() { Name = "Vocal Switcher System", Type = ItemCategory.Furnishings, Price = 100 };
            DB.Items.Add(testItem108);
            DB.Items.Add(testItem109);
            DB.Items.Add(testItem110);
            DB.Items.Add(testItem111);
            DB.Items.Add(testItem112);
            DB.Items.Add(testItem113);
            DB.Items.Add(testItem114);
            DB.Items.Add(testItem115);
            DB.Items.Add(testItem116);
            DB.Items.Add(testItem117);
            Item testItem118 = new Item() { Name = "Scooter, Generic", Type = ItemCategory.Vehicles, Price = 500 };
            Item testItem119 = new Item() { Name = "Scooter, Generic, CC", Type = ItemCategory.Vehicles, Price = 1000 };
            Item testItem120 = new Item() { Name = "Motorcycle, Generic", Type = ItemCategory.Vehicles, Price = 1500 };
            Item testItem121 = new Item() { Name = "Motorcycle, Generic, CC", Type = ItemCategory.Vehicles, Price = 3000 };
            Item testItem122 = new Item() { Name = "KeiCar, Generic", Type = ItemCategory.Vehicles, Price = 2000 };
            Item testItem123 = new Item() { Name = "KeiCar, Generic, CC", Type = ItemCategory.Vehicles, Price = 4000 };
            Item testItem124 = new Item() { Name = "Small Compact, Generic", Type = ItemCategory.Vehicles, Price = 6000 };
            Item testItem125 = new Item() { Name = "Small Compact, Generic, CC", Type = ItemCategory.Vehicles, Price = 12000 };
            Item testItem126 = new Item() { Name = "Sedan, Generic", Type = ItemCategory.Vehicles, Price = 10000 };
            Item testItem127 = new Item() { Name = "Sedan, Generic, CC", Type = ItemCategory.Vehicles, Price = 20000 };
            Item testItem128 = new Item() { Name = "Sports Car, Generic", Type = ItemCategory.Vehicles, Price = 20000 };
            Item testItem129 = new Item() { Name = "Sports Car, Generic, CC", Type = ItemCategory.Vehicles, Price = 40000 };
            Item testItem130 = new Item() { Name = "Luxury Sedan, Generic", Type = ItemCategory.Vehicles, Price = 40000 };
            Item testItem131 = new Item() { Name = "Luxury Sedan, Generic, CC", Type = ItemCategory.Vehicles, Price = 80000 };
            DB.Items.Add(testItem118);
            DB.Items.Add(testItem119);
            DB.Items.Add(testItem120);
            DB.Items.Add(testItem121);
            DB.Items.Add(testItem122);
            DB.Items.Add(testItem123);
            DB.Items.Add(testItem124);
            DB.Items.Add(testItem125);
            DB.Items.Add(testItem126);
            DB.Items.Add(testItem127);
            DB.Items.Add(testItem128);
            DB.Items.Add(testItem129);
            DB.Items.Add(testItem130);
            DB.Items.Add(testItem131);
            Item testItem133 = new Item() { Name = "Cellphone Service, Month", Type = ItemCategory.Lifestyle, Price = 100 };
            Item testItem134 = new Item() { Name = "Standard Phone Service, Month", Type = ItemCategory.Lifestyle, Price = 30 };
            Item testItem135 = new Item() { Name = "CredChip Account, Month", Type = ItemCategory.Lifestyle, Price = 20 };
            Item testItem136 = new Item() { Name = "Health Plan, Month", Type = ItemCategory.Lifestyle, Price = 1000 };
            Item testItem137 = new Item() { Name = "Trauma Team Acct, Month", Type = ItemCategory.Lifestyle, Price = 500 };
            Item testItem138 = new Item() { Name = "Cable TV", Type = ItemCategory.Lifestyle, Price = 40 };
            DB.Items.Add(testItem133);
            DB.Items.Add(testItem134);
            DB.Items.Add(testItem135);
            DB.Items.Add(testItem136);
            DB.Items.Add(testItem137);
            DB.Items.Add(testItem138);
            Item testItem139 = new Item() { Name = "Kibble, per Week", Type = ItemCategory.Groceries, Price = 50 };
            Item testItem140 = new Item() { Name = "Basic Prepak, per Week", Type = ItemCategory.Groceries, Price = 150 };
            Item testItem141 = new Item() { Name = "Good Prepak, per Week", Type = ItemCategory.Groceries, Price = 200 };
            Item testItem142 = new Item() { Name = "Fresh Food, per Week", Type = ItemCategory.Groceries, Price = 300 };
            DB.Items.Add(testItem139);
            DB.Items.Add(testItem140);
            DB.Items.Add(testItem141);
            DB.Items.Add(testItem142);
        }
        private static void CreateTestWeapons()
        {
            Weapon testw1 = new Weapon() { Name = "BudgetArms C13", Type = WeaponCategory.Light_Handgun, WA = "-1", Concealability = "P", Availability = "E", Damage = "1D6", AmmoType = "5mm", Shots = "8", RoF = "2", Reliability = "ST", Range = "50m", Price = 75 };
            Weapon testw2 = new Weapon() { Name = "Dai Lung Cybermag 15", Type = WeaponCategory.Light_Handgun, WA = "-1", Concealability = "P", Availability = "C", Damage = "1D6+1", AmmoType = "6mm", Shots = "10", RoF = "2", Reliability = "UR", Range = "50m", Price = 50 };
            Weapon testw3 = new Weapon() { Name = "Federated Arms X22", Type = WeaponCategory.Light_Handgun, WA = "0", Concealability = "P", Availability = "E", Damage = "1D6+1", AmmoType = "6mm", Shots = "10", RoF = "2", Reliability = "ST", Range = "50m", Price = 150 };
            DB.Weapons.Add(testw1);
            DB.Weapons.Add(testw2);
            DB.Weapons.Add(testw3);
            Weapon testw4 = new Weapon() { Name = "Militech Avenger", Type = WeaponCategory.Medium_Handgun, WA = "0", Concealability = "J", Availability = "E", Damage = "2D6+1", AmmoType = "9mm", Shots = "10", RoF = "2", Reliability = "VR", Range = "50m", Price = 250 };
            Weapon testw5 = new Weapon() { Name = "Dai Lung Streetmaster", Type = WeaponCategory.Medium_Handgun, WA = "0", Concealability = "J", Availability = "E", Damage = "2D6+3", AmmoType = "10mm", Shots = "12", RoF = "2", Reliability = "UR", Range = "50m", Price = 250 };
            Weapon testw6 = new Weapon() { Name = "Federated Arms X9", Type = WeaponCategory.Medium_Handgun, WA = "0", Concealability = "J", Availability = "E", Damage = "2D6+1", AmmoType = "9mm", Shots = "12", RoF = "2", Reliability = "ST", Range = "50m", Price = 300 };
            DB.Weapons.Add(testw4);
            DB.Weapons.Add(testw5);
            DB.Weapons.Add(testw6);
            Weapon testw7 = new Weapon() { Name = "BudgetArms Auto3", Type = WeaponCategory.Heavy_Handgun, WA = "-1", Concealability = "J", Availability = "E", Damage = "3D6", AmmoType = "11mm", Shots = "8", RoF = "2", Reliability = "UR", Range = "50m", Price = 350 };
            Weapon testw8 = new Weapon() { Name = "Sternmeyer T35", Type = WeaponCategory.Heavy_Handgun, WA = "0", Concealability = "J", Availability = "C", Damage = "3D6", AmmoType = "11mm", Shots = "8", RoF = "2", Reliability = "VR", Range = "50m", Price = 400 };
            DB.Weapons.Add(testw7);
            DB.Weapons.Add(testw8);
            Weapon testw9 = new Weapon() { Name = "Armalite 44", Type = WeaponCategory.Very_Heavy_Handgun, WA = "0", Concealability = "J", Availability = "E", Damage = "4D6+1", AmmoType = "12mm", Shots = "8", RoF = "1", Reliability = "ST", Range = "50m", Price = 450 };
            Weapon testw10 = new Weapon() { Name = "Colt AMT 2000", Type = WeaponCategory.Very_Heavy_Handgun, WA = "0", Concealability = "J", Availability = "C", Damage = "4D6+1", AmmoType = "12mm", Shots = "8", RoF = "1", Reliability = "VR", Range = "50m", Price = 500 };
            DB.Weapons.Add(testw9);
            DB.Weapons.Add(testw10);
            Weapon testw11 = new Weapon() { Name = "Uzi Miniauto 9", Type = WeaponCategory.Light_SMG, WA = "+1", Concealability = "J", Availability = "E", Damage = "2D6+1", AmmoType = "9mm", Shots = "30", RoF = "35", Reliability = "VR", Range = "150m", Price = 475 };
            Weapon testw12 = new Weapon() { Name = "H&K MP2013", Type = WeaponCategory.Light_SMG, WA = "+1", Concealability = "J", Availability = "C", Damage = "2D6+3", AmmoType = "10mm", Shots = "35", RoF = "32", Reliability = "ST", Range = "150m", Price = 450 };
            Weapon testw13 = new Weapon() { Name = "Fed. Arms Tech Assault II", Type = WeaponCategory.Light_SMG, WA = "+1", Concealability = "J", Availability = "C", Damage = "1D6+1", AmmoType = "6mm", Shots = "50", RoF = "25", Reliability = "ST", Range = "150m", Price = 400 };
            DB.Weapons.Add(testw11);
            DB.Weapons.Add(testw12);
            DB.Weapons.Add(testw13);
            Weapon testw14 = new Weapon() { Name = "Arasaka Minami 10", Type = WeaponCategory.Medium_SMG, WA = "+1", Concealability = "J", Availability = "E", Damage = "2D6+3", AmmoType = "10mm", Shots = "40", RoF = "20", Reliability = "VR", Range = "200m", Price = 500 };
            Weapon testw15 = new Weapon() { Name = "H&K MPK9", Type = WeaponCategory.Medium_SMG, WA = "+1", Concealability = "J", Availability = "C", Damage = "2D6+1", AmmoType = "9mm", Shots = "35", RoF = "25", Reliability = "ST", Range = "200m", Price = 520 };
            DB.Weapons.Add(testw14);
            DB.Weapons.Add(testw15);
            Weapon testw16 = new Weapon() { Name = "Sternmeyer SMG21", Type = WeaponCategory.Heavy_SMG, WA = "-1", Concealability = "L", Availability = "E", Damage = "3D6", AmmoType = "11mm", Shots = "30", RoF = "15", Reliability = "VR", Range = "200m", Price = 500 };
            Weapon testw17 = new Weapon() { Name = "H&K MPK11", Type = WeaponCategory.Heavy_SMG, WA = "0", Concealability = "L", Availability = "C", Damage = "4D6+1", AmmoType = "12mm", Shots = "30", RoF = "20", Reliability = "ST", Range = "200m", Price = 700 };
            Weapon testw18 = new Weapon() { Name = "Ingram MAC14", Type = WeaponCategory.Heavy_SMG, WA = "-2", Concealability = "L", Availability = "E", Damage = "4D6+1", AmmoType = "12mm", Shots = "20", RoF = "10", Reliability = "ST", Range = "200m", Price = 650 };
            DB.Weapons.Add(testw16);
            DB.Weapons.Add(testw17);
            DB.Weapons.Add(testw18);
            Weapon testw19 = new Weapon() { Name = "Militech Ronin LAR", Type = WeaponCategory.Assault_Rifle, WA = "+1", Concealability = "N", Availability = "C", Damage = "5D6", AmmoType = "5.56", Shots = "35", RoF = "30", Reliability = "VR", Range = "400m", Price = 450 };
            Weapon testw20 = new Weapon() { Name = "AKR-20 MAR", Type = WeaponCategory.Assault_Rifle, WA = "0", Concealability = "N", Availability = "C", Damage = "5D6", AmmoType = "5.56", Shots = "30", RoF = "30", Reliability = "ST", Range = "400m", Price = 500 };
            Weapon testw21 = new Weapon() { Name = "FN-RAL HAR", Type = WeaponCategory.Assault_Rifle, WA = "-1", Concealability = "N", Availability = "C", Damage = "6D6+2", AmmoType = "7.62", Shots = "30", RoF = "30", Reliability = "VR", Range = "400m", Price = 600 };
            Weapon testw22 = new Weapon() { Name = "Kalashnikov A80 HAR", Type = WeaponCategory.Assault_Rifle, WA = "-1", Concealability = "N", Availability = "E", Damage = "6D6+2", AmmoType = "7.62", Shots = "35", RoF = "25", Reliability = "ST", Range = "400m", Price = 550 };
            DB.Weapons.Add(testw19);
            DB.Weapons.Add(testw20);
            DB.Weapons.Add(testw21);
            DB.Weapons.Add(testw22);
            Weapon testw23 = new Weapon() { Name = "Araska RA 12", Type = WeaponCategory.Shotgun, WA = "-1", Concealability = "N", Availability = "C", Damage = "4D6", AmmoType = "00", Shots = "20", RoF = "10", Reliability = "ST", Range = "50m", Price = 900 };
            Weapon testw24 = new Weapon() { Name = "Sternmeyer Stakeout 10", Type = WeaponCategory.Shotgun, WA = "-2", Concealability = "N", Availability = "R", Damage = "4D6", AmmoType = "00", Shots = "10", RoF = "2", Reliability = "ST", Range = "50m", Price = 450 };
            DB.Weapons.Add(testw23);
            DB.Weapons.Add(testw24);

        }

        public static void CreateCyberware()
        {
            Cyberware c1 = new Cyberware() { Name = "Biomonitor", Category = CyberwareCategory.Fashion, Humanity = 1, ModSkill = eSkill.Resist_Torture_Drugs, ModValue = 2, Price = 100, Description = "+2 to Resist Torture/Drugs", Surgery = "N" };
            Cyberware c2 = new Cyberware() { Name = "Skinwatch", Category = CyberwareCategory.Fashion, Humanity = 1, Price = 50, Description = "Subdermal Watch", Surgery = "N" };
            Cyberware c3 = new Cyberware() { Name = "Light Tattoo", Category = CyberwareCategory.Fashion, Humanity = 0.5f, Price = 15, Description = "Decorative Tattoo ", Surgery = "N" };
            Cyberware c4 = new Cyberware() { Name = "Shift-tacts", Category = CyberwareCategory.Fashion, Humanity = 0.5f, Price = 100, Description = "Color changing contact lenses", Surgery = "N" };
            Cyberware_D6 c5 = new Cyberware_D6() { Name = "ChemSkins", Category = CyberwareCategory.Fashion, Humanity = 0.5f, Price = 200, Description = "Color changing skin tints", Surgery = "N" };
            Cyberware_D6 c6 = new Cyberware_D6() { Name = "SynthSkins", Category = CyberwareCategory.Fashion, Humanity = 1.0f, Price = 400, Description = "Color changing artifical skin", Surgery = "N" };
            Cyberware c7 = new Cyberware() { Name = "Techhair", Category = CyberwareCategory.Fashion, Humanity = 2,Price = 100, Description = "Color/Light emitting artifical hair", Surgery = "N" };
            DB.Cyberwares.Add(c1, c2, c3, c4, c5, c6, c7);
            Cyberware_D6 c8 = new Cyberware_D6() { Name = "Neural Processor", Category = CyberwareCategory.Neural, Humanity = 1.0f, Description = "Basic processor. Required for al systems", Price = 1000, Surgery = "M" };
            Cyberware_D6 c9 = new Cyberware_D6() { Name = "Kerenzikov Booster (per Level)", Category = CyberwareCategory.Neural, Humanity = 1.0f, Description = "+1 to Initiative per level", Price = 500, Surgery = "N", Requires = "Neural Processor" };
            Cyberware_D6 c10 = new Cyberware_D6() { Name = "Sandevistan Booster", Category = CyberwareCategory.Neural, Humanity = 0.5f, Description = "+3 to Initiative for 5 turns", Price = 1600, Surgery = "N", Requires = "Neural Processor" };
            Cyberware c11 = new Cyberware() { Name = "Tactile Boost", Category = CyberwareCategory.Neural, Humanity = 2, Price = 100, Description = "+2 to any touch Awareness (Not added to Sheet)", Surgery = "N", Requires = "Neural Processor" };
            Cyberware c12 = new Cyberware() { Name = "Olifactory Boost", Category = CyberwareCategory.Neural, Humanity = 2, Price = 100, Description = "+2 to Awareness via Smell (Not added to Sheet)", Surgery = "N", Requires = "Neural Processor" };
            Cyberware_D6 c13 = new Cyberware_D6() { Name = "Pain Editor", Category = CyberwareCategory.Neural, Humanity = 2.0f, Description = "Endurance checks 2 level lower difficulty", Price = 200, Surgery = "N", Requires = "Neural Processor" };
            Cyberware c14 = new Cyberware() { Name = "Cybermodem Link", Category = CyberwareCategory.Neural, Humanity = 1, Price = 100, Description = "Direct connection for modems", Surgery = "N", Requires = "Neural Processor" };
            Cyberware c15 = new Cyberware() { Name = "Vehicle Link", Category = CyberwareCategory.Neural, Humanity = 3, Price = 100, Description = "Direct vehicle connection", Surgery = "N", Requires = "Neural Processor" };
            Cyberware c16 = new Cyberware() { Name = "Smartgun Link", Category = CyberwareCategory.Neural, Humanity = 2, Price = 100, Description = "Smartgun operation connection", Surgery = "N", Requires = "Neural Processor" };
            Cyberware c17 = new Cyberware() { Name = "Machine/Tech Link", Category = CyberwareCategory.Neural, Humanity = 2, Price = 100, Description = "Autofactories, machines... connection", Surgery = "N", Requires = "Neural Processor" };
            Cyberware c18 = new Cyberware() { Name = "Dataterm Link", Category = CyberwareCategory.Neural, Humanity = 2, Price = 100, Description = "Allows downloading from Dataterms", Surgery = "N", Requires = "Neural Processor" };
            Cyberware_D6 c19 = new Cyberware_D6() { Name = "Interface Plugs", Category = CyberwareCategory.Neural, Humanity = 1.0f,  Description = "Misc. direct neural connection", Price = 200, Surgery = "M", Requires = "Neural Processor" };
            Cyberware_D6 c20 = new Cyberware_D6() { Name = "Chipware Socket", Category = CyberwareCategory.Neural, Humanity = 0.5f,  Description = "Up to 10 chips can be loaded", Price = 200, Surgery = "N", Requires = "Neural Processor" };
            DB.Cyberwares.Add(c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20);

        }
    }
}
