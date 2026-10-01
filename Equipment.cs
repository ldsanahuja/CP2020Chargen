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
        public bool Smart = false;

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
        public string Incompat = string.Empty;
        public int Max = 1;
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
    public class Cyberware_Chip:Cyberware
    {
        public eStats Subclass;
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
        public static bool OverwriteSolutionEquipmentXML = true; //for use only inside VS
        public static Equipment DB = new Equipment();
        public const string itemsXML = @"Data/Items.json";
        public const string weaponsXML = @"Data/Weapons.json";
        public const string cyberwareXML = @"Data/Cyberware.json";

        private static JsonSerializerSettings settings;
        static EquipmentManager()
        {
            settings = new JsonSerializerSettings();
            settings.MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead;
            settings.NullValueHandling = NullValueHandling.Include;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            settings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;
            settings.TypeNameHandling = TypeNameHandling.All;

                LoadItemsXML();
                LoadWeaponsXML();
                LoadCyberwareXML();


        }

        public static void Reload()
        {
            settings = new JsonSerializerSettings();
            settings.MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead;
            settings.NullValueHandling = NullValueHandling.Include;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            settings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;
            settings.TypeNameHandling = TypeNameHandling.All;
            DB.Items.Clear();
            DB.Weapons.Clear();
            DB.Cyberwares.Clear();
            LoadItemsXML();
            LoadWeaponsXML();
            LoadCyberwareXML();
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

        }

        private static void SaveItemsXML()
        {
            string serData = JsonConvert.SerializeObject(DB.Items, Newtonsoft.Json.Formatting.Indented, settings);
            System.IO.File.WriteAllText(itemsXML, serData);
            if (OverwriteSolutionEquipmentXML)
                System.IO.File.Copy(itemsXML, @"../../Data/Items.json", true);
        }
        private static void SaveWeaponsXML()
        {
            string serData = JsonConvert.SerializeObject(DB.Weapons, Newtonsoft.Json.Formatting.Indented, settings);
            System.IO.File.WriteAllText(weaponsXML, serData);
            if(OverwriteSolutionEquipmentXML)
                System.IO.File.Copy(weaponsXML, @"../../Data/Weapons.json", true);
        }
        private static void SaveCyberwareXML()
        {
            string serData = JsonConvert.SerializeObject(DB.Cyberwares, Newtonsoft.Json.Formatting.Indented, settings);
            System.IO.File.WriteAllText(cyberwareXML, serData);
            if (OverwriteSolutionEquipmentXML)
                System.IO.File.Copy(cyberwareXML, @"../../Data/Cyberware.json", true);

        }

        public static void CreateItems()
        {
            DB.Items.Clear();
            Item it = new Item() { Name = "Pants (Generic)", Type = ItemCategory.Fashion, Price = 20 };
            Item it1 = new Item() { Name = "Top (Generic)", Type = ItemCategory.Fashion, Price = 15 };
            Item it2 = new Item() { Name = "Jacket (Generic)", Type = ItemCategory.Fashion, Price = 35 };
            Item it3 = new Item() { Name = "Footwear (Generic)", Type = ItemCategory.Fashion, Price = 25 };
            Item it4 = new Item() { Name = "Jewelry (Generic)", Type = ItemCategory.Fashion, Price = 50 };
            Item it5 = new Item() { Name = "Mirrorshades (Generic)", Type = ItemCategory.Fashion, Price = 25 };
            Item it6 = new Item() { Name = "Contact Lenses (Generic)", Type = ItemCategory.Fashion, Price = 100 };
            Item it7 = new Item() { Name = "Glasses (Generic)", Type = ItemCategory.Fashion, Price = 50 };
            DB.Items.Add(it, it2, it3, it4, it5,it6, it7);
            Item it8 = new Item() { Name = "Techscanner", Type = ItemCategory.Tools, Price = 600 };
            Item it9 = new Item() { Name = "Cutting Torch", Type = ItemCategory.Tools, Price = 40 };
            Item it10 = new Item() { Name = "Tech Toolkit", Type = ItemCategory.Tools, Price = 100 };
            Item it11 = new Item() { Name = "B&E Tools", Type = ItemCategory.Tools, Price = 120 };
            Item it12 = new Item() { Name = "Electronic Toolkit", Type = ItemCategory.Tools, Price = 100 };
            Item it13 = new Item() { Name = "Protective Googles", Type = ItemCategory.Tools, Price = 20 };
            Item it14 = new Item() { Name = "Flashtube", Type = ItemCategory.Tools, Price = 2 };
            Item it15 = new Item() { Name = "Glowstick", Type = ItemCategory.Tools, Price = 1 };
            Item it16 = new Item() { Name = "Paint, ltr", Type = ItemCategory.Tools, Price = 10 };
            Item it17 = new Item() { Name = "Flash Tape, mtr", Type = ItemCategory.Tools, Price = 10 };
            Item it18 = new Item() { Name = "Rope, mtr", Type = ItemCategory.Tools, Price = 2 };
            Item it19 = new Item() { Name = "Breathing Mask", Type = ItemCategory.Tools, Price = 30 };
            DB.Items.Add(it8, it9, it10, it11, it12, it13, it14, it15, it16, it17, it18, it19);
            Item it20 = new Item() { Name = "Holo Generator", Type = ItemCategory.Personal_Electronics, Price = 500 };
            Item it21 = new Item() { Name = "Video Board, mtr2", Type = ItemCategory.Personal_Electronics, Price = 100 };
            Item it22 = new Item() { Name = "Data Chip", Type = ItemCategory.Personal_Electronics, Price = 10 };
            Item it23 = new Item() { Name = "Logcompass", Type = ItemCategory.Personal_Electronics, Price = 50 };
            Item it24 = new Item() { Name = "Digital Recorder", Type = ItemCategory.Personal_Electronics, Price = 300 };
            Item it25 = new Item() { Name = "Digital Camera", Type = ItemCategory.Personal_Electronics, Price = 150 };
            Item it26 = new Item() { Name = "VideoCam", Type = ItemCategory.Personal_Electronics, Price = 800 };
            Item it27 = new Item() { Name = "V/A Tape Player", Type = ItemCategory.Personal_Electronics, Price = 40 };
            Item it28 = new Item() { Name = "Videotape", Type = ItemCategory.Personal_Electronics, Price = 4 };
            Item it29 = new Item() { Name = "Pocket TV", Type = ItemCategory.Personal_Electronics, Price = 80 };
            Item it30 = new Item() { Name = "Digital Chip Player", Type = ItemCategory.Personal_Electronics, Price = 150 };
            Item it31 = new Item() { Name = "Digital Music Chip", Type = ItemCategory.Personal_Electronics, Price = 20 };
            Item it32 = new Item() { Name = "Electric Guitar", Type = ItemCategory.Personal_Electronics, Price = 250 };
            Item it33 = new Item() { Name = "Electric Keyboard", Type = ItemCategory.Personal_Electronics, Price = 700 };
            Item it34 = new Item() { Name = "Drum Synth", Type = ItemCategory.Personal_Electronics, Price = 600 };
            Item it35 = new Item() { Name = "Amplifier", Type = ItemCategory.Personal_Electronics, Price = 750 };
            DB.Items.Add(it21, it22, it23, it24, it25, it26, it27, it28, it29, it30, it31, it32, it33, it34, it35);
            Item it36 = new Item() { Name = "Ammo, Light Pistol/SMG (100)", Type = ItemCategory.Ammo, Price = 15 };
            Item it37 = new Item() { Name = "Ammo, Medium Pistol/SMG (50)", Type = ItemCategory.Ammo, Price = 15 };
            Item it38 = new Item() { Name = "Ammo, Heavy Pistol/SMG (50)", Type = ItemCategory.Ammo, Price = 18 };
            Item it39 = new Item() { Name = "Ammo, Very Heavy Pistol (50)", Type = ItemCategory.Ammo, Price = 20 };
            Item it40 = new Item() { Name = "Ammo, Assault Rifle (100)", Type = ItemCategory.Ammo, Price = 40 };
            Item it41 = new Item() { Name = "Ammo, Shotgun (12)", Type = ItemCategory.Ammo, Price = 15 };
            Item it42 = new Item() { Name = "Ammo, 20mm round (1)", Type = ItemCategory.Ammo, Price = 25 };
            Item it43 = new Item() { Name = "Ammo, Std. Arrows (12)", Type = ItemCategory.Ammo, Price = 24 };
            Item it44 = new Item() { Name = "Ammo, Std. Crossbow bolts (12)", Type = ItemCategory.Ammo, Price = 30 };
            Item it45 = new Item() { Name = "Ammo, Airgun pellets (100)", Type = ItemCategory.Ammo, Price = 6 };
            Item it46 = new Item() { Name = "Ammo, Needlegun rounds (50)", Type = ItemCategory.Ammo, Price = 25 };
            Item it47 = new Item() { Name = "Ammo, Flamethrower (1)", Type = ItemCategory.Ammo, Price = 50 };
            Item it48 = new Item() { Name = "Ammo, Std. Micro Missile (4)", Type = ItemCategory.Ammo, Price = 100 };
            Item it49 = new Item() { Name = "Ammo, AP Light Pistol/SMG (100)", Type = ItemCategory.Ammo, Price = 45 };
            Item it50 = new Item() { Name = "Ammo, Needlegun rounds, acid/drug (50)", Type = ItemCategory.Ammo, Price = 125 };
            DB.Items.Add(it36, it37, it38, it39, it40, it41, it42, it43, it44, it45, it46, it47, it48, it49, it50);
            Item it51 = new Item() { Name = "Silencer", Type = ItemCategory.Weapon_Options, Price = 100 };
            Item it52 = new Item() { Name = "Holster, any", Type = ItemCategory.Weapon_Options, Price = 20 };
            Item it53 = new Item() { Name = "Shoulder sling", Type = ItemCategory.Weapon_Options, Price = 5 };
            Item it54 = new Item() { Name = "Pistol Laser Pointer (+1WA)", Type = ItemCategory.Weapon_Options, Price = 100 };
            DB.Items.Add(it51, it52, it53, it54);
            Armor it55 = new Armor() { Name = "Heavy Leather Jacket", Type = ItemCategory.Armor, Price = 50, Arms = 4, Head = 0, Torso = 4, Legs = 0, EV = 0, IsHard = false };
            Armor it56 = new Armor() { Name = "Heavy Leather Pants", Type = ItemCategory.Armor, Price = 50, Arms = 0, Head = 0, Torso = 0, Legs = 4, EV = 0, IsHard = false };
            Armor it57 = new Armor() { Name = "Kevlar Vest", Type = ItemCategory.Armor, Price = 90, Arms = 0, Head = 0, Torso = 10, Legs = 0, EV = 0, IsHard = false };
            Armor it57_1 = new Armor() { Name = "Steel Helmet", Type = ItemCategory.Armor, Price = 20, Arms = 0, Head = 14, Torso = 0, Legs = 0, EV = 0, IsHard = true };
            Armor it57_2 = new Armor() { Name = "Light Armor Jacket", Type = ItemCategory.Armor, Price = 150, Arms = 14, Head = 0, Torso = 14, Legs = 0, EV = 0, IsHard = false };
            Armor it57_3 = new Armor() { Name = "Medium Armor Jacket", Type = ItemCategory.Armor, Price = 200, Arms = 18, Head = 0, Torso = 18, Legs = 0, EV = 1, IsHard = false };
            Armor it57_4 = new Armor() { Name = "Flak Vest", Type = ItemCategory.Armor, Price = 200, Arms = 0, Head = 0, Torso = 20, Legs = 0, EV = 1, IsHard = true };
            Armor it57_5 = new Armor() { Name = "Flak Pants", Type = ItemCategory.Armor, Price = 200, Arms = 0, Head = 0, Torso = 0, Legs = 20, EV = 1, IsHard = true };
            Armor it58 = new Armor() { Name = "Corp Militar Body Armor", Type = ItemCategory.Armor, Price = 600, Arms = 25, Head = 25, Torso = 25, Legs = 25, EV = 2, IsHard = true };
            DB.Items.Add(it55, it56, it57, it57_1, it57_2, it57_3, it57_4, it57_5, it58);
            Item it60 = new Item() { Name = "Laptop", Type = ItemCategory.Data_Systems, Price = 900 };
            Item it61 = new Item() { Name = "Pocket Computer", Type = ItemCategory.Data_Systems, Price = 100 };
            Item it62 = new Item() { Name = "Interface Cables", Type = ItemCategory.Data_Systems, Price = 25 };
            Item it63 = new Item() { Name = "Low Impedance Cables", Type = ItemCategory.Data_Systems, Price = 60 };
            Item it64 = new Item() { Name = "Trode Set", Type = ItemCategory.Data_Systems, Price = 20 };
            Item it65 = new Item() { Name = "Keyboard (Computer)", Type = ItemCategory.Data_Systems, Price = 100 };
            Item it66 = new Item() { Name = "Terminal (Full PC)", Type = ItemCategory.Data_Systems, Price = 400 };
            DB.Items.Add(it60, it61, it62, it63, it64, it65, it66);
            Item it67 = new Item() { Name = "Mastoid Communicator", Type = ItemCategory.Communications, Price = 100 };
            Item it68 = new Item() { Name = "Pocket Communicator", Type = ItemCategory.Communications, Price = 50 };
            Item it69 = new Item() { Name = "Cellphone", Type = ItemCategory.Communications, Price = 400 };
            Item it70 = new Item() { Name = "Mini Cellphone", Type = ItemCategory.Communications, Price = 800 };
            DB.Items.Add(it67, it68, it69, it70);
            Item it71 = new Item() { Name = "Binoglasses", Type = ItemCategory.Surveillance, Price = 200 };
            Item it72 = new Item() { Name = "Binoculars", Type = ItemCategory.Surveillance, Price = 20 };
            Item it73 = new Item() { Name = "Light Booster Googles", Type = ItemCategory.Surveillance, Price = 200 };
            Item it74 = new Item() { Name = "IR Googles", Type = ItemCategory.Surveillance, Price = 250 };
            Item it75 = new Item() { Name = "IR Flash", Type = ItemCategory.Surveillance, Price = 50 };
            DB.Items.Add(it71, it72, it73, it74, it75);
            Item it76 = new Item() { Name = "Movie", Type = ItemCategory.Entertainment, Price = 10 };
            Item it77 = new Item() { Name = "Chip Rental", Type = ItemCategory.Entertainment, Price = 4 };
            Item it78 = new Item() { Name = "Braindance", Type = ItemCategory.Entertainment, Price = 20 };
            Item it79 = new Item() { Name = "Live/Sports Event", Type = ItemCategory.Entertainment, Price = 50 };
            Item it80 = new Item() { Name = "Fast Food", Type = ItemCategory.Entertainment, Price = 5 };
            DB.Items.Add(it76, it77, it78, it79, it80);
            Item it81 = new Item() { Name = "Keylock, per Level", Type = ItemCategory.Security, Price = 20 };
            Item it82 = new Item() { Name = "Cardlock, per level", Type = ItemCategory.Security, Price = 100 };
            Item it83 = new Item() { Name = "Vocolock, per level", Type = ItemCategory.Security, Price = 200 };
            Item it84 = new Item() { Name = "Line Tap Bug", Type = ItemCategory.Security, Price = 200 };
            Item it85 = new Item() { Name = "Code Decryptor", Type = ItemCategory.Security, Price = 500 };
            Item it86 = new Item() { Name = "Voc Decryptor", Type = ItemCategory.Security, Price = 1000 };
            Item it87 = new Item() { Name = "Security Scanner", Type = ItemCategory.Security, Price = 1500 };
            Item it88 = new Item() { Name = "Poison Sniffer", Type = ItemCategory.Security, Price = 1500 };
            Item it89 = new Item() { Name = "Jamming Transmitter", Type = ItemCategory.Security, Price = 500 };
            Item it90 = new Item() { Name = "Scanner Plate", Type = ItemCategory.Security, Price = 500 };
            Item it91 = new Item() { Name = "Movement Sensor", Type = ItemCategory.Security, Price = 40 };
            Item it92 = new Item() { Name = "Passcard", Type = ItemCategory.Security, Price = 10 };
            Item it93 = new Item() { Name = "Tracking Device", Type = ItemCategory.Security, Price = 1000 };
            Item it94 = new Item() { Name = "Tracer Button", Type = ItemCategory.Security, Price = 50 };
            Item it95 = new Item() { Name = "Remote Sensors", Type = ItemCategory.Security, Price = 700 };
            Item it96 = new Item() { Name = "PlasKuffs", Type = ItemCategory.Security, Price = 100 };
            Item it97 = new Item() { Name = "Stripwire Binders", Type = ItemCategory.Security, Price = 5 };
            DB.Items.Add(it81, it82, it83, it84, it85, it86, it87, it88, it89, it90, it91, it92, it93, it94, it95, it96, it97);
            Item it98 = new Item() { Name = "Dermal Stapler", Type = ItemCategory.Medical, Price = 1000 };
            Item it99 = new Item() { Name = "Spray Skin, per can", Type = ItemCategory.Medical, Price = 50 };            
            Item it101 = new Item() { Name = "Cryotank", Type = ItemCategory.Medical, Price = 100000 };
            Item it102 = new Item() { Name = "Medikit", Type = ItemCategory.Medical, Price = 50 };
            Item it103 = new Item() { Name = "Surgical Kit", Type = ItemCategory.Medical, Price = 400 };
            Item it104 = new Item() { Name = "First Aid Kit", Type = ItemCategory.Medical, Price = 10 };
            Item it105 = new Item() { Name = "Medscanner", Type = ItemCategory.Medical, Price = 300 };
            Item it106 = new Item() { Name = "Drug Analyser", Type = ItemCategory.Medical, Price = 75 };
            Item it107 = new Item() { Name = "Airhypo", Type = ItemCategory.Medical, Price = 100 };
            DB.Items.Add(it98, it99, it101, it102, it103, it104, it105, it106, it107);
            Item it108 = new Item() { Name = "Nylon Carrybag", Type = ItemCategory.Furnishings, Price = 5 };
            Item it109 = new Item() { Name = "Sleeping Bag", Type = ItemCategory.Furnishings, Price = 25 };
            Item it110 = new Item() { Name = "Inflatable Bed", Type = ItemCategory.Furnishings, Price = 25 };
            Item it111 = new Item() { Name = "Futon", Type = ItemCategory.Furnishings, Price = 90 };
            Item it112 = new Item() { Name = "Real Wood Furniture, per piece", Type = ItemCategory.Furnishings, Price = 200 };
            Item it113 = new Item() { Name = "Synthetic Furniture, per piece", Type = ItemCategory.Furnishings, Price = 100 };
            Item it114 = new Item() { Name = "Apartment Cube", Type = ItemCategory.Furnishings, Price = 5000 };
            Item it115 = new Item() { Name = "Lamp", Type = ItemCategory.Furnishings, Price = 20 };
            Item it116 = new Item() { Name = "Cleaning Bot", Type = ItemCategory.Furnishings, Price = 1000 };
            Item it117 = new Item() { Name = "Vocal Switcher System", Type = ItemCategory.Furnishings, Price = 100 };
            DB.Items.Add(it108, it109, it110, it111, it112, it113, it114, it115, it116, it117);
            Item it118 = new Item() { Name = "Scooter, Generic", Type = ItemCategory.Vehicles, Price = 500 };
            Item it119 = new Item() { Name = "Scooter, Generic, CC", Type = ItemCategory.Vehicles, Price = 1000 };
            Item it120 = new Item() { Name = "Motorcycle, Generic", Type = ItemCategory.Vehicles, Price = 1500 };
            Item it121 = new Item() { Name = "Motorcycle, Generic, CC", Type = ItemCategory.Vehicles, Price = 3000 };
            Item it122 = new Item() { Name = "KeiCar, Generic", Type = ItemCategory.Vehicles, Price = 2000 };
            Item it123 = new Item() { Name = "KeiCar, Generic, CC", Type = ItemCategory.Vehicles, Price = 4000 };
            Item it124 = new Item() { Name = "Small Compact, Generic", Type = ItemCategory.Vehicles, Price = 6000 };
            Item it125 = new Item() { Name = "Small Compact, Generic, CC", Type = ItemCategory.Vehicles, Price = 12000 };
            Item it126 = new Item() { Name = "Sedan, Generic", Type = ItemCategory.Vehicles, Price = 10000 };
            Item it127 = new Item() { Name = "Sedan, Generic, CC", Type = ItemCategory.Vehicles, Price = 20000 };
            Item it128 = new Item() { Name = "Sports Car, Generic", Type = ItemCategory.Vehicles, Price = 20000 };
            Item it129 = new Item() { Name = "Sports Car, Generic, CC", Type = ItemCategory.Vehicles, Price = 40000 };
            Item it130 = new Item() { Name = "Luxury Sedan, Generic", Type = ItemCategory.Vehicles, Price = 40000 };
            Item it131 = new Item() { Name = "Luxury Sedan, Generic, CC", Type = ItemCategory.Vehicles, Price = 80000 };
            DB.Items.Add(it118, it119, it120, it121, it122, it123, it124, it125,it126, it127, it128, it129, it130, it131);
            Item it133 = new Item() { Name = "Cellphone Service, Month", Type = ItemCategory.Lifestyle, Price = 100 };
            Item it134 = new Item() { Name = "Standard Phone Service, Month", Type = ItemCategory.Lifestyle, Price = 30 };
            Item it135 = new Item() { Name = "CredChip Account, Month", Type = ItemCategory.Lifestyle, Price = 20 };
            Item it136 = new Item() { Name = "Health Plan, Month", Type = ItemCategory.Lifestyle, Price = 1000 };
            Item it137 = new Item() { Name = "Trauma Team Acct, Month", Type = ItemCategory.Lifestyle, Price = 500 };
            Item it138 = new Item() { Name = "Cable TV", Type = ItemCategory.Lifestyle, Price = 40 };
            DB.Items.Add(it133, it134, it135, it136, it137, it138);
            Item it139 = new Item() { Name = "Kibble, per Week", Type = ItemCategory.Groceries, Price = 50 };
            Item it140 = new Item() { Name = "Basic Prepak, per Week", Type = ItemCategory.Groceries, Price = 150 };
            Item it141 = new Item() { Name = "Good Prepak, per Week", Type = ItemCategory.Groceries, Price = 200 };
            Item it142 = new Item() { Name = "Fresh Food, per Week", Type = ItemCategory.Groceries, Price = 300 };
            DB.Items.Add(it139, it140, it141, it142);

            SaveItemsXML();
        }
        public static void CreateWeapons()
        {
            DB.Weapons.Clear();
            Weapon wp1 = new Weapon() { Name = "BudgetArms C13", Type = WeaponCategory.Light_Handgun, WA = "-1", Concealability = "P", Availability = "E", Damage = "1D6", AmmoType = "5mm", Shots = "8", RoF = "2", Reliability = "ST", Range = "50m", Price = 75 };
            Weapon wp2 = new Weapon() { Name = "Dai Lung Cybermag 15", Type = WeaponCategory.Light_Handgun, WA = "-1", Concealability = "P", Availability = "C", Damage = "1D6+1", AmmoType = "6mm", Shots = "10", RoF = "2", Reliability = "UR", Range = "50m", Price = 50 };
            Weapon wp3 = new Weapon() { Name = "Federated Arms X22", Type = WeaponCategory.Light_Handgun, WA = "0", Concealability = "P", Availability = "E", Damage = "1D6+1", AmmoType = "6mm", Shots = "10", RoF = "2", Reliability = "ST", Range = "50m", Price = 150 };
            DB.Weapons.Add(wp1, wp2, wp3);
            Weapon wp4 = new Weapon() { Name = "Militech Avenger", Type = WeaponCategory.Medium_Handgun, WA = "0", Concealability = "J", Availability = "E", Damage = "2D6+1", AmmoType = "9mm", Shots = "10", RoF = "2", Reliability = "VR", Range = "50m", Price = 250 };
            Weapon wp5 = new Weapon() { Name = "Dai Lung Streetmaster", Type = WeaponCategory.Medium_Handgun, WA = "0", Concealability = "J", Availability = "E", Damage = "2D6+3", AmmoType = "10mm", Shots = "12", RoF = "2", Reliability = "UR", Range = "50m", Price = 250 };
            Weapon wp6 = new Weapon() { Name = "Federated Arms X9", Type = WeaponCategory.Medium_Handgun, WA = "0", Concealability = "J", Availability = "E", Damage = "2D6+1", AmmoType = "9mm", Shots = "12", RoF = "2", Reliability = "ST", Range = "50m", Price = 300 };
            DB.Weapons.Add(wp4, wp5, wp6);
            Weapon wp7 = new Weapon() { Name = "BudgetArms Auto3", Type = WeaponCategory.Heavy_Handgun, WA = "-1", Concealability = "J", Availability = "E", Damage = "3D6", AmmoType = "11mm", Shots = "8", RoF = "2", Reliability = "UR", Range = "50m", Price = 350 };
            Weapon wp8 = new Weapon() { Name = "Sternmeyer T35", Type = WeaponCategory.Heavy_Handgun, WA = "0", Concealability = "J", Availability = "C", Damage = "3D6", AmmoType = "11mm", Shots = "8", RoF = "2", Reliability = "VR", Range = "50m", Price = 400 };
            DB.Weapons.Add(wp7, wp8);            
            Weapon wp9 = new Weapon() { Name = "Armalite 44", Type = WeaponCategory.Very_Heavy_Handgun, WA = "0", Concealability = "J", Availability = "E", Damage = "4D6+1", AmmoType = "12mm", Shots = "8", RoF = "1", Reliability = "ST", Range = "50m", Price = 450 };
            Weapon wp10 = new Weapon() { Name = "Colt AMT 2000", Type = WeaponCategory.Very_Heavy_Handgun, WA = "0", Concealability = "J", Availability = "C", Damage = "4D6+1", AmmoType = "12mm", Shots = "8", RoF = "1", Reliability = "VR", Range = "50m", Price = 500 };
            Weapon wp10_1 = new Weapon() { Name = "*Mallorian Arms 3516", Type = WeaponCategory.Very_Heavy_Handgun, WA = "-1", Concealability = "J", Availability = "R", Damage = "6D6", AmmoType = "14mm", Shots = "6", RoF = "1", Reliability = "VR", Range = "50m", Price = 4525, Smart = true };
            DB.Weapons.Add(wp9, wp10, wp10_1);
            Weapon wp11 = new Weapon() { Name = "Uzi Miniauto 9", Type = WeaponCategory.Light_SMG, WA = "+1", Concealability = "J", Availability = "E", Damage = "2D6+1", AmmoType = "9mm", Shots = "30", RoF = "35", Reliability = "VR", Range = "150m", Price = 475 };
            Weapon wp12 = new Weapon() { Name = "H&K MP2013", Type = WeaponCategory.Light_SMG, WA = "+1", Concealability = "J", Availability = "C", Damage = "2D6+3", AmmoType = "10mm", Shots = "35", RoF = "32", Reliability = "ST", Range = "150m", Price = 450 };
            Weapon wp13 = new Weapon() { Name = "Fed. Arms Tech Assault II", Type = WeaponCategory.Light_SMG, WA = "+1", Concealability = "J", Availability = "C", Damage = "1D6+1", AmmoType = "6mm", Shots = "50", RoF = "25", Reliability = "ST", Range = "150m", Price = 400 };
            DB.Weapons.Add(wp11, wp12, wp13);
            Weapon wp14 = new Weapon() { Name = "Arasaka Minami 10", Type = WeaponCategory.Medium_SMG, WA = "+1", Concealability = "J", Availability = "E", Damage = "2D6+3", AmmoType = "10mm", Shots = "40", RoF = "20", Reliability = "VR", Range = "200m", Price = 500 };
            Weapon wp15 = new Weapon() { Name = "H&K MPK9", Type = WeaponCategory.Medium_SMG, WA = "+1", Concealability = "J", Availability = "C", Damage = "2D6+1", AmmoType = "9mm", Shots = "35", RoF = "25", Reliability = "ST", Range = "200m", Price = 520 };
            DB.Weapons.Add(wp14, wp15);
            Weapon wp16 = new Weapon() { Name = "Sternmeyer SMG21", Type = WeaponCategory.Heavy_SMG, WA = "-1", Concealability = "L", Availability = "E", Damage = "3D6", AmmoType = "11mm", Shots = "30", RoF = "15", Reliability = "VR", Range = "200m", Price = 500 };
            Weapon wp17 = new Weapon() { Name = "H&K MPK11", Type = WeaponCategory.Heavy_SMG, WA = "0", Concealability = "L", Availability = "C", Damage = "4D6+1", AmmoType = "12mm", Shots = "30", RoF = "20", Reliability = "ST", Range = "200m", Price = 700 };
            Weapon wp18 = new Weapon() { Name = "Ingram MAC14", Type = WeaponCategory.Heavy_SMG, WA = "-2", Concealability = "L", Availability = "E", Damage = "4D6+1", AmmoType = "12mm", Shots = "20", RoF = "10", Reliability = "ST", Range = "200m", Price = 650 };
            DB.Weapons.Add(wp16, wp17, wp18);
            Weapon wp19 = new Weapon() { Name = "Militech Ronin LAR", Type = WeaponCategory.Assault_Rifle, WA = "+1", Concealability = "N", Availability = "C", Damage = "5D6", AmmoType = "5.56", Shots = "35", RoF = "30", Reliability = "VR", Range = "400m", Price = 450 };
            Weapon wp20 = new Weapon() { Name = "AKR-20 MAR", Type = WeaponCategory.Assault_Rifle, WA = "0", Concealability = "N", Availability = "C", Damage = "5D6", AmmoType = "5.56", Shots = "30", RoF = "30", Reliability = "ST", Range = "400m", Price = 500 };
            Weapon wp21 = new Weapon() { Name = "FN-RAL HAR", Type = WeaponCategory.Assault_Rifle, WA = "-1", Concealability = "N", Availability = "C", Damage = "6D6+2", AmmoType = "7.62", Shots = "30", RoF = "30", Reliability = "VR", Range = "400m", Price = 600 };
            Weapon wp22 = new Weapon() { Name = "Kalashnikov A80 HAR", Type = WeaponCategory.Assault_Rifle, WA = "-1", Concealability = "N", Availability = "E", Damage = "6D6+2", AmmoType = "7.62", Shots = "35", RoF = "25", Reliability = "ST", Range = "400m", Price = 550 };
            DB.Weapons.Add(wp19, wp20, wp21, wp22);
            Weapon wp23 = new Weapon() { Name = "Araska RA 12", Type = WeaponCategory.Shotgun, WA = "-1", Concealability = "N", Availability = "C", Damage = "4D6", AmmoType = "00", Shots = "20", RoF = "10", Reliability = "ST", Range = "50m", Price = 900 };
            Weapon wp24 = new Weapon() { Name = "Sternmeyer Stakeout 10", Type = WeaponCategory.Shotgun, WA = "-2", Concealability = "N", Availability = "R", Damage = "4D6", AmmoType = "00", Shots = "10", RoF = "2", Reliability = "ST", Range = "50m", Price = 450 };
            DB.Weapons.Add(wp23, wp24);

            SaveWeaponsXML();
        }

        public static void CreateCyberware()
        {
            DB.Cyberwares.Clear();
            Cyberware c1 = new Cyberware() { Name = "Biomonitor", Category = CyberwareCategory.Fashion, Humanity = 1, ModSkill = eSkill.Resist_Torture_Drugs, ModValue = 2, Price = 100, Description = "+2 to Resist Torture/Drugs", Surgery = "N" };
            Cyberware c2 = new Cyberware() { Name = "Skinwatch", Category = CyberwareCategory.Fashion, Humanity = 1, Price = 50, Description = "Subdermal Watch", Surgery = "N" };
            Cyberware c3 = new Cyberware() { Name = "Light Tattoo", Category = CyberwareCategory.Fashion, Humanity = 0.5f, Price = 15, Description = "Decorative Tattoo ", Surgery = "N" };
            Cyberware c4 = new Cyberware() { Name = "Shift-tacts", Category = CyberwareCategory.Fashion, Humanity = 0.5f, Price = 100, Description = "Color changing contact lenses", Surgery = "N" };
            Cyberware_D6 c5 = new Cyberware_D6() { Name = "ChemSkins", Category = CyberwareCategory.Fashion, Humanity = 0.5f, Price = 200, Description = "Color changing skin tints", Surgery = "N" };
            Cyberware_D6 c6 = new Cyberware_D6() { Name = "SynthSkins", Category = CyberwareCategory.Fashion, Humanity = 1.0f, Price = 400, Description = "Color changing artifical skin", Surgery = "N" };
            Cyberware c7 = new Cyberware() { Name = "Techhair", Category = CyberwareCategory.Fashion, Humanity = 2,Price = 100, Description = "Color/Light emitting artifical hair", Surgery = "N" };
            DB.Cyberwares.Add(c1, c2, c3, c4, c5, c6, c7);
            Cyberware_D6 c8 = new Cyberware_D6() { Name = "Neural Processor", Category = CyberwareCategory.Neural, Humanity = 1.0f, Description = "Basic processor. Required for al systems", Price = 1000, Surgery = "M" };
            Cyberware_D6 c9 = new Cyberware_D6() { Name = "Kerenzikov Booster [1]", Category = CyberwareCategory.Neural, Humanity = 1.0f, Description = "+1 to Initiative", Price = 500, Surgery = "N", Requires = "Neural Processor", Incompat = "Kerenzikov Booster[2]" };
            Cyberware_D6 c09 = new Cyberware_D6() { Name = "Kerenzikov Booster [2]", Category = CyberwareCategory.Neural, Humanity = 2.0f, Description = "+2 to Initiative", Price = 1000, Surgery = "N", Requires = "Neural Processor", Incompat = "Kerenzikov Booster [1]" };
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
            DB.Cyberwares.Add(c8, c9, c09, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20); 
            Cyberware c21 = new Cyberware() { Name = "Nasal Filter", Category = CyberwareCategory.Wear, Humanity = 2.0f, Price = 60, Description = "Stops toxic gases/fumes. 70% effective", Surgery = "M" };
            Cyberware_D6 c22 = new Cyberware_D6() { Name = "Gills", Category = CyberwareCategory.Wear, Humanity = 3.0f, Description = "Underwater breathing 4 hours", Price = 400, Surgery = "MA" };
            Cyberware_D6 c23 = new Cyberware_D6() { Name = "Independent Air Supply", Category = CyberwareCategory.Wear, Humanity = 2.0f, Description = "25min of air", Price = 300, Surgery = "MA" };
            Cyberware_D6 c24 = new Cyberware_D6() { Name = "Mr Studd", Category = CyberwareCategory.Wear, Humanity = 3.0f, Description = "All night, every night", Price = 300, Surgery = "MA" };
            Cyberware_D6 c25 = new Cyberware_D6() { Name = "Midnight Lady", Category = CyberwareCategory.Wear, Humanity = 3.0f, Description = "All night, every night", Price = 300, Surgery = "MA" };
            Cyberware c26 = new Cyberware() { Name = "Contraceptive Implant", Category = CyberwareCategory.Wear, Humanity = 0.5f, Price = 100, Description = "Good for 5 years, 98% eff.", Surgery = "N" };
            Cyberware_D6 c27 = new Cyberware_D6() { Name = "Subdermal Pocket", Category = CyberwareCategory.Wear, Humanity = 2.0f, Description = "5x10cm Realskinn storage", Price = 200, Surgery = "M" };
            Cyberware_D6 c28 = new Cyberware_D6() { Name = "Adrenal Booster", Category = CyberwareCategory.Wear, Humanity = 2.0f, Description = "REF +1 for 1D6+2 turns, 3/Day", Price = 400, Surgery = "M" };
            Cyberware_D6 c29 = new Cyberware_D6() { Name = "Subdermal Armor", Category = CyberwareCategory.Wear, Humanity = 2.0f, Description = "Armors torso to CP18\n **ARMOR WILL BE CALCULATED ON FINAL SHEET**", Price = 1200, Surgery = "CR" };
            Cyberware_D6 c30 = new Cyberware_D6() { Name = "Motion Detector", Category = CyberwareCategory.Wear, Humanity = 2.0f, Description = "Detects motion in 2m2. 70% eff.", Price = 200, Surgery = "M" };
            Cyberware c31 = new Cyberware() { Name = "Digital Recorder", Category = CyberwareCategory.Wear, Humanity = 2.0f, Price = 200, Description = "2h video digital storage. Needs source", Surgery = "M" };
            Cyberware c32 = new Cyberware() { Name = "A/V Recorder", Category = CyberwareCategory.Wear, Humanity = 2.0f, Price = 300, Description = "2h storage for Audio/Video. Needs source", Surgery = "M" };
            Cyberware c33 = new Cyberware() { Name = "Radar Sensor", Category = CyberwareCategory.Wear, Humanity = 2.0f, Price = 200, Description = "100m range. Requires Cyberoptics. 70% eff.", Surgery = "M", Requires = "Cyberoptics" };
            Cyberware c34 = new Cyberware() { Name = "Sonar Implant", Category = CyberwareCategory.Wear, Humanity = 2.0f, Price = 300, Description = "50m range. Underwater only. 70% eff.", Surgery = "M" };
            Cyberware c35 = new Cyberware() { Name = "Radiation Detector", Category = CyberwareCategory.Wear, Humanity = 2.0f, Price = 200, Description = "10m range. 80% effective", Surgery = "M" };
            Cyberware c36 = new Cyberware() { Name = "Chemical Analyser", Category = CyberwareCategory.Wear, Humanity = 2.0f, Price = 200, Description = "5m range. 70% effective", Surgery = "M" };
            Cyberware_D6 c37 = new Cyberware_D6() { Name = "Voice Synth", Category = CyberwareCategory.Wear, Humanity = 1.0f, Description = "Mimic any recorded sounds. Up to 10 different. 60% eff.", Price = 600, Surgery = "M" };
            Cyberware_D6 c38 = new Cyberware_D6() { Name = "AudioVox", Category = CyberwareCategory.Wear, Humanity = 2.0f, Description = "Vocal Synth for special effects. +2 PERFORM", Price = 200, Surgery = "M", ModSkill = eSkill.Perform, ModValue = 2 };
            DB.Cyberwares.Add(c21, c22, c23, c24, c25, c26, c27, c28, c29, c30, c31, c32, c33, c34, c35, c36, c37, c38);
            Cyberware_D6 c39 = new Cyberware_D6() { Name = "Grafted Muscle [1]", Category = CyberwareCategory.Bioware, Humanity = 2.0f, Description = "+1 to Body Type", Price = 1000, Surgery = "MA", ModStat = eStats.Body, ModValue = 1, Incompat = "Grafted Muscle [2]" };
            Cyberware_D6 c40 = new Cyberware_D6() { Name = "Grafted Muscle [2]", Category = CyberwareCategory.Bioware, Humanity = 2.0f, Description = "+2 to Body Type", Price = 2000, Surgery = "MA", ModStat = eStats.Body, ModValue = 2, Incompat = "Grafted Muscle [1]" };
            Cyberware_D6 c41 = new Cyberware_D6() { Name = "Muscle and Bone Lace", Category = CyberwareCategory.Bioware, Humanity = 0.5f, Description = "+2 to Body Type", Price = 1500, Surgery = "N", ModStat = eStats.Body, ModValue = 2 };
            Cyberware_D6 c42 = new Cyberware_D6() { Name = "Skin Weave", Category = CyberwareCategory.Bioware, Humanity = 2.0f, Description = "Armors body to SP 12\n **ARMOR WILL BE CALCULATED ON FINAL SHEET**", Price = 2000, Surgery = "N" };
            Cyberware_D6 c43 = new Cyberware_D6() { Name = "Enhanced Antibodies", Category = CyberwareCategory.Bioware, Humanity = 0.5f, Description = "Adds 1 Healing Point/Day", Price = 3000, Surgery = "N"};
            Cyberware_D6 c44 = new Cyberware_D6() { Name = "Toxin Binders", Category = CyberwareCategory.Bioware, Humanity = 0.5f, Description = "+4 Poison/Drug saves", Price = 3000, Surgery = "N"};
            Cyberware_D6 c45 = new Cyberware_D6() { Name = "Nanosurgeons", Category = CyberwareCategory.Bioware, Humanity = 0.5f, Description = "Doubles Healing rate", Price = 6000, Surgery = "N" };
            DB.Cyberwares.Add(c39, c40, c41, c42, c43, c44, c45);
            Cyberware_D6 c46 = new Cyberware_D6() { Name = "Scratchers", Category = CyberwareCategory.Wapons, Humanity = 2.0f, Description = "[Hands] 1D6/2 Damage", Price = 100, Surgery = "N", Max = 2 };
            Cyberware_D6 c47 = new Cyberware_D6() { Name = "Implanted Fangs", Category = CyberwareCategory.Wapons, Humanity = 3.0f, Description = "[Mouth] 1D6/3 Damage", Price = 200, Surgery = "N", Max = 2};
            Cyberware_D6 c48 = new Cyberware_D6() { Name = "Rippers", Category = CyberwareCategory.Wapons, Humanity = 3.0f, Description = "[Hands] 1D6+3 Damage", Price = 400, Surgery = "M", Max = 2};
            Cyberware_D6 c49 = new Cyberware_D6() { Name = "Wolvers", Category = CyberwareCategory.Wapons, Humanity = 3.0f, Description = "[Hands] 3D6 Damage", Price = 600, Surgery = "M", Max = 2 };
            Cyberware_D6 c50 = new Cyberware_D6() { Name = "Big Knucks", Category = CyberwareCategory.Wapons, Humanity = 3.0f, Description = "[Hands] 1D6+2 Damage", Price = 500, Surgery = "M", Max = 2 };
            Cyberware_D6 c51 = new Cyberware_D6() { Name = "Slice N Dice", Category = CyberwareCategory.Wapons, Humanity = 3.0f, Description = "[Hands] 2D6 Damage", Price = 700, Surgery = "M", Max = 2 };
            Cyberware_D6 c52 = new Cyberware_D6() { Name = "Cybersnake", Category = CyberwareCategory.Wapons, Humanity = 4.0f, Description = "Self-controlled, 1D6 Damage", Price = 1200, Surgery = "MA" };
            DB.Cyberwares.Add(c46, c47, c48, c49, c50, c51, c52);
            Cyberware_D6 c53 = new Cyberware_D6() { Name = "Cyberoptics", Category = CyberwareCategory.Optics, Humanity = 2.0f, Description = "Basic Module. 4 Options per eye \n **Eye slots are not enforced**", Price = 500, Surgery = "MA" };
            Cyberware c54 = new Cyberware() { Name = "Colorshift", Category = CyberwareCategory.Optics, Humanity = 0.5f, Price = 300, Description = "Allows color change and special fashion effects", Surgery = "N", Requires = "Cyberoptics" };
            Cyberware c55 = new Cyberware() { Name = "Image Enhancement", Category = CyberwareCategory.Optics, Humanity = 1.0f, Price = 300, Description = "+2 to Awareness using visuals. **Not added to Skills**", Surgery = "N", Requires = "Cyberoptics" };
            Cyberware c56 = new Cyberware() { Name = "Targetting Scope", Category = CyberwareCategory.Optics, Humanity = 2.0f, Price = 400, Description = "+1 to Smartgun Attack", Surgery = "N", Requires = "Cyberoptics" };
            Cyberware c57 = new Cyberware() { Name = "Times Square Marquee", Category = CyberwareCategory.Optics, Humanity = 1f, Price = 300, Description = "LED Screen in vision field", Surgery = "N", Requires = "Cyberoptics" };
            Cyberware c58 = new Cyberware() { Name = "Teleoptics", Category = CyberwareCategory.Optics, Humanity = 0.5f, Price = 150, Description = "Telescope ability to 20x", Surgery = "N", Requires = "Cyberoptics" };
            Cyberware c59 = new Cyberware() { Name = "Microoptics", Category = CyberwareCategory.Optics, Humanity = 0.5f, Price = 150, Description = "Microscope", Surgery = "N", Requires = "Cyberoptics" };
            Cyberware c60 = new Cyberware() { Name = "Anti Dazzle", Category = CyberwareCategory.Optics, Humanity = 0.5f, Price = 200, Description = "Inmune to flash, laser blinding...", Surgery = "N", Requires = "Cyberoptics" };
            Cyberware c61 = new Cyberware() { Name = "Low Lite", Category = CyberwareCategory.Optics, Humanity = 0.5f, Price = 200, Description = "Allows to see in dim light", Surgery = "N", Requires = "Cyberoptics" };
            Cyberware c62 = new Cyberware() { Name = "Thermograph Sensor", Category = CyberwareCategory.Optics, Humanity = 1f, Price = 200, Description = "Allows to see heat patterns", Surgery = "N", Requires = "Cyberoptics" };
            Cyberware c63 = new Cyberware() { Name = "Infrared", Category = CyberwareCategory.Optics, Humanity = 1f, Price = 200, Description = "Allow to see in total darkness, using heat", Surgery = "N", Requires = "Cyberoptics" };
            Cyberware c64 = new Cyberware() { Name = "Ultraviolet", Category = CyberwareCategory.Optics, Humanity = 1f, Price = 200, Description = "Allows to see in darkness using UV Flashlight", Surgery = "N", Requires = "Cyberoptics" };
            Cyberware c65 = new Cyberware() { Name = "MicroVideo Optic", Category = CyberwareCategory.Optics, Humanity = 0.5f, Price = 300, Description = "Video record, up to 20min [Uses 2 eye options]", Surgery = "N", Requires = "Cyberoptics" };
            Cyberware c66 = new Cyberware() { Name = "Digital Camera", Category = CyberwareCategory.Optics, Humanity = 0.5f, Price = 300, Description = "Take up to 20 photos[Uses 2 eye options]", Surgery = "N", Requires = "Cyberoptics" };
            Cyberware c67 = new Cyberware() { Name = "Dartgun", Category = CyberwareCategory.Optics, Humanity = 2f, Price = 200, Description = "Poison Weapon. 1 Dart [Uses 3 eye options]", Surgery = "N", Requires = "Cyberoptics" };
            DB.Cyberwares.Add(c53, c54, c55, c56, c57, c58, c59, c60, c61, c62, c63, c64, c65, c66, c67);
            Cyberware_D6 c68 = new Cyberware_D6() { Name = "Cyberaudio", Category = CyberwareCategory.Audio, Humanity = 2.0f, Description = "Basic hearing Module. No option limit", Price = 500, Surgery = "M" };
            Cyberware c69 = new Cyberware() { Name = "Amplified Hearing", Category = CyberwareCategory.Audio, Humanity = 1f, Price = 200, Description = "+1 Awareness when using auditory cues **Not added to skills**", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c70 = new Cyberware() { Name = "Radio Link", Category = CyberwareCategory.Audio, Humanity = 1.0f, Price = 100, Description = "Radio communication up to 1.5Km", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c71 = new Cyberware() { Name = "Phone Slice", Category = CyberwareCategory.Audio, Humanity = 1.0f, Price = 150, Description = "Full cellular communication", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c72 = new Cyberware() { Name = "Scrambler", Category = CyberwareCategory.Audio, Humanity = 0.5f, Price = 100, Description = "Cannot overhear communications w/o descrambler", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c73 = new Cyberware() { Name = "Bug Detector", Category = CyberwareCategory.Audio, Humanity = 0.5f, Price = 200, Description = "Detects taps and bugs up to 3m. 60% eff", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c74 = new Cyberware() { Name = "Voice Stress Analyzer", Category = CyberwareCategory.Audio, Humanity = 1f, Price = 200, Description = "Lie detector. +2 to Human Perception and Interrogation **Not added to skills**", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c75 = new Cyberware() { Name = "Sound Editing", Category = CyberwareCategory.Audio, Humanity = 0.5f, Price = 150, Description = "+2 to Awareness to overhear conversations **Not added to skills**", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c76 = new Cyberware() { Name = "Enhanced Hearing Range", Category = CyberwareCategory.Audio, Humanity = 2f, Price = 150, Description = "Ability to hear super and subsonic ranges", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c77 = new Cyberware() { Name = "Wearman", Category = CyberwareCategory.Audio, Humanity = 0.5f, Price = 100, Description = "Stereo music system", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c78 = new Cyberware() { Name = "Radar Detector", Category = CyberwareCategory.Audio, Humanity = 0.5f, Price = 150, Description = "Beeps if finds radar beam, and fixes source. 40% eff", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c79 = new Cyberware() { Name = "Homing Tracker", Category = CyberwareCategory.Audio, Humanity = 0.5f, Price = 200, Description = "Can follow a tracer up to 1Km", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c80 = new Cyberware() { Name = "Tightbeam Radio Link", Category = CyberwareCategory.Audio, Humanity = 1f, Price = 200, Description = "Allows untappable radio comm within LoS", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c81 = new Cyberware() { Name = "Wide Band Radio Scanner", Category = CyberwareCategory.Audio, Humanity = 2f, Price = 100, Description = "Picks transmissions on all bands", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c82 = new Cyberware() { Name = "Microrecorder Link", Category = CyberwareCategory.Audio, Humanity = 0.5f, Price = 100, Description = "Transmits to a recorder in body or via plugs", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c83 = new Cyberware() { Name = "Digital recording Link", Category = CyberwareCategory.Audio, Humanity = 0.5f, Price = 100, Description = "Transmits sounds to a digital recorder", Surgery = "N", Requires = "Cyberaudio" };
            Cyberware c84 = new Cyberware() { Name = "Level Damper", Category = CyberwareCategory.Audio, Humanity = 0.5f, Price = 300, Description = "Automatic noise compensation", Surgery = "N", Requires = "Cyberaudio" };
            DB.Cyberwares.Add(c68, c69, c70, c71, c72, c73, c74, c75, c76, c77, c78, c79, c80, c81, c82, c83, c84);

            SaveCyberwareXML();
        }
    }
}
