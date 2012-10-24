using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForbiddenArtsGame.code.images
{
    class TempProj
    {
        public TempProj()
        {
            image = SheetHandler.getSheet("Knight");
            srcRect = image.Bounds;
            origin = new Vector2(srcRect.Center.X, srcRect.Center.Y);
        }
    }
}
