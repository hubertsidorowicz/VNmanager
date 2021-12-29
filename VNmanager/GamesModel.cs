using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace VNmanager
{
    public class GamesModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string GameUrl { get; set; }
        public string LastPlayed { get; set; }
        public string Added { get; set; }
        public byte[] Icon { get; set; }
        public string TitleImage { get; set; }
        public string PageImage { get; set; }
        public double XT { get; set; }
        public double YT { get; set; }
        public double WidthT { get; set; }
        public double HeightT { get; set; }
        public double XP { get; set; }
        public double YP { get; set; }
        public double WidthP { get; set; }
        public double HeightP { get; set; }
    }
}