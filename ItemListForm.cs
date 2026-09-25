using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CP48
{
    public partial class ItemListForm : Form
    {
        private MainForm main;
        private string cutChar = "|";
        public ItemListForm()
        {
            InitializeComponent();
        }
        private List<TreeNode> mainNodes = new List<TreeNode>();
        private List<TreeNode> childNodes = new List<TreeNode>();
        public void InitializeItemWindow(MainForm mainForm)
        {
            this.main = mainForm;
            this.Text = "Item Database";
            Point loc = this.Location;
            loc.X = main.Location.X + main.Width;
            loc.Y = main.Location.Y;
            this.Location = loc;
            List<string> itemcats = System.Enum.GetNames(typeof(ItemCategory)).ToList();
            for(int x = 0; x < itemcats.Count; x++)
            {
                TreeNode maincat = new TreeNode(itemcats[x]);
                List<TreeNode> subitems = new List<TreeNode>();
                foreach(Item i in EquipmentManager.DB.Items)
                {
                    if(itemcats[x].Equals(i.Type.ToString()))
                    {
                        TreeNode ni = new TreeNode(i.Name + " | " + i.Price + "$");
                        subitems.Add(ni);
                    }
                }
                if(subitems.Count > 0)
                {
                    maincat.Nodes.AddRange(subitems.ToArray());                    
                }
                mainNodes.Add(maincat);
            }
            tvItems.Nodes.AddRange(mainNodes.ToArray());
            tvItems.NodeMouseDoubleClick += TvItems_NodeMouseDoubleClick_Item;

        }
        public void InitializeWeaponWindow(MainForm mainForm)
        {
            this.main = mainForm;
            this.Text = "Weapon Database";
            Point loc = this.Location;
            loc.X = main.Location.X + main.Width + this.Width;
            loc.Y = main.Location.Y;
            this.Location = loc;
            List<string> itemcats = System.Enum.GetNames(typeof(WeaponCategory)).ToList();
            for (int x = 0; x < itemcats.Count; x++)
            {
                TreeNode maincat = new TreeNode(itemcats[x]);
                List<TreeNode> subitems = new List<TreeNode>();
                foreach (Weapon i in EquipmentManager.DB.Weapons)
                {
                    if (itemcats[x].Equals(i.Type.ToString()))
                    {
                        TreeNode ni = new TreeNode(i.Name + " | " + i.Price + "$");                        
                        string tooltip = "WA: " + i.WA + " Co: " + i.Concealability + " Dam: " + i.DamageAndAmmo + " Shots: " + i.Shots + "/" + i.RoF;
                        ni.ToolTipText = tooltip;
                        subitems.Add(ni);                        
                    }
                }
                if (subitems.Count > 0)
                {
                    maincat.Nodes.AddRange(subitems.ToArray());
                }
                mainNodes.Add(maincat);
            }
            tvItems.Nodes.AddRange(mainNodes.ToArray());
            tvItems.NodeMouseDoubleClick += TvItems_NodeMouseDoubleClick_Weapon;
        }

        private void TvItems_NodeMouseDoubleClick_Item(object sender, TreeNodeMouseClickEventArgs e)
        {
            string realname = e.Node.Text.Split(cutChar.ToCharArray())[0];
            realname = realname.Substring(0, realname.Length - 1);
            Item t = EquipmentManager.DB.Items.Find(x => x.Name == realname);
            if(t != null)
            {
                main.AddItem(t);
            }
        }
        private void TvItems_NodeMouseDoubleClick_Weapon(object sender, TreeNodeMouseClickEventArgs e)
        {
            string realname = e.Node.Text.Split(cutChar.ToCharArray())[0];
            realname = realname.Substring(0, realname.Length - 1);
            Weapon t = EquipmentManager.DB.Weapons.Find(x => x.Name == realname);
            if (t != null)
            {
                main.AddWeapon(t);
            }
        }

    }
}
