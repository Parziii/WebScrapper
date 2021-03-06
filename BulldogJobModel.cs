namespace BulldogJob
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using R = Newtonsoft.Json.Required;
    using N = Newtonsoft.Json.NullValueHandling;

    public partial class Model
    {
        [J("id")] public string Id { get; set; }
        [J("company")] public Company Company { get; set; }
        [J("denominatedSalaryLong")] public DenominatedSalaryLong DenominatedSalaryLong { get; set; }
        [J("highlight")] public bool Highlight { get; set; }
        [J("city")] public string City { get; set; }
        [J("relocationFlag")] public RelocationFlag? RelocationFlag { get; set; }
        [J("experienceLevel")] public ExperienceLevel ExperienceLevel { get; set; }
        [J("locations")] public List<LocationElement> Locations { get; set; }
        [J("hiddenBrackets")] public bool HiddenBrackets { get; set; }
        [J("position")] public string Position { get; set; }
        [J("remote")] public bool Remote { get; set; }
        [J("environment")] public Environment Environment { get; set; }
        [J("endsAt")] public DateTimeOffset EndsAt { get; set; }
        [J("recruitmentProcess")] public List<string> RecruitmentProcess { get; set; }
        [J("showSalary")] public bool ShowSalary { get; set; }
        [J("state")] public State State { get; set; }
        [J("technologies")] public List<Technology> Technologies { get; set; }
        [J("contractB2b")] public bool ContractB2B { get; set; }
        [J("contractEmployment")] public bool ContractEmployment { get; set; }
        [J("contractOther")] public bool? ContractOther { get; set; }
        [J("locale")] public Locale Locale { get; set; }
        [J("__typename")] public ModelTypename Typename { get; set; }
    }

    public partial class Company
    {
        [J("name")] public string Name { get; set; }
        [J("topTech")] public bool TopTech { get; set; }
        [J("topTechDesc")] public string TopTechDesc { get; set; }
        [J("environment")] public Environment Environment { get; set; }
        [J("jobCover")] public JobCover JobCover { get; set; }
        [J("logo")] public JobCover Logo { get; set; }
        [J("__typename")] public CompanyTypename Typename { get; set; }
    }

    public partial class Environment
    {
        [J("remotePossible")] public long? RemotePossible { get; set; }
        [J("__typename")] public EnvironmentTypename Typename { get; set; }
    }

    public partial class JobCover
    {
        [J("url")] public Uri Url { get; set; }
        [J("__typename")] public JobCoverTypename Typename { get; set; }
    }

    public partial class DenominatedSalaryLong
    {
        [J("money")] public string Money { get; set; }
        [J("currency")] public Currency? Currency { get; set; }
        [J("hidden")] public bool Hidden { get; set; }
        [J("__typename")] public DenominatedSalaryLongTypename Typename { get; set; }
    }

    public partial class LocationElement
    {
        [J("address")] public string Address { get; set; }
        [J("location")] public LocationLocation Location { get; set; }
        [J("__typename")] public PurpleTypename Typename { get; set; }
    }

    public partial class LocationLocation
    {
        [J("cityPl")] public City CityPl { get; set; }
        [J("cityEn")] public City CityEn { get; set; }
        [J("__typename")] public FluffyTypename Typename { get; set; }
    }

    public partial class Technology
    {
        [J("level")] public Level Level { get; set; }
        [J("name")] public string Name { get; set; }
        [J("__typename")] public TechnologyTypename Typename { get; set; }
    }

    public enum EnvironmentTypename { Environment };

    public enum JobCoverTypename { Image };

    public enum CompanyTypename { Company };

    public enum Currency { Eur, Pln, Usd };

    public enum DenominatedSalaryLongTypename { Salary };

    public enum ExperienceLevel { Junior, Medium, Senior };

    public enum Locale { En, Pl };

    public enum City { Anif, Ateny, Athens, Berlin, Białystok, BielskoBiala, BielskoBiała, Brno, Bydgoszcz, Częstochowa, Gdańsk, Gdynia, Gliwice, Gostyń, Gżira, Katowice, KemptenAllgäu, Kielce, KonstancinJeziorna, Koszalin, Krakow, Kraków, Limassol, London, Londyn, Lublin, Pabianice, Paris, Paryż, Pieńków, Piła, Poznań, Praga, Prague, Płock, Rzeszów, Salzburg, Sosnowiec, StarogardGdański, Stockholm, Szczecin, Sztokholm, Tajęcina, Tbilisi, Toruń, Ulm, Warsaw, Warszawa, Wrocław, Zabierzów, ZielonaGóra, Zielonka, Łódź };

    public enum FluffyTypename { Location };

    public enum PurpleTypename { JobLocation };

    public enum RelocationFlag { Any, Ua };

    public enum State { Published };

    public enum Level { Beginner, Excellent, Unspecified, VeryWell };

    public enum TechnologyTypename { Technology };

    public enum ModelTypename { Job };

    public partial class Model
    {
        public static List<Model> FromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<Model>>(json, BulldogJob.Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this List<Model> self)
        {
            return JsonConvert.SerializeObject(self, BulldogJob.Converter.Settings);
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
                ModelTypenameConverter.Singleton,
                CompanyTypenameConverter.Singleton,
                EnvironmentTypenameConverter.Singleton,
                JobCoverTypenameConverter.Singleton,
                DenominatedSalaryLongTypenameConverter.Singleton,
                CurrencyConverter.Singleton,
                ExperienceLevelConverter.Singleton,
                LocaleConverter.Singleton,
                PurpleTypenameConverter.Singleton,
                FluffyTypenameConverter.Singleton,
                CityConverter.Singleton,
                RelocationFlagConverter.Singleton,
                StateConverter.Singleton,
                TechnologyTypenameConverter.Singleton,
                LevelConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ModelTypenameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(ModelTypename) || t == typeof(ModelTypename?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Job")
            {
                return ModelTypename.Job;
            }
            throw new Exception("Cannot unmarshal type ModelTypename");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ModelTypename)untypedValue;
            if (value == ModelTypename.Job)
            {
                serializer.Serialize(writer, "Job");
                return;
            }
            throw new Exception("Cannot marshal type ModelTypename");
        }

        public static readonly ModelTypenameConverter Singleton = new ModelTypenameConverter();
    }

    internal class CompanyTypenameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(CompanyTypename) || t == typeof(CompanyTypename?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Company")
            {
                return CompanyTypename.Company;
            }
            throw new Exception("Cannot unmarshal type CompanyTypename");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (CompanyTypename)untypedValue;
            if (value == CompanyTypename.Company)
            {
                serializer.Serialize(writer, "Company");
                return;
            }
            throw new Exception("Cannot marshal type CompanyTypename");
        }

        public static readonly CompanyTypenameConverter Singleton = new CompanyTypenameConverter();
    }

    internal class EnvironmentTypenameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(EnvironmentTypename) || t == typeof(EnvironmentTypename?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Environment")
            {
                return EnvironmentTypename.Environment;
            }
            throw new Exception("Cannot unmarshal type EnvironmentTypename");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (EnvironmentTypename)untypedValue;
            if (value == EnvironmentTypename.Environment)
            {
                serializer.Serialize(writer, "Environment");
                return;
            }
            throw new Exception("Cannot marshal type EnvironmentTypename");
        }

        public static readonly EnvironmentTypenameConverter Singleton = new EnvironmentTypenameConverter();
    }

    internal class JobCoverTypenameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(JobCoverTypename) || t == typeof(JobCoverTypename?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Image")
            {
                return JobCoverTypename.Image;
            }
            throw new Exception("Cannot unmarshal type JobCoverTypename");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (JobCoverTypename)untypedValue;
            if (value == JobCoverTypename.Image)
            {
                serializer.Serialize(writer, "Image");
                return;
            }
            throw new Exception("Cannot marshal type JobCoverTypename");
        }

        public static readonly JobCoverTypenameConverter Singleton = new JobCoverTypenameConverter();
    }

    internal class DenominatedSalaryLongTypenameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(DenominatedSalaryLongTypename) || t == typeof(DenominatedSalaryLongTypename?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Salary")
            {
                return DenominatedSalaryLongTypename.Salary;
            }
            throw new Exception("Cannot unmarshal type DenominatedSalaryLongTypename");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DenominatedSalaryLongTypename)untypedValue;
            if (value == DenominatedSalaryLongTypename.Salary)
            {
                serializer.Serialize(writer, "Salary");
                return;
            }
            throw new Exception("Cannot marshal type DenominatedSalaryLongTypename");
        }

        public static readonly DenominatedSalaryLongTypenameConverter Singleton = new DenominatedSalaryLongTypenameConverter();
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
                case "EUR":
                    return Currency.Eur;
                case "PLN":
                    return Currency.Pln;
                case "USD":
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
                case Currency.Eur:
                    serializer.Serialize(writer, "EUR");
                    return;
                case Currency.Pln:
                    serializer.Serialize(writer, "PLN");
                    return;
                case Currency.Usd:
                    serializer.Serialize(writer, "USD");
                    return;
            }
            throw new Exception("Cannot marshal type Currency");
        }

        public static readonly CurrencyConverter Singleton = new CurrencyConverter();
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
                case "medium":
                    return ExperienceLevel.Medium;
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
                case ExperienceLevel.Medium:
                    serializer.Serialize(writer, "medium");
                    return;
                case ExperienceLevel.Senior:
                    serializer.Serialize(writer, "senior");
                    return;
            }
            throw new Exception("Cannot marshal type ExperienceLevel");
        }

        public static readonly ExperienceLevelConverter Singleton = new ExperienceLevelConverter();
    }

    internal class LocaleConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(Locale) || t == typeof(Locale?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "en":
                    return Locale.En;
                case "pl":
                    return Locale.Pl;
            }
            throw new Exception("Cannot unmarshal type Locale");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Locale)untypedValue;
            switch (value)
            {
                case Locale.En:
                    serializer.Serialize(writer, "en");
                    return;
                case Locale.Pl:
                    serializer.Serialize(writer, "pl");
                    return;
            }
            throw new Exception("Cannot marshal type Locale");
        }

        public static readonly LocaleConverter Singleton = new LocaleConverter();
    }

    internal class PurpleTypenameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(PurpleTypename) || t == typeof(PurpleTypename?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "JobLocation")
            {
                return PurpleTypename.JobLocation;
            }
            throw new Exception("Cannot unmarshal type PurpleTypename");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PurpleTypename)untypedValue;
            if (value == PurpleTypename.JobLocation)
            {
                serializer.Serialize(writer, "JobLocation");
                return;
            }
            throw new Exception("Cannot marshal type PurpleTypename");
        }

        public static readonly PurpleTypenameConverter Singleton = new PurpleTypenameConverter();
    }

    internal class FluffyTypenameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(FluffyTypename) || t == typeof(FluffyTypename?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Location")
            {
                return FluffyTypename.Location;
            }
            throw new Exception("Cannot unmarshal type FluffyTypename");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FluffyTypename)untypedValue;
            if (value == FluffyTypename.Location)
            {
                serializer.Serialize(writer, "Location");
                return;
            }
            throw new Exception("Cannot marshal type FluffyTypename");
        }

        public static readonly FluffyTypenameConverter Singleton = new FluffyTypenameConverter();
    }

    internal class CityConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(City) || t == typeof(City?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Anif":
                    return City.Anif;
                case "Ateny":
                    return City.Ateny;
                case "Athens":
                    return City.Athens;
                case "Berlin":
                    return City.Berlin;
                case "Białystok":
                    return City.Białystok;
                case "Bielsko-Biala":
                    return City.BielskoBiala;
                case "Bielsko-Biała":
                    return City.BielskoBiała;
                case "Brno":
                    return City.Brno;
                case "Bydgoszcz":
                    return City.Bydgoszcz;
                case "Częstochowa":
                    return City.Częstochowa;
                case "Gdańsk":
                    return City.Gdańsk;
                case "Gdynia":
                    return City.Gdynia;
                case "Gliwice":
                    return City.Gliwice;
                case "Gostyń":
                    return City.Gostyń;
                case "Gżira":
                    return City.Gżira;
                case "Katowice":
                    return City.Katowice;
                case "Kempten (Allgäu)":
                    return City.KemptenAllgäu;
                case "Kielce":
                    return City.Kielce;
                case "Konstancin-Jeziorna":
                    return City.KonstancinJeziorna;
                case "Koszalin":
                    return City.Koszalin;
                case "Krakow":
                    return City.Krakow;
                case "Kraków":
                    return City.Kraków;
                case "Limassol":
                    return City.Limassol;
                case "London":
                    return City.London;
                case "Londyn":
                    return City.Londyn;
                case "Lublin":
                    return City.Lublin;
                case "Pabianice":
                    return City.Pabianice;
                case "Paris":
                    return City.Paris;
                case "Paryż":
                    return City.Paryż;
                case "Pieńków":
                    return City.Pieńków;
                case "Piła":
                    return City.Piła;
                case "Poznań":
                    return City.Poznań;
                case "Praga":
                    return City.Praga;
                case "Prague":
                    return City.Prague;
                case "Płock":
                    return City.Płock;
                case "Rzeszów":
                    return City.Rzeszów;
                case "Salzburg":
                    return City.Salzburg;
                case "Sosnowiec":
                    return City.Sosnowiec;
                case "Starogard Gdański":
                    return City.StarogardGdański;
                case "Stockholm":
                    return City.Stockholm;
                case "Szczecin":
                    return City.Szczecin;
                case "Sztokholm":
                    return City.Sztokholm;
                case "Tajęcina":
                    return City.Tajęcina;
                case "Tbilisi":
                    return City.Tbilisi;
                case "Toruń":
                    return City.Toruń;
                case "Ulm":
                    return City.Ulm;
                case "Warsaw":
                    return City.Warsaw;
                case "Warszawa":
                    return City.Warszawa;
                case "Wrocław":
                    return City.Wrocław;
                case "Zabierzów":
                    return City.Zabierzów;
                case "Zielona Góra":
                    return City.ZielonaGóra;
                case "Zielonka":
                    return City.Zielonka;
                case "Łódź":
                    return City.Łódź;
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
            var value = (City)untypedValue;
            switch (value)
            {
                case City.Anif:
                    serializer.Serialize(writer, "Anif");
                    return;
                case City.Ateny:
                    serializer.Serialize(writer, "Ateny");
                    return;
                case City.Athens:
                    serializer.Serialize(writer, "Athens");
                    return;
                case City.Berlin:
                    serializer.Serialize(writer, "Berlin");
                    return;
                case City.Białystok:
                    serializer.Serialize(writer, "Białystok");
                    return;
                case City.BielskoBiala:
                    serializer.Serialize(writer, "Bielsko-Biala");
                    return;
                case City.BielskoBiała:
                    serializer.Serialize(writer, "Bielsko-Biała");
                    return;
                case City.Brno:
                    serializer.Serialize(writer, "Brno");
                    return;
                case City.Bydgoszcz:
                    serializer.Serialize(writer, "Bydgoszcz");
                    return;
                case City.Częstochowa:
                    serializer.Serialize(writer, "Częstochowa");
                    return;
                case City.Gdańsk:
                    serializer.Serialize(writer, "Gdańsk");
                    return;
                case City.Gdynia:
                    serializer.Serialize(writer, "Gdynia");
                    return;
                case City.Gliwice:
                    serializer.Serialize(writer, "Gliwice");
                    return;
                case City.Gostyń:
                    serializer.Serialize(writer, "Gostyń");
                    return;
                case City.Gżira:
                    serializer.Serialize(writer, "Gżira");
                    return;
                case City.Katowice:
                    serializer.Serialize(writer, "Katowice");
                    return;
                case City.KemptenAllgäu:
                    serializer.Serialize(writer, "Kempten (Allgäu)");
                    return;
                case City.Kielce:
                    serializer.Serialize(writer, "Kielce");
                    return;
                case City.KonstancinJeziorna:
                    serializer.Serialize(writer, "Konstancin-Jeziorna");
                    return;
                case City.Koszalin:
                    serializer.Serialize(writer, "Koszalin");
                    return;
                case City.Krakow:
                    serializer.Serialize(writer, "Krakow");
                    return;
                case City.Kraków:
                    serializer.Serialize(writer, "Kraków");
                    return;
                case City.Limassol:
                    serializer.Serialize(writer, "Limassol");
                    return;
                case City.London:
                    serializer.Serialize(writer, "London");
                    return;
                case City.Londyn:
                    serializer.Serialize(writer, "Londyn");
                    return;
                case City.Lublin:
                    serializer.Serialize(writer, "Lublin");
                    return;
                case City.Pabianice:
                    serializer.Serialize(writer, "Pabianice");
                    return;
                case City.Paris:
                    serializer.Serialize(writer, "Paris");
                    return;
                case City.Paryż:
                    serializer.Serialize(writer, "Paryż");
                    return;
                case City.Pieńków:
                    serializer.Serialize(writer, "Pieńków");
                    return;
                case City.Piła:
                    serializer.Serialize(writer, "Piła");
                    return;
                case City.Poznań:
                    serializer.Serialize(writer, "Poznań");
                    return;
                case City.Praga:
                    serializer.Serialize(writer, "Praga");
                    return;
                case City.Prague:
                    serializer.Serialize(writer, "Prague");
                    return;
                case City.Płock:
                    serializer.Serialize(writer, "Płock");
                    return;
                case City.Rzeszów:
                    serializer.Serialize(writer, "Rzeszów");
                    return;
                case City.Salzburg:
                    serializer.Serialize(writer, "Salzburg");
                    return;
                case City.Sosnowiec:
                    serializer.Serialize(writer, "Sosnowiec");
                    return;
                case City.StarogardGdański:
                    serializer.Serialize(writer, "Starogard Gdański");
                    return;
                case City.Stockholm:
                    serializer.Serialize(writer, "Stockholm");
                    return;
                case City.Szczecin:
                    serializer.Serialize(writer, "Szczecin");
                    return;
                case City.Sztokholm:
                    serializer.Serialize(writer, "Sztokholm");
                    return;
                case City.Tajęcina:
                    serializer.Serialize(writer, "Tajęcina");
                    return;
                case City.Tbilisi:
                    serializer.Serialize(writer, "Tbilisi");
                    return;
                case City.Toruń:
                    serializer.Serialize(writer, "Toruń");
                    return;
                case City.Ulm:
                    serializer.Serialize(writer, "Ulm");
                    return;
                case City.Warsaw:
                    serializer.Serialize(writer, "Warsaw");
                    return;
                case City.Warszawa:
                    serializer.Serialize(writer, "Warszawa");
                    return;
                case City.Wrocław:
                    serializer.Serialize(writer, "Wrocław");
                    return;
                case City.Zabierzów:
                    serializer.Serialize(writer, "Zabierzów");
                    return;
                case City.ZielonaGóra:
                    serializer.Serialize(writer, "Zielona Góra");
                    return;
                case City.Zielonka:
                    serializer.Serialize(writer, "Zielonka");
                    return;
                case City.Łódź:
                    serializer.Serialize(writer, "Łódź");
                    return;
            }
            throw new Exception("Cannot marshal type City");
        }

        public static readonly CityConverter Singleton = new CityConverter();
    }

    internal class RelocationFlagConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(RelocationFlag) || t == typeof(RelocationFlag?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "any":
                    return RelocationFlag.Any;
                case "ua":
                    return RelocationFlag.Ua;
            }
            throw new Exception("Cannot unmarshal type RelocationFlag");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (RelocationFlag)untypedValue;
            switch (value)
            {
                case RelocationFlag.Any:
                    serializer.Serialize(writer, "any");
                    return;
                case RelocationFlag.Ua:
                    serializer.Serialize(writer, "ua");
                    return;
            }
            throw new Exception("Cannot marshal type RelocationFlag");
        }

        public static readonly RelocationFlagConverter Singleton = new RelocationFlagConverter();
    }

    internal class StateConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(State) || t == typeof(State?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "published")
            {
                return State.Published;
            }
            throw new Exception("Cannot unmarshal type State");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (State)untypedValue;
            if (value == State.Published)
            {
                serializer.Serialize(writer, "published");
                return;
            }
            throw new Exception("Cannot marshal type State");
        }

        public static readonly StateConverter Singleton = new StateConverter();
    }

    internal class TechnologyTypenameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(TechnologyTypename) || t == typeof(TechnologyTypename?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Technology")
            {
                return TechnologyTypename.Technology;
            }
            throw new Exception("Cannot unmarshal type TechnologyTypename");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TechnologyTypename)untypedValue;
            if (value == TechnologyTypename.Technology)
            {
                serializer.Serialize(writer, "Technology");
                return;
            }
            throw new Exception("Cannot marshal type TechnologyTypename");
        }

        public static readonly TechnologyTypenameConverter Singleton = new TechnologyTypenameConverter();
    }

    internal class LevelConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(Level) || t == typeof(Level?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "beginner":
                    return Level.Beginner;
                case "excellent":
                    return Level.Excellent;
                case "unspecified":
                    return Level.Unspecified;
                case "very_well":
                    return Level.VeryWell;
            }
            throw new Exception("Cannot unmarshal type Level");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Level)untypedValue;
            switch (value)
            {
                case Level.Beginner:
                    serializer.Serialize(writer, "beginner");
                    return;
                case Level.Excellent:
                    serializer.Serialize(writer, "excellent");
                    return;
                case Level.Unspecified:
                    serializer.Serialize(writer, "unspecified");
                    return;
                case Level.VeryWell:
                    serializer.Serialize(writer, "very_well");
                    return;
            }
            throw new Exception("Cannot marshal type Level");
        }

        public static readonly LevelConverter Singleton = new LevelConverter();
    }
}
