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
    class p90 : Gun
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
        public p90(Texture2D tex, Vector2 pos)
        {
            ammo = 100;
            clip = 50;
            damage = 7;
            delay = 120;
            multiShotCount = 1;
            reloadTime = 3;
            this.tex = tex;
            this.pos = pos;
        }
        public p90()
        {
            ammo = 100;
            clip = 50;
            damage = 7;
            delay = 120;
            multiShotCount = 1;
            reloadTime = 3;
            name = "p90";
        }
    }
}
