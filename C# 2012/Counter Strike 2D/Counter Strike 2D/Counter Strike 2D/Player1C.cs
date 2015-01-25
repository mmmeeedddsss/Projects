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
    class Player1C
    {
        const int speed = 14;
        public Vector2 playerPos;
        Texture2D man;
        int wh;
        int hg;
        int manwh;
        int manhg;
        Color playerColor;
        public Gun gun;
        double lastShoot;
        public bool reload;
        double reloadStart;
        public int clip;
        public int ammo;
        public int health;
        public bool ooammo;
        public int money;

        public Player1C(Vector2 pos, Color color, Texture2D tex, Vector2 screen, int manx, int many)
        {
            playerPos = pos;
            playerColor = Color.White;
            man = tex;
            wh = (int)screen.X;
            hg = (int)screen.Y;
            manwh = manx;
            manhg = many;
            gun = new p90();
            lastShoot = -10000;
            reloadStart = -10000;
            ammo = gun.mammo;
            clip = gun.mclip;
            reload = false;
            health = 100;
            ooammo = false;
            money = 8800;
        }

        public void hitted(int dmg)
        {
            this.health -= dmg;
            if (this.health < 0)
                die();
        }

        public void die()
        {
        }

        public Vector2 Update(GameTime gt)
        {

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.S) && playerPos.Y + manhg*2 < (-((playerPos.Y))) + hg*4)
                playerPos.Y += speed;
            else if (ks.IsKeyDown(Keys.W) && playerPos.Y > (-((playerPos.Y)))-hg * 2 )
                playerPos.Y -= speed;
            if (ks.IsKeyDown(Keys.A) && playerPos.X > (-((playerPos.X))) - wh * 2 )
                playerPos.X -= speed;
            else if (ks.IsKeyDown(Keys.D) && playerPos.X + manwh*2 < (-((playerPos.X))) + wh * 4)
                playerPos.X += speed;
            if (ks.IsKeyDown(Keys.R))
                reload = true;
            if (ooammo == true && (ammo > 0 || clip > 0))
            {
                reload = true;
                ooammo = false;
            }
            if (reload == true && (gt.TotalGameTime.TotalMilliseconds - reloadStart) > gun.mreloadTime * 1000)
                reload = false;

            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed && (gt.TotalGameTime.TotalMilliseconds - lastShoot) > gun.mdelay && gun.mclip > 0 && reload == false && ooammo == false )
            {
                //System.Windows.Forms.MessageBox.Show("" + (playerPos.X + wh) + "," + (playerPos.Y + hg));
                clip--;
                if (clip == 0)
                {
                    if (ammo > 0)
                    {
                        reloadStart = gt.TotalGameTime.TotalMilliseconds;
                        clip = gun.mclip;
                        ammo -= clip;
                        reload = true;
                    }
                    else
                    {
                        ooammo = true;
                        reload = false;
                    }
                }
                lastShoot = gt.TotalGameTime.TotalMilliseconds;
                return new Vector2(ms.X - ((int)(-wh - ((playerPos.X) - (wh / 2)))), ms.Y - ((int)(-hg - ((playerPos.Y) - (hg / 2)))));
            }
            return Vector2.Zero;
        }
    }
}
