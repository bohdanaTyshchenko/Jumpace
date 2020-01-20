/* Game1.cs  
 * Final Project    
 * Bohdana Tyshchenko (8311417)
 * 12/09/2019
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace BTyshchenkoFinalProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        public GraphicsDeviceManager Graphics { get => graphics; }
       
        private SpriteBatch spriteBatch;
        public SpriteBatch Sprite { get => spriteBatch;}
        
        private Vector2 stage;
        public Vector2 Stage { get => stage;}

        public static int screenWidth = 1280;
        public static int screenHeigth = 720;

        private GameScene currentScene;
        
        public MenuScene menuScene;

        private ActionScene actionScene;

        private AboutScene aboutScene;

        private ActionScene actionScene1;
        
        private HelpScene helpScene;

        Song song;
        
        //List of menu items
        private List<string> menuItems = new List<string>
        {
            "Game",
            "Help",
            "About",
            "Quit"
        };
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = screenWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = screenHeigth;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            menuScene = new MenuScene(this, menuItems);
            this.Components.Add(menuScene);
            currentScene = menuScene;
            currentScene.ShowScene();

            actionScene = new ActionScene(this);
            this.Components.Add(actionScene);
            
            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);
            
            //Playing background song
            this.song = Content.Load<Song>("Music/Song");
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.2f;
            MediaPlayer.IsRepeating = true;
           
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            

            base.Update(gameTime);
        }
    
        //Method for swithing between scenes
        //Checking if we need to restart a game
        public void Notify(GameScene sender, string action)
        {
            currentScene.HideScene();
            if (sender is MenuScene)
            {
                switch (action)
                {
                    case "Game":
                        
                        if (actionScene.lost == true)
                        {
                            actionScene1 = new ActionScene(this);
                            this.Components.Add(actionScene1);
                            currentScene = actionScene1;
                        }
                        else
                        {
                            currentScene = actionScene;
                        }
                        break;
                    case "About":
                        currentScene = aboutScene;
                        break;
                    case "Help":
                        currentScene = helpScene;
                        break;
                    case "Quit":
                        Exit();
                        break;
                }
            }
    
            currentScene.ShowScene();
        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
