﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessEngine
{
    class Rook : Piece
    {
        public Rook(bool piececolor, string pieceid)
        {
            Color = piececolor;
            Id = pieceid;
            PieceImage = getImageName("rook");
            Value = 5;
        }
    }
}
