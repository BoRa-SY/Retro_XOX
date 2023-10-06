using PixelBuilder.TextureUtils;
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

        public static Dictionary<char, Bitmap> Chars;


        public static void Init()
        {
            XOXLogo = Resources.XOX_Logo;

            initChars();
            initButtons();
            initTexts();

            void initButtons()
            {
                #region Large Buttons
                TextureFile largeButtonsTexture = new TextureFile(Resources.LargeButtons, 2, new Size(32,11));

                Buttons.btn_Create = largeButtonsTexture.getTextureByIndex(0);
                Buttons.btn_Join = largeButtonsTexture.getTextureByIndex(1);
                
                Buttons.btn_Create__Hover = largeButtonsTexture.getTextureByIndex(2);
                Buttons.btn_Join__Hover = largeButtonsTexture.getTextureByIndex(3);
                #endregion

                #region Small Buttons
                TextureFile smallButtonsTexture = new TextureFile(Resources.SmallButtons, 1, new Size(11,11));

                Buttons.btn_Back = smallButtonsTexture.getTextureByIndex(0);
                Buttons.btn_Back__Hover = smallButtonsTexture.getTextureByIndex(1);

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
            }
        }

    }
}
