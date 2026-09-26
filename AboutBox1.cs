using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CP48
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = "Cyberpunk 2020 Character Helper";
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = "Copyright © 2026 Lluis Sanahuja";
            this.labelCompanyName.Text = "Using iText-dotnet and Newtonsoft JSON ";
            this.textBoxDescription.Text =
                "Cyberpunk 2020 Character Helper is an open-source program designed to quick-create character for the RPG Cyberpunk 2020.\n \n" +
                "This project is an independent, fan-created work and is not affiliated with, sponsored by, or endorsed by R. Talsorian Games, Inc.\n \n" +
                "Licensing\nThe original source code of this project is released under the Apache License 2.0.\n \n" +                
                "You may use, modify, and redistribute the original code in accordance with the terms of the Apache License 2.0.A copy of the license is available at:\n" +
                "https://www.apache.org/licenses/LICENSE-2.0 \n \n" +
                "Third - Party Content \n" +
                "Some names, terminology, rules, game concepts, and other material represented or referenced by this software originate from works published by R. Talsorian Games, Inc. Such material is the property of its respective copyright and trademark holders and is not covered by this project's Apache License.\n" +
                "R. Talsorian Games, Inc. retains all rights to its original intellectual property. Nothing in this project should be interpreted as transferring, relicensing, or claiming ownership of that material. Any use is not intended for any monetary gain and should be considered 'fair use' \n" +
                "This project is intended as an independent fan-created tool and does not claim ownership of the underlying Cyberpunk® game setting, trademarks, characters, artwork, or other proprietary material belonging to R. Talsorian Games, Inc. All the content is compliant with R. Talsorian Games Homebrew Content Policy\n \n" +
                "Disclaimer \n" +
                "This software is provided 'AS IS', without warranty of any kind.The authors and contributors are not responsible for any damages or consequences arising from the use of this software. \n" +
                "The inclusion of references to third - party intellectual property does not constitute endorsement by the respective rights holders.";
        }

        #region Descriptores de acceso de atributos de ensamblado

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
