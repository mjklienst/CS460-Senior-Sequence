using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;

namespace Homework4.Controllers
{
    public class ColorInterpolatorController : Controller
    {

        // From Greg's answer: https://stackoverflow.com/questions/359612/how-to-change-rgb-color-to-hsv/1626175
        // And Wikipedia: https://en.wikipedia.org/wiki/HSL_and_HSV
        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        //Source code from following link helped me: http://www.java2s.com/Code/CSharp/2D-Graphics/RgbLinearInterpolate.htm
        public static List<Color> RgbLinearInterpolate(Color start, Color end, int total)
        {
            List<Color> colList = new List<Color>(); //Creating new list of colors

            for (int i = 0; i < total; i++)
            {
                double r = (double)i / (double)(total - 1);
                double nr = 1.0 - r;
                double A = (nr * start.A) + (r * end.A);
                double R = (nr * start.R) + (r * end.R);
                double G = (nr * start.G) + (r * end.G);
                double B = (nr * start.B) + (r * end.B);

                colList.Add(Color.FromArgb((byte)A, (byte)R, (byte)G, (byte)B));
            }

            return colList;
        }

        // GET: ColorInterpolator
        [HttpGet]
        public ActionResult ColorInterpolator()
        {
            ViewBag.toggle = 0; //Setting toggle to 0 so list in .cshtml is not outputted
            return View();
        }

        [HttpPost]
        public ActionResult ColorInterpolator(string First, string Second, int? Num)
        {
            ViewBag.toggle = 0;
            if (ModelState.IsValid)
            {
                Color color1 = ColorTranslator.FromHtml(First); //Converting from HTML
                ColorToHSV(color1, out double hue, out double saturation, out double value); //Changing from RGB to HSV
                Color color2 = ColorTranslator.FromHtml(Second); //Converting from HTML
                ColorToHSV(color2, out double hue2, out double saturation2, out double value2); //Changing from RGB to HSV
                List<Color> colorList = RgbLinearInterpolate(color1, color2, (int)Num); //Creating list of colors
                List<string> hexList = new List<string>(); //Creating list of hex colors
                IList<colorStruct> output = new List<colorStruct>(); //Creating struct list w/ colors and hex values
                //Adding to the structured list
                for (int i = 0; i < colorList.Count; i++)
                {
                    hexList.Add(ColorTranslator.ToHtml(colorList[i]));
                    output.Add(new colorStruct { hexValue = hexList[i], RGBValue = colorList[i]});
                }
                ViewBag.toggle = 1; //Setting toggle to 1 so we output list of colors and hex
                ViewBag.List = output; //Setting struct to a ViewBag to use in .cshtml file table
                return View();
            }
            else
            {
                ViewBag.Messagee = "Error";
                return View();
            }
        }
        //Creating struct w/ hex and RGB values for table in .cshtml file
        public struct colorStruct
        {
            public string hexValue { get; set; }
            public Color RGBValue { get; set; }
        }
    }

    }
