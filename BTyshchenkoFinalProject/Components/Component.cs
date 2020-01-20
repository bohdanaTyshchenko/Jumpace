/* Component.cs  
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
    //An abstarct class Component is used create abstarct methods Draw and Update
    public abstract class Component
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}
