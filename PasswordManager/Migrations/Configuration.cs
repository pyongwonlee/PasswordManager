namespace PasswordManager.Migrations
{
    using PasswordManager.Models.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PasswordManager.Models.Data.PasswordContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PasswordManager.Models.Data.PasswordContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Provinces.AddOrUpdate(
                p => p.Abbreviation,
                new Province { Name = "Alberta", Abbreviation = "AB" },
                new Province { Name = "British Columbia", Abbreviation = "BC" },
                new Province { Name = "Manitoba", Abbreviation = "MB" },
                new Province { Name = "New Brunswick", Abbreviation = "NB" },
                new Province { Name = "Newfoundland and Labrador", Abbreviation = "NL" },
                new Province { Name = "Nova Scotia", Abbreviation = "NS" },
                new Province { Name = "Ontario", Abbreviation = "ON" },
                new Province { Name = "Prince Edward Island", Abbreviation = "PE" },
                new Province { Name = "Quebec", Abbreviation = "QC" },
                new Province { Name = "Saskatchewan", Abbreviation = "SK" },
                new Province { Name = "Northwest Territories", Abbreviation = "NT" },
                new Province { Name = "Yukon", Abbreviation = "YT" },
                new Province { Name = "Nunavut", Abbreviation = "NU" }
            );
        }
    }
}
