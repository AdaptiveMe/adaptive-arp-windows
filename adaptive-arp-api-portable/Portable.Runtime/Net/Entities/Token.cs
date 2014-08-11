// -----------------------------------------------------------------------
// Copyright (c) David Kean. All rights reserved.
// -----------------------------------------------------------------------
using System;

namespace System.Net.Entities
{
    internal struct Token
    {
        public int StartIndex;
        public int EndIndex;
        public string Text;
        public TokenType Type;
    }
}
