using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entiteter;
using Microsoft.EntityFrameworkCore;

namespace Datalager
{
    public static class DbSeed
    {
        public static void Populate(OOPSU2DbContext _db)
        {
            if (_db.Betalningar.Any())
                return;


            // PERSONAL
            _db.Personaler.Add(new Personal { Namn = "Sara Lindgren", Roll = "Administratör", Losenord = "Admin123!" });
            _db.Personaler.Add(new Personal { Namn = "Johan Karlsson", Roll = "Receptionist", Losenord = "Rec2024!" });
            _db.Personaler.Add(new Personal { Namn = "Elin Persson", Roll = "Instruktör", Losenord = "Traning#1" });
            _db.Personaler.Add(new Personal { Namn = "Markus Svensson", Roll = "Systemansvarig", Losenord = "Sys@456" });

            // MEDLEMMAR
            _db.Medlemmar.Add(new Medlem { Namn = "Anna Andersson", Telefonnummer = "0701234567", Email = "anna@email.se", MedlemskapsNivå = "Fast", Betalstatus = "Betald", Poäng = 150 });
            _db.Medlemmar.Add(new Medlem { Namn = "Erik Eriksson", Telefonnummer = "0739876543", Email = "erik@email.se", MedlemskapsNivå = "Flex", Betalstatus = "Obetald", Poäng = 0 });
            _db.Medlemmar.Add(new Medlem { Namn = "Lisa Svensson", Telefonnummer = "0725558899", Email = "lisa@email.se", MedlemskapsNivå = "Fast", Betalstatus = "Betald" });
            _db.Medlemmar.Add(new Medlem { Namn = "Oskar Nilsson", Telefonnummer = "0761122334", Email = "oskar@email.se", MedlemskapsNivå = "Företag", Betalstatus = "Betald" });

            // RESURSER
            _db.Resurser.Add(new Resurs { Namn = "A01", Typ = "Mötesrum", Kapacitet = 10, Status = "Tillgänglig", Beskrivning = "Rymligt mötesrum, perfekt för styrelsemöten." });
            _db.Resurser.Add(new Resurs { Namn = "B01", Typ = "Datorsal", Kapacitet = 4, Status = "Tillgänglig", Beskrivning = "Finns 4st tillgängliga datorer att använda" });
            _db.Resurser.Add(new Resurs { Namn = "C01", Typ = "Konferenssal", Kapacitet = 25, Status = "Ej tillgänglig", Beskrivning = "Stor sal, perfekt för konferenser" });

            _db.SaveChanges();

            //Vi har hådkodat dessa nedanför för att kunna koppla dem till specifika utrustningar/bokningar/betalningar

            Medlem anna = _db.Medlemmar.First(m => m.Namn == "Anna Andersson");
            Medlem erik = _db.Medlemmar.First(m => m.Namn == "Erik Eriksson");

            Resurs a01 = _db.Resurser.First(r => r.Namn == "A01");
            Resurs b01 = _db.Resurser.First(r => r.Namn == "B01");
            Resurs c01 = _db.Resurser.First(r => r.Namn == "C01");

            // UTRUSTNING
            _db.Utrustningar.Add(new Utrustning { ResursID = a01.ResursID, ArtikelNamn = "Epson Projektor", Kategori = "Projektor", Skick = "Bra" });
            _db.Utrustningar.Add(new Utrustning { ResursID = b01.ResursID, ArtikelNamn = "Samsung Skärm 55\"", Kategori = "Skärm", Skick = "Mycket bra" });
            _db.Utrustningar.Add(new Utrustning { ResursID = c01.ResursID, ArtikelNamn = "Whiteboard Standard", Kategori = "Whiteboard", Skick = "Sliten" });
            _db.Utrustningar.Add(new Utrustning { ResursID = a01.ResursID, ArtikelNamn = "LG Skärm 32\"", Kategori = "Skärm", Skick = "Bra" });

            // BOKNINGAR
            _db.Bokningar.Add(new Bokning { MedlemID = anna.MedlemID, ResursID = a01.ResursID, Datum = new DateTime(2026, 2, 1), Starttid = new TimeSpan(10, 0, 0), Sluttid = new TimeSpan(11, 0, 0) });
            _db.Bokningar.Add(new Bokning { MedlemID = erik.MedlemID, ResursID = b01.ResursID, Datum = new DateTime(2026, 2, 2), Starttid = new TimeSpan(18, 0, 0), Sluttid = new TimeSpan(19, 0, 0) });

            // BETALNINGAR
            _db.Betalningar.Add(new Betalning { MedlemID = anna.MedlemID, Köpdatum = DateTime.Now, Belopp = 250, Forfallodatum = DateTime.Now.AddMonths(1), BataldDatum = DateTime.Now, Status = "Betald" });
            _db.Betalningar.Add(new Betalning { MedlemID = erik.MedlemID, Köpdatum = DateTime.Now, Belopp = 150, Forfallodatum = DateTime.Now.AddMonths(1), BataldDatum = DateTime.Now, Status = "Obetald" });

            _db.SaveChanges();

        }
    }
}
