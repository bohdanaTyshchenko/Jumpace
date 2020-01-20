/* ScrollingBackround.cs  
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
    public class ScrollingBackground : Component
    {
        private bool _constantSpeed;

        private float _layer;

        public Vector2 Velocity;

        private float _scrollingSpeed;

        private List<Sprite> _sprites;
        
        private float _speed;

        public float Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;

                foreach (var sprite in _sprites)
                    sprite.Layer = _layer;
            }
        }

        public ScrollingBackground(Texture2D texture, float scrollingSpeed, bool constantSpeed = false)
          : this(new List<Texture2D>() { texture, texture }, scrollingSpeed, constantSpeed)
        {

        }

        public ScrollingBackground(List<Texture2D> textures, float scrollingSpeed, bool constantSpeed = false)
        {
            //_player = player;

            _sprites = new List<Sprite>();

            for (int i = 0; i < textures.Count; i++)
            {
                var texture = textures[i];

                _sprites.Add(new Sprite(texture)
                {
                    Position = new Vector2(i * texture.Width - Math.Min(i, i + 1), Game1.screenHeigth - texture.Height),
                });
            }

            _scrollingSpeed = scrollingSpeed;

            _constantSpeed = constantSpeed;
        }

        //If user presses Space, velocity of every layer sets to 3f
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Velocity.X = 3f;
            }
            ApplySpeed(gameTime);

            CheckPosition();
        }

        //Speed is applied
        private void ApplySpeed(GameTime gameTime)
        {
            _speed = (float)(_scrollingSpeed * gameTime.ElapsedGameTime.TotalSeconds);

            if (!_constantSpeed || Velocity.X > 0)
                _speed *= Velocity.X;

            foreach (var sprite in _sprites)
            {
                sprite.Position.X -= _speed;
            }
        }

        //Check position of every layer
        //If position is less than 0
        //The layer changes position
        private void CheckPosition()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];

                if (sprite.Rectangle.Right <= 0)
                {
                    var index = i - 1;

                    if (index < 0)
                        index = _sprites.Count - 1;

                    sprite.Position.X = _sprites[index].Rectangle.Right - (_speed * 2);
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var sprite in _sprites)
                sprite.Draw(gameTime, spriteBatch);
        }
    }
}
