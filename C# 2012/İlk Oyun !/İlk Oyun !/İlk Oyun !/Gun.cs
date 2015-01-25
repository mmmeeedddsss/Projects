using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace İlk_Oyun__
{
    class Gun
    {
        Texture2D Texture,bul_tex;
        MouseState ms;
        bool isShooted;
        public LinkedList<Vector2> bullets;
        public Gun(Texture2D tex,Texture2D bullet_texture)
        {
            Texture = tex;
            bul_tex = bullet_texture;
            isShooted = false;
            bullets = new LinkedList<Vector2>();
        }
        
        public void Update(GameTime gametime,float sopax,float sopay,int sopawidht)
        {
            ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed && isShooted != true )
            {
                    bullets.AddLast(new Vector2((int)sopax, (int)sopay));
                    bullets.AddLast(new Vector2((int)sopax + sopawidht, (int)sopay));
                    isShooted = true;
            }
            if (ms.LeftButton == ButtonState.Released)
                isShooted = false;
            LinkedList<Vector2> temp = new LinkedList<Vector2>();

                foreach (Vector2 bullet in bullets)
                {
                    temp.AddLast(new Vector2(bullet.X, bullet.Y - 8));
                }
                bullets = temp;
        }

        public void Draw(SpriteBatch spritebatch,float sopax,float sopay,int sopawidht)
        {
            spritebatch.Begin();

            spritebatch.Draw(Texture, new Vector2(sopax, sopay), Color.White);
            spritebatch.Draw(Texture, new Vector2(sopax + sopawidht, sopay), Color.White);
            if (bullets != null)
            {
                foreach (Vector2 bullet in bullets)
                {
                    spritebatch.Draw(bul_tex, bullet, Color.White);
                }
            }

            spritebatch.End();
        }
    }
}
