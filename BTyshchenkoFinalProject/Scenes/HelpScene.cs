﻿/* HelpScene.cs  
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

namespace BTyshchenkoFinalProject
{
    class HelpScene : GameScene
    {
        private Texture2D background;

        public HelpScene(Game game) : base(game)
        {
            this.background = parent.Content.Load<Texture2D>("Images/Help");
        }

        //Drawing Help Scene
        public override void Draw(GameTime gameTime)
        {
            parent.Sprite.Begin();
            parent.Sprite.Draw(background, new Rectangle(0, 0, 1280, 720), Color.White);
            parent.Sprite.End();
            base.Draw(gameTime);
        }

        //Going back to Menu Scene, if Escape is pressed
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.HideScene();
                parent.menuScene.ShowScene();
            }
            base.Update(gameTime);
        }
    }
}
