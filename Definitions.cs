using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CP48
{
    public enum eGender
    {
        Male,
        Female,
        Other
    }
    public enum eSkill
    {
        Authority,
        Charimatic_Leadership,
        Combat_Sense,
        Credibility,
        Family,
        Interface,
        Jury_Rig,
        Medical_Tech,
        Resources,
        StreetDeal,
        Personal_Grooming,
        Wardrobe,
        Endurance,
        Strength_Feat,
        Swimming,
        Interrogation,
        Intimidate,
        Oratory,
        Resist_Torture_Drugs,
        Streetwise,
        Human_Perception,
        Interview,
        Leadership,
        Seduction,
        Social,
        Persuasion,
        Perform,
        Accounting,
        Anthropology,
        Awareness_Notice,
        Biology,
        Botany,
        Chemistry,
        Composition,
        Diagnose_Illness,
        Education_General_Knowledge,
        Expert,
        Gamble,
        Geology,
        Hide_Evade,
        History,
        Language,
        Library_Search,
        Mathematics,
        Physics,
        Programming,
        Shadow_Track,
        Stock_Market,
        System_Knowledge,
        Teaching,
        Survival,
        Zoology,
        Archery,
        Athletics,
        Brawling,
        Dance,
        Dodge,
        Driving,
        Fencing,
        Handgun,
        Heavy_Weapons,
        Martial_Arts,
        Melee,
        Motorcycle,
        Heavy_Machinery,
        Pilot_Gyro,
        Pilot_Fixed_Wing,
        Pilot_Dirigible,
        Pilot_VTV,
        Rifle,
        Steath,
        Submachine_Gun,
        AeroTech,
        AVTech,
        Basic_Tech,
        Cryotank_Operation,
        Cyberdeck_Design,
        Cybertech,
        Demolitions,
        Disguise,
        Electronics,
        Electronic_Security,
        First_Aid,
        Forgery,
        Gyro_Tech,
        Paint_Draw,
        Photography_Film,
        Pharmaceuticals,
        Pick_Lock,
        Pick_Pocket,
        Play_Instrument,
        Weaponsmith,
        Other
    }
    public enum eStats
    {
        Int,
        Ref,
        Tech,
        Cool,
        Attr,
        Luck,
        MA,
        Body,
        Emp,
        None
    }
    [System.Serializable]
    public class Skill
    {
        public eSkill ID;
        public string AdditionalData = "";
        public int Value;
        public eStats GetSkillRelation()
        {
            switch (ID)
            {
                case eSkill.Authority:
                case eSkill.Charimatic_Leadership:
                case eSkill.Combat_Sense:
                case eSkill.Credibility:
                case eSkill.Family:
                case eSkill.Interface:
                case eSkill.Jury_Rig:
                case eSkill.Medical_Tech:
                case eSkill.Resources:
                case eSkill.StreetDeal:
                    return eStats.None;
                case eSkill.Personal_Grooming:
                case eSkill.Wardrobe:
                    return eStats.Attr;
                case eSkill.Endurance:
                case eSkill.Strength_Feat:
                case eSkill.Swimming:
                    return eStats.Body;
                case eSkill.Interrogation:
                case eSkill.Intimidate:
                case eSkill.Oratory:
                case eSkill.Resist_Torture_Drugs:
                    return eStats.Cool;
                case eSkill.Streetwise:
                case eSkill.Human_Perception:
                case eSkill.Interview:
                case eSkill.Leadership:
                case eSkill.Seduction:
                case eSkill.Social:
                case eSkill.Persuasion:
                case eSkill.Perform:
                    return eStats.Emp;
                case eSkill.Accounting:
                case eSkill.Anthropology:
                case eSkill.Awareness_Notice:
                case eSkill.Biology:
                case eSkill.Botany:
                case eSkill.Chemistry:
                case eSkill.Composition:
                case eSkill.Diagnose_Illness:
                case eSkill.Education_General_Knowledge:
                case eSkill.Expert:
                case eSkill.Gamble:
                case eSkill.Geology:
                case eSkill.Hide_Evade:
                case eSkill.History:
                case eSkill.Language:
                case eSkill.Library_Search:
                case eSkill.Mathematics:
                case eSkill.Physics:
                case eSkill.Programming:
                case eSkill.Shadow_Track:
                case eSkill.Stock_Market:
                case eSkill.System_Knowledge:
                case eSkill.Teaching:
                case eSkill.Survival:
                case eSkill.Zoology:
                    return eStats.Int;
                case eSkill.Archery:
                case eSkill.Athletics:
                case eSkill.Brawling:
                case eSkill.Dance:
                case eSkill.Dodge:
                case eSkill.Driving:
                case eSkill.Fencing:
                case eSkill.Handgun:
                case eSkill.Heavy_Weapons:
                case eSkill.Martial_Arts:
                case eSkill.Melee:
                case eSkill.Motorcycle:
                case eSkill.Heavy_Machinery:
                case eSkill.Pilot_Gyro:
                case eSkill.Pilot_Fixed_Wing:
                case eSkill.Pilot_Dirigible:
                case eSkill.Pilot_VTV:
                case eSkill.Rifle:
                case eSkill.Steath:
                case eSkill.Submachine_Gun:
                    return eStats.Ref;
                case eSkill.AeroTech:
                case eSkill.AVTech:
                case eSkill.Basic_Tech:
                case eSkill.Cryotank_Operation:
                case eSkill.Cyberdeck_Design:
                case eSkill.Cybertech:
                case eSkill.Demolitions:
                case eSkill.Disguise:
                case eSkill.Electronics:
                case eSkill.Electronic_Security:
                case eSkill.First_Aid:
                case eSkill.Forgery:
                case eSkill.Gyro_Tech:
                case eSkill.Paint_Draw:
                case eSkill.Photography_Film:
                case eSkill.Pharmaceuticals:
                case eSkill.Pick_Lock:
                case eSkill.Pick_Pocket:
                case eSkill.Play_Instrument:
                case eSkill.Weaponsmith:
                    return eStats.Tech;
                default:
                    return eStats.None;
            }
        }
    }

    [Serializable]
    public class Stat
    {
        public eStats ID;
        public int Value;
        public int Remaining;
    }
    [Serializable]
    public class StatValues
    {
        public Stat Int = new Stat() { ID = eStats.Int };
        public Stat Ref = new Stat() { ID = eStats.Ref };
        public Stat Tech = new Stat() { ID = eStats.Tech };
        public Stat Cool = new Stat() { ID = eStats.Cool };
        public Stat Attr = new Stat() { ID = eStats.Attr };
        public Stat Luck = new Stat() { ID = eStats.Luck };
        public Stat MA = new Stat() { ID = eStats.MA };
        public Stat Body = new Stat() { ID = eStats.Body };
        public Stat Emp = new Stat() { ID = eStats.Emp };
        public int Run { get { return MA.Value * 3; } }
        public int Leap { get { return Math.Min(Math.Max((int)Math.Floor(Run * 0.25f), 1), 99999); } }
        public int Lift { get { return Body.Value * 40; } }
        public int SaveValue { get { return Body.Value; } }
        public int BTCValue
        {
            get
            {
                if (Body.Value <= 2)
                    return 0;
                if (Body.Value <= 4)
                    return -1;
                if (Body.Value <= 7)
                    return -2;
                if (Body.Value <= 9)
                    return -3;
                if (Body.Value == 10)
                    return -4;
                else
                    return -5;
            }
        }
    }
    public enum eRole
    {
        Solo,
        Rocker,
        Netrunner,
        Media,
        Nomad,
        Fixer,
        Cop,
        Corpo,
        Techie,
        Medtech
    }
    [System.Serializable]
    public class Sheet
    {
        public static readonly int[] AdditionalTextSkills = { 36, 41, 61, 90, 92 };
        public static readonly int[] CanBeRepeatedSkills = { 36, 41, 92 };
        public static readonly Dictionary<eRole, int[]> InitialFundsTable = new Dictionary<eRole, int[]>()
        {
            {eRole.Rocker, new int[11]{0,10,10,10,10,10,15,20,50,80,120} },
            {eRole.Solo, new int[11]{0,20,20,20,20,20,30,45,70,90,120} },
            {eRole.Cop, new int[11]{0,10,10,10,10,10,12,30,50,70,90} },
            {eRole.Corpo, new int[11]{0,15,15,15,15,15,30,50,70,90,120} },
            {eRole.Media, new int[11]{0,10,10,10,10,10,12,30,50,70,100} },
            {eRole.Fixer, new int[11]{0,15,15,15,15,15,30,50,70,80,100} },
            {eRole.Techie, new int[11]{0,10,10,10,10,10,20,30,40,50,80} },
            {eRole.Netrunner, new int[11]{0,10,10,10,10,10,20,30,50,70,100} },
            {eRole.Medtech, new int[11]{0,16,16,16,16,16,30,50,70,100,150} },
            {eRole.Nomad, new int[11]{0,10,10,10,10,10,15,20,30,40,60} }
        };
        [Serializable]
        public class ItemTuple
        {
            public Item item;
            public int quantity;
        }
        [Serializable]
        public class WeaponTuple
        {
            public Weapon weapon;
            public int quantity;
        }
        public string Name;
        public eRole Role;
        public int AvailablePoints;
        public StatValues Stats = new StatValues();
        public List<Skill> Skills = new List<Skill>();
        public int MonthsWorked;
        public int InitialFunds;
        public eGender Gender;
        public int Age;
        public List<ItemTuple> Items = new List<ItemTuple>();
        public List<WeaponTuple> Weapons = new List<WeaponTuple>();
        public int MaxProfessionalSkillPoints { get { return 40; } }
        public int MaxFreeSkillPoints { get { return Stats.Int.Value + Stats.Ref.Value; } }
        public static List<Skill> GetSkillPack(eRole role)
        {
            List<Skill> res = new List<Skill>();
            switch (role)
            {
                case eRole.Solo:
                    Skill a1 = new Skill { ID = eSkill.Combat_Sense };
                    Skill b1 = new Skill { ID = eSkill.Awareness_Notice };
                    Skill c1 = new Skill { ID = eSkill.Handgun };
                    Skill d1 = new Skill { ID = eSkill.Brawling };
                    Skill e1 = new Skill { ID = eSkill.Martial_Arts };
                    Skill f1 = new Skill { ID = eSkill.Melee };
                    Skill g1 = new Skill { ID = eSkill.Weaponsmith };
                    Skill h1 = new Skill { ID = eSkill.Rifle };
                    Skill i1 = new Skill { ID = eSkill.Athletics };
                    Skill j1 = new Skill { ID = eSkill.Submachine_Gun };
                    Skill k1 = new Skill { ID = eSkill.Steath };
                    res.Add(a1);
                    res.Add(b1);
                    res.Add(c1);
                    res.Add(d1);
                    res.Add(e1);
                    res.Add(f1);
                    res.Add(g1);
                    res.Add(h1);
                    res.Add(i1);
                    res.Add(j1);
                    res.Add(k1);
                    return res;
                case eRole.Rocker:
                    Skill a2 = new Skill { ID = eSkill.Charimatic_Leadership };
                    Skill b2 = new Skill { ID = eSkill.Awareness_Notice };
                    Skill c2 = new Skill { ID = eSkill.Perform };
                    Skill d2 = new Skill { ID = eSkill.Wardrobe };
                    Skill e2 = new Skill { ID = eSkill.Composition };
                    Skill f2 = new Skill { ID = eSkill.Brawling };
                    Skill g2 = new Skill { ID = eSkill.Play_Instrument };
                    Skill h2 = new Skill { ID = eSkill.Streetwise };
                    Skill i2 = new Skill { ID = eSkill.Persuasion };
                    Skill j2 = new Skill { ID = eSkill.Seduction };
                    res.Add(a2);
                    res.Add(b2);
                    res.Add(c2);
                    res.Add(d2);
                    res.Add(e2);
                    res.Add(f2);
                    res.Add(g2);
                    res.Add(h2);
                    res.Add(i2);
                    res.Add(j2);
                    return res;
                case eRole.Netrunner:
                    Skill a3 = new Skill { ID = eSkill.Interface };
                    Skill b3 = new Skill { ID = eSkill.Awareness_Notice };
                    Skill c3 = new Skill { ID = eSkill.Basic_Tech };
                    Skill d3 = new Skill { ID = eSkill.Education_General_Knowledge };
                    Skill e3 = new Skill { ID = eSkill.System_Knowledge };
                    Skill f3 = new Skill { ID = eSkill.Cybertech };
                    Skill g3 = new Skill { ID = eSkill.Cyberdeck_Design };
                    Skill h3 = new Skill { ID = eSkill.Composition };
                    Skill i3 = new Skill { ID = eSkill.Electronics };
                    Skill j3 = new Skill { ID = eSkill.Programming };
                    res.Add(a3);
                    res.Add(b3);
                    res.Add(c3);
                    res.Add(d3);
                    res.Add(e3);
                    res.Add(f3);
                    res.Add(g3);
                    res.Add(h3);
                    res.Add(i3);
                    res.Add(j3);
                    return res;
                case eRole.Media:
                    Skill a4 = new Skill { ID = eSkill.Credibility };
                    Skill b4 = new Skill { ID = eSkill.Awareness_Notice };
                    Skill c4 = new Skill { ID = eSkill.Composition };
                    Skill d4 = new Skill { ID = eSkill.Education_General_Knowledge };
                    Skill e4 = new Skill { ID = eSkill.Persuasion };
                    Skill f4 = new Skill { ID = eSkill.Human_Perception };
                    Skill g4 = new Skill { ID = eSkill.Social };
                    Skill h4 = new Skill { ID = eSkill.Streetwise };
                    Skill i4 = new Skill { ID = eSkill.Photography_Film };
                    Skill j4 = new Skill { ID = eSkill.Interview };
                    res.Add(a4);
                    res.Add(b4);
                    res.Add(c4);
                    res.Add(d4);
                    res.Add(e4);
                    res.Add(f4);
                    res.Add(g4);
                    res.Add(h4);
                    res.Add(i4);
                    res.Add(j4);
                    return res;
                case eRole.Nomad:
                    Skill a5 = new Skill { ID = eSkill.Family };
                    Skill b5 = new Skill { ID = eSkill.Awareness_Notice };
                    Skill c5 = new Skill { ID = eSkill.Endurance };
                    Skill d5 = new Skill { ID = eSkill.Melee };
                    Skill e5 = new Skill { ID = eSkill.Rifle };
                    Skill f5 = new Skill { ID = eSkill.Driving };
                    Skill g5 = new Skill { ID = eSkill.Basic_Tech };
                    Skill h5 = new Skill { ID = eSkill.Survival };
                    Skill i5 = new Skill { ID = eSkill.Brawling };
                    Skill j5 = new Skill { ID = eSkill.Athletics };
                    res.Add(a5);
                    res.Add(b5);
                    res.Add(c5);
                    res.Add(d5);
                    res.Add(e5);
                    res.Add(f5);
                    res.Add(g5);
                    res.Add(h5);
                    res.Add(i5);
                    res.Add(j5);
                    return res;
                case eRole.Fixer:
                    Skill a6 = new Skill { ID = eSkill.StreetDeal };
                    Skill b6 = new Skill { ID = eSkill.Awareness_Notice };
                    Skill c6 = new Skill { ID = eSkill.Forgery };
                    Skill d6 = new Skill { ID = eSkill.Handgun };
                    Skill e6 = new Skill { ID = eSkill.Brawling };
                    Skill f6 = new Skill { ID = eSkill.Melee };
                    Skill g6 = new Skill { ID = eSkill.Pick_Lock };
                    Skill h6 = new Skill { ID = eSkill.Pick_Pocket };
                    Skill i6 = new Skill { ID = eSkill.Intimidate };
                    Skill j6 = new Skill { ID = eSkill.Persuasion };
                    res.Add(a6);
                    res.Add(b6);
                    res.Add(c6);
                    res.Add(d6);
                    res.Add(e6);
                    res.Add(f6);
                    res.Add(g6);
                    res.Add(h6);
                    res.Add(i6);
                    res.Add(j6);
                    return res;
                case eRole.Cop:
                    Skill a7 = new Skill { ID = eSkill.Authority };
                    Skill b7 = new Skill { ID = eSkill.Awareness_Notice };
                    Skill c7 = new Skill { ID = eSkill.Handgun };
                    Skill d7 = new Skill { ID = eSkill.Human_Perception };
                    Skill e7 = new Skill { ID = eSkill.Athletics };
                    Skill f7 = new Skill { ID = eSkill.Education_General_Knowledge };
                    Skill g7 = new Skill { ID = eSkill.Brawling };
                    Skill h7 = new Skill { ID = eSkill.Melee };
                    Skill i7 = new Skill { ID = eSkill.Interrogation };
                    Skill j7 = new Skill { ID = eSkill.Streetwise };
                    res.Add(a7);
                    res.Add(b7);
                    res.Add(c7);
                    res.Add(d7);
                    res.Add(e7);
                    res.Add(f7);
                    res.Add(g7);
                    res.Add(h7);
                    res.Add(i7);
                    res.Add(j7);
                    return res;
                case eRole.Corpo:
                    Skill a8 = new Skill { ID = eSkill.Resources };
                    Skill b8 = new Skill { ID = eSkill.Awareness_Notice };
                    Skill c8 = new Skill { ID = eSkill.Human_Perception };
                    Skill d8 = new Skill { ID = eSkill.Education_General_Knowledge };
                    Skill e8 = new Skill { ID = eSkill.Library_Search };
                    Skill f8 = new Skill { ID = eSkill.Social };
                    Skill g8 = new Skill { ID = eSkill.Persuasion };
                    Skill h8 = new Skill { ID = eSkill.Stock_Market };
                    Skill i8 = new Skill { ID = eSkill.Wardrobe };
                    Skill j8 = new Skill { ID = eSkill.Personal_Grooming };
                    res.Add(a8);
                    res.Add(b8);
                    res.Add(c8);
                    res.Add(d8);
                    res.Add(e8);
                    res.Add(f8);
                    res.Add(g8);
                    res.Add(h8);
                    res.Add(i8);
                    res.Add(j8);
                    return res;
                case eRole.Techie:
                    Skill a9 = new Skill { ID = eSkill.Jury_Rig };
                    Skill b9 = new Skill { ID = eSkill.Awareness_Notice };
                    Skill c9 = new Skill { ID = eSkill.Basic_Tech };
                    Skill d9 = new Skill { ID = eSkill.Cybertech };
                    Skill e9 = new Skill { ID = eSkill.Teaching };
                    Skill f9 = new Skill { ID = eSkill.Education_General_Knowledge };
                    Skill g9 = new Skill { ID = eSkill.Electronics };
                    Skill h9 = new Skill { ID = eSkill.Weaponsmith };
                    Skill i9 = new Skill { ID = eSkill.Gyro_Tech };
                    Skill j9 = new Skill { ID = eSkill.Electronic_Security };
                    Skill k9 = new Skill { ID = eSkill.AeroTech };
                    res.Add(a9);
                    res.Add(b9);
                    res.Add(c9);
                    res.Add(d9);
                    res.Add(e9);
                    res.Add(f9);
                    res.Add(g9);
                    res.Add(h9);
                    res.Add(i9);
                    res.Add(j9);
                    res.Add(k9);
                    return res;
                case eRole.Medtech:
                    Skill a0 = new Skill { ID = eSkill.Medical_Tech };
                    Skill b0 = new Skill { ID = eSkill.Awareness_Notice };
                    Skill c0 = new Skill { ID = eSkill.Basic_Tech };
                    Skill d0 = new Skill { ID = eSkill.Diagnose_Illness };
                    Skill e0 = new Skill { ID = eSkill.Education_General_Knowledge };
                    Skill f0 = new Skill { ID = eSkill.Cryotank_Operation };
                    Skill g0 = new Skill { ID = eSkill.Library_Search };
                    Skill h0 = new Skill { ID = eSkill.Pharmaceuticals };
                    Skill i0 = new Skill { ID = eSkill.Zoology };
                    Skill j0 = new Skill { ID = eSkill.Human_Perception };
                    res.Add(a0);
                    res.Add(b0);
                    res.Add(c0);
                    res.Add(d0);
                    res.Add(e0);
                    res.Add(f0);
                    res.Add(g0);
                    res.Add(h0);
                    res.Add(i0);
                    res.Add(j0);
                    return res;
            }
            return res;
        }
        public static string eSkillToString(eSkill skill)
        {
            switch (skill)
            {

                case eSkill.Authority:
                    return "Authority";
                case eSkill.Charimatic_Leadership:
                    return "Charimatic Leadership";
                case eSkill.Combat_Sense:
                    return "Combat Sense";
                case eSkill.Credibility:
                    return "Credibility";
                case eSkill.Family:
                    return "Family";
                case eSkill.Interface:
                    return "Interface";
                case eSkill.Jury_Rig:
                    return "Jury Rig";
                case eSkill.Medical_Tech:
                    return "Medical Tech";
                case eSkill.Resources:
                    return "Resources";
                case eSkill.StreetDeal:
                    return "StreetDeal";
                case eSkill.Personal_Grooming:
                    return "Personal Grooming";
                case eSkill.Wardrobe:
                    return "Wardrobe";
                case eSkill.Endurance:
                    return "Endurance";
                case eSkill.Strength_Feat:
                    return "Strength Feat";
                case eSkill.Swimming:
                    return "Swimming";
                case eSkill.Interrogation:
                    return "Interrogation";
                case eSkill.Intimidate:
                    return "Intimidate";
                case eSkill.Oratory:
                    return "Oratory";
                case eSkill.Resist_Torture_Drugs:
                    return "Resist Torture/Drugs";
                case eSkill.Streetwise:
                    return "Streetwise";
                case eSkill.Human_Perception:
                    return "Human Perception";
                case eSkill.Interview:
                    return "Interview";
                case eSkill.Leadership:
                    return "Leadership";
                case eSkill.Seduction:
                    return "Seduction";
                case eSkill.Social:
                    return "Social";
                case eSkill.Persuasion:
                    return "Persuasion";
                case eSkill.Perform:
                    return "Perform";
                case eSkill.Accounting:
                    return "Accounting";
                case eSkill.Anthropology:
                    return "Anthropology";
                case eSkill.Awareness_Notice:
                    return "Awareness/Notice";
                case eSkill.Biology:
                    return "Biology";
                case eSkill.Botany:
                    return "Botany";
                case eSkill.Chemistry:
                    return "Chemistry";
                case eSkill.Composition:
                    return "Composition";
                case eSkill.Diagnose_Illness:
                    return "Diagnose Illness";
                case eSkill.Education_General_Knowledge:
                    return "Education/General Knowledge";
                case eSkill.Expert:
                    return "Expert";
                case eSkill.Gamble:
                    return "Gamble";
                case eSkill.Geology:
                    return "Geology";
                case eSkill.Hide_Evade:
                    return "Hide/Evade";
                case eSkill.History:
                    return "History";
                case eSkill.Language:
                    return "Language";
                case eSkill.Library_Search:
                    return "Library Search";
                case eSkill.Mathematics:
                    return "Mathematics";
                case eSkill.Physics:
                    return "Physics";
                case eSkill.Programming:
                    return "Programming";
                case eSkill.Shadow_Track:
                    return "Shadow/Track";
                case eSkill.Stock_Market:
                    return "Stock Market";
                case eSkill.System_Knowledge:
                    return "System Knowledge";
                case eSkill.Teaching:
                    return "Teaching";
                case eSkill.Survival:
                    return "Survival";
                case eSkill.Zoology:
                    return "Zoology";
                case eSkill.Archery:
                    return "Archery";
                case eSkill.Athletics:
                    return "Athletics";
                case eSkill.Brawling:
                    return "Brawling";
                case eSkill.Dance:
                    return "Dance";
                case eSkill.Dodge:
                    return "Dodge";
                case eSkill.Driving:
                    return "Driving";
                case eSkill.Fencing:
                    return "Fencing";
                case eSkill.Handgun:
                    return "Handgun";
                case eSkill.Heavy_Weapons:
                    return "Heavy Weapons";
                case eSkill.Martial_Arts:
                    return "Martial Arts";
                case eSkill.Melee:
                    return "Melee";
                case eSkill.Motorcycle:
                    return "Motorcycle";
                case eSkill.Heavy_Machinery:
                    return "Heavy Machinery";
                case eSkill.Pilot_Gyro:
                    return "Pilot - Gyro";
                case eSkill.Pilot_Fixed_Wing:
                    return "Pilot - Fixed_Wing";
                case eSkill.Pilot_Dirigible:
                    return "Pilot - Dirigible";
                case eSkill.Pilot_VTV:
                    return "Pilot - VTV";
                case eSkill.Rifle:
                    return "Rifle";
                case eSkill.Steath:
                    return "Steath";
                case eSkill.Submachine_Gun:
                    return "Submachine Gun";
                case eSkill.AeroTech:
                    return "AeroTech";
                case eSkill.AVTech:
                    return "AVTech";
                case eSkill.Basic_Tech:
                    return "Basic Tech";
                case eSkill.Cryotank_Operation:
                    return "Cryotank Operation";
                case eSkill.Cyberdeck_Design:
                    return "Cyberdeck Design";
                case eSkill.Cybertech:
                    return "Cybertech";
                case eSkill.Demolitions:
                    return "Demolitions";
                case eSkill.Disguise:
                    return "Disguise";
                case eSkill.Electronics:
                    return "Electronics";
                case eSkill.Electronic_Security:
                    return "Electronic Security";
                case eSkill.First_Aid:
                    return "First Aid";
                case eSkill.Forgery:
                    return "Forgery";
                case eSkill.Gyro_Tech:
                    return "Gyro Tech";
                case eSkill.Paint_Draw:
                    return "Paint/Draw";
                case eSkill.Photography_Film:
                    return "Photography/Film";
                case eSkill.Pharmaceuticals:
                    return "Pharmaceuticals";
                case eSkill.Pick_Lock:
                    return "Pick Lock";
                case eSkill.Pick_Pocket:
                    return "Pick Pocket";
                case eSkill.Play_Instrument:
                    return "Play Instrument";
                case eSkill.Weaponsmith:
                    return "Weaponsmith";
                case eSkill.Other:
                    return "Other";
                default:
                    return string.Empty;
            }
        }
        public static eSkill StringToeSkill(string skill)
        {
            string uppercase = skill.ToUpper();
            switch (uppercase)
            {
                case "AUTHORITY":
                    return eSkill.Authority;
                case "CHARIMATIC LEADERSHIP":
                    return eSkill.Charimatic_Leadership;
                case "COMBAT SENSE":
                    return eSkill.Combat_Sense;
                case "CREDIBILITY":
                    return eSkill.Credibility;
                case "FAMILY":
                    return eSkill.Family;
                case "INTERFACE":
                    return eSkill.Interface;
                case "JURY RIG":
                    return eSkill.Jury_Rig;
                case "MEDICAL TECH":
                    return eSkill.Medical_Tech;
                case "RESOURCES":
                    return eSkill.Resources;
                case "STREETDEAL":
                    return eSkill.StreetDeal;
                case "PERSONAL GROOMING":
                    return eSkill.Personal_Grooming;
                case "WARDROBE":
                    return eSkill.Wardrobe;
                case "ENDURANCE":
                    return eSkill.Endurance;
                case "STRENGTH FEAT":
                    return eSkill.Strength_Feat;
                case "SWIMMING":
                    return eSkill.Swimming;
                case "INTERROGATION":
                    return eSkill.Interrogation;
                case "INTIMIDATE":
                    return eSkill.Intimidate;
                case "ORATORY":
                    return eSkill.Oratory;
                case "RESIST TORTURE/DRUGS":
                    return eSkill.Resist_Torture_Drugs;
                case "STREETWISE":
                    return eSkill.Streetwise;
                case "HUMAN PERCEPTION":
                    return eSkill.Human_Perception;
                case "INTERVIEW":
                    return eSkill.Interview;
                case "LEADERSHIP":
                    return eSkill.Leadership;
                case "SEDUCTION":
                    return eSkill.Seduction;
                case "SOCIAL":
                    return eSkill.Social;
                case "PERSUASION":
                    return eSkill.Persuasion;
                case "PERFORM":
                    return eSkill.Perform;
                case "ACCOUNTING":
                    return eSkill.Accounting;
                case "ANTHROPOLOGY":
                    return eSkill.Anthropology;
                case "AWARENESS/NOTICE":
                    return eSkill.Awareness_Notice;
                case "BIOLOGY":
                    return eSkill.Biology;
                case "BOTANY":
                    return eSkill.Botany;
                case "CHEMISTRY":
                    return eSkill.Chemistry;
                case "COMPOSITION":
                    return eSkill.Composition;
                case "DIAGNOSE ILLNESS":
                    return eSkill.Diagnose_Illness;
                case "EDUCATION/GENERAL KNOWLEDGE":
                    return eSkill.Education_General_Knowledge;
                case "EXPERT":
                    return eSkill.Expert;
                case "GAMBLE":
                    return eSkill.Gamble;
                case "GEOLOGY":
                    return eSkill.Geology;
                case "HIDE/EVADE":
                    return eSkill.Hide_Evade;
                case "HISTORY":
                    return eSkill.History;
                case "LANGUAGE":
                    return eSkill.Language;
                case "LIBRARY SEARCH":
                    return eSkill.Library_Search;
                case "MATHEMATICS":
                    return eSkill.Mathematics;
                case "PHYSICS":
                    return eSkill.Physics;
                case "PROGRAMMING":
                    return eSkill.Programming;
                case "SHADOW/TRACK":
                    return eSkill.Shadow_Track;
                case "STOCK MARKET":
                    return eSkill.Stock_Market;
                case "SYSTEM KNOWLEDGE":
                    return eSkill.System_Knowledge;
                case "TEACHING":
                    return eSkill.Teaching;
                case "SURVIVAL":
                    return eSkill.Survival;
                case "ZOOLOGY":
                    return eSkill.Zoology;
                case "ARCHERY":
                    return eSkill.Archery;
                case "ATHLETICS":
                    return eSkill.Athletics;
                case "BRAWLING":
                    return eSkill.Brawling;
                case "DANCE":
                    return eSkill.Dance;
                case "DODGE":
                    return eSkill.Dodge;
                case "DRIVING":
                    return eSkill.Driving;
                case "FENCING":
                    return eSkill.Fencing;
                case "HANDGUN":
                    return eSkill.Handgun;
                case "HEAVY WEAPONS":
                    return eSkill.Heavy_Weapons;
                case "MARTIAL ARTS":
                    return eSkill.Martial_Arts;
                case "MELEE":
                    return eSkill.Melee;
                case "MOTORCYCLE":
                    return eSkill.Motorcycle;
                case "HEAVY MACHINERY":
                    return eSkill.Heavy_Machinery;
                case "PILOT - GYRO":
                    return eSkill.Pilot_Gyro;
                case "PILOT - FIXED_WING":
                    return eSkill.Pilot_Fixed_Wing;
                case "PILOT - DIRIGIBLE":
                    return eSkill.Pilot_Dirigible;
                case "PILOT - VTV":
                    return eSkill.Pilot_VTV;
                case "RIFLE":
                    return eSkill.Rifle;
                case "STEATH":
                    return eSkill.Steath;
                case "SUBMACHINE GUN":
                    return eSkill.Submachine_Gun;
                case "AEROTECH":
                    return eSkill.AeroTech;
                case "AVTECH":
                    return eSkill.AVTech;
                case "BASIC TECH":
                    return eSkill.Basic_Tech;
                case "CRYOTANK OPERATION":
                    return eSkill.Cryotank_Operation;
                case "CYBERDECK DESIGN":
                    return eSkill.Cyberdeck_Design;
                case "CYBERTECH":
                    return eSkill.Cybertech;
                case "DEMOLITIONS":
                    return eSkill.Demolitions;
                case "DISGUISE":
                    return eSkill.Disguise;
                case "ELECTRONICS":
                    return eSkill.Electronics;
                case "ELECTRONIC SECURITY":
                    return eSkill.Electronic_Security;
                case "FIRST AID":
                    return eSkill.First_Aid;
                case "FORGERY":
                    return eSkill.Forgery;
                case "GYRO TECH":
                    return eSkill.Gyro_Tech;
                case "PAINT/DRAW":
                    return eSkill.Paint_Draw;
                case "PHOTOGRAPHY/FILM":
                    return eSkill.Photography_Film;
                case "PHARMACEUTICALS":
                    return eSkill.Pharmaceuticals;
                case "PICK LOCK":
                    return eSkill.Pick_Lock;
                case "PICK POCKET":
                    return eSkill.Pick_Pocket;
                case "PLAY INSTRUMENT":
                    return eSkill.Play_Instrument;
                case "WEAPONSMITH":
                    return eSkill.Weaponsmith;
                case "OTHER":
                    return eSkill.Other;
                default:
                    return eSkill.Other;
            }
        }
        public static void SaveXML(string pathAndFilename, Sheet character)
        {
            try
            {
                var settings = new JsonSerializerSettings();
                settings.NullValueHandling = NullValueHandling.Include;
                settings.PreserveReferencesHandling = PreserveReferencesHandling.All;
                string serData = JsonConvert.SerializeObject(character, Formatting.Indented, settings);
                System.IO.File.WriteAllText(pathAndFilename, serData);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private static Sheet LoadXML(string pathAndFilename)
        {
            Sheet res = new Sheet();
            try
            {
                res = JsonConvert.DeserializeObject<Sheet>(System.IO.File.ReadAllText(pathAndFilename));
                return res;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void AddItem(Item i, int q = 1)
        {
            ItemTuple t = Items.Find(x => x.item.Name == i.Name);
            if(t == null)
            {
                ItemTuple tuple = new ItemTuple() { item = i, quantity = q };
                Items.Add(tuple);
            }
            else
            {
                t.quantity += q;
            }
        }        
        public void AddWeapon(Weapon w, int q = 1)
        {
            WeaponTuple t = Weapons.Find(x => x.weapon.Name == w.Name);
            if (t == null)
            {
                WeaponTuple tuple = new WeaponTuple() { weapon = w, quantity = q };
                Weapons.Add(tuple);
            }
            else
            {
                t.quantity += q;
            }
        }
    }
}
