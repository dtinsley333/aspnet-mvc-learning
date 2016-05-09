using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Quality
{
    public enum BarCodeWeight
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }

    /// <summary>
    /// An ASP.NET port of a WinForms User Control created by Rocky Pulley.
    /// See http://www.codeproject.com/KB/miscctrl/barcodectl.aspx for more details.
    /// </summary>
    public class Code39BarCode
    {
        #region Private Member Variables
        private const int SpacingBetweenBarCodeAndText = 5;
        private const string InterchangeGap = "1";
        private readonly string code39alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*";
        private readonly string[] code39code = 
		{
			/* 0 */ "000110100", 
			/* 1 */ "100100001", 
			/* 2 */ "001100001", 
			/* 3 */ "101100000",
			/* 4 */ "000110001", 
			/* 5 */ "100110000", 
			/* 6 */ "001110000", 
			/* 7 */ "000100101",
			/* 8 */ "100100100", 
			/* 9 */ "001100100", 
			/* A */ "100001001", 
			/* B */ "001001001",
			/* C */ "101001000", 
			/* D */ "000011001", 
			/* E */ "100011000", 
			/* F */ "001011000",
			/* G */ "000001101", 
			/* H */ "100001100", 
			/* I */ "001001100", 
			/* J */ "000011100",
			/* K */ "100000011", 
			/* L */ "001000011", 
			/* M */ "101000010", 
			/* N */ "000010011",
			/* O */ "100010010", 
			/* P */ "001010010", 
			/* Q */ "000000111", 
			/* R */ "100000110",
			/* S */ "001000110", 
			/* T */ "000010110", 
			/* U */ "110000001", 
			/* V */ "011000001",
			/* W */ "111000000", 
			/* X */ "010010001", 
			/* Y */ "110010000", 
			/* Z */ "011010000",
			/* - */ "010000101", 
			/* . */ "110000100", 
			/*' '*/ "011000100",
			/* $ */ "010101000",
			/* / */ "010100010", 
			/* + */ "010001010", 
			/* % */ "000101010", 
			/* * */ "010010100" 
		};
        #endregion

        #region Constructors
        public Code39BarCode() : this(string.Empty) { }
        public Code39BarCode(string barCodeText) : this(barCodeText, 75) { }
        public Code39BarCode(string barCodeText, int height)
        {
            this.BarCodeText = barCodeText;
            this.Height = height;
            this.BarCodePadding = 5;
            this.ShowBarCodeText = true;
            this.BarCodeWeight = BarCodeWeight.Small;
            this.BarCodeTextFont = new Font("Arial", 10.0F);
            this.ImageFormat = ImageFormat.Gif;
        }
        #endregion

        #region Properties
        public string BarCodeText { get; set; }
        public BarCodeWeight BarCodeWeight { get; set; }
        public int BarCodePadding { get; set; }
        public int Height { get; set; }
        public bool ShowBarCodeText { get; set; }
        public Font BarCodeTextFont { get; set; }
        public ImageFormat ImageFormat { get; set; }
        #endregion

        public byte[] Generate()
        {
            // Ensure BarCode property has been set
            if (string.IsNullOrWhiteSpace(this.BarCodeText))
                throw new ArgumentException("BarCode must be set prior to calling Generate");

            // Ensure BarCode does not contain invalid characters
            for (var i = 0; i < this.BarCodeText.Length; i++)
                if (code39alphabet.IndexOf(this.BarCodeText[i]) == -1)
                    throw new ArgumentException(string.Format("Invalid character for barcode: '{0}' is not a valid code 39 character", this.BarCodeText[i]));


            // Create the encoded string
            var codeToGenerate = "*" + this.BarCodeText + "*";
            var encodedString = string.Empty;
            for (var i = 0; i < codeToGenerate.Length; i++)
            {
                if (i > 0)
                    encodedString += InterchangeGap;

                encodedString += code39code[code39alphabet.IndexOf(codeToGenerate[i])];
            }

            // Determine the barcode width
            var widthOfBarCodeString = 0;
            var wideToNarrowRatio = 3.0;
            for (var i = 0; i < encodedString.Length; i++)
            {
                if (encodedString[i] == '1')
                    widthOfBarCodeString += (int)(wideToNarrowRatio * (int)this.BarCodeWeight);
                else
                    widthOfBarCodeString += (int)this.BarCodeWeight;
            }

            var widthOfImage = widthOfBarCodeString + (BarCodePadding * 2);


            // Create Bitmap class
            using (var bmp = new Bitmap(widthOfImage, this.Height, PixelFormat.Format32bppArgb))
            {
                using (var gfx = Graphics.FromImage(bmp))
                {
                    // Start with a white background
                    gfx.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);

                    // Determine offset to center barCodeText and the height of barcode
                    var barCodeTextSize = gfx.MeasureString(this.BarCodeText, this.BarCodeTextFont);
                    var barCodeTextX = (widthOfImage - (int)barCodeTextSize.Width) / 2;
                    var barCodeHeight = this.Height - (this.BarCodePadding * 2);
                    if (this.ShowBarCodeText)
                        barCodeHeight -= SpacingBetweenBarCodeAndText + (int)barCodeTextSize.Height;

                    var x = this.BarCodePadding;
                    var barCodeTop = this.BarCodePadding;
                    var barCodeBottom = barCodeTop + barCodeHeight;

                    for (var i = 0; i < encodedString.Length; i++)
                    {
                        var lineWidth = 0;

                        if (encodedString[i] == '1')
                            lineWidth = (int)(wideToNarrowRatio * (int)this.BarCodeWeight);
                        else
                            lineWidth = (int)this.BarCodeWeight;

                        gfx.FillRectangle(i % 2 == 0 ? Brushes.Black : Brushes.White, x, barCodeTop, lineWidth, barCodeBottom);

                        x += lineWidth;
                    }

                    if (this.ShowBarCodeText)
                    {
                        var barCodeTextTop = barCodeBottom + SpacingBetweenBarCodeAndText;

                        gfx.DrawString(this.BarCodeText, this.BarCodeTextFont, Brushes.Black, barCodeTextX, barCodeTextTop);
                    }

                    var output = new MemoryStream();
                    bmp.Save(output, this.ImageFormat);
                    return output.ToArray();
                }
            }
        }

    }
}