using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{
    class Shot
    {
        public Texture2D texture;
        public Vector2 position, velocity;
        public bool isAlive;
        public Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                return hitbox;
            }
        }

        public void Update()
        {
            position += velocity;

            if (position.X+texture.Width < 0)
            {
                isAlive = false;
            }

            if (position.Y+texture.Height < 0)
            {
                isAlive = false; 
            }

            if (position.X > 800)
            {
                isAlive = false;
            }

            if (position.Y > 480)
            {
                isAlive = false;
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.Wheat);
        }
    }
}
