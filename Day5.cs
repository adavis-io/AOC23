using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace AOC23
{
    public class AlmanacMap
    {
        public long sourceStart, sourceEnd;
        public long destStart, destEnd;
        public long length;

        public AlmanacMap(String mapdesc) 
        {
            long source, dest, len;

            var mapSplit = mapdesc.Split(' ');

            dest = long.Parse(mapSplit[0]);
            source = long.Parse(mapSplit[1]);
            len = long.Parse(mapSplit[2]);

            this.sourceStart = source;
            this.sourceEnd = source + len - 1;
            this.destStart = dest;
            this.destEnd = dest + len - 1;

            this.length = len;
        }

        public long Apply(long source)
        {
            if (source >= this.sourceStart && source <= this.sourceEnd)
            {
                return destStart + (source - this.sourceStart);
            }

            return source;
        }

        public long ReverseApply(long destination)
        {
            if (destination >= this.destStart && destination <= this.destEnd)
            {
                return sourceStart + (destination - this.destStart);
            }

            return destination;
        }

        public override string ToString()
        {
            return String.Format("Map - d: {0}, s: {1}, l: {2}", this.destStart, this.sourceStart, this.length);
        }
    }
    public class Almanac
    {
        public List<AlmanacMap> seedSoil = new();
        public List<AlmanacMap> soilFertilizer = new();
        public List<AlmanacMap> fertilizerWater = new();
        public List<AlmanacMap> waterLight = new();
        public List<AlmanacMap> lightTemperature = new();
        public List<AlmanacMap> tempHumidity = new();
        public List<AlmanacMap> humidityLocation = new();


        public List<long> seeds = new();
        
        public Almanac(List<string> lines) 
        {
            var currentList = seedSoil;

            foreach (var line in lines)
            {
                if (line.Contains("seeds:"))
                {
                    foreach (var seedString in line.Split(' ').Skip(1))
                    {
                        this.seeds.Add(long.Parse(seedString));
                    }
                    continue;
                }

                switch (line.Trim())
                {
                    case "":
                        break;
                    case "seed-to-soil map:":
                        currentList = this.seedSoil;
                        break;
                    case "soil-to-fertilizer map:":
                        currentList = this.soilFertilizer;
                        break;
                    case "fertilizer-to-water map:":
                        currentList = this.fertilizerWater;
                        break;
                    case "water-to-light map:":
                        currentList = this.waterLight;
                        break;
                    case "light-to-temperature map:":
                        currentList = this.lightTemperature;
                        break;
                    case "temperature-to-humidity map:":
                        currentList = this.tempHumidity;
                        break;
                    case "humidity-to-location map:":
                        currentList = this.humidityLocation;
                        break;

                    default:
                        currentList.Add(new AlmanacMap(line));
                        break;
                }
            }
        }

        public void ApplySeedRanges()
        {
            List<long> new_seeds = new();

            for (int i = 0; i < this.seeds.Count; i += 2) 
            {
                for (long j = 0; j < this.seeds[i + 1]; j++)
                {
                    new_seeds.Add(this.seeds[i] + j);
                }
            }

            this.seeds = new_seeds;
        }

        public long ApplyMapping(long source, List<AlmanacMap> maps)
        {
            long dest = source;
            foreach (var map in maps)
            {
                dest = map.Apply(source);

                if (dest != source)
                {
                    break;
                }
                
            }

            return dest;
        }

        public long ApplyReverseMapping(long destination, List<AlmanacMap> maps)
        {
            long source = destination;
            foreach (var map in maps)
            {
                source = map.ReverseApply(destination);

                if (destination != source)
                {
                    break;
                }

            }

            return source;
        }

        public bool IsValidSeed(long seedNumber)
        {
            for (int i = 0; i < this.seeds.Count; i += 2)
            {
                if (seedNumber >= this.seeds[i] && seedNumber < this.seeds[i] + this.seeds[i + 1])
                {
                    return true;
                }
            }

            return false;
        }

        public long GetSeedLocation(long seed)
        { 
            var soil = ApplyMapping(seed, this.seedSoil);

            var fertilizer = ApplyMapping(soil, this.soilFertilizer);
            var water = ApplyMapping(fertilizer, this.fertilizerWater);
            var light = ApplyMapping(water, this.waterLight);
            var temperature = ApplyMapping(light, this.lightTemperature);
            var humidity = ApplyMapping(temperature, this.tempHumidity);
            var location = ApplyMapping(humidity, this.humidityLocation);

            return location;
        }

        public long GetSeedNumber(long location)
        {
            var humidity = ApplyReverseMapping(location, this.humidityLocation);
            var temperature = ApplyReverseMapping(humidity, this.tempHumidity);
            var light = ApplyReverseMapping(temperature, this.lightTemperature);
            var water = ApplyReverseMapping(light, this.waterLight);
            var fertilizer = ApplyReverseMapping(water, this.fertilizerWater);
            var soil = ApplyReverseMapping(fertilizer, this.soilFertilizer);

            return ApplyReverseMapping(soil, this.seedSoil);
        }

        public long[] GetMappedLocationExtents()
        {
            long[] bounds = { long.MaxValue, long.MinValue };
            
            foreach (var m in this.humidityLocation)
            {
                if (m.destStart < bounds[0])
                {
                    bounds[0] = m.destStart;
                }

                if (m.destEnd > bounds[1])
                {
                    bounds[1] = m.destEnd;
                }
            }

            return bounds;
        }
    }
    public class Day5(bool test) : Day(5, test)
    {
        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();

            var almanac = new Almanac(lines);

            long lowest = long.MaxValue;
            foreach (var s in almanac.seeds)
            {
                lowest = Math.Min(almanac.GetSeedLocation(s), lowest);
            }
            Console.WriteLine(lowest);
        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();
            var almanac = new Almanac(lines);
            
            var bounds = almanac.GetMappedLocationExtents();

            for (long i = 0; i <= bounds[1]; i++)
            {
                if (almanac.IsValidSeed(almanac.GetSeedNumber(i)))
                {
                    Console.WriteLine(i);
                    break;
                }
            }


        }
    }
}
