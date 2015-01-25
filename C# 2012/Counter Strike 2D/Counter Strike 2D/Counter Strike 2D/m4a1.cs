using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Counter_Strike_2D
{
    class m4a1 : Gun
    {
        int ammo;
        int clip;
        int damage;
        int multiShotCount;
        int delay;
        int reloadTime;
        Texture2D tex;
        Vector2 pos;
        string name;
        public int mammo
        {
            get
            {
                return ammo;
            }
            set
            {
                ammo = value;
            }
        }
        public string mname
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public int mclip
        {
            get
            {
                return clip;
            }
            set
            {
                clip = value;
            }
        }
        public int mdamage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }
        public int mdelay
        {
            get
            {
                return delay;
            }
            set
            {
                delay = value;
            }
        }
        public int mmultiShotCount
        {
            get
            {
                return multiShotCount;
            }
            set
            {
                multiShotCount = value;
            }
        }
        public int mreloadTime
        {
            get
            {
                return reloadTime;
            }
            set
            {
                reloadTime = value;
            }
        }
        public Texture2D mtex
        {
            get
            {
                return tex;
            }
            set
            {
                tex = value;
            }
        }
        public Vector2 mpos
        {
            get
            {
                return pos;
            }
            set
            {
                pos = value;
            }
        }
        public m4a1(Texture2D tex, Vector2 pos)
        {
            ammo = 120;
            clip = 30;
            damage = 25;
            delay = 320;
            multiShotCount = 1;
            reloadTime = 5;
            this.tex = tex;
            this.pos = pos;
            name = "m4a1";
        }
        public m4a1()
        {
            ammo = 120;
            clip = 30;
            damage = 25;
            delay = 320;
            multiShotCount = 1;
            reloadTime = 5;
            name = "m4a1";
        }
    }
}
