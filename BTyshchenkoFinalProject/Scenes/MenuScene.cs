/* MenuScene.cs  
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
    public class MenuScene : GameScene
    {
        private SpriteFont regular;
        private SpriteFont highlight;
        private Color regularColor = Color.Plum;
        private Color highlightColor = Color.White;
        private Texture2D background;

        private List<string> menuItems;
        public int selectedIndex;

        private Vector2 position;
        private KeyboardState oldState;

        public MenuScene(Game game, List<string> menuItems) : base(game)
        {
            this.regular = parent.Content.Load<SpriteFont>("Fonts/regularFont");
            this.highlight = parent.Content.Load<SpriteFont>("Fonts/highFont");
            this.background= parent.Content.Load<Texture2D>("Images/Back");
            this.menuItems = menuItems;
            position = new Vector2(parent.Stage.X-1200, parent.Stage.Y -100);

        }


        //Drawing background image and menu items
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            parent.Sprite.Begin();
            regular.LineSpacing = 325;
            highlight.LineSpacing = 325;

            parent.Sprite.Draw(background, new Rectangle(0, 0, 1280, 720), Color.White);
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    parent.Sprite.DrawString(highlight, menuItems[i], tempPos, highlightColor);
                    tempPos.X += highlight.LineSpacing;
                }
                else
                {
                    parent.Sprite.DrawString(regular, menuItems[i], tempPos, regularColor);
                    tempPos.X += regular.LineSpacing;
                }
            }
            parent.Sprite.End();
            base.Draw(gameTime);
        }

        //This methid is for swithing between the menu items
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (oldState.IsKeyUp(Keys.Right) && ks.IsKeyDown(Keys.Right))
            {
                selectedIndex = MathHelper.Clamp(selectedIndex + 1, 0, menuItems.Count - 1);
            }
            if (oldState.IsKeyUp(Keys.Left) && ks.IsKeyDown(Keys.Left))
            {
                selectedIndex = MathHelper.Clamp(selectedIndex - 1, 0, menuItems.Count - 1);
            }
            if (oldState.IsKeyUp(Keys.Enter) && ks.IsKeyDown(Keys.Enter))
            {
                parent.Notify(this, menuItems[selectedIndex]);
            }
            oldState = ks;
            base.Update(gameTime);
        }
    }
}
