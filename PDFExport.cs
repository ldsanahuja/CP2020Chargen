using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iText;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;

namespace CP48
{
    public enum PDF_EXPORT_RESULT
    {
        OK,
        FAILURE
    }
    public static class PDFExport
    {
        public const string BaseFile = @"Data\CSheet.pdf";

        public static void ExportToPDF(Sheet characterSheet, string destination)
        {
            if (!File.Exists(BaseFile))
            {
                throw new Exception("Missing PDF base file!");
            }
            FileInfo file = new FileInfo(destination);
            file.Directory.Create();

            StartExport(characterSheet, destination);
        }

        private static void StartExportES(Sheet character, string destination)
        {
            PdfDocument pdf = new PdfDocument(new PdfReader(BaseFile), new PdfWriter(destination));
            PdfAcroForm form = PdfFormCreator.GetAcroForm(pdf, true);

            string cname = character.Name + " | ";
            switch (character.Gender)
            {
                case eGender.Female:
                    cname += "F";
                    break;
                case eGender.Male:
                    cname += "M";
                    break;
                case eGender.Other:
                    cname += "-";
                    break;
            }
            cname += " | " + character.Age.ToString();
            form.GetField("Text2").SetValue(cname);
            form.GetField("int").SetValue(character.Stats.Int.Value.ToString());
            form.GetField("ref").SetValue(character.Stats.Ref.Value.ToString());
            form.GetField("ref2").SetValue(character.Stats.Ref.Remaining.ToString());
            form.GetField("tec").SetValue(character.Stats.Tech.Value.ToString());
            form.GetField("fri").SetValue(character.Stats.Cool.Value.ToString());
            form.GetField("atr").SetValue(character.Stats.Attr.Value.ToString());
            form.GetField("sue").SetValue(character.Stats.Luck.Value.ToString());
            form.GetField("mov").SetValue(character.Stats.MA.Value.ToString());
            form.GetField("tco").SetValue(character.Stats.Body.Value.ToString());
            form.GetField("emp1").SetValue(character.Stats.Emp.Remaining.ToString());
            form.GetField("emp2").SetValue(character.Stats.Emp.Value.ToString());
            form.GetField("carr").SetValue(character.Stats.Run.ToString());
            form.GetField("salto").SetValue(character.Stats.Leap.ToString());
            form.GetField("leva").SetValue(character.Stats.Lift.ToString());

            form.GetField("Text9").SetValue(character.Stats.SaveValue.ToString());
            form.GetField("MTC").SetValue(character.Stats.BTCValue.ToString());

            switch (character.Role)
            {
                case eRole.Solo:
                    form.GetField("MER").SetValue("X");
                    break;
                case eRole.Rocker:
                    form.GetField("ROCKER").SetValue("X");
                    break;
                case eRole.Netrunner:
                    form.GetField("NETR").SetValue("X");
                    break;
                case eRole.Media:
                    form.GetField("PERIOD").SetValue("X");
                    break;
                case eRole.Nomad:
                    form.GetField("NOMAD").SetValue("X");
                    break;
                case eRole.Fixer:
                    form.GetField("ARR").SetValue("X");
                    break;
                case eRole.Cop:
                    form.GetField("POL").SetValue("X");
                    break;
                case eRole.Corpo:
                    form.GetField("EJEC").SetValue("X");
                    break;
                case eRole.Techie:
                    form.GetField("TECNI").SetValue("X");
                    break;
                case eRole.Medtech:
                    form.GetField("TECNOMED").SetValue("X");
                    break;
            }
            form.FlattenFields();
            pdf.Close();

            System.Diagnostics.ProcessStartInfo fileopener = new System.Diagnostics.ProcessStartInfo
            {
                FileName = destination,
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(fileopener);
        }
        private static void StartExport(Sheet character, string destination)
        {
            PdfDocument pdf = new PdfDocument(new PdfReader(BaseFile), new PdfWriter(destination));
            PdfAcroForm form = PdfFormCreator.GetAcroForm(pdf, true);
            try
            {
                string cname = character.Name + " | ";
                switch (character.Gender)
                {
                    case eGender.Female:
                        cname += "F";
                        break;
                    case eGender.Male:
                        cname += "M";
                        break;
                    case eGender.Other:
                        cname += "-";
                        break;
                }
                cname += " | " + character.Age.ToString();
                form.GetField("Name").SetValue(cname);
                form.GetField("INT").SetValue(character.Stats.Int.Value.ToString());
                form.GetField("REF1").SetValue(character.Stats.Ref.Value.ToString());
                form.GetField("REF2").SetValue(character.Stats.Ref.Remaining.ToString());
                form.GetField("TECH").SetValue(character.Stats.Tech.Value.ToString());
                form.GetField("COOL").SetValue(character.Stats.Cool.Value.ToString());
                form.GetField("ATTR").SetValue(character.Stats.Attr.Value.ToString());
                form.GetField("LUCK").SetValue(character.Stats.Luck.Value.ToString());
                form.GetField("MA").SetValue(character.Stats.MA.Value.ToString());
                form.GetField("BODY").SetValue(character.Stats.Body.Value.ToString());
                form.GetField("EMP1").SetValue(character.Stats.Emp.Remaining.ToString());
                form.GetField("EMP2").SetValue(character.Stats.Emp.Value.ToString());
                form.GetField("RUN").SetValue(character.Stats.Run.ToString());
                form.GetField("LEAP").SetValue(character.Stats.Leap.ToString());
                form.GetField("LIFT").SetValue(character.Stats.Lift.ToString());

                form.GetField("SAVE").SetValue(character.Stats.SaveValue.ToString());
                form.GetField("BTM").SetValue(character.Stats.BTCValue.ToString());
                foreach (Skill s in character.Skills)
                {
                    string labelName = "SK" + ((int)s.ID).ToString();
                    form.GetField(labelName).SetValue(s.Value.ToString());
                    if (Sheet.AdditionalTextSkills.Contains((int)s.ID))
                    {
                        string adi = string.IsNullOrEmpty(s.AdditionalData) ? "Unspecified" : s.AdditionalData;
                        form.GetField(labelName + "A").SetValue(adi);
                    }
                }
                List<Armor> boughtArmor = new List<Armor>();
                form.GetField("ItemName1").SetValue("Initial Funds, " + character.InitialFunds.ToString() + "$");
                for (int x = 0; x < character.Items.Count; x++)
                {
                    string res = "";
                    if (character.Items[x].quantity > 1)
                        res += character.Items[x].quantity.ToString() + "x ";
                    res += character.Items[x].item.Name;
                    string labelfield = "ItemName" + (x + 2).ToString(); //padding for 0 and first line being funds
                    string costfield = "ItemCost" + (x + 2).ToString();
                    form.GetField(labelfield).SetValue(res);
                    form.GetField(costfield).SetValue(character.Items[x].item.Price.ToString());
                    if(character.Items[x].item is Armor armor)
                    {
                        boughtArmor.Add(armor); 
                    }
                }
                for(int x = 0; x < character.Weapons.Count; x++)
                {
                    //add to items
                    string res = "";
                    if (character.Weapons[x].quantity > 1)
                        res += character.Weapons[x].quantity.ToString() + "x ";
                    res += character.Weapons[x].weapon.Name;
                    string labelfield = "ItemName" + (x + 2 + character.Items.Count()).ToString(); //padding for 0 and first line being funds, and all items
                    string costfield = "ItemCost" + (x + 2 + character.Items.Count()).ToString();
                    form.GetField(labelfield).SetValue(res);
                    form.GetField(costfield).SetValue(character.Weapons[x].weapon.Price.ToString());
                    //add to weapons
                    string slot = (x + 1).ToString();
                    form.GetField("WeaponName" + slot).SetValue(character.Weapons[x].weapon.Name);
                    form.GetField("WeaponType" + slot).SetValue(character.Weapons[x].weapon.CategoryString);
                    form.GetField("WeaponWA" + slot).SetValue(character.Weapons[x].weapon.WA);
                    form.GetField("WeaponConc" + slot).SetValue(character.Weapons[x].weapon.Concealability);
                    form.GetField("WeaponAvail" + slot).SetValue(character.Weapons[x].weapon.Availability);
                    form.GetField("WeaponDmg" + slot).SetValue(character.Weapons[x].weapon.Damage);
                    form.GetField("WeaponShots" + slot).SetValue(character.Weapons[x].weapon.Shots);
                    form.GetField("WeaponROF" + slot).SetValue(character.Weapons[x].weapon.RoF);
                    form.GetField("WeaponRel" + slot).SetValue(character.Weapons[x].weapon.Reliability);
                }
                int headprot = 0;
                int armprot = 0;                
                int legprot = 0;                
                int torsoprot = 0;
                int refmod = 0;
                if (boughtArmor.Count > 0)
                {
                    boughtArmor = boughtArmor.OrderBy(a => a.IsHard ? 1 : 0).ToList();
                    for (int x = 0; x < boughtArmor.Count; x++)
                    {
                        refmod += boughtArmor[x].EV;
                        if (x == 0)
                        {
                            headprot += boughtArmor[x].Head;
                            armprot += boughtArmor[x].Arms;
                            legprot += boughtArmor[x].Legs;
                            torsoprot += boughtArmor[x].Torso;
                        }
                        if( x > 0)
                        {
                            if (boughtArmor[x].Head > 0)
                            {
                                if (headprot != 0)
                                {
                                    if (headprot < boughtArmor[x].Head)
                                    {
                                        headprot = boughtArmor[x].Head + GetAPDiff(headprot, boughtArmor[x].Head);
                                    }
                                    else
                                    {
                                        headprot += GetAPDiff(headprot, boughtArmor[x].Head);
                                    }
                                    
                                }
                                else
                                    headprot += boughtArmor[x].Head;
                            }
                            if (boughtArmor[x].Arms > 0)
                            {
                                if (armprot != 0)
                                {
                                    if (armprot < boughtArmor[x].Arms)
                                    {
                                        armprot = boughtArmor[x].Arms + GetAPDiff(armprot, boughtArmor[x].Arms);
                                    }
                                    else
                                    {
                                        armprot += GetAPDiff(armprot, boughtArmor[x].Arms);
                                    }

                                }
                                else
                                    armprot += boughtArmor[x].Arms;
                            }
                            if (boughtArmor[x].Legs > 0)
                            {
                                if (legprot != 0)
                                {
                                    if (legprot < boughtArmor[x].Legs)
                                    {
                                        legprot = boughtArmor[x].Legs + GetAPDiff(legprot, boughtArmor[x].Legs);
                                    }
                                    else
                                    {
                                        legprot += GetAPDiff(legprot, boughtArmor[x].Legs);
                                    }

                                }
                                else
                                    legprot += boughtArmor[x].Legs;
                            }
                            if (boughtArmor[x].Torso > 0)
                            {
                                if (torsoprot != 0)
                                {
                                    if (torsoprot < boughtArmor[x].Torso)
                                    {
                                        torsoprot = boughtArmor[x].Torso + GetAPDiff(torsoprot, boughtArmor[x].Torso);
                                    }
                                    else
                                    {
                                        torsoprot += GetAPDiff(torsoprot, boughtArmor[x].Torso);
                                    }

                                }
                                else
                                    torsoprot += boughtArmor[x].Torso;
                            }
                        }
                    }
                }
                form.GetField("REF1").SetValue((character.Stats.Ref.Value - refmod).ToString());
                if(torsoprot > 0)
                    form.GetField("Armor_Torso").SetValue(torsoprot.ToString());
                if (headprot > 0)
                    form.GetField("Armor_Head").SetValue(headprot.ToString());

                if (armprot > 0)
                {
                    form.GetField("Armor_RArm").SetValue(armprot.ToString());
                    form.GetField("Armor_LArm").SetValue(armprot.ToString());
                }
                if(legprot > 0)
                {
                    form.GetField("Armor_RLeg").SetValue(legprot.ToString());
                    form.GetField("Armor_LLeg").SetValue(legprot.ToString());
                }

                /*
                switch (character.Role)
                {
                    case eRole.Solo:
                        form.GetField("MER").SetValue("X");
                        break;
                    case eRole.Rocker:
                        form.GetField("ROCKER").SetValue("X");
                        break;
                    case eRole.Netrunner:
                        form.GetField("NETR").SetValue("X");
                        break;
                    case eRole.Media:
                        form.GetField("PERIOD").SetValue("X");
                        break;
                    case eRole.Nomad:
                        form.GetField("NOMAD").SetValue("X");
                        break;
                    case eRole.Fixer:
                        form.GetField("ARR").SetValue("X");
                        break;
                    case eRole.Cop:
                        form.GetField("POL").SetValue("X");
                        break;
                    case eRole.Corpo:
                        form.GetField("EJEC").SetValue("X");
                        break;
                    case eRole.Techie:
                        form.GetField("TECNI").SetValue("X");
                        break;
                    case eRole.Medtech:
                        form.GetField("TECNOMED").SetValue("X");
                        break;
                }
                */
                // form.FlattenFields();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            pdf.Close();

            System.Diagnostics.ProcessStartInfo fileopener = new System.Diagnostics.ProcessStartInfo
            {
                FileName = destination,
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(fileopener);
        }

        private static int GetAPDiff(int a, int b)
        {
            int diff = Math.Abs(a - b);
            if (diff <= 4)
                return 5;
            if (diff <= 8)
                return 4;
            if (diff <= 14)
                return 3;
            if (diff <= 20)
                return 2;
            if (diff <= 26)
                return 1;
            return 0;
        }
    }
}
