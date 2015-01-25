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
    interface Gun
    {
        int mdamage { get; set; }
        int mdelay { get; set; }
        int mmultiShotCount { get; set; }
        int mclip { get; set; }
        int mammo{ get; set; }
        int mreloadTime { get; set; }
        Texture2D mtex { get; set; }
        Vector2 mpos { get; set; }
        string mname { get; set; }
    }
}
