// *
// * Copyright (C) 2008 Roger Keyrox : http://www.RogerKeyrox.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;
using System.Drawing;
using Keyrox.SourceCode;

namespace Keyrox.Windows.Forms.SyntaxBox.Painter
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPainter : IDisposable
    {
        /// <summary>
        ///  Measures the length of a specific row in pixels
        /// </summary>
        Size MeasureRow(Row xtr, int Count);

        /// <summary>
        /// Renders the entire screen
        /// </summary>
        void RenderAll();

        /// <summary>
        /// Gets the text point pixel pos.
        /// </summary>
        /// <param name="tp">The tp.</param>
        /// <returns></returns>
        Point GetTextPointPixelPos(TextPoint tp);

        /// <summary>
        /// Renders the entire screen
        /// </summary>
        /// <param name="g">Target Graphics object</param>
        void RenderAll(Graphics g);

        /// <summary>
        /// Renders the caret only
        /// </summary>
        /// <param name="g"></param>
        void RenderCaret(Graphics g);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RowIndex"></param>
        void RenderRow(int RowIndex);

        /// <summary>
        /// Returns a Point (Column,Row in the active document) from the x and y screen pixel positions.
        /// </summary>
        TextPoint CharFromPixel(int X, int Y);

        /// <summary>
        /// Called by the control to notify the Painter object that the client area has resized.
        /// </summary>
        void Resize();

        /// <summary>
        /// Called by the control to notify the Painter object that one or more Appearance properties has changed.
        /// </summary>
        void InitGraphics();

        /// <summary>
        /// Measures the length of a string in pixels
        /// </summary>
        Size MeasureString(string str);

        int GetMaxCharWidth();
    }
}