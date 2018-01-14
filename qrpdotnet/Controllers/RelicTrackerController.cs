using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace qrpdotnet.Controllers
{
    [Route("api/[controller]")]
    public class RelicTrackerController : Controller
    {
        public enum Eras { Lith, Neo, Meso, Axi }
        private string[] EraNames = new[]
        {
            "Lith", "Neo", "Meso", "Axi"
        };

        [HttpGet("[action]")]
        public IEnumerable<Relic> GetRelics()
        {
            return Enumerable.Range(1, 4).Select(index => new Relic
            {
                Era = (Eras)(index - 1),
                Type = "V"+index
            });
        }

        public class Relic
        {
            public Eras Era { get; set; }
            public string Type { get; set; }

            private IEnumerable<Drop> _drops;
            public IEnumerable<Drop> Drops
            {
                get
                {
                    return _drops;
                }
                set
                {
                    if (value.Count() == 6)
                        _drops = value;
                    else
                        throw new Exception("Invalid number of drops");
                }
            }

            private int _platinum = 0;
            public int Platinum { 
                get {
                    if (_platinum == 0) {
                        _platinum = Calculate("platinum");
                    }
                    return _platinum;
                } 
            }
            
            private int _ducats = 0;
            public int Ducats { 
                get {
                    if (_ducats == 0) {
                        _ducats = Calculate("ducats");
                    }
                    return _ducats;
                } 
            }

            private int Calculate(string target) {
                return (from drop in Drops
                        select (target == "platinum" ? drop.Platinum : drop.Ducats)).Sum();
            }

            public double DucatsPerPlatinum
            {
                get
                {
                    return (double)Ducats/Platinum;
                }
            }
        }

        public class Drop {
            public class Rarity {
                public string Name { get; set; }
                public IEnumerable<int> Rarities { get; set; }
            }
            public string Name { get; set; }
            public string Component { get; set; }
            public Rarity DropRarity { get; set; }
            public int Platinum { get; set; }
            public int Ducats { get; set; }
        }
    }
}
