/* ActionScene.cs  
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
    public class ActionScene : GameScene
    {
        private List<ScrollingBackground> _scrollingBackgrounds;
        private Player _player;
        private Tree _tree;
        SoundEffect jumpSound;
        SoundEffect endSound;
        Random rnd = new Random();
        private SpriteFont highlight;
        Vector2 heroPosition;
        Vector2 treePosition;
        Texture2D heroTexture;
        Texture2D treeTexture;
        Texture2D deadHeroTexture;
        Texture2D heroTexture2;
        bool collide = false;
        public bool lost = false;
        
        
        //Instantiate Player, Tree, Scrolling Background classes
        //Adds Sound Effects
        //Loads textures for player, tree
        public ActionScene(Game game) : base(game)
        {
            lost = false;
            Score.score = 0;
            heroTexture = parent.Content.Load<Texture2D>("Images/Hero11");
            heroTexture2 = parent.Content.Load<Texture2D>("Images/Hero22");
            treeTexture = parent.Content.Load<Texture2D>("Images/Tree4");
            deadHeroTexture = parent.Content.Load<Texture2D>("Images/HeroDead");

            heroPosition = new Vector2(165, (Game1.screenHeigth - heroTexture.Height) - 20);
            treePosition = new Vector2(1280, 450);

            jumpSound = parent.Content.Load<SoundEffect>("Sound/Up");
            endSound = parent.Content.Load<SoundEffect>("Sound/EndGame");

            this.highlight = parent.Content.Load<SpriteFont>("Fonts/highFont");
            
            _player = new Player(parent, heroTexture, heroTexture2, deadHeroTexture, heroPosition, jumpSound, endSound);
            _tree = new Tree(parent, treeTexture, treePosition, 25, highlight);


            //Creates List of Scrolling Backrounds
            //In this list creates layer structure with textures
            _scrollingBackgrounds = new List<ScrollingBackground>()
            {
                new ScrollingBackground(parent.Content.Load<Texture2D>("Images/Ground111"),80f)
                {
                    Layer=0.99f,
                },
                new ScrollingBackground(parent.Content.Load<Texture2D>("Images/Clouds1"),50f,true)
                {
                    Layer=0.98f,
                },
                new ScrollingBackground(parent.Content.Load<Texture2D>("Images/Clouds2"),30f,true)
                {
                    Layer=0.8f,
                },
                new ScrollingBackground(parent.Content.Load<Texture2D>("Images/Stars1"),20f)
                {
                    Layer=0.79f,
                },
                new ScrollingBackground(parent.Content.Load<Texture2D>("Images/Stars2"),40f)
                {
                    Layer=0.78f,
                },
                new ScrollingBackground(parent.Content.Load<Texture2D>("Images/Sky"),0f)
                {
                    Layer=0.1f,
                },
            };
            
        }
        
        //Updates Action Scene
        //Uses method IsToucking to check player and tree for collision
        //If It collides, scrolling background stops, player stops and trees stop
        //If user presses Escape, it game goes to main scene
        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
            _tree.Update(gameTime);
            collide = IsTouching(_tree, _player);
            if (collide)
            {
                _tree.speed = 0;
                _player.position = heroPosition;
                _player.currentState = Player.State.Dead;
                Score.AddScoreToFile();
                lost = true;
                foreach (ScrollingBackground scr in _scrollingBackgrounds)
                {
                    scr.Velocity.X = 0f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    this.HideScene();
                    parent.menuScene.ShowScene();
                }
                
            }
            else
            {
                foreach (var sb in _scrollingBackgrounds)
                {
                    sb.Update(gameTime);
                }
            }
            
            
            base.Update(gameTime);
        }

        //Draws all components of Action Scene
        public override void Draw(GameTime gameTime)
        {
            parent.Sprite.Begin(SpriteSortMode.FrontToBack);
            
            foreach (var sb in _scrollingBackgrounds)
            {
                sb.Draw(gameTime, parent.Sprite);
            }

            parent.Sprite.End();
            _tree.Draw(gameTime);
            
            _player.Draw(gameTime);
           
            base.Draw(gameTime);
        }

        //Determines if tree and player collides
        private bool IsTouching(Tree tree, Player player)
        {
            bool touch = false;
            if (tree.RectangleTree.Intersects(player.RectangleHero))
            {
                touch = true;
            }
            return touch;
        }
        
    }
}
