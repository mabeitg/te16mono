using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace te16mono
{
    class Player
    {
        public int points;
        public Texture2D texture;
        Vector2 position, velocity;
        float acceleration = (float)0.2;
        public Keys up, down, left, right, attack;
        public List<Shot> shots=new List<Shot>();

        public Rectangle Hitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                return hitbox;
            }
        }

        public void Initialize()
        {
            position = new Vector2();
            velocity = new Vector2();
            //Initiera värden
        }

        public void Update()
        {
            velocity = velocity * (float)0.95;

            //Spellogik
            KeyboardState pressedKeys = Keyboard.GetState();

            if (pressedKeys.IsKeyDown(up))
                velocity.Y -= acceleration;
            if (pressedKeys.IsKeyDown(left))
                velocity.X -= acceleration;
            if (pressedKeys.IsKeyDown(down))
                velocity.Y += acceleration;
            if (pressedKeys.IsKeyDown(right))
                velocity.X += acceleration;
            if(pressedKeys.IsKeyDown(attack))
            {
                Shot shot = new Shot();
                shot.isAlive = true;
                shot.position = position;
                shot.texture = texture;
                shot.velocity = Vector2.Normalize(velocity) * 10;
                shots.Add(shot);
            }
            //Själva: Ordna styrning för a, s, d också

            position += velocity;

            if(position.X<0)
            {
                position.X = 0;
                velocity.X = -velocity.X;
            }

            if (position.Y < 0)
            {
                position.Y = 0;
                velocity.Y = -velocity.Y;
            }

            if (position.X+texture.Width >800)
            {
                position.X = 800-texture.Width;
                velocity.X = -velocity.X;
            }

            if (position.Y + texture.Height> 480)
            {
                position.Y = 480- texture.Height;
                velocity.Y = -velocity.Y;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.Yellow);
            //Rita på skärmen med spriteBatch
        }
    }

}
