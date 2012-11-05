//has a ` in the filename to make sure it is at the top of the folder

using Microsoft.Xna.Framework;

namespace ForbiddenArtsGame.code.states
{
	enum StateReturn
	{
		True,False,ExitParent
	}

    abstract class GameState
    {
        protected GameState child;

        //is true if parent states are allowed to be drawn, eg the main game as a background for ingame menus
        //should be set in every constructor, even if it is the same as the inherited class's, for easier code reading
        protected bool drawParent = false;

        public GameState()
        {
            child = null;
        }
        /* Draw steps:
         * 1: Check child.DrawParent, if true proceed to step 2 otherwise go to step 3
         * 2: Draw whatever it needs to draw
         * 3: If child != null, child.Draw
         */
        
        public abstract void Draw(GameTime gameTime);

        //whether parent states should be drawn
        //called by Draw
        public bool DrawParent()
        {
            if (drawParent)
            {
                if (child != null)
                {
                    return child.DrawParent();
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        //return true if the state should be exited from, eg selecting "return to game" option in ingame menu
        //must call child.Update, if true do nothing if false set child = null and update normally
        public abstract StateReturn Update(GameTime gameTime);

    }
}
