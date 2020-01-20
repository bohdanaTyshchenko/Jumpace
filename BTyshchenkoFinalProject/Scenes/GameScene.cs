/* GameScene.cs  
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
    public class GameScene : DrawableGameComponent
    {
        protected Game1 parent;
        public List<GameComponent> Components { get; set; }

        public GameScene(Game game) : base(game)
        {
            parent = (Game1)game;
            Components = new List<GameComponent>();
            HideScene();
        }

        //Method which sets state for the scene
        public virtual void SetState(bool state)
        {
            this.Enabled = state;
            this.Visible = state;
            foreach (GameComponent item in Components)
            {
                item.Enabled = state;
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    comp.Visible = state;
                }
            }
        }

        //Shows the scene
        public virtual void ShowScene()
        {
            SetState(true);

        }

        //Hides the scene
        public virtual void HideScene()
        {
            SetState(false);
        }

        //Updates the scene
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        //Draws the scene
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent comp = null;
            foreach (GameComponent item in Components)
            {
                if (item is DrawableGameComponent)
                {
                    comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }
    }
}
