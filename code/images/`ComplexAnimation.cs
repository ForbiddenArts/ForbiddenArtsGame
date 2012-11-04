using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ForbiddenArtsGame.code.images {
    public enum SheetDirection { LEFT_TO_RIGHT, TOP_TO_BOTTOM };
    public class SpriteSheet {
        public int col_count;
        public int row_count;
        public int cell_count;
        public int cell_width;
        public int cell_height;
        public int col_spacing;
        public int row_spacing;
        public int left_offset;
        public int top_offset;
        public SheetDirection direction;
        public Texture2D image;
    }
    class ComplexAnimation : Sprite {
        protected int cellCount;
        protected int currentFrame = 0; //range of 0 to frameCount-1
        protected TimeSpan frameTime; // frames per second, default is 10
        protected TimeSpan sinceLastFrame;
        protected List<SpriteSheet> sheets;
        protected int sheetCount;
        protected bool looping;

        public int cycleNum = 0;

        public ComplexAnimation()
            : base() {
            frameTime = new TimeSpan(TimeSpan.TicksPerSecond / 10);
            sinceLastFrame = new TimeSpan(0);
        }
        //Complex ComplexAnimation Constructor to load many spritesheets. May have sheets with different properties, some not completely filled, etc.
        public ComplexAnimation(Texture2D[] spriteSheets, int[] cellCounts, int[] columns, int[] rows, int cell_width, int cell_height, int[] col_spacing, int[] row_spacing, int[] left_offset, int[] top_offset, SheetDirection direction, bool loop) {
            sheetCount = spriteSheets.Length;
            sheets = new List<SpriteSheet>();
            for (int i = 0; i < sheetCount; i++) {
                SpriteSheet s = new SpriteSheet();
                s.cell_count = columns[i] * rows[i];
                cellCount += s.cell_count;
                s.cell_height = cell_height;
                s.cell_width = cell_width;
                s.col_count = columns[i];
                s.col_spacing = col_spacing[i];
                s.direction = direction;
                s.left_offset = left_offset[i];
                s.row_count = rows[i];
                s.row_spacing = row_spacing[i];
                s.image = spriteSheets[i];
                s.top_offset = top_offset[i];
                sheets.Add(s);
            }
            looping = loop;
        }
        // A more simplified version of above, assuming all spritesheets are uniform and completely filled.
        public ComplexAnimation(Texture2D[] spriteSheets, int columns, int rows, int cell_width, int cell_height, int col_spacing, int row_spacing, int left_offset, int top_offset, SheetDirection direction, bool loop) {
            sheetCount = spriteSheets.Length;
            sheets = new List<SpriteSheet>();
            for (int i = 0; i < sheetCount; i++) {
                SpriteSheet s = new SpriteSheet();
                s.cell_count = columns * rows;
                s.cell_height = cell_height;
                s.cell_width = cell_width;
                s.col_count = columns;
                s.col_spacing = col_spacing;
                s.direction = direction;
                s.left_offset = left_offset;
                s.row_count = rows;
                s.row_spacing = row_spacing;
                s.image = spriteSheets[i];
                s.top_offset = top_offset;
                sheets.Add(s);
            }
            cellCount = columns * rows * sheetCount;
            looping = loop;
        }
        public bool isDone() {
            return currentFrame == cellCount -1;
        }
        public void setSpriteCell(int index) {
            if (index > cellCount) {
                throw new IndexOutOfRangeException();
            }
            int i = 0;
            int sheetIndex = 0;
            int subIndex = 0;
            //because sheets may have different amounts of cells on them, we have to count forward till we get to the right sheet.
            while(i < index){
                i += sheets[sheetIndex].cell_count;
                sheetIndex++;
            }
            if (i == cellCount) sheetIndex--;
            subIndex = i - sheets[sheetIndex].cell_count; //return to the beggining of the current sheet and fine tune count
            while (i < index) {
                i++;
                subIndex++;
            }
            this.image = sheets[sheetIndex].image;
            int sx = sheets[sheetIndex].left_offset;
            int sy = sheets[sheetIndex].top_offset;
            if (sheets[sheetIndex].direction == SheetDirection.LEFT_TO_RIGHT) {
                sx += (subIndex % sheets[sheetIndex].col_count) * sheets[sheetIndex].cell_width + (subIndex % sheets[sheetIndex].col_count) * sheets[sheetIndex].col_spacing;
                sy += (subIndex / sheets[sheetIndex].row_count) * sheets[sheetIndex].cell_height + (subIndex / sheets[sheetIndex].row_count) * sheets[sheetIndex].row_spacing;
            }
            else {
                sx += (subIndex / sheets[sheetIndex].col_count) * sheets[sheetIndex].cell_width + (subIndex / sheets[sheetIndex].col_count) * sheets[sheetIndex].col_spacing;
                sy += (subIndex % sheets[sheetIndex].row_count) * sheets[sheetIndex].cell_height + (subIndex % sheets[sheetIndex].row_count) * sheets[sheetIndex].row_spacing;
            }
            srcRect = new Rectangle(sx, sy, sheets[sheetIndex].cell_width, sheets[sheetIndex].cell_height);
        }
        public override void Draw(GameTime gameTime, Vector2 loc, float rotation = 0.0f, int facing = -1) {
            sinceLastFrame += gameTime.ElapsedGameTime;
            if (sinceLastFrame > frameTime) {
                sinceLastFrame -= frameTime;
                currentFrame++;
                if (currentFrame >= cellCount - 1) {
                    if (looping)
                        reset();
                }
                else
                    setSpriteCell(currentFrame);
            }
            base.Draw(gameTime, loc, rotation, facing);
        }

        public virtual void reset() {
            currentFrame = 0;
            cycleNum = 0;
        }
    }
}
