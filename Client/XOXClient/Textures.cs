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

        public static class Colors
        {
            public static readonly Color backgroundColor = Color.FromArgb(0, 188, 212);
        }

        public static class Buttons
        {
            public static Bitmap btn_Create;
            public static Bitmap btn_Create__Hover;

            public static Bitmap btn_Join;
            public static Bitmap btn_Join__Hover;

        }



        public static void Init()
        {
            XOXLogo = Resources.XOX_Logo;

            initButtons();

            Buttons.btn_Create.Save("1.png");
            Buttons.btn_Create__Hover.Save("2.png");
            void initButtons()
            {
                #region Large Buttons
                TextureFile largeButtonsTexture = new TextureFile(Resources.LargeButtons, 2, new Size(32,11));

                Buttons.btn_Create = largeButtonsTexture.getTextureByIndex(0);
                Buttons.btn_Join = largeButtonsTexture.getTextureByIndex(1);

                Buttons.btn_Create__Hover = largeButtonsTexture.getTextureByIndex(2);
                Buttons.btn_Join__Hover = largeButtonsTexture.getTextureByIndex(3);
                #endregion
            }
        }

    }
}
