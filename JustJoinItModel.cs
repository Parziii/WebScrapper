namespace JustJoinIT
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using R = Newtonsoft.Json.Required;
    using N = Newtonsoft.Json.NullValueHandling;

    public partial class Welcome
    {
        [J("title")] public string Title { get; set; }
        [J("street")] public string Street { get; set; }
        [J("city")] public string City { get; set; }
        [J("country_code")] public CountryCode? CountryCode { get; set; }
        [J("address_text")] public string AddressText { get; set; }
        [J("marker_icon")] public MarkerIcon MarkerIcon { get; set; }
        [J("workplace_type")] public WorkplaceType WorkplaceType { get; set; }
        [J("company_name")] public string CompanyName { get; set; }
        [J("company_url")] public CompanyUrlUnion CompanyUrl { get; set; }
        [J("company_size")] public string CompanySize { get; set; }
        [J("experience_level")] public ExperienceLevel ExperienceLevel { get; set; }
        [J("latitude")] public string Latitude { get; set; }
        [J("longitude")] public string Longitude { get; set; }
        [J("published_at")] public DateTimeOffset PublishedAt { get; set; }
        [J("remote_interview")] public bool RemoteInterview { get; set; }
        [J("open_to_hire_ukrainians")] public bool OpenToHireUkrainians { get; set; }
        [J("id")] public string Id { get; set; }
        [J("display_offer")] public bool DisplayOffer { get; set; }
        [J("employment_types")] public List<EmploymentType> EmploymentTypes { get; set; }
        [J("company_logo_url")] public Uri CompanyLogoUrl { get; set; }
        [J("skills")] public List<Skill> Skills { get; set; }
        [J("remote")] public bool Remote { get; set; }
        [J("multilocation")] public List<Multilocation> Multilocation { get; set; }
        [J("way_of_apply")] public WayOfApply WayOfApply { get; set; }
    }

    public partial class EmploymentType
    {
        [J("type")] public TypeEnum Type { get; set; }
        [J("salary")] public Salary Salary { get; set; }
    }

    public partial class Salary
    {
        [J("from")] public long From { get; set; }
        [J("to")] public long To { get; set; }
        [J("currency")] public Currency Currency { get; set; }
    }

    public partial class Multilocation
    {
        [J("city")] public string City { get; set; }
        [J("slug")] public string Slug { get; set; }
        [J("street")] public string Street { get; set; }
    }

    public partial class Skill
    {
        [J("name")] public string Name { get; set; }
        [J("level")] public long Level { get; set; }
    }

    public enum CompanyUrlEnum { CorningOpticalComunication, HttpsWwwResoComPl };

    public enum CountryCode { Ae, At, Au, Be, Bg, Ca, Ch, Cr, Cy, Cz, De, Dk, Ee, Es, Fi, Fr, Gb, Ie, Il, It, Jp, Lt, Lu, Lv, Nl, No, Pl, Pt, Ro, Rs, Sa, Se, Sg, Sk, Tr, Ua, Us };

    public enum Currency { Chf, Eur, Gbp, Pln, Usd };

    public enum TypeEnum { B2B, MandateContract, Permanent };

    public enum ExperienceLevel { Junior, Mid, Senior };

    public enum MarkerIcon { Admin, Analytics, Architecture, C, Data, Devops, Erp, Game, Go, Html, Java, Javascript, Mobile, Net, Other, Php, Pm, Python, Ruby, Scala, Security, Support, Testing, Ux };

    public enum WayOfApply { Form, Redirect };

    public enum WorkplaceType { Office, PartlyRemote, Remote };

    public partial struct CompanyUrlUnion
    {
        public CompanyUrlEnum? Enum;
        public Uri PurpleUri;

        public static implicit operator CompanyUrlUnion(CompanyUrlEnum Enum)
        {
            return new CompanyUrlUnion { Enum = Enum };
        }
        public static implicit operator CompanyUrlUnion(Uri PurpleUri)
        {
            return new CompanyUrlUnion { PurpleUri = PurpleUri };
        }
    }

    public partial class Welcome
    {
        public static List<Welcome> FromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<Welcome>>(json, JustJoinIT.Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this List<Welcome> self)
        {
            return JsonConvert.SerializeObject(self, JustJoinIT.Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                CompanyUrlUnionConverter.Singleton,
                CompanyUrlEnumConverter.Singleton,
                CountryCodeConverter.Singleton,
                CurrencyConverter.Singleton,
                TypeEnumConverter.Singleton,
                ExperienceLevelConverter.Singleton,
                MarkerIconConverter.Singleton,
                WayOfApplyConverter.Singleton,
                WorkplaceTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class CompanyUrlUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(CompanyUrlUnion) || t == typeof(CompanyUrlUnion?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String: 
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    switch (stringValue)
                    {
                        case " https://www.reso.com.pl/":
                            return new CompanyUrlUnion { Enum = CompanyUrlEnum.HttpsWwwResoComPl };
                        case "Corning Optical Comunication":
                            return new CompanyUrlUnion { Enum = CompanyUrlEnum.CorningOpticalComunication };
                    }
                    try
                    {
                        var uri = new Uri(stringValue);
                        return new CompanyUrlUnion { PurpleUri = uri };
                    }
                    catch (UriFormatException) { }
                    break;
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (CompanyUrlUnion)untypedValue;
            if (value.Enum != null)
            {
                switch (value.Enum)
                {
                    case CompanyUrlEnum.HttpsWwwResoComPl:
                        serializer.Serialize(writer, " https://www.reso.com.pl/");
                        return;
                    case CompanyUrlEnum.CorningOpticalComunication:
                        serializer.Serialize(writer, "Corning Optical Comunication");
                        return;
                }
            }
            if (value.PurpleUri != null)
            {
                serializer.Serialize(writer, value.PurpleUri.ToString());
                return;
            }
            throw new Exception("Cannot marshal type CompanyUrlUnion");
        }

        public static readonly CompanyUrlUnionConverter Singleton = new CompanyUrlUnionConverter();
    }

    internal class CompanyUrlEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(CompanyUrlEnum) || t == typeof(CompanyUrlEnum?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case " https://www.reso.com.pl/":
                    return CompanyUrlEnum.HttpsWwwResoComPl;
                case "Corning Optical Comunication":
                    return CompanyUrlEnum.CorningOpticalComunication;
            }
            throw new Exception("Cannot unmarshal type CompanyUrlEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (CompanyUrlEnum)untypedValue;
            switch (value)
            {
                case CompanyUrlEnum.HttpsWwwResoComPl:
                    serializer.Serialize(writer, " https://www.reso.com.pl/");
                    return;
                case CompanyUrlEnum.CorningOpticalComunication:
                    serializer.Serialize(writer, "Corning Optical Comunication");
                    return;
            }
            throw new Exception("Cannot marshal type CompanyUrlEnum");
        }

        public static readonly CompanyUrlEnumConverter Singleton = new CompanyUrlEnumConverter();
    }

    internal class CountryCodeConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(CountryCode) || t == typeof(CountryCode?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "AE":
                    return CountryCode.Ae;
                case "AT":
                    return CountryCode.At;
                case "AU":
                    return CountryCode.Au;
                case "BE":
                    return CountryCode.Be;
                case "BG":
                    return CountryCode.Bg;
                case "CA":
                    return CountryCode.Ca;
                case "CH":
                    return CountryCode.Ch;
                case "CR":
                    return CountryCode.Cr;
                case "CY":
                    return CountryCode.Cy;
                case "CZ":
                    return CountryCode.Cz;
                case "DE":
                    return CountryCode.De;
                case "DK":
                    return CountryCode.Dk;
                case "EE":
                    return CountryCode.Ee;
                case "ES":
                    return CountryCode.Es;
                case "FI":
                    return CountryCode.Fi;
                case "FR":
                    return CountryCode.Fr;
                case "GB":
                    return CountryCode.Gb;
                case "IE":
                    return CountryCode.Ie;
                case "IL":
                    return CountryCode.Il;
                case "IT":
                    return CountryCode.It;
                case "JP":
                    return CountryCode.Jp;
                case "LT":
                    return CountryCode.Lt;
                case "LU":
                    return CountryCode.Lu;
                case "LV":
                    return CountryCode.Lv;
                case "NL":
                    return CountryCode.Nl;
                case "NO":
                    return CountryCode.No;
                case "PL":
                    return CountryCode.Pl;
                case "PT":
                    return CountryCode.Pt;
                case "RO":
                    return CountryCode.Ro;
                case "RS":
                    return CountryCode.Rs;
                case "SA":
                    return CountryCode.Sa;
                case "SE":
                    return CountryCode.Se;
                case "SG":
                    return CountryCode.Sg;
                case "SK":
                    return CountryCode.Sk;
                case "TR":
                    return CountryCode.Tr;
                case "UA":
                    return CountryCode.Ua;
                case "US":
                    return CountryCode.Us;
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (CountryCode)untypedValue;
            switch (value)
            {
                case CountryCode.Ae:
                    serializer.Serialize(writer, "AE");
                    return;
                case CountryCode.At:
                    serializer.Serialize(writer, "AT");
                    return;
                case CountryCode.Au:
                    serializer.Serialize(writer, "AU");
                    return;
                case CountryCode.Be:
                    serializer.Serialize(writer, "BE");
                    return;
                case CountryCode.Bg:
                    serializer.Serialize(writer, "BG");
                    return;
                case CountryCode.Ca:
                    serializer.Serialize(writer, "CA");
                    return;
                case CountryCode.Ch:
                    serializer.Serialize(writer, "CH");
                    return;
                case CountryCode.Cr:
                    serializer.Serialize(writer, "CR");
                    return;
                case CountryCode.Cy:
                    serializer.Serialize(writer, "CY");
                    return;
                case CountryCode.Cz:
                    serializer.Serialize(writer, "CZ");
                    return;
                case CountryCode.De:
                    serializer.Serialize(writer, "DE");
                    return;
                case CountryCode.Dk:
                    serializer.Serialize(writer, "DK");
                    return;
                case CountryCode.Ee:
                    serializer.Serialize(writer, "EE");
                    return;
                case CountryCode.Es:
                    serializer.Serialize(writer, "ES");
                    return;
                case CountryCode.Fi:
                    serializer.Serialize(writer, "FI");
                    return;
                case CountryCode.Fr:
                    serializer.Serialize(writer, "FR");
                    return;
                case CountryCode.Gb:
                    serializer.Serialize(writer, "GB");
                    return;
                case CountryCode.Ie:
                    serializer.Serialize(writer, "IE");
                    return;
                case CountryCode.Il:
                    serializer.Serialize(writer, "IL");
                    return;
                case CountryCode.It:
                    serializer.Serialize(writer, "IT");
                    return;
                case CountryCode.Jp:
                    serializer.Serialize(writer, "JP");
                    return;
                case CountryCode.Lt:
                    serializer.Serialize(writer, "LT");
                    return;
                case CountryCode.Lu:
                    serializer.Serialize(writer, "LU");
                    return;
                case CountryCode.Lv:
                    serializer.Serialize(writer, "LV");
                    return;
                case CountryCode.Nl:
                    serializer.Serialize(writer, "NL");
                    return;
                case CountryCode.No:
                    serializer.Serialize(writer, "NO");
                    return;
                case CountryCode.Pl:
                    serializer.Serialize(writer, "PL");
                    return;
                case CountryCode.Pt:
                    serializer.Serialize(writer, "PT");
                    return;
                case CountryCode.Ro:
                    serializer.Serialize(writer, "RO");
                    return;
                case CountryCode.Rs:
                    serializer.Serialize(writer, "RS");
                    return;
                case CountryCode.Sa:
                    serializer.Serialize(writer, "SA");
                    return;
                case CountryCode.Se:
                    serializer.Serialize(writer, "SE");
                    return;
                case CountryCode.Sg:
                    serializer.Serialize(writer, "SG");
                    return;
                case CountryCode.Sk:
                    serializer.Serialize(writer, "SK");
                    return;
                case CountryCode.Tr:
                    serializer.Serialize(writer, "TR");
                    return;
                case CountryCode.Ua:
                    serializer.Serialize(writer, "UA");
                    return;
                case CountryCode.Us:
                    serializer.Serialize(writer, "US");
                    return;
            }
            throw new Exception("Cannot marshal type CountryCode");
        }

        public static readonly CountryCodeConverter Singleton = new CountryCodeConverter();
    }

    internal class CurrencyConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(Currency) || t == typeof(Currency?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "chf":
                    return Currency.Chf;
                case "eur":
                    return Currency.Eur;
                case "gbp":
                    return Currency.Gbp;
                case "pln":
                    return Currency.Pln;
                case "usd":
                    return Currency.Usd;
            }
            throw new Exception("Cannot unmarshal type Currency");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Currency)untypedValue;
            switch (value)
            {
                case Currency.Chf:
                    serializer.Serialize(writer, "chf");
                    return;
                case Currency.Eur:
                    serializer.Serialize(writer, "eur");
                    return;
                case Currency.Gbp:
                    serializer.Serialize(writer, "gbp");
                    return;
                case Currency.Pln:
                    serializer.Serialize(writer, "pln");
                    return;
                case Currency.Usd:
                    serializer.Serialize(writer, "usd");
                    return;
            }
            throw new Exception("Cannot marshal type Currency");
        }

        public static readonly CurrencyConverter Singleton = new CurrencyConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(TypeEnum) || t == typeof(TypeEnum?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "b2b":
                    return TypeEnum.B2B;
                case "mandate_contract":
                    return TypeEnum.MandateContract;
                case "permanent":
                    return TypeEnum.Permanent;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            switch (value)
            {
                case TypeEnum.B2B:
                    serializer.Serialize(writer, "b2b");
                    return;
                case TypeEnum.MandateContract:
                    serializer.Serialize(writer, "mandate_contract");
                    return;
                case TypeEnum.Permanent:
                    serializer.Serialize(writer, "permanent");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }

    internal class ExperienceLevelConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(ExperienceLevel) || t == typeof(ExperienceLevel?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "junior":
                    return ExperienceLevel.Junior;
                case "mid":
                    return ExperienceLevel.Mid;
                case "senior":
                    return ExperienceLevel.Senior;
            }
            throw new Exception("Cannot unmarshal type ExperienceLevel");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ExperienceLevel)untypedValue;
            switch (value)
            {
                case ExperienceLevel.Junior:
                    serializer.Serialize(writer, "junior");
                    return;
                case ExperienceLevel.Mid:
                    serializer.Serialize(writer, "mid");
                    return;
                case ExperienceLevel.Senior:
                    serializer.Serialize(writer, "senior");
                    return;
            }
            throw new Exception("Cannot marshal type ExperienceLevel");
        }

        public static readonly ExperienceLevelConverter Singleton = new ExperienceLevelConverter();
    }

    internal class MarkerIconConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(MarkerIcon) || t == typeof(MarkerIcon?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "admin":
                    return MarkerIcon.Admin;
                case "analytics":
                    return MarkerIcon.Analytics;
                case "architecture":
                    return MarkerIcon.Architecture;
                case "c":
                    return MarkerIcon.C;
                case "data":
                    return MarkerIcon.Data;
                case "devops":
                    return MarkerIcon.Devops;
                case "erp":
                    return MarkerIcon.Erp;
                case "game":
                    return MarkerIcon.Game;
                case "go":
                    return MarkerIcon.Go;
                case "html":
                    return MarkerIcon.Html;
                case "java":
                    return MarkerIcon.Java;
                case "javascript":
                    return MarkerIcon.Javascript;
                case "mobile":
                    return MarkerIcon.Mobile;
                case "net":
                    return MarkerIcon.Net;
                case "other":
                    return MarkerIcon.Other;
                case "php":
                    return MarkerIcon.Php;
                case "pm":
                    return MarkerIcon.Pm;
                case "python":
                    return MarkerIcon.Python;
                case "ruby":
                    return MarkerIcon.Ruby;
                case "scala":
                    return MarkerIcon.Scala;
                case "security":
                    return MarkerIcon.Security;
                case "support":
                    return MarkerIcon.Support;
                case "testing":
                    return MarkerIcon.Testing;
                case "ux":
                    return MarkerIcon.Ux;
            }
            throw new Exception("Cannot unmarshal type MarkerIcon");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (MarkerIcon)untypedValue;
            switch (value)
            {
                case MarkerIcon.Admin:
                    serializer.Serialize(writer, "admin");
                    return;
                case MarkerIcon.Analytics:
                    serializer.Serialize(writer, "analytics");
                    return;
                case MarkerIcon.Architecture:
                    serializer.Serialize(writer, "architecture");
                    return;
                case MarkerIcon.C:
                    serializer.Serialize(writer, "c");
                    return;
                case MarkerIcon.Data:
                    serializer.Serialize(writer, "data");
                    return;
                case MarkerIcon.Devops:
                    serializer.Serialize(writer, "devops");
                    return;
                case MarkerIcon.Erp:
                    serializer.Serialize(writer, "erp");
                    return;
                case MarkerIcon.Game:
                    serializer.Serialize(writer, "game");
                    return;
                case MarkerIcon.Go:
                    serializer.Serialize(writer, "go");
                    return;
                case MarkerIcon.Html:
                    serializer.Serialize(writer, "html");
                    return;
                case MarkerIcon.Java:
                    serializer.Serialize(writer, "java");
                    return;
                case MarkerIcon.Javascript:
                    serializer.Serialize(writer, "javascript");
                    return;
                case MarkerIcon.Mobile:
                    serializer.Serialize(writer, "mobile");
                    return;
                case MarkerIcon.Net:
                    serializer.Serialize(writer, "net");
                    return;
                case MarkerIcon.Other:
                    serializer.Serialize(writer, "other");
                    return;
                case MarkerIcon.Php:
                    serializer.Serialize(writer, "php");
                    return;
                case MarkerIcon.Pm:
                    serializer.Serialize(writer, "pm");
                    return;
                case MarkerIcon.Python:
                    serializer.Serialize(writer, "python");
                    return;
                case MarkerIcon.Ruby:
                    serializer.Serialize(writer, "ruby");
                    return;
                case MarkerIcon.Scala:
                    serializer.Serialize(writer, "scala");
                    return;
                case MarkerIcon.Security:
                    serializer.Serialize(writer, "security");
                    return;
                case MarkerIcon.Support:
                    serializer.Serialize(writer, "support");
                    return;
                case MarkerIcon.Testing:
                    serializer.Serialize(writer, "testing");
                    return;
                case MarkerIcon.Ux:
                    serializer.Serialize(writer, "ux");
                    return;
            }
            throw new Exception("Cannot marshal type MarkerIcon");
        }

        public static readonly MarkerIconConverter Singleton = new MarkerIconConverter();
    }

    internal class WayOfApplyConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(WayOfApply) || t == typeof(WayOfApply?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "form":
                    return WayOfApply.Form;
                case "redirect":
                    return WayOfApply.Redirect;
            }
            throw new Exception("Cannot unmarshal type WayOfApply");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (WayOfApply)untypedValue;
            switch (value)
            {
                case WayOfApply.Form:
                    serializer.Serialize(writer, "form");
                    return;
                case WayOfApply.Redirect:
                    serializer.Serialize(writer, "redirect");
                    return;
            }
            throw new Exception("Cannot marshal type WayOfApply");
        }

        public static readonly WayOfApplyConverter Singleton = new WayOfApplyConverter();
    }

    internal class WorkplaceTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(WorkplaceType) || t == typeof(WorkplaceType?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "office":
                    return WorkplaceType.Office;
                case "partly_remote":
                    return WorkplaceType.PartlyRemote;
                case "remote":
                    return WorkplaceType.Remote;
            }
            throw new Exception("Cannot unmarshal type WorkplaceType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (WorkplaceType)untypedValue;
            switch (value)
            {
                case WorkplaceType.Office:
                    serializer.Serialize(writer, "office");
                    return;
                case WorkplaceType.PartlyRemote:
                    serializer.Serialize(writer, "partly_remote");
                    return;
                case WorkplaceType.Remote:
                    serializer.Serialize(writer, "remote");
                    return;
            }
            throw new Exception("Cannot marshal type WorkplaceType");
        }

        public static readonly WorkplaceTypeConverter Singleton = new WorkplaceTypeConverter();
    }
}
