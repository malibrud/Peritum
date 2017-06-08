﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChessEngine
{
    class HvsHGame : IGame
    {
        private Piece[] piece = new Piece[32];
        private bool turn;

        public HvsHGame()
        {
            SetupBoard();
        }

        public Piece[] getPieces()
        {
            return piece;
        }

        public Piece[] Pieces
        {
            get { return piece; }
            set { piece = value; }
        }

        public void SetupBoard()
        {
            piece[0] = new Rook(false, "rbk");
            piece[1] = new Knight(false, "nbk");
            piece[2] = new Bishop(false, "bbk");
            piece[3] = new Queen(false, "qb");
            piece[4] = new King(false, "kb");
            piece[5] = new Bishop(false, "bbq");
            piece[6] = new Knight(false, "nbq");
            piece[7] = new Rook(false, "rbq");

            piece[8] = new Pawn(false, "pba");
            piece[9] = new Pawn(false, "pbb");
            piece[10] = new Pawn(false, "pbc");
            piece[11] = new Pawn(false, "pbd");
            piece[12] = new Pawn(false, "pbe");
            piece[13] = new Pawn(false, "pbf");
            piece[14] = new Pawn(false, "pbg");
            piece[15] = new Pawn(false, "pbh");

            piece[16] = new Rook(true, "rwk");
            piece[17] = new Knight(true, "nwk");
            piece[18] = new Bishop(true, "bwk");
            piece[19] = new Queen(true, "qw");
            piece[20] = new King(true, "kw");
            piece[21] = new Bishop(true, "bbw");
            piece[22] = new Knight(true, "nbw");
            piece[23] = new Rook(true, "rbw");

            piece[24] = new Pawn(true, "pba");
            piece[25] = new Pawn(true, "pbb");
            piece[26] = new Pawn(true, "pbc");
            piece[27] = new Pawn(true, "pbd");
            piece[28] = new Pawn(true, "pbe");
            piece[29] = new Pawn(true, "pbf");
            piece[30] = new Pawn(true, "pbg");
            piece[31] = new Pawn(true, "pbh");

            // set the first turn to white
            turn = true;

            // Adds the black home row to the board
            for (int i = 0; i < 8; i++)
            {
                piece[i].Location = new Point(i, 0);
                Point p = piece[i].locationOnBoard();
            }

            // Adds the black pawns to the board
            for (int i = 8; i < 16; i++)
            {
                piece[i].Location = new Point(i - 8, 1);
                Point p = piece[i].locationOnBoard();
            }

            // Adds the white home row to the board
            for (int i = 16; i < 24; i++)
            {
                piece[i].Location = new Point(i - 16, 7);
                Point p = piece[i].locationOnBoard();
            }

            // Adds the White pawns to the board
            for (int i = 24; i < 32; i++)
            {
                piece[i].Location = new Point(i - 24, 6);
                Point p = piece[i].locationOnBoard();
            }
        }

        public void CheckMove(Point position, int index)
        {
            if (index != -1)
            {
                if (piece[index].Color == turn)
                {
                    Point newLoc = position;
                    newLoc = Piece.boardToPiece(newLoc);
                    bool valid = piece[index].movePiece(newLoc);
                    bool nBlocked = Rules.checkPiecePath(piece[index], newLoc, piece);

                    if (valid && nBlocked)
                    {
                        if (Rules.CheckCapture(newLoc, piece, out int capIndex))
                        {
                            piece[capIndex].Captured = true;
                        }
                        piece[index].Location = newLoc;
                        piece[index].Moved = true;
                        flipBoard();
                        turn = !turn;
                    }
                    else if (!valid && nBlocked && piece[index].GetType().Equals(typeof(King)))
                    {
                        Piece rook = new Rook();
                        bool check = Rules.checkCastle(piece[index], newLoc, piece, out int location);

                        if (check)
                        {
                            Point rLoc = piece[location].Location;
                            if (rLoc.X - piece[index].Location.X < 0)
                            {
                                rLoc.X = newLoc.X + 1;
                            }
                            else if (rLoc.X - piece[index].Location.X > 0)
                            {
                                rLoc.X = newLoc.X - 1;
                            }

                            piece[location].Location = rLoc;
                            piece[index].Location = newLoc;
                            piece[location].Moved = true;
                            piece[index].Moved = true;
                            turn = !turn;
                        }
                    }
                    else if (!valid && piece[index].GetType().Equals(typeof(Pawn)))
                    {
                        bool pawnCap = Rules.CheckPawnCapture(newLoc, piece, piece[index], out int capIndex);

                        if (pawnCap)
                        {
                            piece[capIndex].Captured = true;
                        }
                    }

                    index = -1;
                }
            }
        }

        private void flipBoard()
        {
            for(int i = 0; i < 32; i++)
            {
                double x = Math.Abs(piece[i].Location.X - 7);
                double y = Math.Abs(piece[i].Location.Y - 7);
                piece[i].Location = new Point(x, y);
            }
        }
    }
}