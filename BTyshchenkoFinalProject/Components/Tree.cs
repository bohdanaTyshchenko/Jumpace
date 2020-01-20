/* Tree.cs  
 * Final Project    
 * Bohdana Tyshchenko (8311417)
 * 12/09/2019
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BTyshchenkoFinalProject
{
    class Tree : DrawableGameComponent
    {
        Game1 parent;
        Vector2 position;
        public double speed;
        Texture2D texture;
        bool isPressed = false;
        private SpriteFont highlight;
        Vector2 scorePosition = new Vector2(Game1.screenWidth -250, 15);
        Vector2 finalPosition = new Vector2(Game1.screenWidth/2-170, Game1.screenHeigth / 2-40);
       
        public Tree(Game game, Texture2D texture1, Vector2 position1, double speed1, SpriteFont highlight1) : base(game)
        {
            this.parent = (Game1)game;
            texture = texture1;
            position = position1;
            speed = speed1;
            highlight = highlight1;
        }

        //Creates rectangle for the tree, to determine collision of tree and player in future
        public Rectangle RectangleTree
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }

        //Moves the tree to the left, when user presses Space
        //If tree comes over the left boarder, it appears on the right again and adds score
        public override void Update(GameTime gameTime)
        {
            Random rnd = new Random();
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                isPressed = true;
            }
            if (isPressed)
            {
                position.X -= (float)speed;

                if (position.X + texture.Width < 0)
                {
                    Score.score++;
                    position.X = Game1.screenWidth + rnd.Next(30, 200);
                    speed += 0.3;
                }
            }
            
            base.Update(gameTime);
        }

        //Draws the tree and the score
        public override void Draw(GameTime gameTime)
        {
            parent.Sprite.Begin();
            parent.Sprite.Draw(texture, position, Color.White);
            if (speed != 0)
            {
                parent.Sprite.DrawString(highlight, $"Score: {Score.score}", scorePosition, Color.White);
            }
            else if(speed==0)
            {
                
                parent.Sprite.DrawString(highlight, $"Final Score: {Score.score}", finalPosition, Color.White);
            }
            parent.Sprite.End();
            base.Draw(gameTime);
        }
        
    }
}
