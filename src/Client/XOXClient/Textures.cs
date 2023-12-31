﻿using PixelBuilder.TextureUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOXClient.Properties;

namespace XOXClient
{
    public static class Textures
    {
        public static Bitmap XOXLogo;

        public static class Texts
        {
            public static Bitmap EnterRoomCode;
            public static Bitmap ShareRoomCode;
            public static Bitmap NextPlayer;
        }
        public static class Colors
        {
            public static readonly Color backgroundColor = Color.FromArgb(0, 188, 212);
        }

        public static class Buttons
        {
            public static Bitmap btn_Create;
            public static Bitmap btn_Create__Hover;
            public static Bitmap btn_Create__Click;

            public static Bitmap btn_Join;
            public static Bitmap btn_Join__Hover;
            public static Bitmap btn_Join__Click;

            public static Bitmap btn_Back;
            public static Bitmap btn_Back__Hover;
            public static Bitmap btn_Back__Click;

        }

        public static class XO
        {
            public static Bitmap GridX;
            public static Bitmap GridO;

            public static Bitmap IconX;
            public static Bitmap IconO;
        }

        public static Dictionary<char, Bitmap> Chars;


        public static void Init()
        {
            XOXLogo = Resources.XOX_Logo;

            initChars();
            initButtons();
            initTexts();
            initXO();

            void initButtons()
            {
                #region Large Buttons
                TextureFile largeButtonsTexture = new TextureFile(Resources.LargeButtons, 2, new Size(32,11));

                Buttons.btn_Create = largeButtonsTexture.getTextureByIndex(0);
                Buttons.btn_Join = largeButtonsTexture.getTextureByIndex(1);

                
                Buttons.btn_Create__Hover = largeButtonsTexture.getTextureByIndex(2);
                Buttons.btn_Join__Hover = largeButtonsTexture.getTextureByIndex(3);

                Buttons.btn_Create__Click = Buttons.btn_Create;
                Buttons.btn_Join__Click = Buttons.btn_Join;
                #endregion

                #region Small Buttons
                TextureFile smallButtonsTexture = new TextureFile(Resources.SmallButtons, 1, new Size(11,11));

                Buttons.btn_Back = smallButtonsTexture.getTextureByIndex(0);
                Buttons.btn_Back__Hover = smallButtonsTexture.getTextureByIndex(1);
                Buttons.btn_Back__Click = Buttons.btn_Back;

                #endregion
            }

            void initChars()
            {
                TextureFile charsTF = new TextureFile(Resources.chars, 26, new Size(5, 7));

                string charsString = "abcdefghijklmnopqrstuvwxyz123456789";

                Chars = new Dictionary<char, Bitmap>();
                for(int i = 0; i < charsString.Length; i++)
                {
                    Chars.Add(charsString[i], charsTF.getTextureByIndex(i));
                }
            }

            void initTexts()
            {
                Texts.EnterRoomCode = Resources.Text_EnterRoomCode;
                Texts.ShareRoomCode = Resources.Text_ShareRoomCode;
                Texts.NextPlayer = Resources.Text_NextPlayer;
            }

            void initXO()
            {
                TextureFile GridXOTF = new TextureFile(Resources.GridXO,2, new Size(8, 8));
                TextureFile IconXOTF = new TextureFile(Resources.IconXO, 2, new Size(10,10));

                XO.GridX = GridXOTF.getTextureByIndex(0);
                XO.GridO = GridXOTF.getTextureByIndex(1);

                XO.IconX = IconXOTF.getTextureByIndex(1);
                XO.IconO = IconXOTF.getTextureByIndex(0);
            }
        }

    }
}
