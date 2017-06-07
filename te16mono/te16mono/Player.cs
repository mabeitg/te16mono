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
        public Texture2D texture;
        Vector2 position, velocity;
        float acceleration = (float)0.2;
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

            if (pressedKeys.IsKeyDown(Keys.W))
                velocity.Y -= acceleration;
            if (pressedKeys.IsKeyDown(Keys.A))
                velocity.X -= acceleration;
            if (pressedKeys.IsKeyDown(Keys.S))
                velocity.Y += acceleration;
            if (pressedKeys.IsKeyDown(Keys.D))
                velocity.X += acceleration;
            //Själva: Ordna styrning för a, s, d också

            position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.Yellow);
            //Rita på skärmen med spriteBatch
        }
    }

}
