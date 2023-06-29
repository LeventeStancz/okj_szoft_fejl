USE `mydrivers`;
INSERT INTO munkakor(munkakor.mknev) VALUES("Sofőr"),("Titkár");
INSERT INTO felhasznalo(felhasznalo.munkakorid, felhasznalo.jogid, felhasznalo.fnev, felhasznalo.jelszo, felhasznalo.email, felhasznalo.szuletesnap, felhasznalo.kivehetoszabi, felhasznalo.kivettszabi, felhasznalo.nem, felhasznalo.teljesnev, felhasznalo.telefonszam, felhasznalo.lakcim, felhasznalo.adoazon, felhasznalo.bankszamlaszam, felhasznalo.szemelyi, felhasznalo.tajszam, felhasznalo.csatlakozas, felhasznalo.alapber)
VALUES((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Sofőr"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Felhasználó"),
      "NagySanyi", SHA2("nagysanyi",256), "nagysanyivagyok@gmail.com", "1973-03-15", 22, 7, "Férfi", "Nagy Sándor József", "06304691346", "1136, Budapest, Visegrádi utca 5.",
      "41567813549", "325684123545821444786321", "534869AB", "568947", "2000-09-19", 287000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Sofőr"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Felhasználó"),
      "KisPista", SHA2("kispista",256), "kispistafiok@gmail.com", "1991-08-26", 20, 11, "Férfi", "Kis Pista", "06204567851", "1136, Budapest, Bodnár utca 10.",
      "46549715813", "384545821441234786256321", "863495BC", "658945", "2014-10-06", 265000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Sofőr"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Felhasználó"),
      "BarnaZsolt", SHA2("barnazsolt",256), "bzsoltika@gmail.com", "1997-08-26", 20, 1, "Férfi", "Barna Zoltán Zsolt", "06708545671", "1086, Budapest, Rozsda utca 1.",
      "54819715463", "321632144123478845458625", "498356AF", "896455", "2021-12-08", 250000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Sofőr"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Felhasználó"),
      "BorosAndris", SHA2("borosandris",256), "andriska@gmail.com", "1965-11-05", 28, 6, "Férfi", "Boros Andris", "06305685471", "1056, Budapest, Arnold utca 21.",
      "54546381971", "788123586454163214432425", "835649BR", "158195", "1995-01-03", 236000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Titkár"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Operátor"),
      "FejesTamas", SHA2("fejestamas",256), "ftamas@gmail.com", "1985-12-24", 21, 0, "Férfi", "Fejes Tamás", "06307585641", "1026, Budapest, Táncsics utca 55.",
      "58639454171", "864578814253223541643214", "749316GH", "159863", "2020-09-17", 248000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Titkár"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Operátor"),
      "ÉgesCsilla", SHA2("egescsilla",256), "egcsilla@gmail.com", "1991-02-11", 20, 3, "Nő", "Éges Csilla Lilliána", "06205857641", "1016, Budapest, Kormos utca 55.",
      "39454158671", "532235416432164857881424", "931674CE", "986153", "2021-01-03", 250000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Sofőr"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Felhasználó"),
      "JuhászNoel", SHA2("juhasznoel",256), "juhasznoel@gmail.com", "1999-10-01", 20, 0, "Férfi", "Juhász Noel", "06309028369", "1073, Budapest, Kertesz utca 24.",
      "30196153284", "393301961532844115602939", "640877FE", "221162", "2020-02-13", 311000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Sofőr"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Felhasználó"),
      "LukácsKevin", SHA2("lukacskevin",256), "lukacskevin@gmail.com", "1990-04-21", 28, 4, "Férfi", "Lukács Kevin", "06307142941", "1073, Budapest, Erzsebet korut 53.",
      "68594331077", "996859433107714231094825", "231094CA", "996825", "2018-10-20", 325000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Titkár"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Operátor"),
      "BallaSándor", SHA2("ballasandor",256), "ballasandor@gmail.com", "1988-06-11", 30, 7, "Férfi", "Balla Sándor", "06304088572", "1137, Budapest, Pozsonyi ut 12.",
      "40585339296", "104058533929681412069456", "210404AB", "996929", "2015-03-10", 350000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Sofőr"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Felhasználó"),
      "TakácsErnő", SHA2("takacserno",256), "takacserno@gmail.com", "1998-02-08", 20, 1, "Férfi", "Takács Ernő", "06307264332", "1083, Budapest, Prater utca 50.",
      "58533929669", "104814120058533929669445", "104814DB", "200585", "2021-09-12", 280000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Sofőr"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Felhasználó"),
      "BálintRudolf", SHA2("balintrudolf",256), "balintrudolf@gmail.com", "2000-10-01", 20, 6, "Férfi", "Bálint Rudolf", "06303417102", "1052, Budapest, Deak Ferenc utca 15.",
      "05120694669", "853392968141040512069423", "853392AF", "040512", "2020-02-27", 210000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Titkár"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Operátor"),
      "NagyMária", SHA2("nagymaria",256), "nagymaria@gmail.com", "1986-07-17", 28, 10, "Nő", "Nagy Mária", "06202996804", "1118, Budapest, Villanyi út 13.",
      "76737152358", "965285876737152358950381", "652858BC", "892812", "2016-12-10", 333000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Sofőr"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Felhasználó"),
      "FeketeHenriett", SHA2("feketehenriett",256), "feketehenriett@gmail.com", "2000-02-21", 20, 7, "Nő", "Fekete Henriett", "06203135037", "1052, Budapest, Vaci utca 15.",
      "86222294358", "862222943324987341302227", "324987DC", "130222", "2020-10-18", 219000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Sofőr"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Felhasználó"),
      "FehérSzandra", SHA2("feherszandra",256), "feherszandra@gmail.com", "1999-10-11", 20, 3, "Nő", "Fehér Szandra", "06505316163", "1074, Budapest, Rakoczi ut 68.",
      "86589003798", "125890037991265640944094", "944094FG", "991265", "2019-06-19", 200000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Sofőr"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Felhasználó"),
      "SzalaiEvelin", SHA2("szalaievelin",256), "szalaievelin@gmail.com", "1993-02-06", 23, 11, "Nő", "Szalai Evelin", "06503782685", "1065, Budapest, Lazar utca 9.",
      "86582966418", "296641469988211295930132", "821129HG", "921125", "2021-07-19", 282000),
      ((SELECT munkakor.mkid FROM munkakor WHERE munkakor.mknev LIKE "Sofőr"),(SELECT jogosultsag.jogid FROM jogosultsag WHERE jogosultsag.jognev LIKE "Felhasználó"),
      "KelemenKata", SHA2("kelemenkata",256), "kelemenkata@gmail.com", "1996-03-20", 21, 5, "Nő", "Kelemen Kata", "06502773771", "1011, Budapest, Bem rakpart 16-19.",
      "69988620418", "382510826285699886204148", "082628AF", "204125", "2020-09-09", 205000);

INSERT INTO jarmu(jarmu.rendszam, jarmu.evjarat, jarmu.gyarto, jarmu.modell,jarmu.kmora)
VALUES("OHY-815", "2000-02", "Ford", "Transit 2.2 TDCi", 125000),
("FIS-852", "2015-16", "Ford", "Transit 2.2 TDCi", 15000),
("AQN-792", "2000-02", "Ford", "Transit 2.2 TDCi", 200000),
("SWC-494", "2000-02", "Ford", "Transit 2.2 TDCi", 150000),
("XFW-445", "2000-02", "Ford", "Transit 2.2 TDCi", 175000),
("MIQ-685", "2000-02", "Ford", "Transit 2.2 TDCi", 100000),

("ZVJ-622", "1998-00", "Ford", "Transit 2.2 TDCi", 312000),
("LWQ-401", "1998-00", "Ford", "Transit 2.2 TDCi", 319000),

("CVR-254", "2005-06", "Ford", "Transit 2.2 TDCi", 60000),
("LJB-235", "2000-02", "Ford", "Transit 2.2 TDCi", 79000),
("FNK-594", "2000-02", "Ford", "Transit 2.2 TDCi", 50000),
("LEJ-406", "2000-02", "Ford", "Transit 2.2 TDCi", 143000);

INSERT INTO szerviz(szerviz.cim, szerviz.sznev, szerviz.kontaktnev, szerviz.kontaktemail, szerviz.kontakttelszam)
VALUES("1097, Budapest, Könyves Kálmán körút 3.", "CARNET AUTO-FORT", "Tamási Lili", "tamasi.lili@autofort.hu", "06207773353");

INSERT INTO szervizeles(szervizeles.jid, szervizeles.szid, szervizeles.mettol, szervizeles.meddig, szervizeles.indok)
VALUES(7,1,"2022-05-08", "2022-05-15", "Kézi váltó meghibásodás."),(8,1,"2022-05-12", "2022-05-26", "Kuplung elhasználódása.");

INSERT INTO beosztas(beosztas.fid, beosztas.jid, beosztas.datum, beosztas.muszak, beosztas.ora)
VALUES(2,1,"2022-05-23",1,8),(2,1,"2022-05-25",1,8),(2,1,"2022-05-27",1,8),(2,1,"2022-05-30",1,8),(2,1,"2022-06-01",1,8),(2,1,"2022-06-02",1,10),
(3,2,"2022-05-23",1,8),(3,2,"2022-05-25",1,8),(3,2,"2022-05-27",0,8),(3,2,"2022-05-30",1,8),(3,2,"2022-06-01",0,8),(3,2,"2022-06-02",1,8),
(4,3,"2022-05-23",1,8),(4,3,"2022-05-25",0,10),(4,3,"2022-05-27",1,8),(4,1,"2022-05-31",1,8),(4,3,"2022-06-01",1,8),(4,1,"2022-06-03",1,8),
(5,4,"2022-05-23",1,8),(5,4,"2022-05-25",1,8),(5,4,"2022-05-27",1,8),(5,2,"2022-05-31",1,10),(5,4,"2022-06-01",1,8),(5,2,"2022-06-03",1,8),
(8,5,"2022-05-23",1,8),(8,5,"2022-05-25",1,8),(8,5,"2022-05-27",1,8),(8,3,"2022-05-31",1,8),(8,5,"2022-06-01",1,8),(8,3,"2022-06-03",1,8),
(9,2,"2022-05-24",1,8),(9,6,"2022-05-25",1,8),(9,6,"2022-05-27",1,8),(9,4,"2022-05-31",1,8),(9,6,"2022-06-01",1,8),(9,4,"2022-06-03",1,8),
(11,3,"2022-05-24",1,8),(11,2,"2022-05-26",0,8),(11,9,"2022-05-27",1,8),(11,5,"2022-05-31",1,10),(11,5,"2022-06-02",1,8),(11,5,"2022-06-03",0,8),
(12,6,"2022-05-24",1,10),(12,6,"2022-05-26",1,8),(12,6,"2022-05-30",1,8),(12,6,"2022-05-31",1,8),(12,6,"2022-06-02",1,8),(12,6,"2022-06-03",1,8),
(14,9,"2022-05-24",1,8),(14,1,"2022-05-26",1,8),(14,9,"2022-05-30",1,8),(14,9,"2022-05-31",1,8),(14,9,"2022-06-02",1,8),(14,9,"2022-06-03",1,8),
(15,10,"2022-05-24",1,8),(15,10,"2022-05-26",1,8),(15,10,"2022-05-30",0,8),(15,10,"2022-05-31",1,8),(15,10,"2022-06-02",0,8),(15,10,"2022-06-03",1,10),
(16,11,"2022-05-24",1,8),(16,11,"2022-05-26",1,8),(16,11,"2022-05-30",1,8),(16,11,"2022-06-01",1,8),(16,11,"2022-06-02",1,8),(16,11,"2022-06-04",1,8),
(17,12,"2022-05-25",1,10),(17,12,"2022-05-26",1,8),(17,12,"2022-05-30",1,8),(17,12,"2022-06-01",1,8),(17,12,"2022-06-02",1,8),(17,12,"2022-06-04",1,8);

INSERT INTO fuvar(fuvar.fid, fuvar.jid, fuvar.kerid, fuvar.datum, fuvar.kezdes, fuvar.befejezes, fuvar.felvett_csom, fuvar.sikeres_csom)
VALUES
(2,1,2,"2022-05-23", "08:00", "16:00",150,140),(3,2,1,"2022-05-23", "08:00", "16:00",150,120),(4,3,3,"2022-05-23", "08:00", "16:00",150,132),
(5,4,4,"2022-05-23", "08:00", "16:00",155,141),(8,5,6,"2022-05-23", "08:00", "16:00",110,100),
(9,2,11,"2022-05-24", "08:00", "16:00",200,181),(11,3,12,"2022-05-24", "08:00", "16:00",170,149),(12,6,20,"2022-05-24", "08:00", "18:00",123,100),
(14,9,9,"2022-05-24", "08:00", "16:00",200,181),(15,10,2,"2022-05-24", "08:00", "16:00",164,160),(16,11,1,"2022-05-24", "08:00", "16:00",110,99);