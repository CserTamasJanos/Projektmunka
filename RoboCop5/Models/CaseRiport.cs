using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoboCop5.Models
{
    public class CaseRiport
    {
        public int SteelId{ get; set; }
        public int RegistrationNumber { get; set; }
        public int ZipCode { get; set; }
        public List<string> County { get; set; }
        public List<string> City { get; set; }
        public List<string> PublicSpaceType { get; set; }
        public string PublicSpaceName { get; set; }
        public string HouseNumber { get; set; }
        public DateTime DateOfAnnouncement { get; set; }
        public DateTime DateOfCrime { get; set; }
        public List<string> FaceTypes { get; set; }
        public List<string> HairColors { get; set; }
        public List<string> WalkingTypes { get; set; }
        public List<string> Heights { get; set; }
        public List<string> Weights { get; set; }
        public List<string> EyeColors { get; set; }
        public List<string> BodyTypes { get; set; }
        public List<string> Tools { get; set; }
        public string CrimeDescription { get; set; }
        public int UserID { get; set; }
        public string PersonOfAnnouncement { get; set; }
    }
}