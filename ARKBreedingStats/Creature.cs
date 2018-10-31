using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Linq.Mapping;
using System.Xml;
using System.Xml.Serialization;

namespace ARKBreedingStats
{
    [Table(Name = "Creature"),Serializable()]
    public class Creature : IEquatable<Creature>
    {
        //stored in dbguid
        public Guid guid; // the id used in ASB for parent-linking. if ARKID is available from import, it's created using that
        [Column]
        public string species;
        [Column]
        public string name;
        public Sex gender; // remove on 07/2018
        [Column]
        public Sex sex;
        [Column]
        public CreatureStatus status;
        
        // order of the stats is Health, Stamina, Oxygen, Food, Weight, MeleeDamage, Speed, Torpor
        //stored in individual levelswild
        public int[] levelsWild;
        //stored in individual levelsdom
        public int[] levelsDom;

        [Column]
        public double tamingEff;
        [Column]
        public double imprintingBonus;

        [XmlIgnore]
        public double[] valuesBreeding = new double[8];
        [XmlIgnore]
        public double[] valuesDom = new double[8];
        [XmlIgnore]
        public bool[] topBreedingStats = new bool[8]; // indexes of stats that are top for that species in the creaturecollection
        [XmlIgnore]
        public Int16 topStatsCount;
        [XmlIgnore]
        public Int16 topStatsCountBP; // topstatcount with all stats (regardless of considerStatHighlight[]) and without torpor (for breedingplanner)
        [XmlIgnore]
        public bool topBreedingCreature; // true if it has some topBreedingStats and if it's male, no other male has more topBreedingStats
        [XmlIgnore]
        public Int16 topness; // permille of mean of wildlevels compared to toplevels


        [Column]
        public string owner = "";
        [Column]
        public string imprinterName = ""; // todo implement in creatureInfoInbox
        [Column]
        public string tribe = "";
        [Column]
        public string server = "";
        [Column]
        public string note; // user defined note about that creature
        [Column]
        public long ARKID; // the creature's id in ARK
        [Column]
        public bool isBred;
        //sotred in dbfatherguid
        public Guid fatherGuid;
        //storedin dbmotherguid
        public Guid motherGuid;


        [XmlIgnore]
        public string fatherName; // only used during import for missing ancestors
        [XmlIgnore]
        public string motherName; // only used during import for missing ancestors
        [XmlIgnore]
        private Creature father;
        [XmlIgnore]
        private Creature mother;
        [XmlIgnore]
        public int levelFound;


        [Column]
        public int generation; // number of generations from the oldest wild creature

        //stored in individualcolors
        public int[] colors = new int[6] { 0, 0, 0, 0, 0, 0 }; // id of colors


        [Column]
        public DateTime growingUntil = new DateTime(0);
        [Column]
        public DateTime cooldownUntil = new DateTime(0);
        [Column]
        public DateTime domesticatedAt = new DateTime(0);
        [Column]
        public DateTime addedToLibrary = new DateTime(0);
        [Column]
        public bool neutered = false;
        [Column]
        public int mutationCounter; // TODO. remove this field on 07-2018
        [Column]
        public int mutationsMaternal;
        [Column]
        public int mutationsPaternal;

        // stored in tags list
        public List<string> tags = new List<string>();


        [Column]
        public bool placeholder; // if a creature has unknown parents, they are placeholders until they are imported. placeholders are not shown in the library

        public Creature()
        {
            levelsWild = new int[] { -1, -1, -1, -1, -1, -1, -1, -1 }; // unknown wild levels
            levelsDom = new int[] { 0, 0, 0, 0, 0, 0, 0 ,0}; // unknown wild levels
        }

        public Creature(string species, string name, string owner, string tribe, Sex sex, int[] levelsWild, int[] levelsDom = null, double tamingEff = 0, bool isBred = false, double imprinting = 0, int? levelStep = null)
        {
            this.species = species;
            this.name = name;
            this.owner = owner;
            this.tribe = tribe;
            this.sex = sex;
            this.levelsWild = levelsWild;
            this.levelsDom = (levelsDom == null ? new int[] { 0, 0, 0, 0, 0, 0, 0, 0 } : levelsDom);
            if (isBred)
                this.tamingEff = 1;
            else
                this.tamingEff = tamingEff;
            this.isBred = isBred;
            imprintingBonus = imprinting;
            this.status = CreatureStatus.Available;
            calculateLevelFound(levelStep);
        }

        public Creature(Guid guid)
        {
            this.guid = guid;
            levelsWild = new int[] { -1, -1, -1, -1, -1, -1, -1, -1 }; // unknown wild levels
        }


        [Column]
        public int color0
        {
            get
            {
                return colors[0];
            }
            set
            {
                colors[0] = value;
            }
        }
        [Column]
        public int color1
        {
            get
            {
                return colors[1];
            }
            set
            {
                colors[1] = value;
            }
        }
        [Column]
        public int color2
        {
            get
            {
                return colors[2];
            }
            set
            {
                colors[2] = value;
            }
        }
        [Column]
        public int color3
        {
            get
            {
                return colors[3];
            }
            set
            {
                colors[3] = value;
            }
        }
        [Column]
        public int color4
        {
            get
            {
                return colors[4];
            }
            set
            {
                colors[4] = value;
            }
        }
        [Column]
        public int color5
        {
            get
            {
                return colors[5];
            }
            set
            {
                colors[5] = value;
            }
        }

        [Column]
        public string tagsList
        {
            get
            {
                string retString = "";
                foreach (string tag in tags)
                {
                    retString += tag + "\r";
                }
                return retString;
            }
            set
            {
                List<string> setString = new List<string>();
                setString = value.ToString().Split('\r').ToList<string>();
            }
        }

        // order of the stats is Health, Stamina, Oxygen, Food, Weight, MeleeDamage, Speed, Torpor
        [Column,XmlIgnore]
        public int wildhealth
        {
            get
            {
                return levelsWild[0];
            }
            set
            {
                levelsWild[0] = value;
            }
        }
        [Column, XmlIgnore]
        public int wildstamina
        {
            get
            {
                return levelsWild[1];
            }
            set
            {
                levelsWild[1] = value;
            }
        }
        [Column, XmlIgnore]
        public int wildoxygen
        {
            get
            {
                return levelsWild[2];
            }
            set
            {
                levelsWild[2] = value;
            }
        }
        [Column, XmlIgnore]
        public int wildfood
        {
            get
            {
                return levelsWild[3];
            }
            set
            {
                levelsWild[3] = value;
            }
        }
        [Column, XmlIgnore]
        public int wildweight
        {
            get
            {
                return levelsWild[4];
            }
            set
            {
                levelsWild[4] = value;
            }
        }
        [Column, XmlIgnore]
        public int wildmelee
        {
            get
            {
                return levelsWild[5];
            }
            set
            {
                levelsWild[5] = value;
            }
        }
        [Column, XmlIgnore]
        public int wildspeed
        {
            get
            {
                return levelsWild[6];
            }
            set
            {
                levelsWild[6] = value;
            }
        }
        [Column, XmlIgnore]
        public int wildtorpor
        {
            get
            {
                return levelsWild[7];
            }
            set
            {
                levelsWild[7] = value;
            }
        }

        [Column, XmlIgnore]
        public int domhealth
        {
            get
            {
                return levelsDom[0];
            }
            set
            {
                levelsDom[0] = value;
            }
        }
        [Column, XmlIgnore]
        public int domstamina
        {
            get
            {
                return levelsDom[1];
            }
            set
            {
                levelsDom[1] = value;
            }
        }
        [Column, XmlIgnore]
        public int domoxygen
        {
            get
            {
                return levelsDom[2];
            }
            set
            {
                levelsDom[2] = value;
            }
        }
        [Column, XmlIgnore]
        public int domfood
        {
            get
            {
                return levelsDom[3];
            }
            set
            {
                levelsDom[3] = value;
            }
        }
        [Column, XmlIgnore]
        public int domweight
        {
            get
            {
                return levelsDom[4];
            }
            set
            {
                levelsDom[4] = value;
            }
        }
        [Column, XmlIgnore]
        public int dommelee
        {
            get
            {
                return levelsDom[5];
            }
            set
            {
                levelsDom[5] = value;
            }
        }
        [Column, XmlIgnore]
        public int domspeed
        {
            get
            {
                return levelsDom[6];
            }
            set
            {
                levelsDom[6] = value;
            }
        }
        [Column, XmlIgnore]
        public int domtorpor
        {
            get
            {
                return levelsDom[7];
            }
            set
            {
                levelsDom[7] = value;
            }
        }

        [Column, XmlIgnore]
        public string dbFatherGuid
        {
            get
            {
                return this.fatherGuid.ToString();
            }
            set
            {
                this.fatherGuid = Guid.Parse(value);
            }
        }

        [Column, XmlIgnore]
        public string dbMotherGuid
        {
            get
            {
                return this.motherGuid.ToString();
            }
            set
            {
                this.motherGuid = Guid.Parse(value);
            }
        }

        [Column(IsPrimaryKey = true), XmlIgnore]
        public string dbGuid
        {
            get
            {
                return this.guid.ToString();
            }
            set
            {
                this.guid = Guid.Parse(value);
            }
        }

        public bool Equals(Creature other)
        {
            if (other.guid == guid)
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Creature creatureObj = obj as Creature;
            if (creatureObj == null)
                return false;
            else
                return Equals(creatureObj);
        }

        public override int GetHashCode()
        {
            return guid.GetHashCode();
        }

        public void calculateLevelFound(int? levelStep)
        {
            levelFound = 0;
            if (!isBred && tamingEff >= 0)
            {
                if (levelStep.HasValue)
                    levelFound = (int)Math.Round(levelHatched / (1 + tamingEff / 2) / levelStep.Value) * levelStep.Value;
                else
                    levelFound = (int)Math.Ceiling(Math.Round(levelHatched / (1 + tamingEff / 2), 6));
            }
        }

        [XmlIgnore]
        public int levelHatched { get { return levelsWild[7] + 1; } }
        [XmlIgnore]
        public int level { get { return levelHatched + levelsDom.Sum(); } }

        public void recalculateAncestorGenerations()
        {
            generation = ancestorGenerations();
        }

        /// <summary>
        /// Returns the number of generations to the oldest known ancestor
        /// </summary>
        /// <returns></returns>
        private int ancestorGenerations(int g = 0)
        {
            // to detect loop (if a creature is falsely listed as its own ancestor)
            if (g > 99)
                return 0;

            int mgen = 0, fgen = 0;
            if (mother != null)
                mgen = mother.ancestorGenerations(g + 1) + 1;
            if (father != null)
                fgen = father.ancestorGenerations(g + 1) + 1;
            if (isBred && mgen == 0 && fgen == 0)
                return 1;
            if (mgen > fgen)
                return mgen;
            else
                return fgen;
        }

        [XmlIgnore]
        public Creature Mother
        {
            set
            {
                mother = value;
                motherGuid = (mother != null ? mother.guid : Guid.Empty);
            }
            get { return mother; }
        }
        [XmlIgnore]
        public Creature Father
        {
            set
            {
                father = value;
                fatherGuid = (father != null ? father.guid : Guid.Empty);
            }
            get { return father; }
        }

        public void setTopStatCount(bool[] considerStatHighlight)
        {
            Int16 c = 0, cBP = 0;
            for (int s = 0; s < 8; s++)
            {
                if (topBreedingStats[s])
                {
                    if (s < 7)
                        cBP++;
                    if (considerStatHighlight[s])
                        c++;
                }
            }
            topStatsCount = c;
            topStatsCountBP = cBP;
        }

        /// <summary>
        /// call this function to recalculate all stat-values of Creature c according to its levels
        /// </summary>
        public void recalculateCreatureValues(int? levelStep)
        {
            int speciesIndex = Values.V.speciesNames.IndexOf(species);
            if (speciesIndex >= 0)
            {
                for (int s = 0; s < 8; s++)
                {
                    valuesBreeding[s] = Stats.calculateValue(speciesIndex, s, levelsWild[s], 0, true, 1, 0);
                    valuesDom[s] = Stats.calculateValue(speciesIndex, s, levelsWild[s], levelsDom[s], true, tamingEff, imprintingBonus);
                }
            }
            calculateLevelFound(levelStep);
        }

        [XmlIgnore]
        public int Mutations
        {
            get { return mutationsMaternal + mutationsPaternal; }
        }
    }


    public enum Sex
    {
        Unknown = 0,
        Male = 1,
        Female = 2
    };

    public enum CreatureStatus
    {
        Available,
        Dead,
        Unavailable,
        Obelisk,
        Alive = Available // backwards-compatibility
    };
}