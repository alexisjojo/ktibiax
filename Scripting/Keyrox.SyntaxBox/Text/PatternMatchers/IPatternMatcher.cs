// *
// * Copyright (C) 2008 Roger Keyrox : http://www.rogerKeyrox.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

namespace Keyrox.Text.PatternMatchers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPatternMatcher
    {
        string[] DefaultPrefixes { get; }
        int Match(string textToMatch, int matchAtIndex);
    }
}