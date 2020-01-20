/* Player.cs  
 * Final Project    
 * Bohdana Tyshchenko (8311417)
 * 12/09/2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace BTyshchenkoFinalProject
{
    public class Player : DrawableGameComponent
    {
        SoundEffect jumpSound;
        SoundEffect endSound;
        Game1 parent;
        public Vector2 position;
        public Texture2D texture1;
        public Texture2D texture2;
        public Texture2D texture3;
        public double velocity;
        const double g = 5;
        bool hasJumped = false;
        
        //Enum to determine the state of the hero
        public enum State
        {
            Up,
            Down,
            Dead
        }

        public State currentState = new State();
        
        public Player(Game game, Texture2D texture11,Texture2D texture22,Texture2D texture33, Vector2 position1,SoundEffect jump, SoundEffect end) : base(game)
        {
            jumpSound = jump;
            endSound = end;
            this.parent = (Game1)game;
            position = position1;
            texture1 = texture11;
            texture2 = texture22;
            texture3 = texture33;
        }

        //Creates rectangle for the hero, to detect collision in the future
        public Rectangle RectangleHero
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture1.Width, texture1.Height);
            }
        }

        //Method, which allows the hero to jump
        public override void Update(GameTime gameTime)
        {
            float floor = 450;
            if (currentState != State.Dead)
            {
                //If the Has Jumped=true
                //Velocity changes
                //Position Y changes by velocity
                if (hasJumped)
                {
                    velocity -= g;
                    position.Y -= (float)velocity;
                }

                // If Up is pressed
                // Jump sound is played
                // Velocity sets to 70
                // Has Jumped=true
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && !hasJumped)
                {
                    velocity = 70;
                    hasJumped = true;
                    jumpSound.CreateInstance().Play();
                }

                //If position Y is bigger than floor position
                //Hero goes back down to the floor
                // Has Jumped=false
                if (position.Y > floor)
                {
                    position.Y = floor;
                    hasJumped = false;
                }

                //Determines, what in what state hero is
                if (velocity > 0)
                {
                    currentState = State.Up;
                }
                else
                {
                    currentState = State.Down;
                }
            }
           
        }

        //Draws hero with appropriate texture(according to state)
        public override void Draw(GameTime gameTime)
        {
            
            parent.Sprite.Begin();
            if(currentState==State.Down)
            {
                parent.Sprite.Draw(texture1, position, Color.White);
            }
            if (currentState == State.Up)
            {
                parent.Sprite.Draw(texture2, position, Color.White);
            }
            if (currentState == State.Dead)
            {
                parent.Sprite.Draw(texture3, position, Color.White);
            }


            parent.Sprite.End();
            base.Draw(gameTime);
        }
        
    }
}
