﻿using System.Windows;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Controls;
using System;
using System.Windows.Media;

namespace ChessEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        Image[] pieces = new Image[32];
        Piece[] piece = new Piece[32];

        Point pos;
        private Control boardControl;
        private Point previousLocation;

        int gridCol = 0;
        int gridRow = 0;
        bool captured = false;

        public void SetupBoard()
        {
            piece[0] = new Rook(false, "rbk");
            piece[1] = new Knight(false, "nbk");
            piece[2] = new Bishop(false, "bbk");
            piece[3] = new King(false, "kb");
            piece[4] = new Queen(false, "qb");
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
            piece[19] = new King(true, "kw");
            piece[20] = new Queen(true, "qw");
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

            Canvas.SetLeft(piece[0].PieceImage, 5);
            Canvas.SetTop(piece[0].PieceImage, 5);

            ChessBoard.Children.Add(piece[0].PieceImage);
            // ChessBoard.Children.
            /*
            for(int i = 0; i < 32; i++)
            {
                ChessBoard.Children.Add(piece[i].PieceImage);
            }
            
            for(int i = 0; i < 8; i++)
            {
                Grid.SetColumn(piece[i].PieceImage, i);
            }
            for (int i = 0; i < 8; i++)
            {
                Grid.SetRow(piece[i].PieceImage, 0);
            }

            for (int i = 8; i < 16; i++) Grid.SetColumn(piece[i].PieceImage, (i - 8));
            for (int i = 8; i < 16; i++) Grid.SetRow(piece[i].PieceImage, 1);
            for (int i = 24; i < 32; i++) Grid.SetColumn(piece[i].PieceImage, (i - 24));
            for (int i = 24; i < 32; i++) Grid.SetRow(piece[i].PieceImage, 6);
            for (int i = 16; i < 24; i++) Grid.SetColumn(piece[i].PieceImage, (i - 16));
            for (int i = 16; i < 24; i++) Grid.SetRow(piece[i].PieceImage, 7);
            */
        }

        public MainWindow()
        {
            InitializeComponent();
            SetupBoard();

            ChessBoard.AllowDrop = true;
            piece[0].PieceImage.AllowDrop = true;

            piece[0].PieceImage.DragEnter += piece_DragEnter;
            piece[0].PieceImage.MouseLeftButtonDown += new MouseButtonEventHandler(piece_MouseDown);
            piece[0].PieceImage.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(piece_MouseLeave);
            piece[0].PieceImage.MouseEnter += new MouseEventHandler(piece_MouseEnter);
            piece[0].PieceImage.MouseLeave += new MouseEventHandler(piece_MouseR);
        }

        void piece_MouseEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Hand;
        }

        void piece_MouseR(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        void piece_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(ChessBoard);
            Console.WriteLine("Down");
            Console.WriteLine(p.X.ToString());
            Console.WriteLine(p.X.ToString());

            DataObject data = new DataObject(typeof(ImageSource), piece[0].PieceImage.Source);

            int top = Canvas.GetZIndex(piece[0].PieceImage);
            foreach (Image child in ChessBoard.Children)
            {
                if (top < Canvas.GetZIndex(child)) top = Canvas.GetZIndex(child);
            }

            Canvas.SetZIndex(piece[0].PieceImage, top + 1);
            captured = true;
            DragDrop.DoDragDrop(piece[0].PieceImage, data, DragDropEffects.Move);
        }

        void piece_DragEnter(object sender, DragEventArgs e)
        {
            //Console.WriteLine(e.Data.GetData.pos.ToString());
            if (captured)
            {
                Console.WriteLine("Enter");
                Point p = e.GetPosition(ChessBoard);
                Console.Write(p.X.ToString(), ",", p.Y.ToString());
                Canvas.SetTop(piece[0].PieceImage, p.Y);
                Canvas.SetLeft(piece[0].PieceImage, p.X);
            }
            
            //ChessBoard.Children.Add(piece[0].PieceImage);
        }

        void piece_MouseLeave(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Leave");
            Point p = e.GetPosition(ChessBoard);
            Console.Write(p.X.ToString(), ",", p.Y.ToString());

            captured = false;
        }


    }
}