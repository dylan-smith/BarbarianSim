using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using YamlDotNet.Core.Events;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace BarbarianSim
{
    public class GearItemFactory
    {
        private static IEnumerable<GearItem> _allGear;
        private static IEnumerable<GearItem> _equippableMainHand;
        private static IEnumerable<GearItem> _equippableOffHand;
        private static IEnumerable<GearItem> _allEnchants;
        private static IEnumerable<GearItem> _twoHandEnchants;
        private static IEnumerable<GearItem> _allGems;

        private static IDictionary<GearType, IEnumerable<GearItem>> _gearByType;
        private static IDictionary<GearType, IEnumerable<GearItem>> _enchantsByType;

        public static IEnumerable<GearItem> AllGear
        {
            get
            {
                if (_allGear == null)
                {
                    LoadAllGear();
                }

                return _allGear;
            }
        }

        public static IEnumerable<GearItem> AllEquippableMainHands
        {
            get
            {
                if (_equippableMainHand == null)
                {
                    LoadAllGear();
                }

                return _equippableMainHand;
            }
        }

        public static IEnumerable<GearItem> AllEquippableOffHands
        {
            get
            {
                if (_equippableOffHand == null)
                {
                    LoadAllGear();
                }

                return _equippableOffHand;
            }
        }

        public static IEnumerable<GearItem> AllEnchants
        {
            get
            {
                if (_allEnchants == null)
                {
                    LoadAllGear();
                }

                return _allEnchants;
            }
        }

        public static IEnumerable<GearItem> AllGems
        {
            get
            {
                if (_allGems == null)
                {
                    LoadAllGear();
                }

                return _allGems;
            }
        }

        public static IEnumerable<GearItem> AllTwoHandEnchants
        {
            get
            {
                if (_twoHandEnchants == null)
                {
                    LoadAllGear();
                }

                return _twoHandEnchants;
            }
        }

        public static IEnumerable<GearItem> AllAmmo => GetGearByType(GearType.Ammo);

        public static IEnumerable<GearItem> AllBack => GetGearByType(GearType.Back);

        public static IEnumerable<GearItem> AllChest => GetGearByType(GearType.Chest);

        public static IEnumerable<GearItem> AllFeet => GetGearByType(GearType.Feet);

        public static IEnumerable<GearItem> AllFinger => GetGearByType(GearType.Finger);

        public static IEnumerable<GearItem> AllHands => GetGearByType(GearType.Hands);

        public static IEnumerable<GearItem> AllHead => GetGearByType(GearType.Head);

        public static IEnumerable<GearItem> AllLegs => GetGearByType(GearType.Legs);

        public static IEnumerable<GearItem> AllMainHand => GetGearByType(GearType.MainHand);

        public static IEnumerable<GearItem> AllNeck => GetGearByType(GearType.Neck);

        public static IEnumerable<GearItem> AllOffHand => GetGearByType(GearType.OffHand);

        public static IEnumerable<GearItem> AllQuiver => GetGearByType(GearType.Quiver);

        public static IEnumerable<GearItem> AllRanged => GetGearByType(GearType.Ranged);

        public static IEnumerable<GearItem> AllShoulder => GetGearByType(GearType.Shoulder);

        public static IEnumerable<GearItem> AllTrinket => GetGearByType(GearType.Trinket);

        public static IEnumerable<GearItem> AllWaist => GetGearByType(GearType.Waist);

        public static IEnumerable<GearItem> AllWrist => GetGearByType(GearType.Wrist);

        public static IEnumerable<GearItem> AllBackEnchants => GetEnchantsByType(GearType.Back);

        public static IEnumerable<GearItem> AllChestEnchants => GetEnchantsByType(GearType.Chest);

        public static IEnumerable<GearItem> AllFeetEnchants => GetEnchantsByType(GearType.Feet);

        public static IEnumerable<GearItem> AllHandEnchants => GetEnchantsByType(GearType.Hands);

        public static IEnumerable<GearItem> AllHeadEnchants => GetEnchantsByType(GearType.Head);

        public static IEnumerable<GearItem> AllLegEnchants => GetEnchantsByType(GearType.Legs);

        public static IEnumerable<GearItem> AllOneHandEnchants => GetEnchantsByType(GearType.OneHand);

        public static IEnumerable<GearItem> AllRangedEnchants => GetEnchantsByType(GearType.Ranged);

        public static IEnumerable<GearItem> AllShoulderEnchants => GetEnchantsByType(GearType.Shoulder);

        public static IEnumerable<GearItem> AllWristEnchants => GetEnchantsByType(GearType.Wrist);

        public static GearItem Load(string itemName) => GetItem(AllGear, itemName);

        public static GearItem LoadAmmo(string itemName) => GetItem(AllAmmo, itemName);
        
        public static GearItem LoadBack(string itemName) => GetItem(AllBack, itemName);

        public static GearItem LoadChest(string itemName) => GetItem(AllChest, itemName);

        public static GearItem LoadFeet(string itemName) => GetItem(AllFeet, itemName);

        public static GearItem LoadFinger(string itemName) => GetItem(AllFinger, itemName);

        public static GearItem LoadHands(string itemName) => GetItem(AllHands, itemName);

        public static GearItem LoadHead(string itemName) => GetItem(AllHead, itemName);

        public static GearItem LoadLegs(string itemName) => GetItem(AllLegs, itemName);

        public static GearItem LoadMainHand(string itemName) => GetItem(AllEquippableMainHands, itemName);

        public static GearItem LoadNeck(string itemName) => GetItem(AllNeck, itemName);

        public static GearItem LoadOffHand(string itemName) => GetItem(AllEquippableOffHands, itemName);

        public static GearItem LoadQuiver(string itemName) => GetItem(AllQuiver, itemName);

        public static GearItem LoadRanged(string itemName) => GetItem(AllRanged, itemName);

        public static GearItem LoadShoulder(string itemName) => GetItem(AllShoulder, itemName);

        public static GearItem LoadTrinket(string itemName) => GetItem(AllTrinket, itemName);

        public static GearItem LoadWaist(string itemName) => GetItem(AllWaist, itemName);

        public static GearItem LoadWrist(string itemName) => GetItem(AllWrist, itemName);

        public static GearItem LoadEnchant(string enchantName) => GetItem(AllEnchants, enchantName);

        public static GearItem LoadBackEnchant(string enchantName) => GetItem(AllBackEnchants, enchantName);

        public static GearItem LoadChestEnchant(string enchantName) => GetItem(AllChestEnchants, enchantName);

        public static GearItem LoadFeetEnchant(string enchantName) => GetItem(AllFeetEnchants, enchantName);

        public static GearItem LoadHandEnchant(string enchantName) => GetItem(AllHandEnchants, enchantName);

        public static GearItem LoadHeadEnchant(string enchantName) => GetItem(AllHeadEnchants, enchantName);

        public static GearItem LoadLegEnchant(string enchantName) => GetItem(AllLegEnchants, enchantName);

        public static GearItem LoadOneHandEnchant(string enchantName) => GetItem(AllOneHandEnchants, enchantName);

        public static GearItem LoadTwoHandEnchant(string enchantName) => GetItem(AllTwoHandEnchants, enchantName);

        public static GearItem LoadRangedEnchant(string enchantName) => GetItem(AllRangedEnchants, enchantName);

        public static GearItem LoadShoulderEnchant(string enchantName) => GetItem(AllShoulderEnchants, enchantName);

        public static GearItem LoadWristEnchant(string enchantName) => GetItem(AllWristEnchants, enchantName);

        public static GearItem LoadGem(string gemName) => GetItem(AllGems, gemName);

        private static GearItem GetItem(IEnumerable<GearItem> gearList, string itemName) => gearList.Any(x => x.Name == itemName) ? gearList.Single(x => x.Name == itemName) : throw new ArgumentException($"Unrecognized item name [{itemName}]", nameof(itemName));

        private static IEnumerable<GearItem> GetGearByType(GearType gearType)
        {
            if (_allGear == null)
            {
                LoadAllGear();
            }

            return _gearByType[gearType];
        }

        private static IEnumerable<GearItem> GetEnchantsByType(GearType enchantType)
        {
            if (_allEnchants == null)
            {
                LoadAllGear();
            }

            return _enchantsByType[enchantType];
        }

        private static void LoadAllGear()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var gearPath = Path.Join(assemblyPath, "Gear");
            var enchantsPath = Path.Join(assemblyPath, "Enchants");
            var gemsPath = Path.Join(assemblyPath, "Gems");

            _gearByType = new Dictionary<GearType, IEnumerable<GearItem>>();
            _allGear = new List<GearItem>();
            _enchantsByType = new Dictionary<GearType, IEnumerable<GearItem>>();
            _allEnchants = new List<GearItem>();

            foreach (var gearType in Enum.GetValues(typeof(GearType)).Cast<GearType>())
            {
                var gear = LoadAllFromDir(Path.Join(gearPath, gearType.ToString()), gearType);
                _gearByType.Add(gearType, gear);
                _allGear = _allGear.Union(gear);

                var enchants = LoadAllFromDir(Path.Join(enchantsPath, gearType.ToString()), gearType);
                _enchantsByType.Add(gearType, enchants);
                _allEnchants = _allEnchants.Union(enchants);
            }

            _allGear = _allGear.ToList();
            _allEnchants = _allEnchants.ToList();
            
            var gems = LoadAllFromDir(gemsPath, GearType.Gem).ToList();
            gems.AddRange(LoadAllMetaGems());
            _allGems = gems;

            _equippableMainHand = _gearByType[GearType.MainHand].Union(_gearByType[GearType.OneHand]).ToList();
            _equippableOffHand = _gearByType[GearType.OffHand].Union(_gearByType[GearType.OneHand]).ToList();

            _twoHandEnchants = _enchantsByType[GearType.TwoHand].Union(_enchantsByType[GearType.OneHand]).ToList();
        }

        private static IEnumerable<MetaGem> LoadAllMetaGems()
        {
            var metaTypes = typeof(MetaGem).Assembly.GetTypes().Where(t => t.IsClass && t.IsSubclassOf(typeof(MetaGem))).ToList();

            foreach (var metaType in metaTypes)
            {
                yield return (MetaGem)Activator.CreateInstance(metaType);
            }
        }

        private static IEnumerable<GearItem> LoadAllFromDir(string gearPath, GearType gearType)
        {
            if (Directory.Exists(gearPath))
            {
                var files = Directory.GetFiles(gearPath, "*.yml");

                foreach (var file in files)
                {
                    yield return LoadGearItem(File.ReadAllText(file), gearType);
                }
            }
        }

        public static GearItem LoadGearItem(string yaml, GearType gearType)
        {
            ValidateYamlAgainstSchema(yaml);

            using var yamlReader = new StringReader(yaml);
            var yamlStream = new YamlStream();
            yamlStream.Load(yamlReader);

            var rootNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;

            return LoadGearItem(rootNode, gearType);
        }

        private static void ValidateYamlAgainstSchema(string yaml)
        {
            if (string.IsNullOrWhiteSpace(yaml))
            {
                throw new Exception("Empty YAML file");
            }

            DisableLicenseCheck();

            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var schemaPath = Path.Join(assemblyPath, "Config", "GearItem-Schema.json");
            var schemaJson = File.ReadAllText(schemaPath);
            var schema = JSchema.Load(new JsonTextReader(new StringReader(schemaJson)));

            var deserializer = new DeserializerBuilder()
                                   .WithNodeTypeResolver(new InferTypeFromValue())
                                   .Build();
            var data = deserializer.Deserialize(new StringReader(yaml));

            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            var serializer = new JsonSerializer();
            serializer.Serialize(sw, data);

            JObject.Parse(sb.ToString()).Validate(schema);
        }

        private static void DisableLicenseCheck()
        {
            var licenseHelperType = typeof(Newtonsoft.Json.Schema.License).Assembly.GetType("Newtonsoft.Json.Schema.Infrastructure.Licensing.LicenseHelpers");
            var field = licenseHelperType.GetField("_registeredLicense", BindingFlags.Static | BindingFlags.NonPublic);

            var licenseType = typeof(Newtonsoft.Json.Schema.License).Assembly.GetType("Newtonsoft.Json.Schema.Infrastructure.Licensing.LicenseDetails");
            field.SetValue(null, Activator.CreateInstance(licenseType));
        }

        private class InferTypeFromValue : INodeTypeResolver
        {
            public bool Resolve(NodeEvent nodeEvent, ref Type currentType)
            {
                if (nodeEvent is Scalar scalar)
                {
                    if (int.TryParse(scalar.Value, out _))
                    {
                        currentType = typeof(int);
                        return true;
                    }

                    if (decimal.TryParse(scalar.Value, out _))
                    {
                        currentType = typeof(decimal);
                        return true;
                    }

                    if (bool.TryParse(scalar.Value, out _))
                    {
                        currentType = typeof(bool);
                        return true;
                    }
                }

                return false;
            }
        }

        private static GearItem LoadGearItem(YamlMappingNode yamlNode, GearType gearType)
        {
            var result = new GearItem
            {
                GearType = gearType
            };

            foreach (var statItem in yamlNode.Children)
            {
                var statName = ((YamlScalarNode)statItem.Key).Value;

                if (statName.EndsWith("-skill"))
                {
                    var weaponType = statName.ShaveRight("-skill");
                    result.WeaponSkill.Add(weaponType.ToWeaponType(), int.Parse(((YamlScalarNode)statItem.Value).Value));

                    continue;
                }

                if (statName == "threat")
                {
                    result.ThreatDecrease = (0.0 - double.Parse(((YamlScalarNode)statItem.Value).Value)) / 100.0;
                    continue;
                }

                if (statName == "sockets")
                {
                    var socketsNode = (YamlMappingNode)statItem.Value;

                    foreach (var socketItem in socketsNode.Children)
                    {
                        var socketName = ((YamlScalarNode)socketItem.Key).Value;

                        if (socketName == "bonus")
                        {
                            var bonusNode = (YamlMappingNode)socketItem.Value;
                            result.SocketBonus = LoadGearItem(bonusNode, GearType.SocketBonus);
                            continue;
                        }

                        var socketColor = socketName.ToSocketColor();
                        var socketCount = int.Parse(((YamlScalarNode)socketItem.Value).Value);

                        for (var i = 0; i < socketCount; i++)
                        {
                            result.Sockets.Add(new Socket(socketColor));
                        }
                    }

                    continue;
                }

                var prop = result.GetType().GetProperties().Single(p => p.GetCustomAttributes<YamlProperty>().Any(a => a.PropertyName == statName));

                if (prop.PropertyType == typeof(string))
                {
                    prop.SetValue(result, ((YamlScalarNode)statItem.Value).Value);
                }

                if (prop.PropertyType == typeof(double))
                {
                    prop.SetValue(result, double.Parse(((YamlScalarNode)statItem.Value).Value));
                }

                if (prop.PropertyType == typeof(int))
                {
                    prop.SetValue(result, int.Parse(((YamlScalarNode)statItem.Value).Value));
                }

                if (prop.PropertyType == typeof(bool))
                {
                    prop.SetValue(result, bool.Parse(((YamlScalarNode)statItem.Value).Value));
                }

                if (prop.PropertyType == typeof(WeaponType))
                {
                    var typeValue = ((YamlScalarNode)statItem.Value).Value;
                    prop.SetValue(result, typeValue.ToWeaponType());
                }

                if (prop.PropertyType == typeof(GearSource))
                {
                    var typeValue = ((YamlScalarNode)statItem.Value).Value;
                    prop.SetValue(result, typeValue.ToGearSource());
                }

                if (prop.PropertyType == typeof(GemColor))
                {
                    var typeValue = ((YamlScalarNode)statItem.Value).Value;
                    prop.SetValue(result, typeValue.ToGemColor());
                }

                // TODO: in schema specify allowed values for type, source, phase, color, etc
            }

            return result;
        }
    }
}
