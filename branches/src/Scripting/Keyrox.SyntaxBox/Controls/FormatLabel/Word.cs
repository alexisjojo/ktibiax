// *
// * Copyright (C) 2008 Roger Keyrox : http://www.RogerKeyrox.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System.Drawing;

namespace Keyrox.Windows.Forms.FormatLabel
{
    public class Word
    {
        public Element Element;
        public int Height;
        public Image Image;
        public Rectangle ScreenArea = new Rectangle(0, 0, 0, 0);
        public string Text = "";
        public int Width;
        //	public bool Link=false;
    }
}